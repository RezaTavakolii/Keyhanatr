using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Order;
using Keyhanatr.Data.Domain.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Keyhanatr.Data.Domain.Pay;
using Keyhanatr.Data.ViewModel.Pay;
using Keyhanatr.Services;

namespace Keyhanatr.Controllers
{
    public class ShoppingCartController : Controller
    {
        private KeyhanatrContext _context;
        public ShoppingCartController(KeyhanatrContext context)
        {
            _context = context;
        }


        #region AddToCart
        [Authorize]
        public void AddToCart(int productId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            if (order == null)
            {
                order = new Order()
                {
                    CreateDate = DateTime.Now,
                    IsFinaly = false,
                    UserId = userId
                };
                _context.Add(order);
                _context.SaveChanges();
            }

            var detail =
                _context.OrderDetails.FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productId);
            if (detail != null)
            {
                detail.Count += 1;
                _context.Update(detail);
            }
            else
            {
                _context.OrderDetails.Add(new OrderDetail()
                {
                    ProductId = productId,
                    Count = 1,
                    Price = _context.Products.Find(productId).Price,
                    OrderId = order.OrderId,
                });
            }
            _context.SaveChanges();
        }
        #endregion

        #region ShowCart
        public IActionResult ShowCart()
        {
            List<OrderDetail> list = new List<OrderDetail>();

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _context.Orders.FirstOrDefault(o => !o.IsFinaly && o.UserId == userId);

                if (order != null)
                {
                    list.AddRange(_context.OrderDetails
                        .Where(d => d.OrderId == order.OrderId).Include(p => p.Product).ToList());
                }
            }
            return PartialView(list);
        }
        #endregion

        #region CountShopCart
        public int CountShopCart()
        {
            int count = 0;
            List<OrderDetail> list = new List<OrderDetail>();

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _context.Orders.FirstOrDefault(o => !o.IsFinaly && o.UserId == userId);
                if (order != null)
                {
                    list.AddRange(_context.OrderDetails
                        .Where(d => d.OrderId == order.OrderId).Include(p => p.Product).ToList());
                    count = list.Sum(d => d.Count);
                }
            }

            return count;
        }
        #endregion

        #region ShowOrder
        [Authorize]
        [Route("ShowOrder")]
        public IActionResult ShowOrder()
        {
            return View();
        }
        public IActionResult Order()
        {
            List<OrderDetail> list = new List<OrderDetail>();

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _context.Orders.FirstOrDefault(o => !o.IsFinaly && o.UserId == userId);
                if (order != null)
                {
                    list.AddRange(_context.OrderDetails
                        .Where(d => d.OrderId == order.OrderId).Include(p => p.Product).ToList());
                }
            }
            return PartialView(list);
        }
        #endregion

        #region DeleteOrder
        [Authorize]
        public void DeleteOrder(int id)
        {
            var detail = _context.OrderDetails.Find(id);
            _context.Remove(detail);
            _context.SaveChanges();
        }
        #endregion

        #region ChangeOrder
        [Authorize]
        public void ChangeOrder(int id, int count)
        {
            var detail = _context.OrderDetails.Find(id);
            detail.Count = count;
            _context.Update(detail);
            _context.SaveChanges();
        }

        #endregion


        string redirectPage = "https://localhost:5001/Home/CallBack";
        string GroupTelegram = "623332525";
        public async Task<IActionResult> Payment()
        {
            await TelegramService.SendMessageToDynamicGroupPost("دکمه پرداخت زده شد", GroupTelegram);
            try
            {
                long token = 0;
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _context.Orders.FirstOrDefault(o => !o.IsFinaly && o.UserId == userId);
                int amount = _context.OrderDetails.Where(d => d.OrderId == order.OrderId).Sum(d => d.Count * d.Price);

                string loginaccount = "U2s0fa5rlFWv740s6351";
                string terminal = "81346758";


                // ایجاد یک شی از 
                //ParsianServiceReference
                //برای پرداخت
                var saleData = new BankPayment.ClientSaleRequestData();
                saleData.LoginAccount = loginaccount; // 1- پین
                saleData.Amount = amount; // 2- مبلغ
                saleData.OrderId = order.OrderId;  // 3- شمار سفارش
                saleData.CallBackUrl = redirectPage;   // 4- آدرس برگشت

                // ایجاد یک شی از سرویس فوق

                var saleService = new BankPayment.SaleServiceSoapClient(BankPayment.SaleServiceSoapClient.EndpointConfiguration.SaleServiceSoap);
                var requestResult = saleService.SalePaymentRequestAsync(saleData).GetAwaiter().GetResult();


                await TelegramService.SendMessageToDynamicGroupPost(JsonConvert.SerializeObject(requestResult), GroupTelegram);
                // بررسی وجود اطلاعات ارسال شده از درگاه بانک
                if (requestResult.Body.SalePaymentRequestResult.Status == 0 && requestResult.Body.SalePaymentRequestResult.Token > 0)
                {
                    // ویرایش اطلاعات در دیتابیس
                    //await UpdatePayment(paymentId, null, null, requestResult.Body.SalePaymentRequestResult.Token, null, 0, null, false);

                    // اتصال به درگاه بانک
                    Response.Redirect("https://pec.shaparak.ir/NewIPG/?Token=" + requestResult.Body.SalePaymentRequestResult.Token);
                }
                else
                {
                    // ویرایش اطلاعات ارسالی از بانک در صورت عدم اتصال
                    //await UpdatePayment(paymentId, requestResult.Body.SalePaymentRequestResult.Status.ToString(), requestResult.Body.SalePaymentRequestResult.Message, 0, null, 0, null, false);

                    // بانک پیام علت عدم اتصال به بانک را هم ارسال می کند که می توانید به کاربر نمایش دهید
                    // استفاده کنید PaymentResult  یا اینکه از کلاس
                    //requestResult.Result.Body.SalePaymentRequestResult.Message


                    // نمایش خطا به کاربر                    
                    ViewBag.Message = Infrastructure.PaymentResult.Parsian(requestResult.Body.SalePaymentRequestResult.Status.ToString());
                }

            }
            catch (Exception)
            {

                ViewBag.message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
            }
            return View("PaymentError", ViewBag.message);
        }

        public async Task<T> CallApi<T>(string apiUrl, object value) where T : new()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                var json = JsonConvert.SerializeObject(value);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var w = client.PostAsync(apiUrl, content);
                w.Wait();

                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    result.Wait();
                    return JsonConvert.DeserializeObject<T>(result.Result);
                }
                return new T();
            }
        }
    }
}




