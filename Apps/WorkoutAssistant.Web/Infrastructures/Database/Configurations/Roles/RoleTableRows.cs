using System;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;

public class RoleTableRows
{
    public Guid AdminId
    {
        get
        {
            return Guid.Parse(input: "24e72417-cfef-4cd6-9e23-cbbf608a9274");
        }
    }
    
    public Guid CoachId
    {
        get
        {
            return Guid.Parse(input: "1ef60b87-94cf-47e2-8590-5c6d63c96328");
        }
    }
    
    public Guid TrainerId
    {
        get
        {
            return Guid.Parse(input: "d03c694c-3cb9-4258-a434-5fb944e64709");
        }
    }
}