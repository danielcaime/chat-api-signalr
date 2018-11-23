using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace com.coto.app.azure.chat.api.ServiceConnector
{
    public class MessageGetters
    {
        public List<string> GetMessages()
        {
            List<string> messagesList = new List<string>();
            var url = "http://cotoapp-lab-appservice.azurewebsites.net/api/LinkCaseQry";

            //var filter = new LinkCaseQryFilters { CustomerId = "", FromDate = Convert.ToDateTime(""), RefId = 0, SubscriptionId = new Guid("") };

            try
            {
                // Conexión Back API
                HttpClient httpclient = new HttpClient();      // url;//BackserviceHelper.GetClient(base.ActionContext);

                //object httpclient = null;
                // Result
                //HttpResponseMessage resp = httpclient.PostAsJsonAsync("api/DispatchPointUpdate", dispatchPoint).Result;
                //resp.EnsureSuccessStatusCode();

                //var jsonresult = resp.Content.ReadAsStringAsync().Result;

                //JObject jres = JObject.Parse(jsonresult);

                //return Ok(new { Error = false, ErrorMessage = String.Empty, Data = jres });
            }
            catch (Exception ex)
            {
                //return Ok(new { Error = true, ErrorMessage = ex.Message });
            }
            return messagesList;
        }
        

    }

    public class LinkCaseQryFilters
    {
        public Guid SubscriptionId { get; set; }
        public DateTime FromDate { get; set; }
        public string RefId { get; set; }
        public string CustomerId { get; set; }
    }
}