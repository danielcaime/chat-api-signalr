using com.coto.app.azure.chat.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Models
{
    public class ImageMessage : IMessage
    {
        public string Type
        {
            get { return "image"; }
        }
        public byte[] ImageBinary { get; set; }
        public string ImageHeaders { get; set; }
    }
}