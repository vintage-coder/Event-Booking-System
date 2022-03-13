using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EBSystem.Models.Models;

namespace EBSystem.Models.DBContexts
{
    public partial class EMSDBContext : DbContext
    {
        public EMSDBContext()
        {

        }

        public EMSDBContext(DbContextOptions<EMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventCategoryTbl> EventCategoryTbls { get; set; } = null!;
        public virtual DbSet<EventTbl> EventTbls { get; set; } = null!;
        public virtual DbSet<PaymentTypeTbl> PaymentTypeTbls { get; set; } = null!;
        public virtual DbSet<TicketCategoryTbl> TicketCategoryTbls { get; set; } = null!;
        public virtual DbSet<UserTbl> UserTbls { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=GOWTHAM-T48OBM8\\SQLEXPRESS;Initial Catalog=EMSDB;Integrated Security=True;");

        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventCategoryTbl>(entity =>
            {
                entity.HasKey(e => e.EventCategoryId);

                entity.ToTable("EventCategoryTbl", "Master");

                entity.Property(e => e.EventCategoryId).HasColumnName("Event_Category_ID");

                entity.Property(e => e.EventCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Event_Category_Name");
            });

            modelBuilder.Entity<EventTbl>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("EventTbl", "Master");

                entity.Property(e => e.EventId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Event_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.EventCategoryId).HasColumnName("Event_Category_ID");

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Event_Name");

                entity.Property(e => e.NoOfTickets).HasColumnName("No_Of_Tickets");

                entity.Property(e => e.PromoCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROMO_CODE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.TicketCategoryId).HasColumnName("Ticket_Category_ID");

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.EventTbl)
                    .HasForeignKey<EventTbl>(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventTbl_Event_Category");

                entity.HasOne(d => d.TicketCategory)
                    .WithMany(p => p.EventTbls)
                    .HasForeignKey(d => d.TicketCategoryId)
                    .HasConstraintName("FK_EventTbl_Ticket_Category");
            });

            modelBuilder.Entity<PaymentTypeTbl>(entity =>
            {
                entity.HasKey(e => e.PaymentTypeId);

                entity.ToTable("PaymentTypeTbl", "Payment");

                entity.Property(e => e.PaymentTypeId).HasColumnName("Payment_Type_ID");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Payment_Type");
            });

            modelBuilder.Entity<TicketCategoryTbl>(entity =>
            {
                entity.HasKey(e => e.TicketCategoryId);

                entity.ToTable("TicketCategoryTbl", "Master");

                entity.Property(e => e.TicketCategoryId).HasColumnName("Ticket_Category_ID");

                entity.Property(e => e.TicketCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Category_Name");

                entity.Property(e => e.TicketPrice).HasColumnName("Ticket_Price");
            });

            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserTbl", "User");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_ID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
