namespace Polls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),                    
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.UserID);

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
                "dbo.UserAnswers",
                c => new
                {
                    Answer_AnswerID = c.Int(nullable: false),
                    User_UserID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Answer_AnswerID, t.User_UserID })
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: false)
                .Index(t => t.Answer_AnswerID)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserAnswers", new[] { "User_UserID" });
            DropIndex("dbo.UserAnswers", new[] { "Answer_AnswerID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropIndex("dbo.Questions", new[] { "UserID" });
            DropForeignKey("dbo.UserAnswers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.UserAnswers", "Answer_AnswerID", "dbo.Answers");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "UserID", "dbo.Users");
            DropTable("dbo.UserAnswers");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Users");
        }
    }
}
