using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;
using WorkoutAssistant.Web.Infrastructures.Database.Migrations;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

public abstract class UserTableMigration : TableMigration<UserTable, UserTableColumns, UserTableIndexes, UserTableRows>
{
}