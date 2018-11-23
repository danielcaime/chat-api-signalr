using com.coto.app.azure.chat.api.Interfaces;
using com.coto.app.azure.chat.api.Models;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Helpers
{
    public class MessageFactory
    {
        public static TextMessage MessageCreator(TrasnportMessage message)
        {
            TextMessage msg;
            try
            {
                msg = new TextMessage { Title = message.Title, Text = message.Text };
            }
            catch (Exception)
            {

                throw;
            }

            return msg;
        }        
    }
}