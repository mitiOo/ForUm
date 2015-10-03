

using System.Collections.Generic;
using Forum.Models;

namespace Forum.ViewModels
{
    public class SubForumViewModel
    {
        public int SubForumId { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public int ThreadCount { get; set; }
        public virtual List<ForumThread> ForumThreadId { get; set; }
        public virtual Models.Forum Forum { get; set; }
    }
}