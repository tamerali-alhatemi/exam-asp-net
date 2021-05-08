using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Currencies
    {
        [Key]
        public int Currency_id { get; set; }
        public string Name { get; set; }
        public string symbol { get; set; }

        public virtual ICollection<Accountcurrencies> accountcurrencies { get; set; }
    }
}