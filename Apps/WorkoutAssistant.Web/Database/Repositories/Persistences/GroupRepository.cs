using System;
using WorkoutAssistant.Web.Database.Contexts;
using WorkoutAssistant.Web.Database.Repositories.Contracts;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Repositories.Persistences;

public class GroupRepository : Repository<Group, Guid>, IGroupRepository
{
    public GroupRepository(ApplicationContext context) : base(context)
    {
    }
}