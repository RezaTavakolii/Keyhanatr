using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Discounts
{
    public interface IDiscountServices
    {
        Product GetProductByProductId(int productId);
        Discount GetDiscountByProductId(int productId);
        void AddDiscountToOneProduct(Discount discount, int productId);
        void AddDiscountToAllProducts();

        void deleteExpiredDiscounts();

        void EditDiscount(Discount discount,string startDate,string endDate);

        void DeleteDiscountByProductId(int productId);
    }
}
