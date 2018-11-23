using com.coto.app.azure.chat.api.Chat;
using com.coto.app.azure.chat.api.Interfaces;
using com.coto.app.azure.chat.api.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Timers;

namespace com.coto.app.azure.chat.api.Helpers
{
    public class HubContextHelper
    {

        /// <summary>
        /// Send a message to a group
        /// </summary>
        /// <param name="user">group name</param>
        /// <param name="message"></param>
        /// <param name="message">IMessage</param>
        public static void SendMessage(Connection user, IMessage message, string sendFrom)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            //persistir el mensaje
            context.Clients.Group(user.Name).broadcastMessage(sendFrom, message);
        }

        //public static void TimedProcess()//string customerId, Guid linkChannelId, string userName)
        //{
        //    System.Timers.Timer aTimer = new System.Timers.Timer(5000);
        //    aTimer.Elapsed += new System.Timers.ElapsedEventHandler(VerifyMessages);
        //    aTimer.Enabled = true;
        //}

        internal static void VerifyMessages(object sender, ElapsedEventArgs e)
        {
            List<LinkCaseMessagesData> list;
            var fromDate = DateTime.Parse("01/01/2018");
            
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            //persistir el mensaje
            //context.Clients.Group(user.Name).broadcastMessage("test", message);
            if (ChatHub.connList.Count() > 0)
            {
                System.Threading.Tasks.Parallel.For(0, ChatHub.connList.Count(), x =>
                {
                    var conn = ChatHub.connList[x];
                    if (conn.isConnected && !string.IsNullOrEmpty(conn.RefId))
                    {
                        list = HubContextHelper.GetCasesMessages(conn.CustomerId, fromDate, conn.RefId, Guid.Parse(conn.SubscriptionId)).FirstOrDefault().LinkCaseMessagesData;

                        if (list.Count() == 0)
                            return;

                        list.ForEach(m =>
                        {
                            context.Clients.Group(conn.Name).broadcastMessage(m.AvatarName, new TextMessage { Text = m.Message, Title = m.Message, Id = m.Id });
                            //if(!ChatHub.msgList.Any(n => n.Id == m.Id))//verifico si ya existe el mensaje, si no lo agrego y lo envío 
                            //{
                            //    ChatHub.msgList.Add(m);
                            //    context.Clients.Group(conn.Name).broadcastMessage(m.AvatarName, new TextMessage { Text = m.Message, Title = m.Message });
                            //}
                        });
                    }
                });
            }
        }

        internal static string GetCases(string customerId, Guid linkChannelId, string userName)
        {
            var subs = getSubscription(customerId, linkChannelId, userName);
            //ChatHub.connList.Find(c => c.CustomerId == customerId);
            return subs;
        }

        internal static List<Datum> GetCasesMessages(string customerId, DateTime fromDate, string refId, Guid subscriptionId)
        {
            Uri baseAddress = new Uri("https://cotoapp-lab-appservice.azurewebsites.net/");
            //Uri baseAddress = new Uri("http://localhost:62888/");

            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = baseAddress;

            //data contract servicio primero
            LinkCaseQryFilters linkCaseQryFilters = new LinkCaseQryFilters { CustomerId = customerId, FromDate = fromDate, RefId = refId, SubscriptionId = subscriptionId };
            try
            {
                //llamada al 2do servicio
                HttpResponseMessage resp = httpclient.PostAsJsonAsync("api/LinkCaseQry", linkCaseQryFilters).Result;
                resp.EnsureSuccessStatusCode();
                var result = resp.Content.ReadAsStringAsync().Result;

                LinkCaseResult linkCaseResult = JsonConvert.DeserializeObject<LinkCaseResult>(result);

                var linkCases = linkCaseResult.Data;

                return linkCaseResult.Data;
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                //throw;
                return new List<Datum>();
            }

        }

        private static string getSubscription(string customerId, Guid linkChannelId, string userName)
        {
            //data contract servicio primero
            LinkCustomerSubscriptionNew lcsn = new LinkCustomerSubscriptionNew { CustomerId = customerId, LinkChannelId = linkChannelId, UserName = userName };
            //base uri
            Uri baseAddress = new Uri("https://cotoapp-lab-appservice.azurewebsites.net/");
            //Uri baseAddress = new Uri("http://localhost:62888/");

            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = baseAddress;

            HttpResponseMessage resp = httpclient.PostAsJsonAsync("api/LinkCustomerSubscription", lcsn).Result;
            if (!resp.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            resp.EnsureSuccessStatusCode();
            var result = resp.Content.ReadAsStringAsync().Result;
            SubscriptionResult subscriptionResult = JsonConvert.DeserializeObject<SubscriptionResult>(result);
            return subscriptionResult.SubscriptionId;
        }

        internal static void SendClientMessage(Connection conn, TextMessage msg)
        {
            LinkCaseMessagePost message = new LinkCaseMessagePost
            {
                CustomerId = conn.CustomerId,
                Date = DateTime.Now,
                Message = msg.Text,
                RefId = conn.RefId,
                SendEmail = false,
                SendEmailTo = string.Empty,
                SendPush = false,
                SubscriptionId = Guid.Parse(conn.SubscriptionId)
            };
            Uri baseAddress = new Uri("https://cotoapp-lab-appservice.azurewebsites.net/");

            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = baseAddress;

            HttpResponseMessage resp = httpclient.PostAsJsonAsync("api/LinkCaseMessage", message).Result;
            //if (!resp.IsSuccessStatusCode)

            resp.EnsureSuccessStatusCode();
            var result = resp.Content.ReadAsStringAsync().Result;
            SubscriptionResult subscriptionResult = JsonConvert.DeserializeObject<SubscriptionResult>(result);

        }
    }
}