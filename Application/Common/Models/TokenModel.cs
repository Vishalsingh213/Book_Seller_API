using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class TokenModel
    {
        public int userid { get; set; }
        public string username { get; set; }
        public int roleid { get; set; }
        public string accesstoken { get; set; }
        public HttpContext request { get; set; }

        public static implicit operator TokenModel(string v)
        {
            throw new NotImplementedException();
        }
    }
}
