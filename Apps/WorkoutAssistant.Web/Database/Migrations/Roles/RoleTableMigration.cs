using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;
using WorkoutAssistant.Web.Infrastructures.Database.Migrations;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

public abstract class RoleTableMigration : TableMigration<RoleTable, RoleTableColumns, RoleTableIndexes, RoleTableRows>
{
}