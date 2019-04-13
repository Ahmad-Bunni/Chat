using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Domain.Model
{
    public class Authentication
    {
        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
