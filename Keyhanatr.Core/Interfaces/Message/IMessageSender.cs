using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Message
{
    public interface IMessageSender
    {
        void SendMessage(string mobile, string activeCode, string text);
    }
}
