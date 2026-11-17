using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagmentSystem.EF.Migrations
{
    public partial class enhancForGeneratId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccessListPrivileges",
                keyColumn: "Id",
                keyValue: "79cd0e60-626c-423b-88ea-d37b7016d299");

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegeId", "RoleId" },
                keyValues: new object[] { "fed35f2e-8398-42f8-b78b-b2223ccbf921", "5db0a025-dd01-44c4-ad96-925c02b31968" });

            migrationBuilder.DeleteData(
                table: "UserPositionRoles",
                keyColumn: "Id",
                keyValue: "b1c1af20-a95f-4ce6-b79d-67fe411c4e16");

            migrationBuilder.DeleteData(
                table: "Privileges",
                keyColumn: "Id",
                keyValue: "fed35f2e-8398-42f8-b78b-b2223ccbf921");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5db0a025-dd01-44c4-ad96-925c02b31968");

            migrationBuilder.DeleteData(
                table: "UsersPositions",
                keyColumn: "Id",
                keyValue: "1173961b-074b-4cb5-b85a-c363c30ddd77");

            migrationBuilder.DeleteData(
                table: "Positiones",
                keyColumn: "Id",
                keyValue: "fff01c6e-6013-4f48-b7fb-af2e8e148ece");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "fd9da150-2850-4c2b-a93c-4b0acc1c7719");

            migrationBuilder.DeleteData(
                table: "Departmentes",
                keyColumn: "Id",
                keyValue: "ad49ccc1-36b6-4da8-ac27-4b05d28cd899");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Departmentes",
                columns: new[] { "Id", "AName", "Code", "DepCode", "DepartmentParentId", "DepartmentType", "EName", "IsActive" },
                values: new object[] { "ad49ccc1-36b6-4da8-ac27-4b05d28cd899", "الإدارة العامة", "GD", "0001", "", "GeneralDepartment", "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "fed35f2e-8398-42f8-b78b-b2223ccbf921", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "5db0a025-dd01-44c4-ad96-925c02b31968", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "fd9da150-2850-4c2b-a93c-4b0acc1c7719", "مدير", "النظام", "None", "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAEOdOur1OsgOFYvRoY5j5WcA3rzd6ZYGT9wwgx2uNhu+g0pAUR0V4KUfhGXt51P0GKg==", "0953244518", "admin", "Employee" });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "fff01c6e-6013-4f48-b7fb-af2e8e148ece", "المدير العام", "ad49ccc1-36b6-4da8-ac27-4b05d28cd899", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "fed35f2e-8398-42f8-b78b-b2223ccbf921", "5db0a025-dd01-44c4-ad96-925c02b31968", "55c29c0a-f802-4b75-928f-0a0c42adb08e" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "1173961b-074b-4cb5-b85a-c363c30ddd77", new DateTime(2026, 2, 1, 12, 54, 41, 454, DateTimeKind.Local).AddTicks(142), 1ul, "fff01c6e-6013-4f48-b7fb-af2e8e148ece", new DateTime(2025, 2, 1, 12, 54, 41, 452, DateTimeKind.Local).AddTicks(7767), "HR", "fd9da150-2850-4c2b-a93c-4b0acc1c7719", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "79cd0e60-626c-423b-88ea-d37b7016d299", 1f, "1173961b-074b-4cb5-b85a-c363c30ddd77" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "b1c1af20-a95f-4ce6-b79d-67fe411c4e16", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5db0a025-dd01-44c4-ad96-925c02b31968", "1173961b-074b-4cb5-b85a-c363c30ddd77" });
        }
    }
}
