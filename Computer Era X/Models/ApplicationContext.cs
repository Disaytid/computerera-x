﻿using System.Data.Entity;
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
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<GameValue> Values { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<House> Houses { get; set; }
    }
}
