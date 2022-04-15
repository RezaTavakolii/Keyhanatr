using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Core.Interfaces.Sliders;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.ViewModel.Pay;
using Keyhanatr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Controllers
{
    public class HomeController : Controller
    {

        private KeyhanatrContext _context;
        public HomeController(KeyhanatrContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CallBack(CallbackRequestPayment result)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == result.OrderId);
            if (order == null)
            {
                return NotFound();
            }
            string loginaccount = "U2s0fa5rlFWv740s6351";
            string terminal = "81346758";

            //var byteData = Encoding.UTF8.GetBytes(result.Token);

            //var algorithm = SymmetricAlgorithm.Create("TripleDes");
            //algorithm.Mode = CipherMode.ECB;
            //algorithm.Padding = PaddingMode.PKCS7;

            //var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(loginaccount), new byte[8]);
            //string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {
                Token = result.Token,
                Status = result.Status
            };
            var verifyRes = CallApi<VerifyResultData>("https://pec.shaparak.ir/NewIPGServices/Confirm/ConfirmService", data).Result;
            if (verifyRes.Status == 0)
            {
                order.IsFinaly = true;

                _context.Orders.Update(order);
                _context.SaveChanges();

                return View("SuccessPaymentView", verifyRes);
            }
            else
            {
                return View("ErrorPaymentView", verifyRes);
            }

            return Content("");
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
