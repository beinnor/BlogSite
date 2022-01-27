using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Shared.Domain
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }        
    }
}
