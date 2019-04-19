using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.UI.Areas.Member.Models.VM
{
    public class CommentVM
    {
        public Guid ID { get; set; }
     
        public string Content { get; set; }
    }
}