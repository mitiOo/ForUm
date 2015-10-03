using System.Globalization;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Forum.Controllers
{
    public class ForumThreadController : Controller
    {
        //
        // GET: /ForumThread/
        readonly ApplicationDbContext _db = new ApplicationDbContext();
            
        public ActionResult Index(int? page, int id = (int)Models.Enums.SubForums.Mainforum)
        {
            var forumthreads = _db.ForumThreads.Where(ft => ft.SubForumId == id).ToList();
            TempData["SubForumId"] = id;

            ViewBag.MsgCount = _db.Messages.Count(m => m.ForumThread.ForumThreadId == id);

          
            return View(forumthreads.ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ForumThread forumthread)
        {
            if(ModelState.IsValid)
            {
                //int parsedsubforumid = 0;
                //Int32.TryParse(subforumid, out parsedsubforumid);

                forumthread.CreatedOn = System.DateTime.Now;                
                _db.ForumThreads.Add(forumthread);                
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int id, int? page)
        {
            ThreadViewModel tvm = new ThreadViewModel();

            List<Message> messages = _db.Messages.Where(m => m.ForumThread.ForumThreadId == id).ToList();

            tvm.Message = messages.ToPagedList(page ?? 1, 10);            
            ViewData["ThreadId"] = id.ToString(CultureInfo.InvariantCulture);
            tvm.Title = _db.ForumThreads.FirstOrDefault(f => f.ForumThreadId == id).Title;
            tvm.SubForumId = _db.ForumThreads.FirstOrDefault(f => f.ForumThreadId == id).SubForumId;

            tvm.CountMsgs = _db.Messages.Count(m => m.ForumThread.ForumThreadId == id);
            tvm.ForumMessage = new Message();

            return View(tvm);
        }

    }
}
