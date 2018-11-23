using com.coto.app.azure.chat.api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace com.coto.app.azure.chat.api.Helpers
{
    public class GetMessageHelper
    {
        //private entity.appservice.APPServiceContext db = new entity.appservice.APPServiceContext();

        //internal List<LinkCaseMessagesData> GetMessage(Guid linkChannelId, DateTime fromDate, string customerId, string refId)
        //{
        //    //data contract servicio primero
        //    LinkCustomerSubscriptionNew lcsn = new LinkCustomerSubscriptionNew { CustomerId = customerId, LinkChannelId = linkChannelId, UserName = "pepe" };

        //    //base uri
        //    Uri baseAddress = new Uri("http://cotoapp-lab-appservice.azurewebsites.net/");


        //    HttpClient httpclient = new HttpClient();
        //    httpclient.BaseAddress = baseAddress;

        //    //llamada al 1er servicio
        //    HttpResponseMessage resp = httpclient.PostAsJsonAsync("api/LinkCustomerSubscription", lcsn).Result;
            
        //    resp.EnsureSuccessStatusCode();
        //    var result = resp.Content.ReadAsStringAsync().Result;
        //    SubscriptionResult subscriptionResult = JsonConvert.DeserializeObject<SubscriptionResult>(result);
        //    //var subscriptionId = result.SubscriptionId;

        //    //data contract servicio primero
        //    LinkCaseQryFilters linkCaseQryFilters = new LinkCaseQryFilters { CustomerId = customerId, FromDate = fromDate, RefId = "", SubscriptionId =  Guid.Parse(subscriptionResult.SubscriptionId) };
        //    //llamada al 2do servicio
        //    HttpResponseMessage resp2 = httpclient.PostAsJsonAsync("api/LinkCaseQry", linkCaseQryFilters).Result;
        //    resp.EnsureSuccessStatusCode();
        //    var result2 = resp2.Content.ReadAsStringAsync().Result;

        //    LinkCaseResult linkCaseResult = JsonConvert.DeserializeObject<LinkCaseResult>(result2);

        //    var linkCases = linkCaseResult.Data;

        //    var list = linkCaseResult.Data.FirstOrDefault().LinkCaseMessagesData;
        //    return list;
        //}
    }

    public class LinkCaseQryFilters
    {
        public Guid SubscriptionId { get; set; }
        public DateTime FromDate { get; set; }
        public string RefId { get; set; }
        public string CustomerId { get; set; }
    }

    public class LinkCustomerSubscriptionNew
    {
        public string CustomerId { get; set; }
        public string UserName { get; set; }
        public Guid LinkChannelId { get; set; }
    }

    public class SubscriptionResult
    {
        public string SubscriptionId { get; set; }
        public bool AccessGranted { get; set; }
        public bool Error { get; set; }
        public object ErrorMessage { get; set; }
        public object HostIP { get; set; }
    }

    //****************************************************************
    public class LinkCaseMetadata
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class LinkCaseMessagesData
    {
        public string Id { get; set; }
        public string LinkCaseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSystemMessage { get; set; }
        public string SystemCode { get; set; }
        public bool IsUserMessage { get; set; }
        public bool IsCustomerMessage { get; set; }
        public string AvatarName { get; set; }
        public string AvatarImageURL { get; set; }
        public string Message { get; set; }
        public string TrackingSharedId { get; set; }
    }

    public class Datum
    {
        public string Id { get; set; }
        public string Ref { get; set; }
        public string RefId { get; set; }
        public DateTime Date { get; set; }
        public string LinkChannelId { get; set; }
        public List<LinkCaseMetadata> LinkCaseMetadatas { get; set; }
        public List<LinkCaseMessagesData> LinkCaseMessagesData { get; set; }
        public string CustomerId { get; set; }
    }

    public class LinkCaseResult
    {
        public bool Error { get; set; }
        public List<Datum> Data { get; set; }
    }

    public class LinkCaseMessagePost
    {
        public Guid SubscriptionId { get; set; }
        public string CustomerId { get; set; }
        public string RefId { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Message { get; set; }
        public bool SendEmail { get; set; }
        public string SendEmailTo { get; set; }
        public bool SendPush { get; set; }
        public string SendPushTo { get; set; }
    }
}