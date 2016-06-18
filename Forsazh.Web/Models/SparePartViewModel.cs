using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SaleOfDetails.Domain.Models;
using SaleOfDetails.Web.Models.Mapping;

namespace SaleOfDetails.Web.Models
{
    /// <summary>
    /// Зап. часть
    /// </summary>
    public class SparePartViewModel : IHaveCustomMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int SparePartId { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string SparePartName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Количество в наличии / на складе
        /// </summary>
        public int InStock { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SparePart, SparePartViewModel>("SparePart");

            configuration.CreateMap<SparePartViewModel, SparePart>("SparePart");
        }
    }

}
