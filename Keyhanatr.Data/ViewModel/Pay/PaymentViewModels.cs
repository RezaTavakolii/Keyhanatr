using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.ViewModel.Pay
{
    public class RequestPaymentResult
    {
        public int Status { get; set; }
        public long Token { get; set; }
    }

    public class CallbackRequestPayment
    {
        public string Token { get; set; }
        public int Status { get; set; }
        public long OrderId { get; set; }
        public string TerminalNo { get; set; }
        public string RRN { get; set; }
        public string HashCardNumber { get; set; }
        public string Amount { get; set; }
        //public string Message { get; set; }
    }

    public class VerifyResultData
    {
        public int Status { get; set; }
        public string LoginAccount { get; set; }
        public string Token { get; set; }

        //public int ResCode { get; set; }
        //public string Description { get; set; }
        //public string Amount { get; set; }
        //public string RetrivalRefNo { get; set; }
        //public string SystemTraceNo { get; set; }
        //public string OrderId { get; set; }
    }
}
