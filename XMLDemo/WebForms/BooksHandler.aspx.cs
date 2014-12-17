using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace XMLDemo.WebForms
{
    public partial class BooksHandler : System.Web.UI.Page
    {
        List<string> imageArr = new List<string>();
        List<string> isbnArr = new List<string>();
        List<string> titleArr = new List<string>();
        List<string> authorArr = new List<string>();
        List<string> seriesArr = new List<string>();
        List<string> publisherArr = new List<string>();
        List<string> publishYearArr = new List<string>();
        List<string> priceArr = new List<string>();

        int booksNumber = 0;
        static int index = 0;

        private void bind()
        {
            loadContent();
            contenAtIndex(index);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bind();
            }
        }

        protected void modify_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            loadContent();
            string oldid = isbnArr[index];
            foreach (string isbnTemp in isbnArr)
            {
                if (isbnTemp == isbn.Text && oldid != isbn.Text)
                    isValid = false;
            }

            if(isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("..//Data//Books.xml"));

                foreach (XmlNode book in doc.DocumentElement.ChildNodes[index].ChildNodes)
                {
                    if (book.Name == "Title")
                        book.InnerText = title.Text;
                    if (book.Name == "Author")
                        book.InnerText = author.Text;
                    if (book.Name == "Series")
                        book.InnerText = series.Text;
                    if (book.Name == "Publisher")
                        book.InnerText = publisher.Text;
                    if (book.Name == "PublishYear")
                        book.InnerText = publishYear.Text;
                    if (book.Name == "Price")
                        book.InnerText = price.Text;
                }
 
                doc.Save(Server.MapPath("..//Data//Books.xml"));
                Response.Write("<script>alert('修改成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意ISBN是否唯一或者其他非法输入');</script>");
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            loadContent();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("..//Data//Books.xml"));
            //XmlNode book = doc.SelectSingleNode("/xx:Books/xx:Book[@ISBN=" + isbn.Text + "]");
            XmlNode book = doc.DocumentElement.ChildNodes[index];
            book.ParentNode.RemoveChild(book);
            doc.Save(Server.MapPath("..//Data//Books.xml"));
            Response.Write("<script>alert('删除成功');</script>");
            index = 0;
            booksNumber = 0;
            bind();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            loadContent();
            foreach (string isbnTemp in isbnArr)
            {
                if (isbnTemp == isbn.Text)
                    isValid = false;
            }

            if(isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("..//Data//Books.xml"));
                // 创建 book 节点开始
                XmlNode bookNode = doc.CreateElement("Book", doc.DocumentElement.NamespaceURI);

                XmlNode titleNode = doc.CreateElement("Title", doc.DocumentElement.NamespaceURI);
                titleNode.InnerText = title.Text;
                XmlNode authorNode = doc.CreateElement("Author", doc.DocumentElement.NamespaceURI);
                authorNode.InnerText = author.Text;
                XmlNode seriesNode = doc.CreateElement("Series", doc.DocumentElement.NamespaceURI);
                seriesNode.InnerText = series.SelectedValue;
                XmlNode publisherNode = doc.CreateElement("Publisher", doc.DocumentElement.NamespaceURI);
                publisherNode.InnerText = publisher.Text;
                XmlNode publishNode = doc.CreateElement("PublishYear", doc.DocumentElement.NamespaceURI);
                publishNode.InnerText = publishYear.Text;
                XmlNode priceNode = doc.CreateElement("Price", doc.DocumentElement.NamespaceURI);
                priceNode.InnerText = price.Text;

                bookNode.AppendChild(titleNode);
                bookNode.AppendChild(authorNode);
                bookNode.AppendChild(seriesNode);
                bookNode.AppendChild(publisherNode);
                bookNode.AppendChild(publishNode);
                bookNode.AppendChild(priceNode);

                XmlAttribute isbnAttr = doc.CreateAttribute("ISBN");
                isbnAttr.InnerText = isbn.Text;
                bookNode.Attributes.Append(isbnAttr);
                // 创建 book 节点结束
                doc.DocumentElement.AppendChild(bookNode);
                doc.Save(Server.MapPath("..//Data//Books.xml"));
                Response.Write("<script>alert('添加成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意ISBN是否唯一或者其他非法输入');</script>");
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
            if(index < booksNumber-1)
            {
                index++;
                contenAtIndex(index);       
            }
        }

        private void contenAtIndex(int i)
        {
            isbn.Text = isbnArr[i];
            title.Text = titleArr[i];
            author.Text = authorArr[i];
            series.Items.Clear();
            foreach(string serie in seriesArr)
            {
                series.Items.Add(serie);    
            }
            series.SelectedValue = seriesArr[i];
            publisher.Text = publisherArr[i];
            publishYear.Text = publishYearArr[i];
            price.Text = priceArr[i];
            image.Src = imageArr[i];
        }

        private void loadContent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("..//Data//Books.xml"));
            foreach(XmlNode books in doc.DocumentElement.ChildNodes)
            {
                isbnArr.Add(books.Attributes["ISBN"].Value);

                foreach (XmlNode book in books.ChildNodes)
                {
                    if(book.Name == "Title")
                    {
                        titleArr.Add(book.InnerText);
                        imageArr.Add("../Statics/images/" + book.InnerText + ".jpg");
                    }
                    if (book.Name == "Author")
                        authorArr.Add(book.InnerText);
                    if (book.Name == "Series")
                        seriesArr.Add(book.InnerText);
                    if (book.Name == "Publisher")
                        publisherArr.Add(book.InnerText);
                    if (book.Name == "PublishYear")
                        publishYearArr.Add(book.InnerText);
                    if (book.Name == "Price")
                        priceArr.Add(book.InnerText);
                }
                booksNumber++;
            }

        }

    }
}