using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PengajuanCuti.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Divisi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JenisCuti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisCuti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Karyawan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false),
                    NIK = table.Column<string>(nullable: false),
                    Jenis_Kelamin = table.Column<string>(nullable: false),
                    Tanggal_Lahir = table.Column<DateTime>(nullable: false),
                    Alamat = table.Column<string>(nullable: true),
                    Nomor_Telp = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Divisi_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karyawan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karyawan_Divisi_Divisi_Id",
                        column: x => x.Divisi_Id,
                        principalTable: "Divisi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Karyawan_Id = table.Column<int>(nullable: false),
                    JenisCuti_Id = table.Column<int>(nullable: false),
                    TanggalMulai = table.Column<DateTime>(nullable: false),
                    TanggalAkhir = table.Column<DateTime>(nullable: false),
                    Keterangan = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuti_JenisCuti_JenisCuti_Id",
                        column: x => x.JenisCuti_Id,
                        principalTable: "JenisCuti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuti_Karyawan_Karyawan_Id",
                        column: x => x.Karyawan_Id,
                        principalTable: "Karyawan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Karyawan_Id",
                        column: x => x.Id,
                        principalTable: "Karyawan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuti_JenisCuti_Id",
                table: "Cuti",
                column: "JenisCuti_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cuti_Karyawan_Id",
                table: "Cuti",
                column: "Karyawan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Karyawan_Divisi_Id",
                table: "Karyawan",
                column: "Divisi_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Role_Id",
                table: "UserRole",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_User_Id",
                table: "UserRole",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuti");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "JenisCuti");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Karyawan");

            migrationBuilder.DropTable(
                name: "Divisi");
        }
    }
}
