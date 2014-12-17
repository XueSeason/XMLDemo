using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XMLDemo.WebForms.WebService
{
    public class Result
    {
        public string _ReaderID { get; set; }
        public string _ReaderName { get; set; }
        public string _BookID { get; set; }
        public string _BookTitle { get; set; }
        public string _BorrowDate { get; set; }
        public string _ReturnDate { get; set; }

        public Result()
        {

        }

        public Result(string ReaderID, string ReaderName, string BookID, string BookTitle, string BorrowDate, string ReturnDate)
		{
            _ReaderID = ReaderID;
            _ReaderName = ReaderName;
            _BookID = BookID;
            _BookTitle = BookTitle;
            _BorrowDate = BorrowDate;
            _ReturnDate = ReturnDate;
		}
    }
}