namespace Polls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InintialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),                        
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Answer_AnswerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Answer_AnswerID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: false)
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Answer_AnswerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserAnswers", "Answer_AnswerID", "dbo.Answers");
            DropForeignKey("dbo.UserAnswers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "UserID" });
            DropIndex("dbo.UserAnswers", new[] { "Answer_AnswerID" });
            DropIndex("dbo.UserAnswers", new[] { "User_UserID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropTable("dbo.UserAnswers");
            DropTable("dbo.Users");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
