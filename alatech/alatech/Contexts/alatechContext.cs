using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using alatech.Domains;

#nullable disable

namespace alatech.Contexts
{
    public partial class alatechContext : DbContext
    {
        public alatechContext()
        {
        }

        public alatechContext(DbContextOptions<alatechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Graphiccard> Graphiccards { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Machinehasstoragedevice> Machinehasstoragedevices { get; set; }
        public virtual DbSet<Motherboard> Motherboards { get; set; }
        public virtual DbSet<Powersupply> Powersupplies { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
        public virtual DbSet<Rammemory> Rammemories { get; set; }
        public virtual DbSet<Rammemorytype> Rammemorytypes { get; set; }
        public virtual DbSet<Sockettype> Sockettypes { get; set; }
        public virtual DbSet<Storagedevice> Storagedevices { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;password=Senai@132;database=alatechmachines", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Graphiccard>(entity =>
            {
                entity.ToTable("graphiccard");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.MemorySize).HasColumnName("memorySize");

                entity.Property(e => e.MemoryType)
                    .IsRequired()
                    .HasColumnType("enum('gddr5','gddr6')")
                    .HasColumnName("memoryType");

                entity.Property(e => e.MinimumPowerSupply).HasColumnName("minimumPowerSupply");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.SupportMultiGpu).HasColumnName("supportMultiGpu");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Graphiccards)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("graphiccard_ibfk_1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine");

                entity.HasIndex(e => e.GraphicCardId, "graphicCardId");

                entity.HasIndex(e => e.MotherboardId, "motherboardId");

                entity.HasIndex(e => e.PowerSupplyId, "powerSupplyId");

                entity.HasIndex(e => e.ProcessorId, "processorId");

                entity.HasIndex(e => e.RamMemoryId, "ramMemoryId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.GraphicCardAmount).HasColumnName("graphicCardAmount");

                entity.Property(e => e.GraphicCardId).HasColumnName("graphicCardId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.MotherboardId).HasColumnName("motherboardId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.PowerSupplyId).HasColumnName("powerSupplyId");

                entity.Property(e => e.ProcessorId).HasColumnName("processorId");

                entity.Property(e => e.RamMemoryAmount).HasColumnName("ramMemoryAmount");

                entity.Property(e => e.RamMemoryId).HasColumnName("ramMemoryId");

                entity.HasOne(d => d.GraphicCard)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.GraphicCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machine_ibfk_4");

                entity.HasOne(d => d.Motherboard)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.MotherboardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machine_ibfk_1");

                entity.HasOne(d => d.PowerSupply)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.PowerSupplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machine_ibfk_5");

                entity.HasOne(d => d.Processor)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.ProcessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machine_ibfk_2");

                entity.HasOne(d => d.RamMemory)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.RamMemoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machine_ibfk_3");
            });

            modelBuilder.Entity<Machinehasstoragedevice>(entity =>
            {
                entity.HasKey(e => new { e.MachineId, e.StorageDeviceId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("machinehasstoragedevice");

                entity.HasIndex(e => e.StorageDeviceId, "storageDeviceId");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.StorageDeviceId).HasColumnName("storageDeviceId");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Machinehasstoragedevices)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machinehasstoragedevice_ibfk_1");

                entity.HasOne(d => d.StorageDevice)
                    .WithMany(p => p.Machinehasstoragedevices)
                    .HasForeignKey(d => d.StorageDeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machinehasstoragedevice_ibfk_2");
            });

            modelBuilder.Entity<Motherboard>(entity =>
            {
                entity.ToTable("motherboard");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.HasIndex(e => e.RamMemoryTypeId, "ramMemoryTypeId");

                entity.HasIndex(e => e.SocketTypeId, "socketTypeId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.M2Slots).HasColumnName("m2Slots");

                entity.Property(e => e.MaxTdp).HasColumnName("maxTdp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.PciSlots).HasColumnName("pciSlots");

                entity.Property(e => e.RamMemorySlots).HasColumnName("ramMemorySlots");

                entity.Property(e => e.RamMemoryTypeId).HasColumnName("ramMemoryTypeId");

                entity.Property(e => e.SataSlots).HasColumnName("sataSlots");

                entity.Property(e => e.SocketTypeId).HasColumnName("socketTypeId");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("motherboard_ibfk_3");

                entity.HasOne(d => d.RamMemoryType)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.RamMemoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("motherboard_ibfk_1");

                entity.HasOne(d => d.SocketType)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.SocketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("motherboard_ibfk_2");
            });

            modelBuilder.Entity<Powersupply>(entity =>
            {
                entity.ToTable("powersupply");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Badge80Plus)
                    .IsRequired()
                    .HasColumnType("enum('none','white','bronze','silver','gold','platinum','titanium')")
                    .HasColumnName("badge80Plus");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.Potency).HasColumnName("potency");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Powersupplies)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("powersupply_ibfk_1");
            });

            modelBuilder.Entity<Processor>(entity =>
            {
                entity.ToTable("processor");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.HasIndex(e => e.SocketTypeId, "socketTypeId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseFrequency).HasColumnName("baseFrequency");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.CacheMemory).HasColumnName("cacheMemory");

                entity.Property(e => e.Cores).HasColumnName("cores");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.MaxFrequency).HasColumnName("maxFrequency");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.SocketTypeId).HasColumnName("socketTypeId");

                entity.Property(e => e.Tdp).HasColumnName("tdp");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Processors)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("processor_ibfk_2");

                entity.HasOne(d => d.SocketType)
                    .WithMany(p => p.Processors)
                    .HasForeignKey(d => d.SocketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("processor_ibfk_1");
            });

            modelBuilder.Entity<Rammemory>(entity =>
            {
                entity.ToTable("rammemory");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.HasIndex(e => e.RamMemoryTypeId, "ramMemoryTypeId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.RamMemoryTypeId).HasColumnName("ramMemoryTypeId");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Rammemories)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rammemory_ibfk_2");

                entity.HasOne(d => d.RamMemoryType)
                    .WithMany(p => p.Rammemories)
                    .HasForeignKey(d => d.RamMemoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rammemory_ibfk_1");
            });

            modelBuilder.Entity<Rammemorytype>(entity =>
            {
                entity.ToTable("rammemorytype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sockettype>(entity =>
            {
                entity.ToTable("sockettype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Storagedevice>(entity =>
            {
                entity.ToTable("storagedevice");

                entity.HasIndex(e => e.BrandId, "brandId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnName("name");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.StorageDeviceInterface)
                    .IsRequired()
                    .HasColumnType("enum('sata','m2')")
                    .HasColumnName("storageDeviceInterface");

                entity.Property(e => e.StorageDeviceType)
                    .IsRequired()
                    .HasColumnType("enum('hdd','ssd')")
                    .HasColumnName("storageDeviceType");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Storagedevices)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("storagedevice_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("accessToken");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
