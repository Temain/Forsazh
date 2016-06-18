using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity.EntityFramework;
using SaleOfDetails.Domain.Models;

namespace SaleOfDetails.Domain.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<CrashType> CarCrashTypes { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        public ApplicationDbContext()
            : base("ForsazhConnection", throwIfV1Schema: false)
        {
            // this.Configuration.LazyLoadingEnabled = false;      
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Отключаем каскадное удаление данных в связанных таблицах
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Запрещаем создание имен таблиц в множественном числе в т.ч. при связи многие к многим
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
