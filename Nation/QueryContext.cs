using System.Data.Entity;

namespace Nation
{
    public class QueryContext: DbContext
    {
        public QueryContext()
        {

        }

        public DbSet<DbSupplier> Suppliers { get; set; }
        public DbSet<DbQuotation> dbQuotations { get; set; }
    }
}
