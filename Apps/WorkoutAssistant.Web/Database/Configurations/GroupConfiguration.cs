using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(keyExpression: group => group.Id);

        builder.Property(propertyExpression: group => group.Name)
            .IsRequired();

        builder.HasIndex(indexExpression: group => group.Name);
    }
}