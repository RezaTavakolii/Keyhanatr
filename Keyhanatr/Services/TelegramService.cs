using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Services
{
    public class TelegramService
    {
        public async Task SendMessageToGroup(string MessageText, string Chatid)
        {


            try
            {
                var url = "https://tel.asanglobal.ir/api/TelegramMessage/SendMessageToDynamicGroup/" + MessageText + "/" + Chatid;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string STRResult = await reader.ReadToEndAsync();

                }

            }
            catch (Exception E)
            {

            }


        }

        public class SendMessageToDynamicGroupPostCls
        {
            public string Text { get; set; }

            public string Chaid { get; set; }
        }

        public class SendMessageToDynamicGroupPostClsResponse
        {
            public bool Send { get; set; }
        }

        public static async Task<bool> SendMessageToDynamicGroupPost(string MessageText, string Chatid)
        {

            try
            {
                SendMessageToDynamicGroupPostCls NewRequest = new SendMessageToDynamicGroupPostCls();
                NewRequest.Text = MessageText;
                NewRequest.Chaid = Chatid;



                HttpClient Client = new HttpClient();
                HttpResponseMessage Response = new HttpResponseMessage();
                string json = JsonConvert.SerializeObject(NewRequest);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Random r = new Random();
                Response = await Client.PostAsync("https://tel.asanglobal.ir/api/TelegramMessage/SendMessageToDynamicGroupPost", httpContent);

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    string StrResponse = Response.Content.ReadAsStringAsync().Result;

                    var results = JsonConvert.DeserializeObject<SendMessageToDynamicGroupPostClsResponse>(StrResponse);

                    return results.Send;
                }
            }
            catch (Exception E)
            {

            }

            return false;

        }

       
    }
}

