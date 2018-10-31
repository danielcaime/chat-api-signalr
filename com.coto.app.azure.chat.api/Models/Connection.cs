using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Models
{
    public class Connection
    {
        public string ConnectionID { get; set; }
        public bool isConnected { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public Connection(string connectionId, int customerid)
        {
            this.ConnectionID = connectionId;
            CustomerId = customerid;
            isConnected = true;
        }
    }
}