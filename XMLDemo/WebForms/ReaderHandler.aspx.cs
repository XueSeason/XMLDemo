using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace XMLDemo.WebForms
{
    public partial class ReaderHandler : System.Web.UI.Page
    {

        List<string> readerIdArr = new List<string>();
        List<string> nameArr = new List<string>();
        List<string> statusArr = new List<string>();
        List<string> cityArr = new List<string>();
        List<string> districtArr = new List<string>();
        List<string> streetArr = new List<string>();
        List<string> noArr = new List<string>();
        List<string> phoneArr = new List<string>();
        List<string> emailArr = new List<string>();

        List<string> statusSet = new List<string>();

        int readersNumber = 0;
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
            string oldid = readerIdArr[index];
            foreach (string idTemp in readerIdArr)
            {
                if (idTemp == readerId.Text && oldid != readerId.Text)
                    isValid = false;
            }

            if (isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../Data/Readers.xml"));

                foreach (XmlNode reader in doc.DocumentElement.ChildNodes[index].ChildNodes)
                {
                    if (reader.Name == "Name")
                        reader.InnerText = name.Text;
                    if (reader.Name == "Status")
                        reader.InnerText = status.Text;
                    if (reader.Name == "Address")
                    {
                        foreach (XmlNode node in reader.ChildNodes)
                        {
                            if (node.Name == "City")
                                node.InnerText = city.Text;
                            if (node.Name == "District")
                                node.InnerText = district.Text;
                            if (node.Name == "Street")
                                node.InnerText = street.Text;
                            if (node.Name == "No")
                                node.InnerText = no.Text;
                        }
                    }
                    if (reader.Name == "Phone")
                        reader.InnerText = phone.Text;
                    if (reader.Name == "Email")
                        reader.InnerText = email.Text;

                }

                doc.Save(Server.MapPath("../Data/Readers.xml"));
                Response.Write("<script>alert('修改成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意读者ID是否唯一或者其他非法输入');</script>");
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            loadContent();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Data/Readers.xml"));
            //XmlNode book = doc.SelectSingleNode("/xx:Books/xx:Book[@ISBN=" + isbn.Text + "]");
            XmlNode reader = doc.DocumentElement.ChildNodes[index];
            reader.ParentNode.RemoveChild(reader);
            doc.Save(Server.MapPath("../Data/Readers.xml"));
            Response.Write("<script>alert('删除成功');</script>");
            index = 0;
            readersNumber = 0;
            bind();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            loadContent();
            foreach (string idTemp in readerIdArr)
            {
                if (idTemp == readerId.Text)
                    isValid = false;
            }

            if (isValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../Data/Readers.xml"));
                // 创建 book 节点开始
                XmlNode readerNode = doc.CreateElement("Reader", doc.DocumentElement.NamespaceURI);

                XmlNode nameNode = doc.CreateElement("Name", doc.DocumentElement.NamespaceURI);
                nameNode.InnerText = name.Text;
                XmlNode statusNode = doc.CreateElement("Status", doc.DocumentElement.NamespaceURI);
                statusNode.InnerText = status.SelectedValue;

                XmlNode addressNode = doc.CreateElement("Address", doc.DocumentElement.NamespaceURI);
                XmlNode cityNode = doc.CreateElement("City", doc.DocumentElement.NamespaceURI);
                cityNode.InnerText = city.Text;
                XmlNode districtNode = doc.CreateElement("District", doc.DocumentElement.NamespaceURI);
                districtNode.InnerText = district.Text;
                XmlNode streetNode = doc.CreateElement("Street", doc.DocumentElement.NamespaceURI);
                streetNode.InnerText = street.Text;
                XmlNode noNode = doc.CreateElement("No", doc.DocumentElement.NamespaceURI);
                noNode.InnerText = no.Text;
                addressNode.AppendChild(cityNode);
                addressNode.AppendChild(districtNode);
                addressNode.AppendChild(streetNode);
                addressNode.AppendChild(noNode);

                XmlNode phoneNode = doc.CreateElement("Phone", doc.DocumentElement.NamespaceURI);
                phoneNode.InnerText = phone.Text;
                XmlNode emailNode = doc.CreateElement("Email", doc.DocumentElement.NamespaceURI);
                emailNode.InnerText = email.Text;

                readerNode.AppendChild(nameNode);
                readerNode.AppendChild(statusNode);
                readerNode.AppendChild(addressNode);
                readerNode.AppendChild(phoneNode);
                readerNode.AppendChild(emailNode);

                XmlAttribute idAttr = doc.CreateAttribute("ID");
                idAttr.InnerText = readerId.Text;
                readerNode.Attributes.Append(idAttr);
                // 创建 book 节点结束
                doc.DocumentElement.AppendChild(readerNode);
                doc.Save(Server.MapPath("../Data/Readers.xml"));
                Response.Write("<script>alert('添加成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('注意读者ID是否唯一或者其他非法输入');</script>");
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
            if (index < readersNumber - 1)
            {
                index++;
                contenAtIndex(index);
            }
        }

        private void contenAtIndex(int i)
        {
            readerId.Text = readerIdArr[i];
            name.Text = nameArr[i];
            status.Items.Clear();
            foreach (string serie in statusSet)
            {
                status.Items.Add(serie);
            }
            status.SelectedValue = statusArr[i];
            city.Text = cityArr[i];
            district.Text = districtArr[i];
            street.Text = streetArr[i];
            no.Text = noArr[i];
            phone.Text = phoneArr[i];
            email.Text = emailArr[i];
        }

        private void loadContent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Data/Readers.xml"));
            foreach (XmlNode readers in doc.DocumentElement.ChildNodes)
            {
                readerIdArr.Add(readers.Attributes["ID"].Value);

                foreach (XmlNode reader in readers.ChildNodes)
                {
                    if (reader.Name == "Name")
                        nameArr.Add(reader.InnerText);
                    if (reader.Name == "Status")
                    {
                        statusArr.Add(reader.InnerText);
                        if (!statusSet.Contains(reader.InnerText))
                        {
                            statusSet.Add(reader.InnerText);
                        }
                    }
                    if (reader.Name == "Address")
                    {
                        foreach(XmlNode node in reader.ChildNodes)
                        {
                            if (node.Name == "City")
                                cityArr.Add(node.InnerText);
                            if (node.Name == "District")
                                districtArr.Add(node.InnerText);
                            if (node.Name == "Street")
                                streetArr.Add(node.InnerText);
                            if (node.Name == "No")
                                noArr.Add(node.InnerText);
                        }
                    }
                    if (reader.Name == "Phone")
                        phoneArr.Add(reader.InnerText);
                    if (reader.Name == "Email")
                        emailArr.Add(reader.InnerText);
                }
                readersNumber++;
            }

        }
    }
}