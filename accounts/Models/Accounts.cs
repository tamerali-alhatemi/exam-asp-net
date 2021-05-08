using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Accounts
    {
        [Key]
        public int Account_id { get; set; }
        [Required(ErrorMessage = "Account Name is Required")]
        [DataType(DataType.Text)]
        public string Account_name { get; set; }
        [Required(ErrorMessage = "Account number is Required")]
        public int Account_number { get; set; }
        public int Main_id { get; set; }
        
        public virtual Mainaccounts mainaccount { get; set; }

        public virtual ICollection<Accountcurrencies> accountcurrencies { get; set; }
    }
}