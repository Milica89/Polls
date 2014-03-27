namespace Polls.Migrations
{
    using Polls.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Polls.DAL.PollsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Polls.DAL.PollsContext context)
        {
            WebSecurity.InitializeDatabaseConnection("PollsContext", "Users", "UserID", "Name", true);


            if (!WebSecurity.UserExists("Milica"))
                WebSecurity.CreateUserAndAccount("Milica", "milica89");

            if (!WebSecurity.UserExists("Marko"))
                WebSecurity.CreateUserAndAccount("Marko", "marko021");

            if (!WebSecurity.UserExists("Tamara"))
                WebSecurity.CreateUserAndAccount("Tamara", "tamara89");

            if (!WebSecurity.UserExists("Nikola"))
                WebSecurity.CreateUserAndAccount("Nikola", "xxxyyy");

            if (!WebSecurity.UserExists("Ana"))
                WebSecurity.CreateUserAndAccount("Ana", "654321");
         


            var questions = new List<Question> 
            {
                new Question {
                    UserID= context.Users.Single(p=>p.Name=="Marko").UserID,
                    StartDate=DateTime.Parse("2013-10-12"),
                    EndDate=DateTime.Parse("2013-10-22"),
                    Text="Koji termin za bioskop vam najvise odgovara?"                    
                },
                new Question {
                    UserID=context.Users.Single(p=>p.Name=="Ana").UserID,
                    StartDate=DateTime.Parse("2013-10-10"),
                    EndDate=DateTime.Parse("2013-10-15"),
                    Text="Sta vam se gleda u bioskopu?"
                    
                },
                new Question {
                    UserID=context.Users.Single(p=>p.Name=="Tamara").UserID,
                    StartDate=DateTime.Parse("2012-03-20"),
                    EndDate=DateTime.Parse("2014-03-27"),
                    Text="Koliko cesto putujete van drzave?"                   
                },
                new Question {
                    UserID=context.Users.Single(p=>p.Name=="Nikola").UserID,
                    StartDate=DateTime.Parse("2012-07-27"),
                    EndDate=DateTime.Parse("2014-08-27"),
                    Text="Koji sport najcesce pratite?"
                }
            };
            foreach (Question q in questions)
            {
                var questionInDB = context.Questions.Where(s => s.Text == q.Text).SingleOrDefault();
                if (questionInDB == null)
                {
                    context.Questions.Add(q);
                }
            }
            context.SaveChanges();

            var answers = new List<Answer>
            {
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji termin za bioskop vam najvise odgovara?").QuestionID,
                     Text="ponedeljak, 19:30",
                     

                 },
                 
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji termin za bioskop vam najvise odgovara?").QuestionID,
                     Text="utorak, 20:45"
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji termin za bioskop vam najvise odgovara?").QuestionID,
                     Text="sreda, 19:30",
                    
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji termin za bioskop vam najvise odgovara?").QuestionID,
                     Text="petak, 23:00"
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Sta vam se gleda u bioskopu?").QuestionID,
                     Text="Avatar"
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Sta vam se gleda u bioskopu?").QuestionID,
                     Text="Hobbit",
                     
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Sta vam se gleda u bioskopu?").QuestionID,
                     Text="Catch me if you can",
                   
                 }, 
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Sta vam se gleda u bioskopu?").QuestionID,
                     Text="Pulp fiction"
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Sta vam se gleda u bioskopu?").QuestionID,
                     Text="Local hero",
             
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koliko cesto putujete van drzave?").QuestionID,
                     Text="0-2 puta godisnje",
            
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koliko cesto putujete van drzave?").QuestionID,
                     Text="3-5 puta godisnje",
        
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koliko cesto putujete van drzave?").QuestionID,
                     Text="preko 5 puta godisnje"
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji sport najcesce pratite?").QuestionID,
                     Text="fudbal",
              
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji sport najcesce pratite?").QuestionID,
                     Text="kosarka",
               
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji sport najcesce pratite?").QuestionID,
                     Text="odbojka",
                     
                 },
                 new Answer {
                     QuestionID=context.Questions.Single(p => p.Text=="Koji sport najcesce pratite?").QuestionID,
                     Text="tenis"
                 }               
                 
            };
            foreach (Answer a in answers)
            {
                var answerInDB = context.Answers.Where(s => s.Text == a.Text).SingleOrDefault();
                if (answerInDB == null)
                {
                    context.Answers.Add(a);
                }

            }
            context.SaveChanges();

            var Votes = new List<Vote> 
            {
                new Vote {UserID=context.Users.Single(p => p.Name == "Tamara").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="tenis").AnswerID},
                     
                         new Vote {UserID=context.Users.Single(p => p.Name == "Ana").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="odbojka").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Milica").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="kosarka").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Marko").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="fudbal").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Ana").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="3-5 puta godisnje").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Nikola").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="3-5 puta godisnje").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Milica").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="0-2 puta godisnje").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Marko").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="0-2 puta godisnje").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Nikola").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="Local hero").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Milica").UserID,
                             AnswerID=context.Answers.Single(p => p.Text=="Hobbit").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Ana").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="Hobbit").AnswerID},
                          new Vote {UserID=context.Users.Single(p => p.Name == "Tamara").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="Catch me if you can").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Ana").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="sreda, 19:30").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Milica").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="ponedeljak, 19:30").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Nikola").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="ponedeljak, 19:30").AnswerID},
                         new Vote {UserID=context.Users.Single(p => p.Name == "Tamara").UserID,
                         AnswerID=context.Answers.Single(p => p.Text=="ponedeljak, 19:30").AnswerID}
            };
            foreach (Vote a in Votes)
            {
                var voteInDB = context.Votes.Where(s => s.UserID == a.UserID && s.AnswerID==a.AnswerID).SingleOrDefault();
                if (voteInDB == null)
                {
                    context.Votes.Add(a);
                }

            }
            context.SaveChanges();
        }
    }
}
