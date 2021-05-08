using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Checkboxviewmodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string symbol { get; set; }
        public bool Checked { get; set; }
    }
}