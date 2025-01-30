using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NapplesPizzeria.Models;

public partial class NaplesPizzeriaContext : DbContext
{
    public NaplesPizzeriaContext()
    {
    }

    public NaplesPizzeriaContext(DbContextOptions<NaplesPizzeriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MtabCategory> MtabCategories { get; set; }

    public virtual DbSet<MtabOrder> MtabOrders { get; set; }

    public virtual DbSet<MtabOwner> MtabOwners { get; set; }

    public virtual DbSet<MtabProduct> MtabProducts { get; set; }

    public virtual DbSet<MtabService> MtabServices { get; set; }

    public virtual DbSet<MtabTable> MtabTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-NGS93VA0\\SQLEXPRESS;Initial Catalog=NaplesPizzeria;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MtabCategory>(entity =>
        {
            entity.HasKey(e => e.InMtCatPky).HasName("PK__MtabCate__35F58CC55181AF6A");

            entity.Property(e => e.InMtCatPky).HasColumnName("inMtCat_pky");
            entity.Property(e => e.SvMtCatName)
                .HasMaxLength(50)
                .HasColumnName("svMtCat_name");
        });

        modelBuilder.Entity<MtabOrder>(entity =>
        {
            entity.HasKey(e => e.InMtOrdPky).HasName("PK__MtabOrde__6FD3B138E7EFBD01");

            entity.Property(e => e.InMtOrdPky).HasColumnName("inMtOrd_pky");
            entity.Property(e => e.InMtOrdProductFky).HasColumnName("inMtOrd_product_fky");
            entity.Property(e => e.InMtOrdServiceFky).HasColumnName("inMtOrd_service_fky");

            entity.HasOne(d => d.InMtOrdProductFkyNavigation).WithMany(p => p.MtabOrders)
                .HasForeignKey(d => d.InMtOrdProductFky)
                .HasConstraintName("FK__MtabOrder__inMtO__6754599E");

            entity.HasOne(d => d.InMtOrdServiceFkyNavigation).WithMany(p => p.MtabOrders)
                .HasForeignKey(d => d.InMtOrdServiceFky)
                .HasConstraintName("FK__MtabOrder__inMtO__66603565");
        });

        modelBuilder.Entity<MtabOwner>(entity =>
        {
            entity.HasKey(e => e.InMtOwnPky).HasName("PK__MTabOwne__06836F17588353F1");

            entity.ToTable("MTabOwner");

            entity.Property(e => e.InMtOwnPky).HasColumnName("inMtOwn_pky");
            entity.Property(e => e.SvMtOwnPassword)
                .HasMaxLength(255)
                .HasColumnName("svMtOwn_password");
            entity.Property(e => e.SvMtOwnUsername)
                .HasMaxLength(20)
                .HasColumnName("svMtOwn_username");
        });

        modelBuilder.Entity<MtabProduct>(entity =>
        {
            entity.HasKey(e => e.InMtProPky).HasName("PK__MtabProd__EAEC6A80D3C618A1");

            entity.Property(e => e.InMtProPky).HasColumnName("inMtPro_pky");
            entity.Property(e => e.DeMtProPrice)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("deMtPro_price");
            entity.Property(e => e.InMtProCategorieFky).HasColumnName("inMtPro_categorie_fky");
            entity.Property(e => e.SvMtProDescription)
                .HasMaxLength(1000)
                .HasColumnName("svMtPro_description");
            entity.Property(e => e.SvMtProName)
                .HasMaxLength(255)
                .HasColumnName("svMtPro_name");

            entity.HasOne(d => d.InMtProCategorieFkyNavigation).WithMany(p => p.MtabProducts)
                .HasForeignKey(d => d.InMtProCategorieFky)
                .HasConstraintName("FK__MtabProdu__inMtP__4D94879B");
        });

        modelBuilder.Entity<MtabService>(entity =>
        {
            entity.HasKey(e => e.InMtSerPky).HasName("PK__MtabServ__F4C51274EB79A9D1");

            entity.Property(e => e.InMtSerPky).HasColumnName("inMtSer_pky");
            entity.Property(e => e.BoMtSerIsOpen).HasColumnName("boMtSer_isOpen");
            entity.Property(e => e.DeMtSerSpent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("deMtSer_spent");
            entity.Property(e => e.DtMtSerEntry)
                .HasColumnType("datetime")
                .HasColumnName("dtMtSer_entry");
            entity.Property(e => e.DtMtSerOut)
                .HasColumnType("datetime")
                .HasColumnName("dtMtSer_out");
            entity.Property(e => e.InMtSerTable).HasColumnName("inMtSer_table");

            entity.HasOne(d => d.InMtSerTableNavigation).WithMany(p => p.MtabServices)
                .HasForeignKey(d => d.InMtSerTable)
                .HasConstraintName("FK__MtabServi__inMtS__6383C8BA");
        });

        modelBuilder.Entity<MtabTable>(entity =>
        {
            entity.HasKey(e => e.InMtTabPky).HasName("PK__MtabTabl__0F802182AD94AA5A");

            entity.Property(e => e.InMtTabPky).HasColumnName("inMtTab_pky");
            entity.Property(e => e.InMtTabNumber).HasColumnName("inMtTab_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
