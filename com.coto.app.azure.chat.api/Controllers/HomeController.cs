using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.coto.app.azure.chat.api.Controllers
{
    public class HomeController : ApiController
    {
        public async Task<string> Get()
        {
            try
            {
                string[] userTag = new string[2];
                userTag[0] = "username:" + "root";
                userTag[1] = "from:" + "root";

                var notif = "{ \"data\" : {\"message\":\"" + "From " + "user" + ": " + "mensaje" + "\"}}";

                return String.Format("Success ");// {0} Failure {1}", outcome.Success, outcome.Failure);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
