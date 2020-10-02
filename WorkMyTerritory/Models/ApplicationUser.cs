using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;
using WorkMyTerritory.Models.ModelExtentions;

namespace WorkMyTerritory.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string PublisherFirstName { get; set; }

        public string PublisherLastName { get; set; }

        //To handle the publishersex enum
        [Column("EnumPublisherSex")]
        public string PublisherSex
        {
            get { return EnumPublisherSex.ToString(); }
            private set { EnumPublisherSex = value.ParseEnum<EnumPublisherSex>(); }
        }
        [NotMapped]
        public EnumPublisherSex EnumPublisherSex { get; set; }

        //To handle the publisherprivileges enum
        [Column("EnumPublisherPrivileges")]
        public string PublisherPrivileges
        {
            get { return EnumPublisherPrivileges.ToString(); }
            private set { EnumPublisherPrivileges = value.ParseEnum<EnumPublisherPrivileges>(); }
        }

        [NotMapped]
        public EnumPublisherPrivileges EnumPublisherPrivileges { get; set; }


        public int FKPublisherCongregation { get; set; }

        //To handle the RecordStatus enum
        [Column("EnumRecordStatus")]
        public string PublisherActive 
        {
            get { return EnumRecordStatus.ToString(); }
            private set { EnumRecordStatus = value.ParseEnum<EnumRecordStatus>(); }
        }
        [NotMapped]
        public EnumRecordStatus EnumRecordStatus { get; set; }
    }
}
