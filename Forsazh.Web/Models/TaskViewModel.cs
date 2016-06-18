using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models.Mapping;

namespace SaleOfDetails.Web.Models
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class TaskViewModel : IHaveCustomMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// ФИО клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Поломка, вид работ
        /// </summary>
        public int CrashTypeId { get; set; }

        public IEnumerable<CrashTypeViewModel> CrashTypes{ get; set; }

        /// <summary>
        /// Зап. части
        /// </summary>
        public IEnumerable<SparePartViewModel> SpareParts { get; set; } 

        /// <summary>
        /// Общая стоимость 
        /// </summary>
        // public decimal TotalCost { get; set; }

        public decimal TotalCostView { get; set; }

        /// <summary>
        /// Сотрудник / продавец
        /// </summary>
        public int EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        /// <summary>
        /// Дата заявки
        /// </summary>
        public DateTime? TaskDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Task, TaskViewModel>("Task")
                .ForMember(m => m.SpareParts, opt => opt.MapFrom(s => s.SpareParts))
                .ForMember(m => m.EmployeeFullName, opt => opt.MapFrom(s => s.Employee.Person.FullName))
                .ForMember(m => m.TotalCostView, opt => opt.MapFrom(s => s.CrashType.RepairCost + s.SpareParts.Sum(x => x.Cost)));
        }
    }

}
