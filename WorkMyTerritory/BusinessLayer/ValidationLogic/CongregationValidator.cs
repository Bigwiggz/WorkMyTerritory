using FluentValidation;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Models.ValidationLogic
{
    public class CongregationValidator: AbstractValidator<CongregationViewModel>
    {
        private readonly IEnumerable<Congregation> _congregation;

        public CongregationValidator(IEnumerable<Congregation> congregation)
        {

            _congregation = congregation;
            //Congregation Rule Name
            RuleFor(c => c.CongregationName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} cannot be blank")
                .Length(1, 30).WithMessage("{PropertyName} cannot be more than 30 characaters")
                .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Characters");

            //Congregation Street Address
            RuleFor(c => c.CongregationStreetAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} cannot be blank")
                .Length(1, 50).WithMessage("{PropertyName} cannot be more than 50 characaters");

            //Congregation City Name
            RuleFor(c=>c.CongregationCity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} cannot be blank")
                .Length(1, 30).WithMessage("{PropertyName} cannot be more than 30 characaters");

            //Congregation State Name
            RuleFor(c=>c.CongregationState)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} cannot be blank")
                .Length(2, 2).WithMessage("{PropertyName} must be 2 characaters");

            //Congregation ZIP Code
            RuleFor(c => c.CongregationZIPCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} cannot be blank")
                .Must(IsUSZipCode).WithMessage("{PropertyName} must be a valid US Zip Code");

            //Congregation Lattitude
            RuleFor(c => c.CongregationLatitude)
                .ScalePrecision(3, 20).WithMessage("{PropertyName} have at least 3 digits");

            //Congregation Lattitude
            RuleFor(c => c.CongregationLongitude)
                .ScalePrecision(3, 20).WithMessage("{PropertyName} have at least 3 digits");

            //Congregation Number
            RuleFor(c => c.CongregationNumber)
                .Matches(@"^\d{8}$").WithMessage("{PropertyName} must have less than 8 digits");

            //Unique Congregation Entry
            RuleFor(c => c.CongregationName)
                .Must(BeUniqueCongregationEntry).WithMessage("This congregation record is not unique or already exists.");
            
        }
        protected bool BeAValidName(string Name)
        {
            Name = Name.Replace(" ", "");
            Name = Name.Replace("-", "");

            return Name.All(Char.IsLetter);
        }

        protected bool IsUSZipCode(string ZipCode)
        {
            
            bool validZipCode = true;
            string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            if (!Regex.Match(ZipCode,_usZipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        protected bool BeUniqueCongregationEntry( 
            CongregationViewModel congregationEntry, string newValue)
        {
            var congregationRecords = _congregation;
            // what makes a congregation entry unique
            string uniqueCongIdentifier = congregationEntry.CongregationCity + 
                congregationEntry.CongregationActive + 
                congregationEntry.CongregationLanguage + 
                congregationEntry.CongregationName + 
                congregationEntry.CongregationNumber;

            bool uniqueEntryGiven = true;

            foreach(Congregation cong in _congregation)
            {
                //Item to check for a unique identifier
                string uniqueComparison= cong.CongregationCity +
                cong.CongregationActive +
                cong.CongregationLanguage +
                cong.CongregationName +
                cong.CongregationNumber;

                if (uniqueComparison!=uniqueCongIdentifier)
                {
                    uniqueEntryGiven = false;
                }
            }
            return uniqueEntryGiven;
        }

    }
}
