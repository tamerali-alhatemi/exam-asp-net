using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Accountcurrencies
    {
        [Key]
        public int Id { get; set; }
        public int Account_id { get; set; }
        public int Currency_id { get; set; }

        public virtual Accounts accoutns { get; set; }
        public virtual Currencies currencies { get; set; }
    }
}