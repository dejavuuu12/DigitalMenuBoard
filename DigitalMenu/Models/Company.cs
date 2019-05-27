using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalMenu.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPerson { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}