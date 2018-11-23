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
            try
            {
                var message = MessageFactory.MessageCreator(req);

                //verificar de que manera vamos a buscar a los usuarios conectados: id, connectionId, customerId, etc
                var user = ChatHub.connList.Find(c => c.Name == req.SendTo);
                //validar si el usuario esta conectado por si es necesario un reintento del mensaje
                //user.isConnected
                
                HubContextHelper.SendMessage(user, message, req.SendFrom);

                return String.Format("Success ");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
