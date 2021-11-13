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
            var api = new Kavenegar.KavenegarApi("36763371524379327039656F75536C6C504B62746C426E697A3542524E64494E764D5146725A39436E736B3D"); 
            api.VerifyLookup(receptor,token, message);
        }
    }
}
