using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SaleOfDetails.Domain.Models
{
    /// <summary>
    /// Зап. часть (склад)
    /// </summary>
    [Table("SparePart", Schema = "dbo")]
    public class SparePart
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int SparePartId { get; set; }

        /// <summary>
        /// Название зап. части
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

        /// <summary>
        /// Дата создания записи
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Дата удаления записи
        /// </summary>
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }


        public ICollection<Task> Tasks { get; set; } 
    }

}
