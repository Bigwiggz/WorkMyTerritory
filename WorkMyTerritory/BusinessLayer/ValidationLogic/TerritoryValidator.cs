using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.BusinessLayer.ValidationLogic
{
    public class TerritoryValidator:AbstractValidator<TerritoryViewModel>
    {
        private readonly IEnumerable<CongregationTerritories> _congregationTerritory;

        public TerritoryValidator(IEnumerable<CongregationTerritories> congregationTerritory)
        {
            _congregationTerritory = congregationTerritory;

            //Territory Number Rule
            RuleFor(c => c.TerritoryNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide a territory number")
                .Must(BeUniqueTerritoryNumber).WithMessage("This territory number {PropertyName} already exists");
                
            //Territory Notes
            RuleFor(c => c.TerritoryHiddenNotes)
                .MaximumLength(200).WithMessage("Hidden Notes Field with {TotalLength} characters cannot be more than 200 characters in length.");

            //Territory Boundaries
            RuleFor(c => c.TerritoryBoundariesText)
                .NotEmpty().WithMessage("Please draw a valid territory boundary.")
                .Must(BeValidShape).WithMessage("Please draw a valid territory boundary.");

            //Territory FK CongregationID
            RuleFor(c => c.FKtblCongregationId)
                .NotNull().WithMessage("Error:  Please logout and log back in.  If error persists, please see administrator");

            //Territory Notes
            RuleFor(c => c.LastUpdated)
                .NotNull().WithMessage("The record is not updated with the proper date.  Please call the administrator.");

            //Territory
            RuleFor(c => c.TerritorySpecialNotes)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(200).WithMessage("Special Notes Field with {TotalLength} characters cannot be more than 200 characters in length.");
            
        }

        protected bool BeUniqueTerritoryNumber(TerritoryViewModel territoryEntry, string newValue)
        {
            var territoryRecords = _congregationTerritory;

            //What makes a territory unique
            string uniqueTerritoryRecord = territoryEntry.TerritoryNumber +
                territoryEntry.FKtblCongregationId;

            //Cross
            bool isUniqueEntry = true;

            foreach(CongregationTerritories territory in territoryRecords)
            {
                string crossCheckUniqueTerritoryRec = territory.TerritoryNumber +
                    territory.FKtblCongregationId;

                if(uniqueTerritoryRecord==crossCheckUniqueTerritoryRec)
                {
                    isUniqueEntry = false;
                }
            }

            return isUniqueEntry;
        }

        protected bool BeValidShape(TerritoryViewModel territoryEntry, string newValue)
        {
            var shapeString = territoryEntry.TerritoryBoundariesText;
            SqlGeography shape = SqlGeography.STPolyFromText(new SqlChars(shapeString), 4326);
            //Test to see if shape is valid
            var isValidShape = shape.STIsValid();
            return (bool)isValidShape;
        }
    }
}
