using System;
using System.Collections;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class UserLoanModel
    {
        public string title { get; set; }
        public string primary_author { get; set; }
        public int id { get; set; }
        public DateTime checked_out{ get; set; }
        public DateTime due_back { get; set; }
        public decimal late_fee { get; set; }
    }
}