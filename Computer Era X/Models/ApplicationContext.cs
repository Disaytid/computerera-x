using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Computer_Era_X.DataTypes.Objects;

namespace Computer_Era_X.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") {}

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public DbSet<Item> Items { get; set; }
        public DbSet<BaseCurrency> BaseCurrencies { get; set; }
    }
}
