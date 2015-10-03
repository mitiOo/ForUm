using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        //
        // GET: /Forum/
        readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var forums = _db.Forums.Select(x => new ForumViewModel
            {
                ForumTitle = x.ForumTitle,
                SubForum = x.SubForum,
                SubForumsCount = x.SubForum.Count
            }).ToList();
           
          
            return View(forums);
        }

        public ActionResult Details(int? id)
        {
            Forum.Models.Forum forum = _db.Forums.FirstOrDefault(f => f.ForumId == id);
            return View(forum);
        }               

        public ActionResult Search(string search, string searchBy, string sortBy, int? page)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            var searchResults = new List<ForumThread>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var thread in _db.ForumThreads)
                {
                    searchResults.Add(thread);
                }

                if (!string.IsNullOrWhiteSpace(search))
                    searchResults = searchResults.Where(m => m.Title.ToLower().Contains(search)).ToList();

                switch (sortBy)
                {
                    case "Name desc":
                        searchResults = searchResults.OrderByDescending(a => a.ForumThreadId).ToList();
                        break;
                    default:
                        searchResults = searchResults.OrderBy(a => a.ForumThreadId).ToList();
                        break;
                }
            }
            

            return View(searchResults.ToPagedList(page ?? 1, 3));
        }

    }
}
