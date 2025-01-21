using Microsoft.EntityFrameworkCore;
using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.Context
{
    public class QuotationSystemContext
    {
        #region DbSets
        public DbSet<Movement> Movement { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Quote> Quote { get; set; }
        #endregion
    }
}
