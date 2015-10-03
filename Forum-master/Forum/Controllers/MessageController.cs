using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Forum.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Messages/
        readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {            
            var results = _db.Messages.ToList();
            return View(results);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Message msg, string id)
        {
            var userName = User.Identity.GetUserName();
            if(ModelState.IsValid && msg.Content!=null)
            {
               
                var parsedid =0;
                Int32.TryParse(id, out parsedid);

                
                    msg.Created = 
                    
                        DateTime.Now;
                    //msg.User = fe.Users.Single(u => u.UserName == User.Identity.Name);
              
                    msg.ForumThread =  _db.ForumThreads.Single(f => f.ForumThreadId == parsedid);
                    _db.Messages.Add(msg);
                    _db.SaveChanges();
                    return RedirectToAction("Details", "ForumThread", new { id = id });

                
            }
            return RedirectToAction("Details", "ForumThread", new { id = id });
            //return View();
        }

    }
}
