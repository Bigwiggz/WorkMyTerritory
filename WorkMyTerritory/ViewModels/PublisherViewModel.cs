using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;
using WorkMyTerritory.Models.ModelExtentions;

namespace WorkMyTerritory.ViewModels
{
    public class PublisherViewModel
    {
        public int Id { get; set; }

        public int FKPublisherCongregation { get; set; }

        //User Information
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }


        [MaxLength(30)]
        public string PublisherFirstName { get; set; }

        [MaxLength(30)]
        public string PublisherLastName { get; set; }


        public EnumPublisherSex EnumPublisherSex { get; set; }


        [Display(Name = "Publisher Privileges")]
        public EnumPublisherPrivileges EnumPublisherPrivileges { get; set; }

        /// <summary>
        /// Set Publisher Record Status to Active if the Publisher is first registering
        /// </summary>
        public EnumRecordStatus EnumRecordStatus { get => EnumRecordStatus.Active; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
