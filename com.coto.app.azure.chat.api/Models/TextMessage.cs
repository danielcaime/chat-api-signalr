using com.coto.app.azure.chat.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Models
{
    public class TextMessage : IMessage
    {
        public string Type
        {
            get { return "text"; }
        }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}