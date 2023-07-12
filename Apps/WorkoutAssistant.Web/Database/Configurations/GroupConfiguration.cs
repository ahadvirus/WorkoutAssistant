using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Groups;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Configurations;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class GroupConfiguration : EntityConfiguration<Group, Guid, GroupTable, GroupTableColumns, GroupTableIndexes, GroupTableRows>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(keyExpression: group => group.Id)
            .HasName(name: Configuration.ColumnsName.Primary);

        builder.Property(propertyExpression: group => group.Name)
            .HasColumnName(name: Configuration.ColumnsName.Name)
            .IsRequired();

        builder.HasIndex(indexExpression: group => group.Name, name: Configuration.IndexesName.NameUnique)
            .IsUnique();

        builder.ToTable(name: Configuration.TableName);
    }
}