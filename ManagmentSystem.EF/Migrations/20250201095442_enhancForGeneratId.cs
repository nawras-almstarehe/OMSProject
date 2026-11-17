using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagmentSystem.EF.Migrations
{
    public partial class enhancForGeneratId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccessListPrivileges",
                keyColumn: "Id",
                keyValue: "28d4676e-0d61-4793-b567-3a50390fd534");

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegeId", "RoleId" },
                keyValues: new object[] { "073c30c6-30f5-4355-84b3-83ecf8f30841", "b4585b0f-939a-4879-a5cd-d768773b0266" });

            migrationBuilder.DeleteData(
                table: "UserPositionRoles",
                keyColumn: "Id",
                keyValue: "bcf4c001-c2b6-4f96-bc7a-529c15b05630");

            migrationBuilder.DeleteData(
                table: "Privileges",
                keyColumn: "Id",
                keyValue: "073c30c6-30f5-4355-84b3-83ecf8f30841");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b4585b0f-939a-4879-a5cd-d768773b0266");

            migrationBuilder.DeleteData(
                table: "UsersPositions",
                keyColumn: "Id",
                keyValue: "c6f11443-3cf6-4900-9921-8d14d6fc1745");

            migrationBuilder.DeleteData(
                table: "Positiones",
                keyColumn: "Id",
                keyValue: "448661d3-4a79-4461-a520-fdd98e8bf32c");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "9cc6946a-1adc-4f74-92af-8244216deef9");

            migrationBuilder.DeleteData(
                table: "Departmentes",
                keyColumn: "Id",
                keyValue: "f3bb378c-7c75-492c-9c8a-200ec2cec921");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "f3bb378c-7c75-492c-9c8a-200ec2cec921", "الإدارة العامة", "GD", "0001", "", "GeneralDepartment", "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "073c30c6-30f5-4355-84b3-83ecf8f30841", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "b4585b0f-939a-4879-a5cd-d768773b0266", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "9cc6946a-1adc-4f74-92af-8244216deef9", "مدير", "النظام", "None", "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAEIlxiBm1ypefLZ5yoS1axwz6vEBGTb7CizFG99mlVwIQfUOKpF4WpiWewoG9/dj70g==", "0953244518", "admin", "Employee" });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "448661d3-4a79-4461-a520-fdd98e8bf32c", "المدير العام", "f3bb378c-7c75-492c-9c8a-200ec2cec921", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "073c30c6-30f5-4355-84b3-83ecf8f30841", "b4585b0f-939a-4879-a5cd-d768773b0266", "705346b5-4503-406f-ae25-876745de69ef" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "c6f11443-3cf6-4900-9921-8d14d6fc1745", new DateTime(2025, 10, 29, 19, 45, 13, 761, DateTimeKind.Local).AddTicks(6409), 1ul, "448661d3-4a79-4461-a520-fdd98e8bf32c", new DateTime(2024, 10, 29, 19, 45, 13, 759, DateTimeKind.Local).AddTicks(4782), "HR", "9cc6946a-1adc-4f74-92af-8244216deef9", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "28d4676e-0d61-4793-b567-3a50390fd534", 1f, "c6f11443-3cf6-4900-9921-8d14d6fc1745" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "bcf4c001-c2b6-4f96-bc7a-529c15b05630", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b4585b0f-939a-4879-a5cd-d768773b0266", "c6f11443-3cf6-4900-9921-8d14d6fc1745" });
        }
    }
}
