using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.Models.MockModelGeneration
{
    public class BogusTerritory
    {
        private static List<CongregationTerritories> GetBogusData()
        {
            //Convert Enums to lists
            string[] EnumTerrTypeList =Enum.GetNames(typeof(EnumTerritoryType));


            var TerritoryFaker = new Faker<CongregationTerritories>()
           .RuleFor(o => o.CongregationTerritoriesId, f => f.IndexFaker)
           .RuleFor(o => o.TerritoryNumber, f => f.IndexFaker.ToString())
           .RuleFor(o => o.TerritorySpecialNotes, f => f.Lorem.Sentence(150))
           .RuleFor(o => o.TerritoryHiddenNotes, f => f.Lorem.Sentence(150))
           .RuleFor(o => o.EnumTerritoryType, f => f.PickRandom<EnumTerritoryType>())
           .RuleFor(o=>o.EnumRecordStatus, f=>f.PickRandom<EnumRecordStatus>())
           .RuleFor(o=>o.LastUpdated, f=>f.Date.Recent(100));


            //Put in the amount of items to generate
            var bogusTerritories = TerritoryFaker.Generate(1000);
            return bogusTerritories;
        }
       
    }
}
