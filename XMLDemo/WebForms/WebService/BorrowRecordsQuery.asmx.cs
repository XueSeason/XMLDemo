using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace XMLDemo.WebForms.WebService
{
    /// <summary>
    /// BorrowRecordsQuery 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BorrowRecordsQuery : System.Web.Services.WebService
    {

        [WebMethod(Description = "根据读者编号查询借阅信息")]
        public List<Result> QueryByReaderID(string readerID)
        {
            return query("ReaderID", readerID);
        }

        [WebMethod(Description = "根据图书编号查询借阅信息")]
        public List<Result> QueryByBookID(string bookID)
        {
            return query("BookID", bookID);
        }

        private List<Result> query(string content, string key)
        {
            List<Result> result = new List<Result>();
            List<string> bookIdArr = new List<string>();
            List<string> titleArr = new List<string>();
            List<string> readerIdArr = new List<string>();
            List<string> nameArr = new List<string>();
            List<string> borrowDateArr = new List<string>();
            List<string> returnDateArr = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../../Data/Records.xml"));

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ab", "http://tempuri.org/Records.xsd");
            XmlNodeList records;
            if (key != "")
                records = doc.SelectNodes("//ab:Record[ab:" + content + "='" + key + "']", nsmgr);
            else
                records = doc.SelectNodes("//ab:Record", nsmgr);

            foreach (XmlNode record in records)
            {
                foreach (XmlNode node in record)
                {
                    if (node.Name == "ReaderID")                       
                        readerIdArr.Add(node.InnerText);
                    if (node.Name == "BookID")
                        bookIdArr.Add(node.InnerText);
                    if (node.Name == "BorrowDate")
                        borrowDateArr.Add(node.InnerText);
                    if (node.Name == "ReturnDate")
                        returnDateArr.Add(node.InnerText);
                }
            }

            doc.Load(Server.MapPath("../../Data/Books.xml"));
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("tt", "http://tempuri.org/Books.xsd");
            foreach (string bookId in bookIdArr)
            {
                titleArr.Add(doc.SelectSingleNode("//tt:Book[@ISBN='" + bookId + "']/tt:Title", nsmgr).InnerText);
            }

            doc.Load(Server.MapPath("../../Data/Readers.xml"));
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rd", "http://tempuri.org/Readers.xsd");
            foreach (string readerId in readerIdArr)
            {
                nameArr.Add(doc.SelectSingleNode("//rd:Reader[@ID='" + readerId + "']/rd:Name", nsmgr).InnerText);
            }

            for(int i=0; i<readerIdArr.Count; i++)
            {
                Result node = new Result();
                node._BookID = bookIdArr[i];
                node._BookTitle = titleArr[i];
                node._ReaderID = readerIdArr[i];
                node._ReaderName = nameArr[i];
                node._BorrowDate = borrowDateArr[i];
                node._ReturnDate = returnDateArr[i];
                result.Add(node);
            }

            return result;
        }

    }
}
