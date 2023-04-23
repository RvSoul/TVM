using Microsoft.EntityFrameworkCore;
using TVM.Model.CM;

namespace TVM.Model
{
    public class TVMContext : DbContext
    {
        public TVMContext(DbContextOptions<TVMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<TransportationRecords> TransportationRecords { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<AbnormalRecords> AbnormalRecords { get; set; }
        public virtual DbSet<ScalageRecords> ScalageRecords { get; set; }
        public virtual DbSet<Scale> Scale { get; set; }

    }

   
}