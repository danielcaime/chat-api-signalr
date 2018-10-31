using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Text;
using com.coto.app.azure.chat.api.Models;
using com.coto.app.azure.chat.api.Interfaces;

namespace com.coto.app.azure.chat.api.Chat
{
    public class ChatHub : Hub
    {
        //lista de conexiones 
        public static List<Connection> connList = new List<Connection>();

        /// <summary>
        /// Enviar un mensaje al chat group
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            var msg = new TextMessage { Title = message, Text = message };
            var id = Context.ConnectionId;
            //atencion para enviar un mensaje a un grupo determinado se selecciona el grupo y se hace un broadcast a este
            //(todos los ejemplos con otros metodos no funcionan)
            //enviar un mensaje a todo el grupo
            Clients.Group(name).broadcastMessage(name, msg);
        }

        public void SendMessage(string name, IMessage message )
        {
            Clients.Group(name).broadcastMessage(name, message);
        }

        /// <summary>
        /// Enviar imagen al chat group
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendImage(string name, string message)
        {
            var s = "iV BORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/ 9hAAAABGdBTUEAAK / INwWK6QAAABl0RVh0U29mdHd hcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAHjSURBVDjLdZO / alVBEMZ / 5 + TemxAbFUUskqAoSOJNp4KC 4AsoPoGFIHY + gA + jiJXaKIiChbETtBYLUbSMRf6Aydndmfks9kRjvHdhGVh2fvN9uzONJK7fe7Ai6algA 3FZCAmQqEF / dnihpK1v7x7dPw0woF64Izg3Xl5s1n9uIe0lQYUFCtjc + sVuEqHBKfpVAXB1vLzQXFtdYP HkGFUCoahVo1Y / fnie + bkBV27c5R8A0pHxyhKvPn5hY2MHRQAQeyokFGJze4cuZfav3gLNYDTg7Pklzpw 4ijtIQYRwFx6BhdjtCk + erU0CCPfg +/ o2o3ZI13WUlLGo58YMg + GIY4dmCWkCAAgPzAspJW5ePFPlV3VI 4uHbz5S5IQfy / yooHngxzFser30iFcNcuAVGw3A0Ilt91IkAsyCXQg5QO0szHEIrogkiguwN2acCoJhjn ZGKYx4Ujz5WOA2YD1BMU + BBSYVUvNpxkXuIuWgbsOxTHrG3UHIFWIhsgXtQQpTizNBS5jXZQkhkcywZqQ QlAjdRwiml7wU5xWLaL1AvZa8WIjALzIRZ7YVWDW5CiIj48Z8F2pYLl1ZR0 + AuzEX0UX035mxIkLq0dhD w5vXL97fr5O3rfwQHJhPx4uuH57f2AL8BfPrVlrs6xwsAAAAASUVORK5CYII = ";
            // alt="libro azul"

            byte[] arr = Convert.FromBase64String(s);// Encoding.ASCII.GetBytes( ("holamundo");// 
            //var arr2 = Convert.ToBase64String(arr);

            var imageMessage = new ImageMessage
            {
                ImageHeaders = "data:image / png;base64,", //"data:" + file.ContentType + ";base64,",
                ImageBinary = arr
            };

            //atencion para enviar un mensaje a un grupo determinado se selecciona el grupo y se hace un broadcast a este
            //(todos los ejemplos con otros metodos no funcionan)
            //enviar un mensaje a todo el grupo
            Clients.Group(name).broadcastMessage(name, imageMessage);
        }

        //public void Send(string name, string message)
        //{
        //    //atencion para enviar un mensaje a un grupo determinado se selecciona el grupo y se hace un broadcast a este
        //    //(todos los ejemplos con otros metodos no funcionan)
        //    //enviar un mensaje a todo el grupo
        //    Clients.Group(name).broadcastMessage(name, message);
        //}

        public override Task OnConnected()
        {
            //mantenemos una lista de conexiones y luego completamos los datos de los usuarios.
            connList.Add(new Connection(Context.ConnectionId, 0));
            return base.OnConnected();
        }

        public async Task JoinGroup(string groupName)
        {
            //await Groups.Add(Context.ConnectionId, groupName);
            //Clients.Group(groupName).addChatMessage(Context.User.Identity.Name + " joined.");
        }
        public void RegisterClient(string name, string channel)
        {
            //al registrarse el cliente se arma un grupo con este - un grupo por cada cliente.
            Groups.Add(Context.ConnectionId, name);
            connList.Find(c => c.ConnectionID == Context.ConnectionId).Name = name;
        }

        //cualquier funcion publica definida en esta clase, será publicada y estará disponible para llamar desde el jscript cliente
        //NOTA: los nombres de funcion con letra capital, seran trasnformados a minúscula ejemplo   Mifuncion() => mifuncion()

        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).ReceiveMessage(" joined.");
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //en la desconexion cambiamos de estado
            connList.Find(c => c.ConnectionID == Context.ConnectionId).isConnected = false;

            if (stopCalled)
            {
                Console.WriteLine(String.Format("El cliente {0} se desconectó.", Context.ConnectionId));
            }
            else
            {
                Console.WriteLine(String.Format("Expiró el tiempo del cliente {0} .", Context.ConnectionId));
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            //en la reconexion cambiamos de estado
            connList.Find(c => c.ConnectionID == Context.ConnectionId).isConnected = true;
            Console.WriteLine(String.Format("El cliente {0} se reconectó.", Context.ConnectionId));
            return base.OnReconnected();
        }
    }
}