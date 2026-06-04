using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CatrazAl.Data.Models;

public partial class PrisonDbContext : DbContext
{
    public PrisonDbContext()
    {
    }

    public PrisonDbContext(DbContextOptions<PrisonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cell> Cells { get; set; }

    public virtual DbSet<Crime> Crimes { get; set; }

    public virtual DbSet<Guard> Guards { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<PrisonBlock> PrisonBlocks { get; set; }

    public virtual DbSet<Prisoner> Prisoners { get; set; }

    public virtual DbSet<Punishment> Punishments { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-7G89GA4\\SQLEXPRESS;Database=prison_db;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cell>(entity =>
        {
            entity.HasKey(e => e.CellId).HasName("PK__cells__6C2811E7E1A1800F");

            entity.ToTable("cells");

            entity.Property(e => e.CellId).HasColumnName("cell_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Kind)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kind");
            entity.Property(e => e.PrisonBlockId).HasColumnName("prison_block_id");

            entity.HasOne(d => d.PrisonBlock).WithMany(p => p.Cells)
                .HasForeignKey(d => d.PrisonBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cells__prison_bl__398D8EEE");
        });

        modelBuilder.Entity<Crime>(entity =>
        {
            entity.HasKey(e => e.CrimeId).HasName("PK__crimes__C10AEBBD1114FFEF");

            entity.ToTable("crimes");

            entity.Property(e => e.CrimeId).HasColumnName("crime_id");
            entity.Property(e => e.Crime1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("crime");
        });

        modelBuilder.Entity<Guard>(entity =>
        {
            entity.HasKey(e => e.GuardId).HasName("PK__guards__96F42A74F5889F94");

            entity.ToTable("guards");

            entity.Property(e => e.GuardId).HasColumnName("guard_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.GuardRank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("guard_rank");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");

            entity.HasOne(d => d.Shift).WithMany(p => p.Guards)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__guards__shift_id__47DBAE45");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__medical___BFCFB4DD8FC48AA8");

            entity.ToTable("medical_records");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctor_first_name");
            entity.Property(e => e.DoctorLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctor_last_name");
            entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");
            entity.Property(e => e.RecordDate)
                .HasColumnType("datetime")
                .HasColumnName("record_date");
            entity.Property(e => e.Treatment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("treatment");
            entity.Property(e => e.TreatmentDays).HasColumnName("treatment_days");

            entity.HasOne(d => d.Prisoner).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PrisonerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__medical_r__priso__4D94879B");
        });

        modelBuilder.Entity<PrisonBlock>(entity =>
        {
            entity.HasKey(e => e.PrisonBlockId).HasName("PK__prison_b__1590500BC9FC5531");

            entity.ToTable("prison_blocks");

            entity.Property(e => e.PrisonBlockId).HasColumnName("prison_block_id");
            entity.Property(e => e.PrisonBlock1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("prison_block");
        });

        modelBuilder.Entity<Prisoner>(entity =>
        {
            entity.HasKey(e => e.PrisonerId).HasName("PK__prisoner__38310BC4B49FD028");

            entity.ToTable("prisoners");

            entity.HasIndex(e => e.Egn, "UQ__prisoner__C1902746E10CC2F0").IsUnique();

            entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");
            entity.Property(e => e.CellId).HasColumnName("cell_id");
            entity.Property(e => e.CrimeId).HasColumnName("crime_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EGN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PrisonBlockId).HasColumnName("prison_block_id");
            entity.Property(e => e.Released).HasColumnName("released");
            entity.Property(e => e.SentenceEnd).HasColumnName("sentence_end");
            entity.Property(e => e.SentenceMonths).HasColumnName("sentence_months");
            entity.Property(e => e.SentenceStart).HasColumnName("sentence_start");

            entity.HasOne(d => d.Cell).WithMany(p => p.Prisoners)
                .HasForeignKey(d => d.CellId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__prisoners__cell___403A8C7D");

            entity.HasOne(d => d.Crime).WithMany(p => p.Prisoners)
                .HasForeignKey(d => d.CrimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__prisoners__crime__3F466844");

            entity.HasOne(d => d.PrisonBlock).WithMany(p => p.Prisoners)
                .HasForeignKey(d => d.PrisonBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__prisoners__priso__412EB0B6");
        });

        modelBuilder.Entity<Punishment>(entity =>
        {
            entity.HasKey(e => e.PunishmentId).HasName("PK__punishme__E62BCF7E45A1DB59");

            entity.ToTable("punishments");

            entity.Property(e => e.PunishmentId).HasColumnName("punishment_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");
            entity.Property(e => e.PunishmentDays).HasColumnName("punishment_days");
            entity.Property(e => e.PunishmentType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("punishment_type");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.Prisoner).WithMany(p => p.Punishments)
                .HasForeignKey(d => d.PrisonerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__punishmen__priso__5070F446");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__shifts__7B2672206743F36D");

            entity.ToTable("shifts");

            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.PrisonBlockId).HasColumnName("prison_block_id");
            entity.Property(e => e.ShiftName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("shift_name");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.PrisonBlock).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.PrisonBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shifts__prison_b__44FF419A");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__visits__375A75E1455A2DBE");

            entity.ToTable("visits");

            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.DurationMinuits).HasColumnName("duration_minuits");
            entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");
            entity.Property(e => e.VisitDate)
                .HasColumnType("datetime")
                .HasColumnName("visit_date");
            entity.Property(e => e.VisitorFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("visitor_first_name");
            entity.Property(e => e.VisitorLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("visitor_last_name");
            entity.Property(e => e.VisitorRelation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("visitor_relation");

            entity.HasOne(d => d.Prisoner).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PrisonerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__visits__prisoner__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
