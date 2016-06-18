using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SaleOfDetails.Domain.Models
{
    public class CrashType
    {
        public int CrashTypeId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string CrashTypeName { get; set; }

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public int RepairCost { get; set; }

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
