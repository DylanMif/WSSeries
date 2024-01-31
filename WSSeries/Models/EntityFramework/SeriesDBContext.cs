using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WSSeries.Models.EntityFramework
{
    public partial class SeriesDBContext : DbContext
    {
        public SeriesDBContext()
        {
        }

        public SeriesDBContext(DbContextOptions<SeriesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Serie> Series { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
