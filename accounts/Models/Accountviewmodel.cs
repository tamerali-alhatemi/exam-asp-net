using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Accountviewmodel
    {
        public int Account_id { get; set; }
        public string Account_name { get; set; }
        public int Account_number { get; set; }
        public int Main_id { get; set; }
        public virtual Mainaccounts mainaccount { get; set; }
        public List<Checkboxviewmodel> currncies { get; set; }
    }
}