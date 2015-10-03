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
    public class SubForumController : Controller
    {
        //
        // GET: /SubForum/
        readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index(int? id, int? page)
        {
            var forumthreads = _db.ForumThreads.Where(f => f.ForumThreadId == id).ToPagedList(page ?? 1, 3);

            return View(forumthreads);
        }

    }
}
