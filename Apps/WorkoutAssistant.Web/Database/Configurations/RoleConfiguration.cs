using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Configurations;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class RoleConfiguration : EntityConfiguration<Role, Guid, RoleTable, RoleTableColumns, RoleTableIndexes, RoleTableRows>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(keyExpression: role => role.Id)
            .HasName(name: Configuration.ColumnsName.Primary);
        
        builder.Property(propertyExpression: role => role.Name)
            .HasColumnName(name: Configuration.ColumnsName.Name)
            .IsRequired();
        
        builder.HasIndex(indexExpression: role => role.Name, name: Configuration.IndexesName.NameUnique)
            .IsUnique();

        builder.ToTable(name: Configuration.TableName);
    }
}