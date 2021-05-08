using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class DisplayAccounts
    {
        public int Account_id { get; set; }
        public string Account_name { get; set; }
        public int Account_number { get; set; }
        public string FatherName { get; set; }
        public List<Checkboxviewmodel> currncies { get; set; }
    }
}