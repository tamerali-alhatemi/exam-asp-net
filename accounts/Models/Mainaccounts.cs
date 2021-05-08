using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Mainaccounts
    {
        [Key]
        public int Main_id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Accounts> accounts { get; set; }
    }
}