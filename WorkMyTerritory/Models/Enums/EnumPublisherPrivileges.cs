using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.Enums
{
    public enum EnumPublisherPrivileges
    {
        [Display(Name =("Unbaptized Publisher"))]
        Unbaptized_Publisher,
        [Display(Name =("Publisher"))]
        Publisher,
        [Display(Name =("Reg Auxiliary Pioneer"))]
        Reg_Auxiliary_Pioneer,
        [Display(Name =("Regular Pioneer"))]
        Regular_Pioneer,
        [Display(Name =("Special Pioneer"))]
        Special_Pioneer,
        [Display(Name =("Missionary"))]
        Missionary
    }
}
