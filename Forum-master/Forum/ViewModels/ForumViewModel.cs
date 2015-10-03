

using System.Collections.Generic;
using Forum.Models;

namespace Forum.ViewModels
{
    public class ForumViewModel
    {
       
        public string ForumTitle { get; set; }

        public int SubForumsCount { get; set; }
        

        public virtual List<SubForum> SubForum { get; set; }
    }
}