using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Abstractions;

namespace EgyptCurrencyRates.Models;

public partial class EgyptCurrencyRatesContext : DbContext
{
    public EgyptCurrencyRatesContext()
    {
    }

    public EgyptCurrencyRatesContext(DbContextOptions<EgyptCurrencyRatesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleType> ArticleTypes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ContactU> ContactUs { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<ExceptionsLog> ExceptionsLogs { get; set; }

    public virtual DbSet<GoldPrice> GoldPrices { get; set; }

    public virtual DbSet<GoldPriceLog> GoldPriceLogs { get; set; }

    public virtual DbSet<GoldUnit> GoldUnits { get; set; }

    public virtual DbSet<GoldUnitType> GoldUnitTypes { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupType> GroupTypes { get; set; }

    public virtual DbSet<GroupTypeAccount> GroupTypeAccounts { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemArea> ItemAreas { get; set; }

    public virtual DbSet<ItemKeyword> ItemKeywords { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostGroup> PostGroups { get; set; }

    public virtual DbSet<PostType> PostTypes { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<RateLog> RateLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public string Connection
    {
        get
        {
            /*
             "Data Source=38.242.135.75,1433;Initial Catalog=EgyptCurrencyRates;Persist Security Info=True;User ID=sa;Password=CP1466582*;Trust Server Certificate=True"
              Data Source=VMI1721774\SQLEXPRESS;Initial Catalog=EgyptCurrencyRates;User ID=FawzyAdmin;Password=CP1466582*;
             */

            //@"Data Source=38.242.135.75,1433;Initial Catalog=EgyptCurrencyRates;Persist Security Info=True;User ID=sa;Password=CP1466582*;";
            //@"Data Source=38.242.135.75,1433;Initial Catalog=EgyptCurrencyRates;Persist Security Info=True;User ID=sa;Password=CP1466582*;TrustServerCertificate=true;";

            //@"Server=38.242.135.75;Database=EgyptCurrencyRates;User Id=sa;Password=FawCon$39;TrustServerCertificate=True;";

            //string machineName = Environment.MachineName;

            if (Environment.MachineName.Contains("DESKTOP-MDNALHH"))
            {
                return "Server=DESKTOP-MDNALHH\\SQLEXPRESS01;Database=EgyptCurrencyRates;Trusted_Connection=True;TrustServerCertificate=true;";
            }
            else
            {
                return @"Server=38.242.135.75;Database=EgyptCurrencyRates;User Id=sa;Password=CP1466582*;TrustServerCertificate=True;";
            }

            //return @"Server=38.242.135.75;Database=EgyptCurrencyRates;User Id=sa;Password=CP1466582*;TrustServerCertificate=True;";
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.
            //optionsBuilder.
            optionsBuilder.UseSqlServer(this.Connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("Area");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CityId).HasColumnName("City_ID");
            entity.Property(e => e.Title).HasMaxLength(180);

            entity.HasOne(d => d.City).WithMany(p => p.Areas)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Area_City");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("Article");

            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.Bank).WithMany(p => p.Articles)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_Article_Bank");

            entity.HasOne(d => d.Currency).WithMany(p => p.Articles)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_Article_Currency");

            entity.HasOne(d => d.Type).WithMany(p => p.Articles)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Article_ArticleType");
        });

        modelBuilder.Entity<ArticleType>(entity =>
        {
            entity.ToTable("ArticleType");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.ToTable("Bank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Logo).HasMaxLength(180);
            entity.Property(e => e.Name).HasMaxLength(180);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<ContactU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Contact_US");

            entity.ToTable("ContactUS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(180);
            entity.Property(e => e.Message).HasMaxLength(180);
            entity.Property(e => e.Name).HasMaxLength(180);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(180);
            entity.Property(e => e.Logo).HasMaxLength(180);
            entity.Property(e => e.Name).HasMaxLength(180);
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Exception");

            entity.Property(e => e.Message).HasMaxLength(180);
            entity.Property(e => e.Source).HasMaxLength(180);
        });

        modelBuilder.Entity<ExceptionsLog>(entity =>
        {
            entity.ToTable("ExceptionsLog");

            entity.Property(e => e.Exeption).HasMaxLength(180);
            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.Url).HasMaxLength(180);
        });

        modelBuilder.Entity<GoldPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Gold_Price");

            entity.ToTable("GoldPrice");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EgyptianPound)
                .HasMaxLength(150)
                .HasColumnName("Egyptian_Pound");
            entity.Property(e => e.GoldUnit).HasColumnName("Gold_Unit");
            entity.Property(e => e.UsaDollar)
                .HasMaxLength(150)
                .HasColumnName("USA_Dollar");

            entity.HasOne(d => d.GoldUnitNavigation).WithMany(p => p.GoldPrices)
                .HasForeignKey(d => d.GoldUnit)
                .HasConstraintName("FK_Gold_Price_Gold_Unit");
        });

        modelBuilder.Entity<GoldPriceLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Gold_Price_Log");

            entity.ToTable("GoldPriceLog");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EgyptianPound)
                .HasMaxLength(150)
                .HasColumnName("Egyptian_Pound");
            entity.Property(e => e.GoldUnit).HasColumnName("Gold_Unit");
            entity.Property(e => e.UsaDollar)
                .HasMaxLength(150)
                .HasColumnName("USA_Dollar");

            entity.HasOne(d => d.GoldUnitNavigation).WithMany(p => p.GoldPriceLogs)
                .HasForeignKey(d => d.GoldUnit)
                .HasConstraintName("FK_Gold_Price_Log_Gold_Unit");
        });

        modelBuilder.Entity<GoldUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Gold_Unit");

            entity.ToTable("GoldUnit");

            entity.Property(e => e.GoldUnitType).HasColumnName("Gold_Unit_Type");
            entity.Property(e => e.TitleAr)
                .HasMaxLength(150)
                .HasColumnName("Title_Ar");
            entity.Property(e => e.TitleEn)
                .HasMaxLength(150)
                .HasColumnName("Title_En");

            entity.HasOne(d => d.GoldUnitTypeNavigation).WithMany(p => p.GoldUnits)
                .HasForeignKey(d => d.GoldUnitType)
                .HasConstraintName("FK_Gold_Unit_Gold_Unit_Type");
        });

        modelBuilder.Entity<GoldUnitType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Gold_Price_Type");

            entity.ToTable("GoldUnitType");

            entity.Property(e => e.TitleAr)
                .HasMaxLength(150)
                .HasColumnName("Title_Ar");
            entity.Property(e => e.TitleEn)
                .HasMaxLength(150)
                .HasColumnName("Title_En");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNumber)
                .HasMaxLength(180)
                .HasColumnName("ID_Number");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<GroupType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Group_Type");

            entity.ToTable("GroupType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<GroupTypeAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Group_Type_Account");

            entity.ToTable("GroupTypeAccount");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.GroupId).HasColumnName("Group_ID");
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");

            entity.HasOne(d => d.Account).WithMany(p => p.GroupTypeAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Group_Type_Account_Account");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupTypeAccounts)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Group_Type_Account_Group");

            entity.HasOne(d => d.Type).WithMany(p => p.GroupTypeAccounts)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Group_Type_Account_Group_Type");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(180);
            entity.Property(e => e.Email).HasMaxLength(180);
            entity.Property(e => e.Fax).HasMaxLength(180);
            entity.Property(e => e.Logo)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Map).HasMaxLength(180);
            entity.Property(e => e.Phones).HasMaxLength(180);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(180)
                .HasColumnName("Swift_Code");
            entity.Property(e => e.Title).HasMaxLength(180);
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");
            entity.Property(e => e.Url)
                .HasMaxLength(180)
                .HasColumnName("URL");

            entity.HasOne(d => d.Type).WithMany(p => p.Items)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Item_Category");
        });

        modelBuilder.Entity<ItemArea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Item_Area");

            entity.ToTable("ItemArea");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AreaId).HasColumnName("Area_ID");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");

            entity.HasOne(d => d.Area).WithMany(p => p.ItemAreas)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_Item_Area_Area");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemAreas)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Item_Area_Item");
        });

        modelBuilder.Entity<ItemKeyword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Item_Keyword");

            entity.ToTable("ItemKeyword");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.KeyWordId).HasColumnName("KeyWord_ID");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemKeywords)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Item_Keyword_Item");

            entity.HasOne(d => d.KeyWord).WithMany(p => p.ItemKeywords)
                .HasForeignKey(d => d.KeyWordId)
                .HasConstraintName("FK_Item_Keyword_Keywords");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.Title).HasMaxLength(180);

            entity.HasOne(d => d.Category).WithMany(p => p.Keywords)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Keywords_Category");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");

            entity.HasOne(d => d.Type).WithMany(p => p.Posts)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Post_Post_Type");
        });

        modelBuilder.Entity<PostGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Post_Group");

            entity.ToTable("PostGroup");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GroupId).HasColumnName("Group_ID");
            entity.Property(e => e.PostId).HasColumnName("Post_ID");

            entity.HasOne(d => d.Group).WithMany(p => p.PostGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Post_Group_Group");

            entity.HasOne(d => d.Post).WithMany(p => p.PostGroups)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Post_Group_Post");
        });

        modelBuilder.Entity<PostType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Post_Type");

            entity.ToTable("PostType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rates");

            entity.ToTable("Rate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("Bank_ID");
            entity.Property(e => e.BuyArrow)
                .HasMaxLength(50)
                .HasColumnName("Buy_Arrow");
            entity.Property(e => e.BuyPrice).HasColumnName("Buy_Price");
            entity.Property(e => e.CurrencyId).HasColumnName("Currency_ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.SaleArrow)
                .HasMaxLength(50)
                .HasColumnName("Sale_Arrow");
            entity.Property(e => e.SalePrice).HasColumnName("Sale_Price");
            entity.Property(e => e.TransferBuy).HasColumnName("Transfer_Buy");
            entity.Property(e => e.TransferSale).HasColumnName("Transfer_Sale");

            entity.HasOne(d => d.Bank).WithMany(p => p.Rates)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rates_Bank");

            entity.HasOne(d => d.Currency).WithMany(p => p.Rates)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rates_Currency");
        });

        modelBuilder.Entity<RateLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rates_Log");

            entity.ToTable("RateLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("Bank_ID");
            entity.Property(e => e.BuyPrice).HasColumnName("Buy_Price");
            entity.Property(e => e.CurrencyId).HasColumnName("Currency_ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.SalePrice).HasColumnName("Sale_Price");
            entity.Property(e => e.TransferBuy).HasColumnName("Transfer_Buy");
            entity.Property(e => e.TransferSale).HasColumnName("Transfer_Sale");

            entity.HasOne(d => d.Bank).WithMany(p => p.RateLogs)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rates_Log_Bank");

            entity.HasOne(d => d.Currency).WithMany(p => p.RateLogs)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rates_Log_Currency");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Hostname).HasMaxLength(50);
            entity.Property(e => e.Ip).HasMaxLength(50);
            entity.Property(e => e.Loc).HasMaxLength(50);
            entity.Property(e => e.Org).HasMaxLength(50);
            entity.Property(e => e.Region).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
