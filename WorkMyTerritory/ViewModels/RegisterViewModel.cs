using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.ViewModels
{
    public class RegisterViewModel
    {
        //User Information
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse",controller:"Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Display(Name = "First Name")]
        public string PublisherFirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string PublisherLastName { get; set; }
  
        [Required]
        [Display(Name = "Gender")]
        public EnumPublisherSex PublisherSex { get; set; }


        [Display(Name = "Publisher Privileges")]
        public EnumPublisherPrivileges PublisherPrivileges { get; set; }

        public string FKFieldServiceGroup { get; set; }
        /// <summary>
        /// Set Publisher Record Status to Active if the Publisher is first registering
        /// </summary>
        public EnumRecordStatus PublisherActive { get=> EnumRecordStatus.Active; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
