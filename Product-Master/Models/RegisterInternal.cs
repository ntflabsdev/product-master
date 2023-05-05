using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_Master.Models
{
    public class RegisterInternal
    {
        public string username { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
        public string userid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string designation { get; set; }
        public string employee_id { get; set; }
        public string email_id { get; set; }
        public string confirmPassword { get; set; }
    }
}