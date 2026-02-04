using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ML.PCM.Migrations
{
    /// <inheritdoc />
    public partial class Added_Full_PCM_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    MainContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDecisionDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDecisionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDecisionDocuments_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDecisionDocuments_AppProjects_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPermits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPermits_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPermits_AppProjects_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProcurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AnnouncementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ControlPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    WinningBidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BidOpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BidWinningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WinningBidder = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProcurements_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProcurements_AppProjects_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SigningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PartyB = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsVirtual = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppContracts_AppProcurements_ProcurementId",
                        column: x => x.ProcurementId,
                        principalTable: "AppProcurements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppContracts_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppContracts_AppProjects_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DecisionDocumentProcurement",
                columns: table => new
                {
                    RelatedDecisionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedProcurementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionDocumentProcurement", x => new { x.RelatedDecisionsId, x.RelatedProcurementsId });
                    table.ForeignKey(
                        name: "FK_DecisionDocumentProcurement_AppDecisionDocuments_RelatedDecisionsId",
                        column: x => x.RelatedDecisionsId,
                        principalTable: "AppDecisionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DecisionDocumentProcurement_AppProcurements_RelatedProcurementsId",
                        column: x => x.RelatedProcurementsId,
                        principalTable: "AppProcurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppDeliverables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDeliverables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDeliverables_AppContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "AppContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDeliverables_AppContracts_ContractId1",
                        column: x => x.ContractId1,
                        principalTable: "AppContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: true),
                    PaymentNumber = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPayments_AppContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "AppContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPayments_AppContracts_ContractId1",
                        column: x => x.ContractId1,
                        principalTable: "AppContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractDecisionDocument",
                columns: table => new
                {
                    RelatedContractsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedDecisionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDecisionDocument", x => new { x.RelatedContractsId, x.RelatedDecisionsId });
                    table.ForeignKey(
                        name: "FK_ContractDecisionDocument_AppContracts_RelatedContractsId",
                        column: x => x.RelatedContractsId,
                        principalTable: "AppContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDecisionDocument_AppDecisionDocuments_RelatedDecisionsId",
                        column: x => x.RelatedDecisionsId,
                        principalTable: "AppDecisionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppContracts_ProcurementId",
                table: "AppContracts",
                column: "ProcurementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppContracts_ProjectId",
                table: "AppContracts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppContracts_ProjectId1",
                table: "AppContracts",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppDecisionDocuments_ProjectId",
                table: "AppDecisionDocuments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDecisionDocuments_ProjectId1",
                table: "AppDecisionDocuments",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppDeliverables_ContractId",
                table: "AppDeliverables",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDeliverables_ContractId1",
                table: "AppDeliverables",
                column: "ContractId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppPayments_ContractId",
                table: "AppPayments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPayments_ContractId1",
                table: "AppPayments",
                column: "ContractId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppPermits_ProjectId",
                table: "AppPermits",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPermits_ProjectId1",
                table: "AppPermits",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppProcurements_ProjectId",
                table: "AppProcurements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProcurements_ProjectId1",
                table: "AppProcurements",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjects_ProjectCode",
                table: "AppProjects",
                column: "ProjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDecisionDocument_RelatedDecisionsId",
                table: "ContractDecisionDocument",
                column: "RelatedDecisionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionDocumentProcurement_RelatedProcurementsId",
                table: "DecisionDocumentProcurement",
                column: "RelatedProcurementsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDeliverables");

            migrationBuilder.DropTable(
                name: "AppPayments");

            migrationBuilder.DropTable(
                name: "AppPermits");

            migrationBuilder.DropTable(
                name: "ContractDecisionDocument");

            migrationBuilder.DropTable(
                name: "DecisionDocumentProcurement");

            migrationBuilder.DropTable(
                name: "AppContracts");

            migrationBuilder.DropTable(
                name: "AppDecisionDocuments");

            migrationBuilder.DropTable(
                name: "AppProcurements");

            migrationBuilder.DropTable(
                name: "AppProjects");
        }
    }
}
