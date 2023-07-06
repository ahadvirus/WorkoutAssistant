namespace WorkoutAssistant.Web.Infrastructures.Contracts;

public interface IEntity<TPrimary> where TPrimary : struct
{
    TPrimary Id { get; set; }
}