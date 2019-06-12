using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneRainGraph
{
    class TokenModel
    {
        //format
        public string f { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int expiration { get; set; }

        public string client { get; set; }

        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="expiration"></param>
        /// <param name="f"></param>
        public TokenModel(string username, string password, string ip, int expiration, string f = "json")
        {
            this.expiration = expiration;
            this.f = f;
            //this.ip = ip;
            this.password = password;
            this.username = username;
        }

        /// <summary>
        /// modelin query string halinde dönüşünü sağlar
        /// </summary>
        /// <returns></returns>
        public string GetQueryStringParameter()
        {
            //"&ip=" + this.ip
            return "f=" + this.f + "&username=" + this.username + "&password=" + this.password  + "&expiration=" + this.expiration;
        }
    }
}
