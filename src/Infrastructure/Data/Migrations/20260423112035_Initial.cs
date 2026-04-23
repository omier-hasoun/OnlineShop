using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "UserClaimSequence");

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Key", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    SkuName = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    LogoUrl = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorys_Categorys_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    LogoUrl = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PaymentProviderName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ExternalTransactionId = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    SenderId = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SenderType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ReceiverId = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ReceiverType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(1000)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginProviders",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    ProviderKey = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginProviders", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "UsersPaymentMethodsLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProviderBrandName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    ProviderCustomerId = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IsBlacklisted = table.Column<bool>(type: "bit", nullable: false),
                    Details = table.Column<string>(type: "NVARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPaymentMethodsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(25)", maxLength: 25, nullable: false),
                    CountryCode = table.Column<string>(type: "NVARCHAR(2)", fixedLength: true, maxLength: 2, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: false),
                    AddressLine1 = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    AddressLine2 = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    Longitude = table.Column<decimal>(type: "DECIMAL(9,6)", precision: 18, scale: 4, nullable: true),
                    Latitude = table.Column<decimal>(type: "DECIMAL(9,6)", precision: 18, scale: 4, nullable: true),
                    Notes = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    StateProvince = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    HouseNo = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalItemsPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ShippingFees = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    AverageRating = table.Column<double>(type: "FLOAT", nullable: false),
                    MaxQuantityPerCustomer = table.Column<short>(type: "SMALLINT", nullable: false),
                    DefaultOriginalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DefaultDiscountPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DefaultDiscountPercentage = table.Column<byte>(type: "TINYINT", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    IsDangerousGood = table.Column<bool>(type: "bit", nullable: false),
                    IsBiological = table.Column<bool>(type: "bit", nullable: false),
                    IsSerialized = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attributes = table.Column<string>(type: "NVARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("CK_Product_AverageRating", "[AverageRating] between 0 and 5");
                    table.CheckConstraint("CK_Product_MaxQuantityPerCustomer", "[MaxQuantityPerCustomer]  between 1 and 1000");
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Customers_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkedLoginProviderLoginProvider = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    LinkedLoginProviderProviderKey = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "CHAR(36)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "CHAR(36)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserLoginProviders_LinkedLoginProviderLoginProvider_LinkedLoginProviderProviderKey",
                        columns: x => new { x.LinkedLoginProviderLoginProvider, x.LinkedLoginProviderProviderKey },
                        principalTable: "UserLoginProviders",
                        principalColumns: new[] { "LoginProvider", "ProviderKey" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    CountryCode = table.Column<string>(type: "CHAR(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    UserPaymentMethodLogId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceFileName = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPayments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPayments_Transactions_Id",
                        column: x => x.Id,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderPayments_UsersPaymentMethodsLogs_UserPaymentMethodLogId",
                        column: x => x.UserPaymentMethodLogId,
                        principalTable: "UsersPaymentMethodsLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    EstimatedDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressFrom = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    AddressTo = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    CarrierName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    TrackingNumber = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<byte>(type: "TINYINT", nullable: false),
                    Comment = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.CheckConstraint("CK_ProductReview_Rating", "Rating between 1 and 5");
                    table.ForeignKey(
                        name: "FK_ProductReviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DiscountPercentage = table.Column<byte>(type: "TINYINT", nullable: false),
                    Sku = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayBaseProductImages = table.Column<bool>(type: "bit", nullable: false),
                    Specifications = table.Column<string>(type: "NVARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.CheckConstraint("CK_ProductVariant_DiscountPercentage", "[DiscountPercentage] between 0 and 80");
                    table.CheckConstraint("CK_ProductVariant_DiscountPrice", "[DiscountPrice] between 2 and 499500");
                    table.CheckConstraint("CK_ProductVariant_OriginalPrice", "[OriginalPrice] between 2 and 500000");
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "varchar(32)", nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(32)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "char(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UserClaimSequence]"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "NVARCHAR(64)", nullable: false),
                    ClaimValue = table.Column<string>(type: "NVARCHAR(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "VARCHAR(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductVariantId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.CheckConstraint("CK_CartItems_Quantity", "[Quantity] between 1 and 1000");
                    table.ForeignKey(
                        name: "FK_CartItems_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductVariantId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    SerialNumbers = table.Column<string>(type: "NVARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductVariantId = table.Column<long>(type: "bigint", nullable: true),
                    SortOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(40)", nullable: false),
                    FileSize = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsStock",
                columns: table => new
                {
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ProductVariantId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsStock", x => new { x.ProductVariantId, x.WarehouseId })
                        .Annotation("SqlServer:Clustered", true);
                    table.CheckConstraint("CK_ProductsStock_ReservedQuantity", "[ReservedQuantity] <= [Quantity]");
                    table.ForeignKey(
                        name: "FK_ProductsStock_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsStock_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnItemRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ReasonType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CustomerMessage = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShippingFees = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ReturnedQuantity = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnItemRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequests_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnItemRequestAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnItemRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(40)", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnItemRequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequestAttachments_ReturnItemRequests_ReturnItemRequestId",
                        column: x => x.ReturnItemRequestId,
                        principalTable: "ReturnItemRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnItemRequestsReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DecisionType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    DecisionReason = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    AdditionalFees = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnItemRequestsReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequestsReviews_Customers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequestsReviews_Customers_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequestsReviews_ReturnItemRequests_Id",
                        column: x => x.Id,
                        principalTable: "ReturnItemRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                unique: true,
                filter: "[IsDefault] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomerId",
                table: "CartItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantId_CustomerId",
                table: "CartItems",
                columns: new[] { "ProductVariantId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_ParentCategoryId",
                table: "Categorys",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductVariantId",
                table: "OrderItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_OrderId",
                table: "OrderPayments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_UserPaymentMethodLogId",
                table: "OrderPayments",
                column: "UserPaymentMethodLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductVariantId",
                table: "ProductImages",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_CustomerId",
                table: "ProductReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LastModifiedBy",
                table: "Products",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsStock_WarehouseId",
                table: "ProductsStock",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_Sku",
                table: "ProductVariants",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemRequestAttachments_ReturnItemRequestId",
                table: "ReturnItemRequestAttachments",
                column: "ReturnItemRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemRequests_OrderItemId",
                table: "ReturnItemRequests",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemRequestsReviews_CreatedBy",
                table: "ReturnItemRequestsReviews",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemRequestsReviews_LastModifiedBy",
                table: "ReturnItemRequestsReviews",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AppUserId",
                table: "Roles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDisplayName",
                table: "UserLoginProviders",
                column: "ProviderDisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedEmail",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedUserName",
                table: "Users",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LinkedLoginProviderLoginProvider_LinkedLoginProviderProviderKey",
                table: "Users",
                columns: new[] { "LinkedLoginProviderLoginProvider", "LinkedLoginProviderProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_AppUserId",
                table: "UserTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_LoginProvider",
                table: "UserTokens",
                column: "LoginProvider");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_AddressId",
                table: "Warehouses",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderPayments");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductsStock");

            migrationBuilder.DropTable(
                name: "ReturnItemRequestAttachments");

            migrationBuilder.DropTable(
                name: "ReturnItemRequestsReviews");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UsersPaymentMethodsLogs");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ReturnItemRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "UserLoginProviders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropSequence(
                name: "UserClaimSequence");
        }
    }
}
