
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Mathimize.com.Models
{
    public class Terms
    {
        public int Term1 { get; set; }
        public int Term2 { get; set; }
    }


    public class BasicArithmeticViewModel 
    {
        [Required(ErrorMessage = "Required"), Range(typeof(int), "1", "9", ErrorMessage = "Please enter a number between 1 and 9")]
        public int Rows { get; set; }

        [Required(ErrorMessage = "Required"), Range(typeof(int), "1", "7", ErrorMessage="Please enter a number between 1 and 7")]
        public int Cols { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MaxInt { get; set; }

        public string Op { get; set; }

        public string ResultId { get; set; }

    }

    public class BasicArithmeticResultViewModel : BasicArithmeticViewModel
    {
        public IList<Terms> Terms { get; set; }

    }

}