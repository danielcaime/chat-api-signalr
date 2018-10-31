using com.coto.app.azure.chat.api.Chat;
using com.coto.app.azure.chat.api.Helpers;
using com.coto.app.azure.chat.api.Interfaces;
using com.coto.app.azure.chat.api.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.coto.app.azure.chat.api.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public async Task<string> Send(TrasnportMessage req)
        {
            var message = new TextMessage { Text = req.Text, Title = req.Title };
            var name = req.SendTo;
            try
            {
                var user = ChatHub.connList.Find(c => c.Name == "pepe");

                HubContextHelper.SendMessage(user, message);

                return String.Format("Success ");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
