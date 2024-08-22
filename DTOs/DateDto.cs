using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DateDto
    {
        public DateTime FromDate { get; set; }=DateTime.Now.Date;
        public DateTime ToDate { get; set; }
    }
}
