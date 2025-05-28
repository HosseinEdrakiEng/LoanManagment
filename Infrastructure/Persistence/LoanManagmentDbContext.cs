using Application.Abstraction;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public partial class LoanManagmentDbContext : DbContext, ILoanManagmentDbContext
    {
        public LoanManagmentDbContext()
        {
        }

        public LoanManagmentDbContext(DbContextOptions<LoanManagmentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreditLevel> CreditLevels { get; set; }

        public virtual DbSet<CreditLimit> CreditLimits { get; set; }

        public virtual DbSet<CreditPlan> CreditPlans { get; set; }

        public virtual DbSet<CreditPlanRequest> CreditPlanRequests { get; set; }

        public virtual DbSet<CreditRate> CreditRates { get; set; }

        public virtual DbSet<NotRegisterationCreditPlanRequest> NotRegisterationCreditPlanRequests { get; set; }

        public virtual DbSet<UserLimit> UserLimits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditLevel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Level_PK");

                entity.ToTable("CreditLevel");

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<CreditLimit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CreditLimit_PK");

                entity.ToTable("CreditLimit");

                entity.HasOne(d => d.CreditLevel).WithMany(p => p.CreditLimits)
                    .HasForeignKey(d => d.CreditLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditLimit_CreditLevel_FK");

                entity.HasOne(d => d.CreditRate).WithMany(p => p.CreditLimits)
                    .HasForeignKey(d => d.CreditRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditLimit_CreditRate_FK");
            });

            modelBuilder.Entity<CreditPlan>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Plan_PK");

                entity.ToTable("CreditPlan");

                entity.Property(e => e.Title).HasMaxLength(300);
            });

            modelBuilder.Entity<CreditPlanRequest>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CreditPlanRequest_PK");

                entity.ToTable("CreditPlanRequest");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreditPlan).WithMany(p => p.CreditPlanRequests)
                    .HasForeignKey(d => d.CreditPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditPlanRequest_CreditPlan_FK");
            });

            modelBuilder.Entity<CreditRate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("CreditRate_PK");

                entity.ToTable("CreditRate");

                entity.Property(e => e.Range).HasMaxLength(200);
            });

            modelBuilder.Entity<NotRegisterationCreditPlanRequest>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("NotCompletedCreditPlanRequest_PK");

                entity.ToTable("NotRegisterationCreditPlanRequest");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.ClientId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.Level)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.NationalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLimit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("UserLimit_PK");

                entity.ToTable("UserLimit");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreditLimit).WithMany(p => p.UserLimits)
                    .HasForeignKey(d => d.CreditLimitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserLimit_CreditLimit_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
