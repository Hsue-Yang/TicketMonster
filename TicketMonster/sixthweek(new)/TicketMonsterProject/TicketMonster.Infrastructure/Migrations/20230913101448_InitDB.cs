using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketMonster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Sports"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "BlackPink"),
                    About = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Kpop"),
                    CategoryID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false, comment: "2"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "台北小巨蛋"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "台北市"),
                    Capacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "2000"),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false, comment: "113.2456754"),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false, comment: "48.123468"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Basketball"),
                    CatagoryID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category",
                        column: x => x.CatagoryID,
                        principalTable: "Category",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueSvg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PerformerPic",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfomerID = table.Column<int>(type: "int", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerPic", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PerformerPic_Performers",
                        column: x => x.PerfomerID,
                        principalTable: "Performers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "VenueAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenuesID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    SeatArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "A"),
                    SeatsCount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "001"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueSeat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VenueAreas_Venues1",
                        column: x => x.VenuesID,
                        principalTable: "Venues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "BlackPink"),
                    CategoryID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    VenueID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "2023/12/07"),
                    TotalTime = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "12(hours)"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "true"),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_SubCategory",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategory",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Events_Venues1",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    EventSeat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScannedTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "QRcode掃描時間"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EventPerform",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false, comment: "1 同個活動多個演出者"),
                    PerfomerID = table.Column<int>(type: "int", nullable: false, comment: "3"),
                    IsPrimary = table.Column<int>(type: "int", nullable: false, comment: "判斷主客隊"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerform", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventPerform_Events",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EventPerform_Performers",
                        column: x => x.PerfomerID,
                        principalTable: "Performers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EventSeat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false, comment: "2"),
                    SeatRowBegin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "001"),
                    SeatRowEnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "100"),
                    SeatArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "區塊名稱"),
                    SeatPrice = table.Column<decimal>(type: "money", nullable: false, comment: "78.54"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSeat_1", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventSeat_Events1",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EventsPic",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "1")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false, comment: "1"),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "svg || url"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "照片順序"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastEditBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsPic", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventsPic_Events",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "ID", "Capacity", "CreateBy", "CreateTime", "LastEditBy", "Latitude", "Location", "Longitude", "ModifyTime", "VenueName" },
                values: new object[] { 9, "2000人", null, null, null, 51m, "台北市", 12m, null, "台北小巨蛋" });

            migrationBuilder.CreateIndex(
                name: "IX_EventPerform_EventID",
                table: "EventPerform",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventPerform_PerfomerID",
                table: "EventPerform",
                column: "PerfomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SubCategoryID",
                table: "Events",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeat_EventID",
                table: "EventSeat",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventsPic_EventID",
                table: "EventsPic",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerPic_PerfomerID",
                table: "PerformerPic",
                column: "PerfomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CatagoryID",
                table: "SubCategory",
                column: "CatagoryID");

            migrationBuilder.CreateIndex(
                name: "IX_VenueAreas_VenuesID",
                table: "VenueAreas",
                column: "VenuesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPerform");

            migrationBuilder.DropTable(
                name: "EventSeat");

            migrationBuilder.DropTable(
                name: "EventsPic");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PerformerPic");

            migrationBuilder.DropTable(
                name: "VenueAreas");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
