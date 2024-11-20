using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ClientCompany = table.Column<string>(type: "TEXT", nullable: false),
                    ContractorCompany = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectManagerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => new { x.ProjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, "Loyal46@hotmail.com", "Brenna", "Reichert", "Erica" },
                    { 2, "Aniya5@hotmail.com", "Archibald", "Champlin", "Trystan" },
                    { 3, "Reggie_Hickle@hotmail.com", "Antonetta", "Walsh", "Iliana" },
                    { 4, "Tressie.Marks54@hotmail.com", "Carmela", "Douglas", "Lura" },
                    { 5, "Clark41@gmail.com", "Montana", "Brakus", "Esta" },
                    { 6, "Mona82@yahoo.com", "Alexandro", "Weimann", "Christine" },
                    { 7, "Kaley_Will17@yahoo.com", "Carter", "Carter", "Sid" },
                    { 8, "Vaughn52@gmail.com", "Kendra", "Kirlin", "Dulce" },
                    { 9, "Yasmine_Witting20@gmail.com", "Thaddeus", "Bogisich", "Concepcion" },
                    { 10, "Beulah24@gmail.com", "Jacey", "Langosh", "Catharine" },
                    { 11, "Edwardo_Watsica@yahoo.com", "Cayla", "Witting", "Serena" },
                    { 12, "Tre76@gmail.com", "Claudia", "Leannon", "Anabelle" },
                    { 13, "Fleta.Berge@hotmail.com", "Nyasia", "Rodriguez", "Delores" },
                    { 14, "Nicolas_Jacobs51@yahoo.com", "Eleazar", "Pouros", "Ottilie" },
                    { 15, "Merl_Sporer23@gmail.com", "Reanna", "Kuhic", "Abigale" },
                    { 16, "Damien.Predovic28@yahoo.com", "Geovanny", "Jenkins", "Jaqueline" },
                    { 17, "Keenan.West@gmail.com", "Bill", "Pacocha", "Kelton" },
                    { 18, "Rashawn88@gmail.com", "Hortense", "Lang", "Garnett" },
                    { 19, "Richard46@hotmail.com", "Fiona", "Bergstrom", "Gay" },
                    { 20, "Abbie.Kling64@gmail.com", "Keira", "Frami", "Jedediah" },
                    { 21, "Blanca28@yahoo.com", "Jammie", "Swift", "Matteo" },
                    { 22, "Sean_Hilll@gmail.com", "Asia", "Pfannerstill", "Eleanora" },
                    { 23, "Dandre_Abbott@yahoo.com", "Erik", "Hoeger", "Ebony" },
                    { 24, "Toby_Hahn14@hotmail.com", "Marielle", "Von", "Kenton" },
                    { 25, "Rosina28@hotmail.com", "Marielle", "Brekke", "Andrew" },
                    { 26, "Wilford67@gmail.com", "Winfield", "Fisher", "Kaelyn" },
                    { 27, "Peter_Lesch15@gmail.com", "Ronaldo", "Hoppe", "Jeffery" },
                    { 28, "Leta35@hotmail.com", "Phoebe", "Homenick", "Lewis" },
                    { 29, "Charlie_Kovacek33@hotmail.com", "Meghan", "Mann", "Rafael" },
                    { 30, "Andres29@yahoo.com", "Luther", "Powlowski", "Curt" },
                    { 31, "Coy_Fay72@yahoo.com", "Elody", "Tillman", "Melissa" },
                    { 32, "Adonis_Kuhlman@gmail.com", "Phoebe", "Robel", "Freida" },
                    { 33, "Daisha52@yahoo.com", "Friedrich", "Quitzon", "Connie" },
                    { 34, "Allen52@hotmail.com", "Shyanne", "Rutherford", "Joana" },
                    { 35, "Mallory_McDermott30@hotmail.com", "Samantha", "Champlin", "Aglae" },
                    { 36, "Annabel37@hotmail.com", "Lourdes", "Gusikowski", "Litzy" },
                    { 37, "Matteo_Feil37@hotmail.com", "Leopoldo", "Lang", "Dangelo" },
                    { 38, "Joany93@gmail.com", "Wanda", "Kirlin", "Rocio" },
                    { 39, "Reinhold57@hotmail.com", "Erin", "Lueilwitz", "Hilma" },
                    { 40, "Kaitlin_Kuphal8@gmail.com", "Junius", "Collins", "Heaven" },
                    { 41, "Edmond.Feeney28@hotmail.com", "Moses", "Ritchie", "Ally" },
                    { 42, "Yadira26@gmail.com", "Vinnie", "Bruen", "Ernesto" },
                    { 43, "Colleen_Tremblay41@hotmail.com", "Gregory", "Considine", "John" },
                    { 44, "Myrtle90@hotmail.com", "Abe", "Wisozk", "Jany" },
                    { 45, "Alda_Runolfsson@hotmail.com", "Annalise", "Reichel", "Juliet" },
                    { 46, "Sylvan_Collins8@hotmail.com", "Xavier", "Ankunding", "Doyle" },
                    { 47, "Lillie.Runolfsdottir82@hotmail.com", "Ezequiel", "Rau", "Merle" },
                    { 48, "Jamie0@hotmail.com", "Keira", "Satterfield", "Bertha" },
                    { 49, "Turner.Renner51@hotmail.com", "Brianne", "Baumbach", "Carole" },
                    { 50, "Mustafa_Waelchi@gmail.com", "Alverta", "Goyette", "Nia" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ClientCompany", "ContractorCompany", "EndDate", "Name", "Priority", "ProjectManagerId", "StartDate" },
                values: new object[,]
                {
                    { 1, "Wilderman - Okuneva", "Denesik - Padberg", new DateTime(2025, 10, 24, 11, 52, 13, 779, DateTimeKind.Local).AddTicks(6095), "Project 1", 2, 17, new DateTime(2023, 12, 13, 15, 12, 33, 103, DateTimeKind.Local).AddTicks(3083) },
                    { 2, "Tillman - Cassin", "Nicolas - Kuhic", new DateTime(2025, 7, 29, 19, 29, 59, 937, DateTimeKind.Local).AddTicks(1833), "Project 2", 5, 1, new DateTime(2023, 7, 6, 2, 5, 54, 104, DateTimeKind.Local).AddTicks(1877) },
                    { 3, "Runolfsson - O'Keefe", "McCullough, Hyatt and Moore", new DateTime(2025, 1, 9, 9, 24, 48, 692, DateTimeKind.Local).AddTicks(4451), "Project 3", 2, 43, new DateTime(2023, 1, 2, 9, 43, 26, 311, DateTimeKind.Local).AddTicks(3344) },
                    { 4, "Steuber - Lueilwitz", "Daugherty - Kreiger", new DateTime(2025, 9, 27, 20, 4, 35, 938, DateTimeKind.Local).AddTicks(7926), "Project 4", 3, 5, new DateTime(2024, 11, 17, 12, 49, 43, 792, DateTimeKind.Local).AddTicks(6545) },
                    { 5, "Rosenbaum Inc", "Lesch, Stracke and Abshire", new DateTime(2025, 10, 2, 19, 33, 16, 877, DateTimeKind.Local).AddTicks(1450), "Project 5", 1, 14, new DateTime(2023, 9, 10, 13, 42, 59, 113, DateTimeKind.Local).AddTicks(5000) },
                    { 6, "O'Conner - Oberbrunner", "West, Cruickshank and Stiedemann", new DateTime(2025, 4, 4, 7, 22, 5, 206, DateTimeKind.Local).AddTicks(1787), "Project 6", 4, 22, new DateTime(2024, 3, 14, 3, 38, 40, 388, DateTimeKind.Local).AddTicks(4762) },
                    { 7, "O'Conner - Schamberger", "Schuppe and Sons", new DateTime(2025, 3, 14, 10, 37, 45, 187, DateTimeKind.Local).AddTicks(4194), "Project 7", 2, 9, new DateTime(2023, 3, 13, 11, 15, 25, 542, DateTimeKind.Local).AddTicks(2612) },
                    { 8, "Rowe, Beier and Bayer", "Champlin LLC", new DateTime(2025, 2, 2, 12, 32, 34, 244, DateTimeKind.Local).AddTicks(4062), "Project 8", 1, 49, new DateTime(2023, 10, 1, 18, 2, 56, 867, DateTimeKind.Local).AddTicks(8833) },
                    { 9, "Nader, Marquardt and Johnson", "Crona - Ullrich", new DateTime(2025, 9, 23, 1, 29, 56, 83, DateTimeKind.Local).AddTicks(5157), "Project 9", 5, 42, new DateTime(2022, 12, 24, 4, 5, 47, 724, DateTimeKind.Local).AddTicks(3867) },
                    { 10, "Cruickshank - Hudson", "Sipes - Huel", new DateTime(2025, 8, 13, 13, 4, 45, 452, DateTimeKind.Local).AddTicks(3265), "Project 10", 3, 50, new DateTime(2024, 7, 13, 21, 48, 42, 424, DateTimeKind.Local).AddTicks(1677) },
                    { 11, "Schowalter, Borer and Kuhic", "Farrell - Lueilwitz", new DateTime(2025, 3, 6, 14, 4, 44, 706, DateTimeKind.Local).AddTicks(8880), "Project 11", 4, 14, new DateTime(2023, 11, 12, 4, 8, 44, 862, DateTimeKind.Local).AddTicks(1491) },
                    { 12, "Aufderhar Inc", "Harris and Sons", new DateTime(2025, 1, 7, 1, 52, 29, 413, DateTimeKind.Local).AddTicks(8117), "Project 12", 1, 39, new DateTime(2024, 10, 28, 11, 28, 12, 452, DateTimeKind.Local).AddTicks(1504) },
                    { 13, "Bechtelar - Jenkins", "Schmeler Group", new DateTime(2025, 6, 21, 0, 18, 42, 237, DateTimeKind.Local).AddTicks(8529), "Project 13", 4, 36, new DateTime(2023, 11, 20, 6, 18, 49, 233, DateTimeKind.Local).AddTicks(1613) },
                    { 14, "Torphy Inc", "Farrell Inc", new DateTime(2025, 2, 11, 14, 49, 55, 672, DateTimeKind.Local).AddTicks(4381), "Project 14", 5, 36, new DateTime(2023, 3, 30, 17, 21, 54, 279, DateTimeKind.Local).AddTicks(3704) },
                    { 15, "Reynolds - Kautzer", "Hills - Koch", new DateTime(2025, 1, 21, 9, 24, 52, 371, DateTimeKind.Local).AddTicks(6101), "Project 15", 2, 23, new DateTime(2024, 3, 25, 8, 55, 24, 805, DateTimeKind.Local).AddTicks(1455) },
                    { 16, "Gutmann Inc", "Schinner - Swaniawski", new DateTime(2025, 9, 29, 16, 27, 46, 318, DateTimeKind.Local).AddTicks(4020), "Project 16", 3, 4, new DateTime(2022, 12, 5, 21, 53, 10, 101, DateTimeKind.Local).AddTicks(2465) },
                    { 17, "Windler - Koss", "Flatley LLC", new DateTime(2025, 10, 26, 5, 34, 40, 466, DateTimeKind.Local).AddTicks(3497), "Project 17", 5, 46, new DateTime(2024, 8, 28, 13, 17, 5, 950, DateTimeKind.Local).AddTicks(2451) },
                    { 18, "Gerlach, Altenwerth and Kris", "Hilll Group", new DateTime(2025, 4, 30, 10, 5, 40, 795, DateTimeKind.Local).AddTicks(4386), "Project 18", 2, 5, new DateTime(2023, 1, 9, 0, 54, 13, 458, DateTimeKind.Local).AddTicks(9954) },
                    { 19, "Beahan LLC", "Lesch - Haag", new DateTime(2025, 7, 15, 6, 29, 12, 974, DateTimeKind.Local).AddTicks(2066), "Project 19", 3, 49, new DateTime(2024, 1, 19, 4, 11, 22, 624, DateTimeKind.Local).AddTicks(2081) },
                    { 20, "Wuckert - Rosenbaum", "Huel and Sons", new DateTime(2025, 8, 3, 18, 22, 24, 651, DateTimeKind.Local).AddTicks(4140), "Project 20", 1, 44, new DateTime(2023, 5, 10, 18, 29, 7, 399, DateTimeKind.Local).AddTicks(2818) }
                });

            migrationBuilder.InsertData(
                table: "ProjectEmployees",
                columns: new[] { "EmployeeId", "ProjectId" },
                values: new object[,]
                {
                    { 6, 1 },
                    { 13, 1 },
                    { 16, 1 },
                    { 46, 1 },
                    { 47, 1 },
                    { 12, 2 },
                    { 19, 2 },
                    { 28, 2 },
                    { 35, 2 },
                    { 49, 2 },
                    { 9, 3 },
                    { 22, 3 },
                    { 23, 3 },
                    { 35, 3 },
                    { 50, 3 },
                    { 5, 4 },
                    { 11, 4 },
                    { 15, 4 },
                    { 46, 4 },
                    { 48, 4 },
                    { 5, 5 },
                    { 7, 5 },
                    { 9, 5 },
                    { 41, 5 },
                    { 45, 5 },
                    { 19, 6 },
                    { 27, 6 },
                    { 42, 6 },
                    { 46, 6 },
                    { 47, 6 },
                    { 2, 7 },
                    { 11, 7 },
                    { 26, 7 },
                    { 28, 7 },
                    { 31, 7 },
                    { 19, 8 },
                    { 22, 8 },
                    { 28, 8 },
                    { 38, 8 },
                    { 45, 8 },
                    { 3, 9 },
                    { 8, 9 },
                    { 40, 9 },
                    { 41, 9 },
                    { 47, 9 },
                    { 2, 10 },
                    { 24, 10 },
                    { 32, 10 },
                    { 34, 10 },
                    { 43, 10 },
                    { 15, 11 },
                    { 22, 11 },
                    { 33, 11 },
                    { 37, 11 },
                    { 44, 11 },
                    { 2, 12 },
                    { 9, 12 },
                    { 10, 12 },
                    { 45, 12 },
                    { 46, 12 },
                    { 5, 13 },
                    { 14, 13 },
                    { 38, 13 },
                    { 47, 13 },
                    { 50, 13 },
                    { 7, 14 },
                    { 32, 14 },
                    { 37, 14 },
                    { 42, 14 },
                    { 49, 14 },
                    { 5, 15 },
                    { 10, 15 },
                    { 28, 15 },
                    { 38, 15 },
                    { 49, 15 },
                    { 2, 16 },
                    { 3, 16 },
                    { 20, 16 },
                    { 34, 16 },
                    { 44, 16 },
                    { 6, 17 },
                    { 19, 17 },
                    { 32, 17 },
                    { 45, 17 },
                    { 47, 17 },
                    { 4, 18 },
                    { 35, 18 },
                    { 44, 18 },
                    { 46, 18 },
                    { 48, 18 },
                    { 28, 19 },
                    { 36, 19 },
                    { 37, 19 },
                    { 40, 19 },
                    { 42, 19 },
                    { 9, 20 },
                    { 18, 20 },
                    { 24, 20 },
                    { 30, 20 },
                    { 45, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectEmployees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
