using Keyhanatr.Core.Interfaces.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Senders
{
    public class MessageSender : IMessageSender
    {
        public void SendMessage(string mobile, string activeCode, string text)
        {
            //var sender = "100047778";
            var receptor = mobile;
            var token = activeCode;
            var message = text;
            var api = new Kavenegar.KavenegarApi("53716F306C4839477468394F79344565306F5A47745A503865476E31756576677964566643517635442F303D"); 
            api.VerifyLookup(receptor,token, message);
        }
    }
}
