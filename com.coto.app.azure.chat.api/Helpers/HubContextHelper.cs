using com.coto.app.azure.chat.api.Chat;
using com.coto.app.azure.chat.api.Interfaces;
using com.coto.app.azure.chat.api.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Helpers
{
    public class HubContextHelper
    {
        public static void SendMessage(Connection user, IMessage message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            //persistir el mensaje
            context.Clients.Group(user.Name).broadcastMessage("test", message);
        }
    }
}