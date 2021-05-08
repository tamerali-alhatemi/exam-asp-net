using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace accounts.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="User Name Is Requaired")]
        [Display(Name="User Name")]
        [StringLength(50)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password Is Requaired")]
        [Display(Name = "Password")]
        [StringLength(50)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isActive { get; set; }
    }
}