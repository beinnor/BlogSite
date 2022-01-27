using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Shared.Domain
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }     
    }
}
