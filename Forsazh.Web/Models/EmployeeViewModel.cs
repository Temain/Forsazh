using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models.Mapping;

namespace SaleOfDetails.Web.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class EmployeeViewModel : IHaveCustomMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Физическое лицо
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(500)]
        public string MiddleName { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string EmployeeFullName { get; set; }

        /// <summary>
        /// Дата приема на работу
        /// </summary>
        public DateTime? EmployeeDateStart { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Employee, EmployeeViewModel>("Employee")
                .ForMember(m => m.EmployeeFullName, opt => opt.MapFrom(s => s.Person.FullName))
                .ForMember(m => m.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(m => m.FirstName, opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(m => m.MiddleName, opt => opt.MapFrom(s => s.Person.MiddleName));

            configuration.CreateMap<EmployeeViewModel, Employee>("Employee")
                .ForMember(m => m.Person, opt => opt.MapFrom(s => s));

            configuration.CreateMap<EmployeeViewModel, Person>("Employee");
        }
    }

}
