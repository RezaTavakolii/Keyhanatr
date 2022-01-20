using Keyhanatr.Core.Convertor;
using Keyhanatr.Core.Interfaces.Discounts;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Discounts
{
    public class DiscountServices : IDiscountServices
    {
        private KeyhanatrContext _context;
        private object product;

        public DiscountServices(KeyhanatrContext context)
        {
            _context = context;
        }


        public void AddDiscountToAllProducts()
        {
            throw new NotImplementedException();
        }

        public void AddDiscountToOneProduct(Discount discount, int productId)
        {
            //check if there is alredy a discount
            if (_context.Products.FirstOrDefault(p => p.ProductId == productId).DiscountId != null)
            {
                var discountId = _context.Products.FirstOrDefault(p => p.ProductId == productId).DiscountId;
                var currentDiscount = _context.Discounts.FirstOrDefault(p => p.DiscountId == discountId);

                _context.Discounts.Remove(currentDiscount);
            }

            _context.Discounts.Add(discount);
            _context.SaveChanges();

            var CurrentDiscount = _context.Discounts.Find(discount.DiscountId);
            var product = GetProductByProductId(productId);
            product.DiscountId = CurrentDiscount.DiscountId;
            _context.Products.Update(product);

            _context.SaveChanges();


        }



        public Product GetProductByProductId(int productId)
        {
            return _context.Products.Include(d => d.Discount).FirstOrDefault(p => p.ProductId == productId);
        }


        public Discount GetDiscountByProductId(int productId)
        {
            return _context.Products.Include(d => d.Discount)
                .FirstOrDefault(p => p.ProductId == productId).Discount;
        }


        public void deleteExpiredDiscounts()
        {
            if (_context.Discounts.Any(d => d.EndDate < DateTime.Now))
            {

                var products = _context.Products.Include(d => d.Discount).Where(p => p.DiscountId != null).ToList();
                foreach (var item in products)
                {
                    if (item.Discount.EndDate != null && item.Discount.EndDate.Value.ToShamsi().ToMiladiDateTime() < DateTime.Now)
                    {
                        var d = item.Discount.EndDate;

                        item.DiscountId = null;
                        _context.Products.Update(item);
                        _context.Discounts.Remove(item.Discount);
                    }
                }
                _context.SaveChanges();

                //var discounts=_context.Discounts.Where(d => d.EndDate < DateTime.Now).ToList();
                ////var products= _context.Products.Where(p => p.DiscountId==).ToList();

                //discounts.ForEach(d=>
                //_context.Products.Where(p => p.DiscountId ==d.DiscountId).ToList().ForEach(

                //    p=> p.DiscountId=null,
                //    )

                //);


                //discounts.ForEach(
                //    d =>
                //    _context.Discounts.Remove(d)
                //    );
            }
        }

        public void EditDiscount(Discount discount, string startDate, string endDate)
        {

            //var currentDiscount=_context.Discounts.Find()

            if (!string.IsNullOrEmpty(startDate))
            {
                string[] std = startDate.Split("/");
                discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }
            else
            {
                discount.StartDate = discount.StartDate;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                string[] std = endDate.Split("/");
                discount.EndDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }
            else
            {
                discount.EndDate = discount.EndDate;
            }

            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public void DeleteDiscountByProductId(int productId)
        {
            //get current DiscountId of this Product before assigning null into this product in order to then remove this discount!
            var discountId = GetProductByProductId(productId).Discount.DiscountId;
            
            var product=_context.Products.FirstOrDefault(p => p.ProductId == productId);
            product.DiscountId = null;
            
            var discount = _context.Discounts.FirstOrDefault(d => d.DiscountId == discountId);
            _context.Discounts.Remove(discount);
            
            _context.Products.Update(product);

            _context.SaveChanges();
        }
    }
}
