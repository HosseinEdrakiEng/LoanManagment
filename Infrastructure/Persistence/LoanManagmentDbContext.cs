using Application.Abstraction;
using Domain;
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


        public virtual DbSet<CreditLevelModel> CreditLevels { get; set; }

        public virtual DbSet<CreditLoanModel> CreditLoans { get; set; }

        public virtual DbSet<CreditPlanModel> CreditPlans { get; set; }

        public virtual DbSet<CreditRequestModel> CreditRequests { get; set; }

        public virtual DbSet<InstallmentModel> Installments { get; set; }

        public virtual DbSet<LimitationModel> Limitations { get; set; }

        public virtual DbSet<LoanTypeModel> LoanTypes { get; set; }

        public virtual DbSet<PaymentTypeModel> PaymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditLevelModel>(entity =>
            {
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<CreditLoanModel>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreditRequest).WithMany(p => p.CreditLoans)
                    .HasForeignKey(d => d.CreditRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditLoans_CreditRequests");
            });

            modelBuilder.Entity<CreditPlanModel>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<CreditRequestModel>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreditPlan).WithMany(p => p.CreditRequests)
                    .HasForeignKey(d => d.CreditPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditRequests_CreditPlans");
            });

            modelBuilder.Entity<InstallmentModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Installment");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreditLoan).WithMany(p => p.Installments)
                    .HasForeignKey(d => d.CreditLoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Installments_CreditLoans");

                entity.HasOne(d => d.InstallmentRef).WithMany(p => p.InverseInstallmentRef)
                    .HasForeignKey(d => d.InstallmentRefId)
                    .HasConstraintName("FK_Installment_Installment");
            });

            modelBuilder.Entity<LimitationModel>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreditLevel).WithMany(p => p.Limitations)
                    .HasForeignKey(d => d.CreditLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Limitations_CreditLevels");

                entity.HasOne(d => d.CreditPlan).WithMany(p => p.Limitations)
                    .HasForeignKey(d => d.CreditPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Limitations_CreditPlans");
            });

            modelBuilder.Entity<LoanTypeModel>(entity =>
            {
                entity.ToTable("LoanType");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PaymentTypeModel>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
