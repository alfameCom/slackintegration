using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SlackIntegration.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<SlackConfiguration> SlackConfiguration { get; set; }
        public virtual DbSet<SlackSuccess> SlackSuccess { get; set; }
        public virtual DbSet<SlackSuccessReceiver> SlackSuccessReceiver { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<SlackSuccess>(entity =>
            {
                entity.Property(e => e.SuccessDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SlackSuccessReceiver>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Success)
                    .WithMany(p => p.SlackSuccessReceiver)
                    .HasForeignKey(d => d.SuccessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlackSuccessReceiver_SlackSuccess");
            });
        }
    }
}