using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models.Mapping;

namespace SaleOfDetails.Web.Models
{
    /// <summary>
    /// Поломка, вид работ
    /// </summary>
    public class CrashTypeViewModel : IHaveCustomMappings
    {
        public int CrashTypeId { get; set; }

        public string CrashTypeName { get; set; }

        public int RepairCost { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CrashType, CrashTypeViewModel>("CrashType");

            configuration.CreateMap<CrashTypeViewModel, CrashType>("CrashType");
        }
    }

}
