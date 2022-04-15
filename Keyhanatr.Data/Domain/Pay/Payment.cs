using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Pay
{

    public class Payment
    {
        #region سازنده پیش فرض
        //public Payment()
        //{
        //    InsertDatetime = System.DateTime.Now;
        //}
        #endregion

        #region Property
        [Key]
        [Display(Name = "شماره پرداخت")]
        public int PaymentId { get; set; }

        public long Token { get; set; }

        public string TerminalNo { get; set; }


        [Display(Name = "شماره مرجع")]
        public long RRN { get; set; }


        [Display(Name = "شماره کارت کاربر بصورت ماسک شده")]
        public string HashCardNumber { get; set; }


        [Display(Name = "پیام وضعیت تراکنش")]
        [MaxLength(200)]
        public string StatusMessage { get; set; }


        [Display(Name = "وضعیت پرداخت")]
        [MaxLength(100)]
        public string StatusPayment { get; set; }


        // فقط در صورتی که این فید ترو باشد پرداخت موفق بوده است
        [Display(Name = "وضعیت پرداخت نهایی")]
        public bool PaymentFinished { get; set; }


        [Display(Name = "مبلغ")]
        public long Amount { get; set; }


        [Display(Name = "نام بانک")]
        [MaxLength(50)]
        public string BankName { get; set; }


        [Display(Name = "تاریخ خرید")]
        public DateTime InsertDatetime { get; set; }


        //[ForeignKey("UserId")]
        //public User.User User { get; set; }
        #endregion

    }
}
