using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagmentSystem.EF.Migrations
{
    public partial class makeEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_UserName",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AccessListPrivileges",
                keyColumn: "Id",
                keyValue: "cabcf242-3e4e-4ad9-a82f-1b357efffb49");

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegeId", "RoleId" },
                keyValues: new object[] { "15408bef-2786-47bd-b958-900d73492ed3", "c0de3799-b907-4951-8541-f7da578f5cd7" });

            migrationBuilder.DeleteData(
                table: "UserPositionRoles",
                keyColumn: "Id",
                keyValue: "1eedc01c-a94a-44f3-85e8-a7c732a57037");

            migrationBuilder.DeleteData(
                table: "Privileges",
                keyColumn: "Id",
                keyValue: "15408bef-2786-47bd-b958-900d73492ed3");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c0de3799-b907-4951-8541-f7da578f5cd7");

            migrationBuilder.DeleteData(
                table: "UsersPositions",
                keyColumn: "Id",
                keyValue: "b0e4c635-0c47-4f8c-b97c-99b055bd267a");

            migrationBuilder.DeleteData(
                table: "Positiones",
                keyColumn: "Id",
                keyValue: "05fb1167-86ce-4a88-91b5-0a065ae66e6a");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "c6d10ce6-94ca-40a7-a19a-38485a79c0cb");

            migrationBuilder.DeleteData(
                table: "Departmentes",
                keyColumn: "Id",
                keyValue: "b5079173-69ba-40b1-b375-9d7958bea99c");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_UserName_Email",
                table: "Users",
                columns: new[] { "Id", "UserName", "Email" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_UserName_Email",
                table: "Users");

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
                values: new object[] { "b5079173-69ba-40b1-b375-9d7958bea99c", "الإدارة العامة", "GD", "0001", "", "GeneralDepartment", "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "15408bef-2786-47bd-b958-900d73492ed3", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "c0de3799-b907-4951-8541-f7da578f5cd7", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "c6d10ce6-94ca-40a7-a19a-38485a79c0cb", "مدير", "النظام", "None", "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAENRBDSaUZGPpRa2bFRTX5gB6oif6S4N+uoUZJgkR5orpYrlyEaVriHcDzQXXLMm/Hg==", "0953244518", "admin", "Employee" });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "05fb1167-86ce-4a88-91b5-0a065ae66e6a", "المدير العام", "b5079173-69ba-40b1-b375-9d7958bea99c", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "15408bef-2786-47bd-b958-900d73492ed3", "c0de3799-b907-4951-8541-f7da578f5cd7", "4cd39865-785a-4e6e-bdf0-3c257827e6a9" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "b0e4c635-0c47-4f8c-b97c-99b055bd267a", new DateTime(2025, 10, 26, 1, 17, 10, 760, DateTimeKind.Local).AddTicks(6664), 1ul, "05fb1167-86ce-4a88-91b5-0a065ae66e6a", new DateTime(2024, 10, 26, 1, 17, 10, 760, DateTimeKind.Local).AddTicks(6219), "HR", "c6d10ce6-94ca-40a7-a19a-38485a79c0cb", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "cabcf242-3e4e-4ad9-a82f-1b357efffb49", 1f, "b0e4c635-0c47-4f8c-b97c-99b055bd267a" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "1eedc01c-a94a-44f3-85e8-a7c732a57037", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c0de3799-b907-4951-8541-f7da578f5cd7", "b0e4c635-0c47-4f8c-b97c-99b055bd267a" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_UserName",
                table: "Users",
                columns: new[] { "Id", "UserName" },
                unique: true);
        }
    }
}
