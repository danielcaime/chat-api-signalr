using com.coto.app.azure.chat.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Models
{
    public class CaseMessage : IMessage
    {
        public string Type
        {
            get { return "case"; }
        }
        public string Ref { get; set; }
        public string RefId { get; set; }

    }
}