using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class DBConnectionMigrationCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Obs = table.Column<string>(type: "Nvarchar(4000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KnowLocais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowLocais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KnowProduto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Versao = table.Column<string>(type: "Nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteAnexos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    IdCliente = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Arquivo = table.Column<byte[]>(type: "varbinary(MAX)", nullable: true),
                    Tipo = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Tamanho = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteAnexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteAnexos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteConexoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    IdCliente = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    IP = table.Column<string>(type: "Nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Senha = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Obs = table.Column<string>(type: "Nvarchar(4000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteConexoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteConexoes_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    IdCliente = table.Column<int>(nullable: false),
                    Link = table.Column<string>(type: "Nvarchar(2000)", nullable: true),
                    Usuario = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Senha = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteLinks_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    Erro = table.Column<string>(type: "Nvarchar(4000)", nullable: true),
                    Descricao = table.Column<string>(type: "Nvarchar(500)", nullable: true),
                    Solucao = table.Column<string>(type: "Nvarchar(4000)", nullable: true),
                    Obs = table.Column<string>(type: "Nvarchar(4000)", nullable: true),
                    ClientesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowBase_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowBaseAnexos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    IdKnowbase = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Arquivo = table.Column<byte[]>(type: "varbinary(MAX)", nullable: true),
                    Tipo = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Nome = table.Column<string>(type: "Nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowBaseAnexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowBaseAnexos_KnowBase_IdKnowbase",
                        column: x => x.IdKnowbase,
                        principalTable: "KnowBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAnexos_IdCliente",
                table: "ClienteAnexos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteConexoes_IdCliente",
                table: "ClienteConexoes",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteLinks_IdCliente",
                table: "ClienteLinks",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_KnowBase_ClientesId",
                table: "KnowBase",
                column: "ClientesId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowBaseAnexos_IdKnowbase",
                table: "KnowBaseAnexos",
                column: "IdKnowbase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteAnexos");

            migrationBuilder.DropTable(
                name: "ClienteConexoes");

            migrationBuilder.DropTable(
                name: "ClienteLinks");

            migrationBuilder.DropTable(
                name: "KnowBaseAnexos");

            migrationBuilder.DropTable(
                name: "KnowLocais");

            migrationBuilder.DropTable(
                name: "KnowProduto");

            migrationBuilder.DropTable(
                name: "KnowBase");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
