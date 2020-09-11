using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection
{
    public partial class CoreDbContext : DbContext
    {
        public string connection { set; get; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

            //var builder = new ConfigurationBuilder();
            //builder.Add("appsettings.json", options: false);

            //var configuration = builder.Build();

            //var connectionString = configuration["connectionStrings:YourDBConnectionString"];

            //connection = options.ToString();
        }

        public CoreDbContext()
        {
        }

        public virtual DbSet<Clientes> clientes { get; set; }
        public virtual DbSet<ClienteConexoes> clienteConexoes { get; set; }
        public virtual DbSet<ClienteAnexos> clienteAnexos { get; set; }
        public virtual DbSet<ClienteLinks> clienteLinks { get; set; }
        public virtual DbSet<KnowBase> knowBase { get; set; }
        public virtual DbSet<KnowBaseAnexos> knowBaseAnexo { get; set; }
        public virtual DbSet<KnowLocais> knowLocais { get; set; }
        public virtual DbSet<KnowProduto> knowProduto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>();
            modelBuilder.Entity<ClienteAnexos>();
            modelBuilder.Entity<ClienteConexoes>();
            modelBuilder.Entity<ClienteLinks>();
            modelBuilder.Entity<KnowBase>();
            modelBuilder.Entity<KnowBaseAnexos>();
            modelBuilder.Entity<KnowLocais>();
            modelBuilder.Entity<KnowProduto>();

            //modelBuilder.Entity<Clientes>
            //    ( entity => 
            //{
            //    entity.Property(e => e.nome)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.obs)
            //            .HasColumnType("NVARCHAR(4000)");
            //});

            //modelBuilder.Entity<ClienteAnexos>(entity =>
            //{
            //    entity.Property(e => e.nome)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.tipo)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.arquivo)
            //            .HasColumnType("varbinary(MAX)");

            //} ) ;

            ////.HasIndex(i => i.IdCliente)
            ////  .HasName("IX_IdCliAnexo");

            //modelBuilder.Entity<ClienteConexoes>(entity =>
            //{
            //    entity.Property(e => e.ip)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.descricao)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.obs)
            //            .HasColumnType("NVARCHAR(4000)");

            //    entity.Property(e => e.usuario)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.senha)
            //            .HasColumnType("NVARCHAR(100)");

            //});
            //            //.HasIndex(i => i.IdCliente)
            //            //.HasName("IX_IdCliCon");



            //modelBuilder.Entity<ClienteLinks>(entity =>
            //{
            //    entity.Property(e => e.link)
            //            .HasColumnType("NVARCHAR(500)");

            //    entity.Property(e => e.usuario)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.senha)
            //            .HasColumnType("NVARCHAR(100)");


            //});
            //            //.HasIndex(i => i.IdCliente)
            //            //.HasName("IX_IdCliLink");
            ////
            //modelBuilder.Entity<KnowBase>(entity =>
            //{
            //    entity.Property(e => e.erro)
            //            .HasColumnType("NVARCHAR(4000)");

            //    entity.Property(e => e.descricao)
            //            .HasColumnType("NVARCHAR(4000)");

            //    entity.Property(e => e.solucao)
            //            .HasColumnType("NVARCHAR(4000)");

            //    entity.Property(e => e.obs)
            //            .HasColumnType("NVARCHAR(4000)");

            //    entity.Property(e => e.versao)
            //            .HasColumnType("NVARCHAR(15)");

            //});
            //            //.HasIndex(i => i.IdCliente)
            //            //.HasName("IX_IdCliKnow");
            //modelBuilder.Entity<KnowLocais>(entity =>
            //{
            //    entity.Property(e => e.nome)
            //            .HasColumnType("NVARCHAR(100)");

            //});

            //modelBuilder.Entity<KnowProduto>(entity =>
            //{
            //    entity.Property(e => e.nome)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.versao)
            //            .HasColumnType("NVARCHAR(15)");

            //});

            //modelBuilder.Entity<KnowBaseAnexos>(entity =>
            //{
            //    entity.Property(e => e.nome)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.tipo)
            //            .HasColumnType("NVARCHAR(100)");

            //    entity.Property(e => e.arquivo)
            //            .HasColumnType("varbinary(MAX)");

            //});


            base.OnModelCreating(modelBuilder);
        }


    }


}
