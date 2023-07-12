using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.RoleUsers;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Configurations;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class RoleUserConfiguration : EntityConfiguration<RoleUser, Guid, RoleUserTable, RoleUserTableColumns, RoleUserTableIndex, RoleUserTableRows>
{
    public override void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.HasKey(keyExpression: roleUser => roleUser.Id)
            .HasName(name: Configuration.ColumnsName.Primary);

        builder.Property(propertyExpression: roleUser => roleUser.RoleId)
            .HasColumnName(name: Configuration.ColumnsName.RoleId)
            .IsRequired();
        
        builder.Property(propertyExpression: roleUser => roleUser.UserId)
            .HasColumnName(name: Configuration.ColumnsName.UserId)
            .IsRequired();
        
        builder.HasIndex(indexExpression: roleUser => new { roleUser.RoleId, roleUser.UserId }, name: Configuration.IndexesName.RoleIdUserIdUnique)
            .IsUnique();

        builder.ToTable(name: Configuration.TableName);
    }
}