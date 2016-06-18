using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleOfDetails.Domain.Models
{
    public class CrashType
    {
        public int CrashTypeId { get; set; }

        public string CrashTypeName { get; set; }

        public int RepairCost { get; set; }

        public ICollection<Task> Tasks { get; set; } 
    }
}
