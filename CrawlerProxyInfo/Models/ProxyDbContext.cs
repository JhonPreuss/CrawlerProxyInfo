using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrawlerProxyInfo.Models
{
    public partial class ProxyDbContext : DbContext
    {
        public ProxyDbContext()
        {
        }

        public ProxyDbContext(DbContextOptions<ProxyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crawlerinfo> Crawlerinfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=proxy_crawler;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Crawlerinfo>(entity =>
            {
                entity.ToTable("crawlerinfo");

                entity.Property(e => e.Id).HasColumnType("int(11) unsigned");

                entity.Property(e => e.EndCrawlerPages).HasColumnType("datetime");

                entity.Property(e => e.JsonProxysInfo).HasColumnType("json");

                entity.Property(e => e.StartCrawlerPages).HasColumnType("datetime");

                entity.Property(e => e.TotalPages).HasColumnType("int(11)");

                entity.Property(e => e.TotalRowsPages).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
