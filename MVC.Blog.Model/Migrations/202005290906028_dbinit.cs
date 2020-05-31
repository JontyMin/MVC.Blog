namespace MVC.Blog.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        UserId = c.Guid(nullable: false),
                        GoodCount = c.Int(nullable: false),
                        BadCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        Password = c.String(nullable: false, maxLength: 30, unicode: false),
                        ImagePath = c.String(nullable: false, maxLength: 300, unicode: false),
                        FansCount = c.Int(nullable: false),
                        FocusCount = c.Int(nullable: false),
                        SiteName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleToCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryName = c.String(),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Content = c.String(nullable: false, maxLength: 800),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        FocusUserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FocusUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FocusUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fans", "UserId", "dbo.Users");
            DropForeignKey("dbo.Fans", "FocusUserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleToCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "UserId", "dbo.Users");
            DropForeignKey("dbo.ArticleToCategories", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropIndex("dbo.Fans", new[] { "FocusUserId" });
            DropIndex("dbo.Fans", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "UserId" });
            DropIndex("dbo.ArticleToCategories", new[] { "ArticleId" });
            DropIndex("dbo.ArticleToCategories", new[] { "CategoryId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropTable("dbo.Fans");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.ArticleToCategories");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
