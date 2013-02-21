using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Mathimize.com.Models
{
    public class HourMinutes
    {
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }

    public class TimeViewModel : AMmathimizeViewModel
    {
        //[Required(ErrorMessage = "Required"), Range(typeof(int), "1", "6", ErrorMessage = "Please enter a number between 1 and 6")]
        //public int Rows { get; set; }

        //[Required(ErrorMessage = "Required"), Range(typeof(int), "1", "3", ErrorMessage = "Please enter a number between 1 and 3")]
        //public int Cols { get; set; }

        //public string Op { get; set; }

        //public string ResultId { get; set; }

    }

    public class TimeResultViewModel : TimeViewModel
    {
        public IList<HourMinutes> HourMinutes { get; set; }
    }
}