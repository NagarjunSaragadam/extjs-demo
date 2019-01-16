using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Net;
using Newtonsoft.Json;
using System.Web.Http;
using System.Xml.Linq;


namespace webapi.Controllers
{
    public class HomeController : ApiController
    {  

        public HttpResponseMessage getdata()
        {
            string path = HostingEnvironment.MapPath("~/Employee.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);            
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            string jsonText = JsonConvert.SerializeXmlNode(doc);          
            String replace="\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"utf-8\"},";
            response.Content = new StringContent(jsonText.Replace(replace, ""), Encoding.UTF8, "application/json");
            return response;
        }


        public HttpResponseMessage getraw()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string path = HostingEnvironment.MapPath("~/Employee.xml");
            ds.ReadXml(path);
            dt = ds.Tables[0];
            List<Employee> _records = new List<Employee>();
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Employee rec = new Employee();
                rec.Sno = dt.Rows[i]["Sno"].ToString();
                rec.Username = dt.Rows[i]["Name"].ToString();
                rec.Gender = dt.Rows[i]["Gender"].ToString();
                rec.Email = dt.Rows[i]["Emailaddress"].ToString();
                rec.Phonenumber = dt.Rows[i]["Phonenumber"].ToString();
                _records.Add(rec);
            }
            var json = JsonConvert.SerializeObject(new
            {
                operations = _records
            });
            String result = json.ToString();
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            String replace="{\"operations\":[";
            response.Content = new StringContent("["+result.Replace(replace, "").Replace("]}","")+"]", Encoding.UTF8, "application/json");
            return response;
        }



        public HttpResponseMessage insertdata(String uName, String ugender, String uemail, String uphonenumber)
        {
            String reply = "";
            Random rnd = new Random();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HostingEnvironment.MapPath("~/Employee.xml"));
            XmlElement ParentElement = xmldoc.CreateElement("Employee");
            XmlElement lID = xmldoc.CreateElement("Sno");
            lID.InnerText = rnd.Next(1,1000).ToString();

            XmlElement luname = xmldoc.CreateElement("Name");
            luname.InnerText = uName;

            XmlElement lgender = xmldoc.CreateElement("Gender");
            lgender.InnerText = ugender; ;

            XmlElement lemail = xmldoc.CreateElement("Emailaddress");
            lemail.InnerText = uemail; ;

            XmlElement lmobile = xmldoc.CreateElement("Phonenumber");
            lmobile.InnerText = uphonenumber; ;

            ParentElement.AppendChild(lID);
            ParentElement.AppendChild(luname);
            ParentElement.AppendChild(lgender);
            ParentElement.AppendChild(lemail);
            ParentElement.AppendChild(lmobile);            
            xmldoc.DocumentElement.AppendChild(ParentElement);
            xmldoc.Save(HostingEnvironment.MapPath("~/Employee.xml"));
            String result = "Success";
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            String replace = "";
            response.Content = new StringContent("[" + result.Replace(replace, "").Replace("]}", "") + "]", Encoding.UTF8, "application/json");
            return response;
        }

        public String transctionsdatabypost(Employee emp)
        {
            if (emp.Openumber == "0")
                return deletedatabypost(emp.Sno);

            else if (emp.Openumber == "1")
                return insertdatabypost(emp);

            else if (emp.Openumber == "2")
                return updatedatabypost(emp);

            else return "No Operation Done";
        }

        public string updatedatabypost(Employee emp)
        {
            int xmlRow=0;
            DataSet ds = new DataSet();
            ds.ReadXml(HostingEnvironment.MapPath("~/Employee.xml"));
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Sno"].ToString() == emp.Sno)
                {
                    xmlRow = i;
                    break;
                }
            }
            ds.Tables[0].Rows[xmlRow]["Name"] = emp.Username;
            ds.Tables[0].Rows[xmlRow]["Gender"] = emp.Gender;
            ds.Tables[0].Rows[xmlRow]["Emailaddress"] = emp.Email;
            ds.Tables[0].Rows[xmlRow]["Phonenumber"] = emp.Phonenumber;            
            ds.WriteXml(HostingEnvironment.MapPath("~/Employee.xml"));
            String result = "Success";
            return result;
        }

        public string insertdatabypost(Employee emp)
        {
            Random rnd = new Random();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HostingEnvironment.MapPath("~/Employee.xml"));
            XmlElement ParentElement = xmldoc.CreateElement("Employee");
            XmlElement lID = xmldoc.CreateElement("Sno");
            lID.InnerText = rnd.Next(1, 1000).ToString();

            XmlElement luname = xmldoc.CreateElement("Name");
            luname.InnerText = emp.Username;

            XmlElement lgender = xmldoc.CreateElement("Gender");
            lgender.InnerText = emp.Gender; ;

            XmlElement lemail = xmldoc.CreateElement("Emailaddress");
            lemail.InnerText = emp.Email; ;

            XmlElement lmobile = xmldoc.CreateElement("Phonenumber");
            lmobile.InnerText = emp.Phonenumber;

            ParentElement.AppendChild(lID);
            ParentElement.AppendChild(luname);
            ParentElement.AppendChild(lgender);
            ParentElement.AppendChild(lemail);
            ParentElement.AppendChild(lmobile);
            xmldoc.DocumentElement.AppendChild(ParentElement);
            xmldoc.Save(HostingEnvironment.MapPath("~/Employee.xml"));
            String result = "Success";
            return result;
        }

        public String deletedatabypost(String Sno)
        {            
            DataSet ds = new DataSet();
            ds.ReadXml(HostingEnvironment.MapPath("~/Employee.xml"));
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Sno"].ToString() == Sno)
                {
                    ds.Tables[0].Rows.RemoveAt(i);
                    break;
                }
            }   
            ds.WriteXml(HostingEnvironment.MapPath("~/Employee.xml"));
            String result = "Success";            
            return result;
        }

        
        public class Employee
        {
            private string sno;

            public string Sno
            {
                get { return sno; }
                set { sno = value; }
            }
            private string username;

            public string Username
            {
                get { return username; }
                set { username = value; }
            }

            private string gender;

            public string Gender
            {
                get { return gender; }
                set { gender = value; }
            }
            private string email;

            public string Email
            {
                get { return email; }
                set { email = value; }
            }
            private string phonenumber;

            public string Phonenumber
            {
                get { return phonenumber; }
                set { phonenumber = value; }
            }
            private string openumber;

            public string Openumber
            {
                get { return openumber; }
                set { openumber = value; }
            }
        }
    }
}
