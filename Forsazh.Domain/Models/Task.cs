using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SaleOfDetails.Domain.Models
{
    /// <summary>
    /// Заявка
    /// </summary>
    [Table("Task" , Schema = "dbo")]
    public class Task
    {
        public int TaskId { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// Гос. номер
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// Поломка
        /// </summary>
        public int CrashTypeId { get; set; }
        public CrashType CrashType { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

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

        public ICollection<SparePart> SpareParts { get; set; }
    }

    public enum Status
    {
        InJob,
        Completed,
        Failure
    }
}
