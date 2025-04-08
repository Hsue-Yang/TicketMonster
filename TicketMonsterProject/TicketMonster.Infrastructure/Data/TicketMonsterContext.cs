using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.Infrastructure.Data;

public partial class TicketMonsterContext : DbContext
{
    public TicketMonsterContext()
    {
    }

    public TicketMonsterContext(DbContextOptions<TicketMonsterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlockToken> BlockTokens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventPerform> EventPerforms { get; set; }

    public virtual DbSet<EventSeat> EventSeats { get; set; }

    public virtual DbSet<EventsPic> EventsPics { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Performer> Performers { get; set; }

    public virtual DbSet<PerformerPic> PerformerPics { get; set; }

    public virtual DbSet<SeatNum> SeatNums { get; set; }

    public virtual DbSet<SeatSection> SeatSections { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<TempOrder> TempOrders { get; set; }

    public virtual DbSet<TempOrderDetail> TempOrderDetails { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<VenueArea> VenueAreas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TicketMonsterConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Chinese_Taiwan_Stroke_CI_AS");

        modelBuilder.Entity<BlockToken>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BlockToken");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Sports");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Pic)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasIndex(e => e.SubCategoryId, "IX_Events_SubCategoryID");

            entity.HasIndex(e => e.VenueId, "IX_Events_VenueID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId)
                .HasComment("1")
                .HasColumnName("CategoryID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.EventDate)
                .HasComment("2023/12/07")
                .HasColumnType("datetime");
            entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("BlackPink");
            entity.Property(e => e.IsDeleted).HasComment("true");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.SubCategoryId)
                .HasComment("1")
                .HasColumnName("SubCategoryID");
            entity.Property(e => e.TotalTime)
                .HasComment("12(hours)")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VenueId)
                .HasComment("1")
                .HasColumnName("VenueID");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Events)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_SubCategory");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Venues1");
        });

        modelBuilder.Entity<EventPerform>(entity =>
        {
            entity.ToTable("EventPerform");

            entity.HasIndex(e => e.EventId, "IX_EventPerform_EventID");

            entity.HasIndex(e => e.PerfomerId, "IX_EventPerform_PerfomerID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasComment("1 同個活動多個演出者")
                .HasColumnName("EventID");
            entity.Property(e => e.IsPrimary).HasComment("判斷主客隊");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.PerfomerId)
                .HasComment("3")
                .HasColumnName("PerfomerID");

            entity.HasOne(d => d.Event).WithMany(p => p.EventPerforms)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventPerform_Events");

            entity.HasOne(d => d.Perfomer).WithMany(p => p.EventPerforms)
                .HasForeignKey(d => d.PerfomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventPerform_Performers");
        });

        modelBuilder.Entity<EventSeat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EventSeat_1");

            entity.ToTable("EventSeat");

            entity.HasIndex(e => e.EventId, "IX_EventSeat_EventID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasComment("2")
                .HasColumnName("EventID");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.SeatArea)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("區塊名稱");
            entity.Property(e => e.SeatList).IsRequired();
            entity.Property(e => e.SeatPrice)
                .HasComment("78.54")
                .HasColumnType("money");
            entity.Property(e => e.SeatRowBegin)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("001");
            entity.Property(e => e.SeatRowEnd)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("100");

            entity.HasOne(d => d.Event).WithMany(p => p.EventSeats)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventSeat_Events1");
        });

        modelBuilder.Entity<EventsPic>(entity =>
        {
            entity.ToTable("EventsPic");

            entity.HasIndex(e => e.EventId, "IX_EventsPic_EventID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasComment("1")
                .HasColumnName("EventID");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Pic)
                .IsRequired()
                .HasComment("svg || url");
            entity.Property(e => e.Sort).HasComment("照片順序");

            entity.HasOne(d => d.Event).WithMany(p => p.EventsPics)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventsPic_Events");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BillingAmount).HasColumnType("money");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).IsRequired();
            entity.Property(e => e.EventPic).IsRequired();
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.VenueLocation).IsRequired();
            entity.Property(e => e.VenueName).IsRequired();
            entity.Property(e => e.VenueSvg).IsRequired();

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EventSeat).IsRequired();
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ScannedTime)
                .HasComment("QRcode掃描時間")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");
        });

        modelBuilder.Entity<Performer>(entity =>
        {
            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.About)
                .IsRequired()
                .HasMaxLength(2000)
                .HasComment("Kpop");
            entity.Property(e => e.CategoryId)
                .HasComment("1")
                .HasColumnName("CategoryID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("BlackPink");
            entity.Property(e => e.SubCategoryId)
                .HasComment("2")
                .HasColumnName("SubCategoryID");

            entity.HasOne(d => d.Category).WithMany(p => p.Performers)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Performers_Category");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Performers)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Performers_SubCategory");
        });

        modelBuilder.Entity<PerformerPic>(entity =>
        {
            entity.ToTable("PerformerPic");

            entity.HasIndex(e => e.PerfomerId, "IX_PerformerPic_PerfomerID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.PerfomerId).HasColumnName("PerfomerID");
            entity.Property(e => e.Pic).IsRequired();

            entity.HasOne(d => d.Perfomer).WithMany(p => p.PerformerPics)
                .HasForeignKey(d => d.PerfomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerformerPic_Performers");
        });

        modelBuilder.Entity<SeatNum>(entity =>
        {
            entity.ToTable("SeatNum");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RetainTime).HasColumnType("datetime");
            entity.Property(e => e.SeatNum1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SeatNum");
            entity.Property(e => e.SeatSectionId).HasColumnName("SeatSectionID");

            entity.HasOne(d => d.SeatSection).WithMany(p => p.SeatNums)
                .HasForeignKey(d => d.SeatSectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeatNum_SeatSection");

            entity.HasOne(d => d.Venue).WithMany(p => p.SeatNums)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK_SeatNum_Venues");
        });

        modelBuilder.Entity<SeatSection>(entity =>
        {
            entity.ToTable("SeatSection");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.SectionName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.SectionPrice).HasColumnType("money");
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.Event).WithMany(p => p.SeatSections)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeatSection_Events");

            entity.HasOne(d => d.Venue).WithMany(p => p.SeatSections)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeatSection_Venues");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.HasIndex(e => e.CatagoryId, "IX_SubCategory_CatagoryID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CatagoryId)
                .HasComment("1")
                .HasColumnName("CatagoryID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Pic)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.SubCategoryName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Basketball");

            entity.HasOne(d => d.Catagory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CatagoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<TempOrder>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BillingAmount).HasColumnType("money");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).IsRequired();
            entity.Property(e => e.EventPic).IsRequired();
            entity.Property(e => e.MerchantTradeNo)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.VenueLocation).IsRequired();
            entity.Property(e => e.VenueName).IsRequired();
            entity.Property(e => e.VenueSvg).IsRequired();
        });

        modelBuilder.Entity<TempOrderDetail>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EventSeat).IsRequired();
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ScannedTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.Capacity)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("2000");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Latitude)
                .HasComment("113.2456754")
                .HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("台北市");
            entity.Property(e => e.Longitude)
                .HasComment("48.123468")
                .HasColumnType("decimal(10, 7)");
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.Pic)
                .IsRequired()
                .HasComment("Svg");
            entity.Property(e => e.VenueName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("台北小巨蛋");
        });

        modelBuilder.Entity<VenueArea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VenueSeat");

            entity.HasIndex(e => e.VenuesId, "IX_VenueAreas_VenuesID");

            entity.Property(e => e.Id)
                .HasComment("1")
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LastEditBy)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            entity.Property(e => e.SeatArea)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("A");
            entity.Property(e => e.SeatsCount)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("001");
            entity.Property(e => e.VenuesId)
                .HasComment("1")
                .HasColumnName("VenuesID");

            entity.HasOne(d => d.Venues).WithMany(p => p.VenueAreas)
                .HasForeignKey(d => d.VenuesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VenueAreas_Venues1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
