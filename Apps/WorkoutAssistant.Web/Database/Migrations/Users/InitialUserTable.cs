﻿using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

[Migration(version: 20230706100911)]
public class InitialUserTable : UserTableMigration
{
    public override void Up()
    {
        Create
            .Table(tableName: TableName)
            .WithColumn(name: "Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: UsernameColumnName).AsString().NotNullable()
            .WithColumn(name: "Password").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table(tableName: TableName);
    }
}