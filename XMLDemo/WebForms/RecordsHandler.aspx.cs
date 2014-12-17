using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace XMLDemo.WebForms
{
    public partial class RecordsHandler : System.Web.UI.Page
    {
        List<string> recordIdArr = new List<string>();
        List<string> readerIdArr = new List<string>();
        List<string> bookIdArr = new List<string>();
        List<string> borrowDateArr = new List<string>();
        List<string> returnDateArr = new List<string>();
        List<string> remarkArr = new List<string>();

        int recordsNumber = 0;
        static int index = 0;

        private void bind()
        {
            loadContent();
            contenAtIndex(index);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }

        protected void modify_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            loadContent();
            string oldid = recordIdArr[index];
            foreach (string isbnTemp in recordIdArr)
            {
                if (isbnTemp == recordId.Text && oldid != recordId.Text)
                    isValid = false;
            }

            if (isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../Data/Records.xml"));

                foreach (XmlNode record in doc.DocumentElement.ChildNodes[index].ChildNodes)
                {
                    if (record.Name == "ReaderID")
                        record.InnerText = readerId.Text;
                    if (record.Name == "BookID")
                        record.InnerText = bookId.Text;
                    if (record.Name == "BorrowDate")
                        record.InnerText = borrowDate.Text;
                    if (record.Name == "ReturnDate")
                        record.InnerText = returnDate.Text;
                    if (record.Name == "Remark")
                        record.InnerText = remark.Text;
                }

                doc.Save(Server.MapPath("../Data/Records.xml"));
                Response.Write("<script>alert('修改成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意借阅编号是否唯一或者其他非法输入');</script>");
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            loadContent();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Data/Records.xml"));
            //XmlNode record = doc.SelectSingleNode("/xx:records/xx:record[@ISBN=" + isbn.Text + "]");
            XmlNode record = doc.DocumentElement.ChildNodes[index];
            record.ParentNode.RemoveChild(record);
            doc.Save(Server.MapPath("../Data/Records.xml"));
            Response.Write("<script>alert('删除成功');</script>");
            index = 0;
            recordsNumber = 0;
            bind();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            loadContent();
            foreach (string isbnTemp in recordIdArr)
            {
                if (isbnTemp == recordId.Text)
                    isValid = false;
            }

            if (isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../Data/Records.xml"));
                // 创建 record 节点开始
                XmlNode recordNode = doc.CreateElement("Record", doc.DocumentElement.NamespaceURI);

                XmlNode readerIdNode = doc.CreateElement("ReaderID", doc.DocumentElement.NamespaceURI);
                readerIdNode.InnerText = readerId.SelectedValue;
                XmlNode bookIdNode = doc.CreateElement("BookID", doc.DocumentElement.NamespaceURI);
                bookIdNode.InnerText = bookId.SelectedValue;
                XmlNode borrowDateNode = doc.CreateElement("BorrowDate", doc.DocumentElement.NamespaceURI);
                borrowDateNode.InnerText = borrowDate.Text;
                XmlNode returnDateNode = doc.CreateElement("ReturnDate", doc.DocumentElement.NamespaceURI);
                returnDateNode.InnerText = returnDate.Text;
                XmlNode remarkNode = doc.CreateElement("Remark", doc.DocumentElement.NamespaceURI);
                remarkNode.InnerText = remark.Text;


                recordNode.AppendChild(readerIdNode);
                recordNode.AppendChild(bookIdNode);
                recordNode.AppendChild(borrowDateNode);
                recordNode.AppendChild(returnDateNode);
                recordNode.AppendChild(remarkNode);

                XmlAttribute recordIdAttr = doc.CreateAttribute("ID");
                recordIdAttr.InnerText = recordId.Text;
                recordNode.Attributes.Append(recordIdAttr);
                // 创建 record 节点结束
                doc.DocumentElement.AppendChild(recordNode);
                doc.Save(Server.MapPath("../Data/Records.xml"));
                Response.Write("<script>alert('添加成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意借阅编号是否唯一或者其他非法输入');</script>");
            }
        }

        protected void pre_Click(object sender, EventArgs e)
        {
            bind();
            if (index > 0)
            {
                index--;
                contenAtIndex(index);
            }

        }

        protected void next_Click(object sender, EventArgs e)
        {
            bind();
            if (index < recordsNumber - 1)
            {
                index++;
                contenAtIndex(index);
            }
        }

        private void contenAtIndex(int i)
        {
            recordId.Text = recordIdArr[i];

            readerId.Items.Clear();
            foreach (string reader in readerIdArr)
            {
                readerId.Items.Add(reader);
            }
            readerId.SelectedValue = readerIdArr[i];

            bookId.Items.Clear();
            foreach (string book in bookIdArr)
            {
                bookId.Items.Add(book);
            }
            bookId.SelectedValue = bookIdArr[i];

            borrowDate.Text = borrowDateArr[i];
            returnDate.Text = returnDateArr[i];
            remark.Text = remarkArr[i];
        }

        private void loadContent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Data/Records.xml"));
            foreach (XmlNode records in doc.DocumentElement.ChildNodes)
            {
                recordIdArr.Add(records.Attributes["ID"].Value);

                foreach (XmlNode record in records.ChildNodes)
                {
                    if (record.Name == "ReaderID")
                        readerIdArr.Add(record.InnerText);
                    if (record.Name == "BookID")
                        bookIdArr.Add(record.InnerText);
                    if (record.Name == "BorrowDate")
                        borrowDateArr.Add(record.InnerText);
                    if (record.Name == "ReturnDate")
                        returnDateArr.Add(record.InnerText);
                    if (record.Name == "Remark")
                        remarkArr.Add(record.InnerText);
                }
                recordsNumber++;
            }

        }
    }
}