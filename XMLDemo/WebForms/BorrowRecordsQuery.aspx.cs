using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XMLDemo.WebForms
{
    public partial class BorrowRecordsQuery : System.Web.UI.Page
    {
        List<string> bookIdArr = new List<string>();
        List<string> titleArr = new List<string>();

        List<string> readerIdArr = new List<string>();
        List<string> nameArr = new List<string>();

        List<string> borrowDateArr = new List<string>();
        List<string> returnDateArr = new List<string>();

        string bookQueryKey;
        string readerQueryKey;

        protected void Page_Load(object sender, EventArgs e)
        {
            bookQueryKey = bookText.Text;
            readerQueryKey = readerText.Text;
        }

        protected void readerQuery_Click(object sender, EventArgs e)
        {
            query("ReaderID", readerQueryKey);
            createXMLFile();
            transXSL();
        }

        protected void bookQuery_Click(object sender, EventArgs e)
        {
            query("BookID", bookQueryKey);
            createXMLFile();
            transXSL();
        }

        private void transXSL()
        {
            string xmlPath = Server.MapPath("./XMLFile/QueryResult.xml");
            string xslPath = Server.MapPath("./XMLFile/BorrowRecords.xsl");
            try
            {
                XPathDocument myXPathDoc = new XPathDocument(xmlPath);
                XslTransform myXslTrans = new XslTransform();
                myXslTrans.Load(xslPath);

                XmlTextWriter myWriter = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
                myXslTrans.Transform(myXPathDoc, null, myWriter);
                myWriter.Close();
            }
            catch (Exception e)
            {

            }
        }

        public void createXMLFile()
        {
            XmlDocument doc = new XmlDocument();   
            //建立Xml的定义声明   
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);   
            
            doc.AppendChild(dec);   
            //创建根节点   
            XmlElement root = doc.CreateElement("Records");

            for (int i = 0; i < bookIdArr.Count; i++)
            {
                XmlNode readerId = doc.CreateElement("ReaderID");
                readerId.InnerText = readerIdArr[i];
                XmlNode name = doc.CreateElement("ReaderName");
                name.InnerText = nameArr[i];
                XmlNode bookId = doc.CreateElement("BookID");
                bookId.InnerText = bookIdArr[i];
                XmlNode title = doc.CreateElement("BookTitle");
                title.InnerText = titleArr[i];
                XmlNode borrowDate = doc.CreateElement("BorrowDate");
                borrowDate.InnerText = borrowDateArr[i];
                XmlNode returnDate = doc.CreateElement("ReturnDate");
                returnDate.InnerText = returnDateArr[i];

                XmlNode record = doc.CreateElement("Record");
                record.AppendChild(readerId);
                record.AppendChild(name);
                record.AppendChild(bookId);
                record.AppendChild(title);
                record.AppendChild(borrowDate);
                record.AppendChild(returnDate);

                root.AppendChild(record);
            }

            doc.AppendChild(root);
            doc.Save(Server.MapPath("./XMLFile/QueryResult.xml"));
        }

        public void query(string content, string key)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Data/Records.xml"));

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ab", "http://tempuri.org/Records.xsd");
            XmlNodeList records;
            if(key!="")
                records = doc.SelectNodes("//ab:Record[ab:"+content+"='" + key + "']", nsmgr);
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

            doc.Load(Server.MapPath("../Data/Books.xml"));
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("tt", "http://tempuri.org/Books.xsd");
            foreach (string bookId in bookIdArr)
            {
                titleArr.Add(doc.SelectSingleNode("//tt:Book[@ISBN='" + bookId + "']/tt:Title", nsmgr).InnerText);
            }

            doc.Load(Server.MapPath("../Data/Readers.xml"));
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rd", "http://tempuri.org/Readers.xsd");
            foreach (string readerId in readerIdArr)
            {
                nameArr.Add(doc.SelectSingleNode("//rd:Reader[@ID='" + readerId + "']/rd:Name", nsmgr).InnerText);
            }
        }
    }
}