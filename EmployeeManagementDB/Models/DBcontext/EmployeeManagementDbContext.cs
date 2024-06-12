using System;
using System.Collections.Generic;
using EmployeeManagementDB.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementDB.Models.DBcontext;

public partial class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext()
    {
    }

    public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<AccessLog> AccessLogs { get; set; }

    public virtual DbSet<AccessRestriction> AccessRestrictions { get; set; }

    public virtual DbSet<AccessRight> AccessRights { get; set; }

    public virtual DbSet<ContactInformation> ContactInformations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    public virtual DbSet<Ipaddress> Ipaddresses { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccessRight> RoleAccessRights { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HGKHAJI;Initial Catalog=EmployeeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<AccessLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AccessLo__5E5499A897B5DE82");

            entity.ToTable("AccessLog");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("LogID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ActionPerformed)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.AccessLogs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__AccessLog__Accou__534D60F1");
        });

        modelBuilder.Entity<AccessRestriction>(entity =>
        {
            entity.HasKey(e => e.RestrictionId).HasName("PK__AccessRe__529D869AC89DE05E");

            entity.Property(e => e.RestrictionId)
                .ValueGeneratedNever()
                .HasColumnName("RestrictionID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.AccessRestrictions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__AccessRes__Emplo__398D8EEE");
        });

        modelBuilder.Entity<AccessRight>(entity =>
        {
            entity.HasKey(e => e.AccessRightId).HasName("PK__AccessRi__4E70726E5FE8C90C");

            entity.Property(e => e.AccessRightId)
                .ValueGeneratedNever()
                .HasColumnName("AccessRightID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__ContactI__5C6625BB56B22D6A");

            entity.ToTable("ContactInformation");

            entity.Property(e => e.ContactId)
                .ValueGeneratedNever()
                .HasColumnName("ContactID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ContactIn__Emplo__5BE2A6F2");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDE6DFFC96");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            entity.HasOne(d => d.Organization).WithMany(p => p.Departments)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Departmen__Organ__4E88ABD4");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF16ED8F400");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.ContactInformation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Identifier)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.EmployeeRoleId).HasName("PK__Employee__346186861AD96A91");

            entity.Property(e => e.EmployeeRoleId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeRoleID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeR__Emplo__3E52440B");

            entity.HasOne(d => d.Role).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__EmployeeR__RoleI__3F466844");
        });

        modelBuilder.Entity<EmploymentHistory>(entity =>
        {
            entity.HasKey(e => e.EmploymentId).HasName("PK__Employme__FDC872D6FCB20B26");

            entity.ToTable("EmploymentHistory");

            entity.Property(e => e.EmploymentId)
                .ValueGeneratedNever()
                .HasColumnName("EmploymentID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Supervisor)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employmen__Depar__619B8048");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Employmen__Emplo__5EBF139D");

            entity.HasOne(d => d.Organization).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Employmen__Organ__60A75C0F");

            entity.HasOne(d => d.Position).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Employmen__Posit__5FB337D6");
        });

        modelBuilder.Entity<Ipaddress>(entity =>
        {
            entity.HasKey(e => e.IpaddressId).HasName("PK__IPAddres__74CD262C93CAD409");

            entity.ToTable("IPAddresses");

            entity.Property(e => e.IpaddressId)
                .ValueGeneratedNever()
                .HasColumnName("IPAddressID");
            entity.Property(e => e.AccessRestrictionId).HasColumnName("AccessRestrictionID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AccessRestriction).WithMany(p => p.Ipaddresses)
                .HasForeignKey(d => d.AccessRestrictionId)
                .HasConstraintName("FK__IPAddress__Acces__59063A47");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B728C661016");

            entity.Property(e => e.OrganizationId)
                .ValueGeneratedNever()
                .HasColumnName("OrganizationID");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("INN");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ogrn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("OGRN");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Organizat__Paren__4BAC3F29");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A595C19A01A");

            entity.Property(e => e.PositionId)
                .ValueGeneratedNever()
                .HasColumnName("PositionID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A90E1EB69");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleAccessRight>(entity =>
        {
            entity.HasKey(e => e.RoleAccessRightId).HasName("PK__RoleAcce__FC9142C9A9C0AB4D");

            entity.Property(e => e.RoleAccessRightId)
                .ValueGeneratedNever()
                .HasColumnName("RoleAccessRightID");
            entity.Property(e => e.AccessRightId).HasColumnName("AccessRightID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.AccessRight).WithMany(p => p.RoleAccessRights)
                .HasForeignKey(d => d.AccessRightId)
                .HasConstraintName("FK__RoleAcces__Acces__44FF419A");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccessRights)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__RoleAcces__RoleI__440B1D61");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__UserAcco__349DA586F15AE6ED");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__UserAccou__Emplo__47DBAE45");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserAccou__RoleI__48CFD27E");
        });

        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__WorkSche__9C8A5B694B16277A");

            entity.Property(e => e.ScheduleId)
                .ValueGeneratedNever()
                .HasColumnName("ScheduleID");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkSchedules)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__WorkSched__Emplo__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
