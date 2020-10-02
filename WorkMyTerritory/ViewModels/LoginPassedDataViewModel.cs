using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;

namespace WorkMyTerritory.ViewModels
{
    public class LoginPassedDataViewModel
    {
        public int Id { get; set; }
        public int CongregationId { get; set; }

    }
}
