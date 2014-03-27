using Polls.DAL;
using System;
using Polls.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Polls.ViewModels;

namespace Polls.Controllers
{

    public class QuestionsController : Controller
    {
        public PollsContext db = new PollsContext();


        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["message"];
            return View(db.Questions.ToList());
        }

        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            else if (question.EndDate < DateTime.Now)
            {
                TempData["message"] = "You can no longer answer this question!";
                return RedirectToAction("Index");
            }

            return View(question);
        }


        [Authorize]
        public ActionResult Submit()
        {
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Submit(FormCollection collection, Vote vote)
        {

            int chosenAnswer = int.Parse(collection["answer"]);
            int question = db.Answers.Single(a => a.AnswerID == chosenAnswer).QuestionID;
            vote = db.Votes.Where(v => v.Answer.QuestionID == question && v.User.Name == User.Identity.Name).SingleOrDefault();
            var voteToDb = new Vote();
            voteToDb.AnswerID = db.Answers.Single(a => a.AnswerID == chosenAnswer).AnswerID;
            voteToDb.UserID = db.Users.Single(u => u.Name == User.Identity.Name).UserID;
            if (vote == null)
            {
                db.Votes.Add(voteToDb);
            }
            else
            {
                db.Votes.Remove(vote);
                db.Votes.Add(voteToDb);
            }
            db.SaveChanges();
            return View();

        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }


        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserID = db.Users.Single(u => u.Name == User.Identity.Name).UserID;
                db.Questions.Add(question);
                db.SaveChanges();
                Session["question"] = question.QuestionID;
                return RedirectToAction("AddAnswer");
            }
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", question.UserID);
            return View(question);          //question
        }

        [Authorize]
        public ActionResult AddAnswer()
        {
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.QuestionID = int.Parse(Session["question"].ToString());
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("AddAnswer");
            }
            return RedirectToAction("AddAnswer");
        }


        public ActionResult Statistics(string question, int id = 0)
        {

            ViewBag.Question = question;
            var numberGroups = from n in db.Votes
                               where n.Answer.QuestionID == id
                               group n by new { n.AnswerID, n.Answer.Text } into g
                               select new QuestionStatistics { Answer = g.Key.Text, NumberOfVotes = g.Count() };


            return View(numberGroups);
        }

        [Authorize]
        public ActionResult MyPolls()
        {
            ViewBag.ErrorMessage = TempData["message"];
            return View(db.Questions.ToList());
        }

        [Authorize]
        public ActionResult Edit(int id) //int id
        {
            Question question = db.Questions.Find(id);
            Session["question"] = question.QuestionID;
            if (question.StartDate < DateTime.Now)
            {
                TempData["message"] = "You cannot change active questions!";
                return RedirectToAction("MyPolls");

            }
            return View(question);  //question
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Question question)
        {
            //Question question = db.Questions.Find(id);
            if (ModelState.IsValid)
            {
                question.UserID = db.Users.Single(u => u.Name == User.Identity.Name).UserID;
                db.Questions.Add(question);
                db.SaveChanges();
                Session["question"] = question.QuestionID;
                return RedirectToAction("AddAnswer");
            }
            
            return View(question);
        }

        [Authorize]
        public ActionResult ChangeAnswers(int id)
        {            
            return View(db.Answers.Where(a => a.QuestionID==id).ToList());
        }

        [Authorize]
        public ActionResult DeleteAnswer(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("ChangeAnswers", new {id=answer.QuestionID });
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("MyPolls");
        }
    }
}
