using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Configurations;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class UserConfiguration : EntityConfiguration<User, Guid, UserTable, UserTableColumns, UserTableIndexes, UserTableRows>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(keyExpression: user => user.Id)
            .HasName(name: Configuration.ColumnsName.Primary);

        builder.Property(propertyExpression: user => user.Username)
            .HasColumnName(name: Configuration.ColumnsName.Username)
            .IsRequired();
        
        builder.Property(propertyExpression: user => user.Password)
            .HasColumnName(name: Configuration.ColumnsName.Password)
            .IsRequired();

        builder.HasIndex(indexExpression: user => user.Username, name: Configuration.IndexesName.UsernameUnique)
            .IsUnique();

        builder.ToTable(name: Configuration.TableName);
    }
}