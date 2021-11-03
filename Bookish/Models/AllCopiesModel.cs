using System.Collections.Generic;

namespace Bookish.Models
{
    public class AllCopiesModel
    {
        public IEnumerable<CopyCountModel> copies { get; set; }
    }

    public class CopyCountModel
    {
        public int id { get; set; }
        public int count { get; set; }
    }
}