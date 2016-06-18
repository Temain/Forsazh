using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models.Mapping;

namespace SaleOfDetails.Web.Models
{
    // Модели, возвращенные действиями MeController.
    public class MeViewModel : IHaveCustomMappings
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? Birthday { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, MeViewModel>("User")
                .ForMember(m => m.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(m => m.FirstName, opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(m => m.MiddleName, opt => opt.MapFrom(s => s.Person.MiddleName))
                .ForMember(m => m.Birthday, opt => opt.MapFrom(s => s.Person.Birthday));

            configuration.CreateMap<MeViewModel, ApplicationUser>("User")
                .ForMember(m => m.Person, opt => opt.MapFrom(s => s));

            configuration.CreateMap<MeViewModel, Person>("User");

        }
    }
}