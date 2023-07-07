using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(keyExpression: user => user.Id);

        builder.Property(propertyExpression: user => user.Username)
            .IsRequired();
        
        builder.Property(propertyExpression: user => user.Password)
            .IsRequired();

        builder.HasIndex(indexExpression: user => user.Username);
    }
}