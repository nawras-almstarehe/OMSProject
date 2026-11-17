using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagmentSystem.EF.Migrations
{
    public partial class editcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccessListPrivileges",
                keyColumn: "Id",
                keyValue: "dc23a697-fc4c-4998-a957-b49cec132c47");

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegeId", "RoleId" },
                keyValues: new object[] { "b3f87e86-5fc5-4322-9499-a6b9e32e86be", "b8f1bd3a-1342-405f-a273-8aea25955e41" });

            migrationBuilder.DeleteData(
                table: "UserPositionRoles",
                keyColumn: "Id",
                keyValue: "614a68c0-00f2-40e9-b750-9bf1c6f4dd00");

            migrationBuilder.DeleteData(
                table: "Privileges",
                keyColumn: "Id",
                keyValue: "b3f87e86-5fc5-4322-9499-a6b9e32e86be");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b8f1bd3a-1342-405f-a273-8aea25955e41");

            migrationBuilder.DeleteData(
                table: "UsersPositions",
                keyColumn: "Id",
                keyValue: "85b5441e-2aea-469f-9344-39b2f4a3ba33");

            migrationBuilder.DeleteData(
                table: "Positiones",
                keyColumn: "Id",
                keyValue: "5a80a13a-3a9e-4bdc-9569-b643c9e31cd5");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4dbb1c30-f4e0-492a-bde7-29aa2c85cb50");

            migrationBuilder.DeleteData(
                table: "Departmentes",
                keyColumn: "Id",
                keyValue: "18717c4e-dd50-4c8d-b381-6a1be6a085ee");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "UsersPositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "BlockedType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldDefaultValue: "Request")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Privileges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentType",
                table: "Departmentes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Departmentes",
                columns: new[] { "Id", "AName", "Code", "DepCode", "DepartmentParentId", "DepartmentType", "EName", "IsActive" },
                values: new object[] { "de3eb995-b4bd-42a5-8bab-a3923e386794", "الإدارة العامة", "GD", "0001", "", 1, "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "8d9ae229-15c5-4d44-a520-e4deddb34a19", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", 0 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "b4df9438-6a18-4d83-a331-3aac6c9b61db", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "26ef1664-6200-4061-adf0-4ba778e793d9", "مدير", "النظام", 0, "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAEB5C0f70dV/4eXQneJC5mFfueNv8dBRlLNNgxHwws4QRvB0ITIXJdvpJt+Ibcw2oig==", "0953244518", "admin", 1 });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "d2af2201-10d1-4568-9237-454ac8a5adb0", "المدير العام", "de3eb995-b4bd-42a5-8bab-a3923e386794", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "8d9ae229-15c5-4d44-a520-e4deddb34a19", "b4df9438-6a18-4d83-a331-3aac6c9b61db", "69a28de0-17b0-4bc1-ae91-aa3dfae4d09e" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "9e7e9687-8cb3-48fe-8912-40fc6ecb5b90", new DateTime(2026, 2, 15, 2, 32, 33, 468, DateTimeKind.Local).AddTicks(7046), 1ul, "d2af2201-10d1-4568-9237-454ac8a5adb0", new DateTime(2025, 2, 15, 2, 32, 33, 461, DateTimeKind.Local).AddTicks(6700), 1, "26ef1664-6200-4061-adf0-4ba778e793d9", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "3a8e9723-4070-4be5-92d1-f027d4993810", 1f, "9e7e9687-8cb3-48fe-8912-40fc6ecb5b90" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "71d6caeb-8f49-4c99-96fa-e1b6484bd00d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b4df9438-6a18-4d83-a331-3aac6c9b61db", "9e7e9687-8cb3-48fe-8912-40fc6ecb5b90" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccessListPrivileges",
                keyColumn: "Id",
                keyValue: "3a8e9723-4070-4be5-92d1-f027d4993810");

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegeId", "RoleId" },
                keyValues: new object[] { "8d9ae229-15c5-4d44-a520-e4deddb34a19", "b4df9438-6a18-4d83-a331-3aac6c9b61db" });

            migrationBuilder.DeleteData(
                table: "UserPositionRoles",
                keyColumn: "Id",
                keyValue: "71d6caeb-8f49-4c99-96fa-e1b6484bd00d");

            migrationBuilder.DeleteData(
                table: "Privileges",
                keyColumn: "Id",
                keyValue: "8d9ae229-15c5-4d44-a520-e4deddb34a19");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b4df9438-6a18-4d83-a331-3aac6c9b61db");

            migrationBuilder.DeleteData(
                table: "UsersPositions",
                keyColumn: "Id",
                keyValue: "9e7e9687-8cb3-48fe-8912-40fc6ecb5b90");

            migrationBuilder.DeleteData(
                table: "Positiones",
                keyColumn: "Id",
                keyValue: "d2af2201-10d1-4568-9237-454ac8a5adb0");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "26ef1664-6200-4061-adf0-4ba778e793d9");

            migrationBuilder.DeleteData(
                table: "Departmentes",
                keyColumn: "Id",
                keyValue: "de3eb995-b4bd-42a5-8bab-a3923e386794");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "UsersPositions",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Users",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BlockedType",
                table: "Users",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Processes",
                type: "varchar(20)",
                nullable: true,
                defaultValue: "Request",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Privileges",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentType",
                table: "Departmentes",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Departmentes",
                columns: new[] { "Id", "AName", "Code", "DepCode", "DepartmentParentId", "DepartmentType", "EName", "IsActive" },
                values: new object[] { "18717c4e-dd50-4c8d-b381-6a1be6a085ee", "الإدارة العامة", "GD", "0001", "", "GeneralDepartment", "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "b3f87e86-5fc5-4322-9499-a6b9e32e86be", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "b8f1bd3a-1342-405f-a273-8aea25955e41", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "4dbb1c30-f4e0-492a-bde7-29aa2c85cb50", "مدير", "النظام", "None", "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAELqG3iREopw9zxjFMhH+pUIDDmoAMEYInpRFRd1M4JEKKFDr9KYUBJ31I2vKKAUbNw==", "0953244518", "admin", "Employee" });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "5a80a13a-3a9e-4bdc-9569-b643c9e31cd5", "المدير العام", "18717c4e-dd50-4c8d-b381-6a1be6a085ee", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "b3f87e86-5fc5-4322-9499-a6b9e32e86be", "b8f1bd3a-1342-405f-a273-8aea25955e41", "cce15c5d-c748-468b-a0e9-24b9c9a07df0" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "85b5441e-2aea-469f-9344-39b2f4a3ba33", new DateTime(2026, 2, 1, 13, 34, 20, 46, DateTimeKind.Local).AddTicks(470), 1ul, "5a80a13a-3a9e-4bdc-9569-b643c9e31cd5", new DateTime(2025, 2, 1, 13, 34, 20, 43, DateTimeKind.Local).AddTicks(1849), "HR", "4dbb1c30-f4e0-492a-bde7-29aa2c85cb50", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "dc23a697-fc4c-4998-a957-b49cec132c47", 1f, "85b5441e-2aea-469f-9344-39b2f4a3ba33" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "614a68c0-00f2-40e9-b750-9bf1c6f4dd00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b8f1bd3a-1342-405f-a273-8aea25955e41", "85b5441e-2aea-469f-9344-39b2f4a3ba33" });
        }
    }
}
