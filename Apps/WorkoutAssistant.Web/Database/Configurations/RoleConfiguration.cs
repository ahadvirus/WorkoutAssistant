using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(keyExpression: role => role.Id);
        
        builder.Property(propertyExpression: role => role.Name)
            .IsRequired();
        
        builder.HasIndex(indexExpression: role => role.Name);
    }
}