using com.coto.app.azure.chat.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Models
{
    public class TrasnportMessage
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string SendTo { get; set; }
    }
}