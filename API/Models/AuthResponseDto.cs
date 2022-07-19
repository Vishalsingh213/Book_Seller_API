using System;
using System.Collections.Generic;

namespace API.Models
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public Dictionary<string, string> Access_Permission { get; set; }
    }
}
