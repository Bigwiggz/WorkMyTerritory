using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;

namespace WorkMyTerritory.BusinessLayer.ValidationLogic
{
    public class PublisherValidator : AbstractValidator<ApplicationUser>
    {
        public PublisherValidator()
        {
            //
        }
    }
}
