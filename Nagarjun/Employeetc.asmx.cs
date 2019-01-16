using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Xml;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;



namespace Nagarjun
{
    /// <summary>
    /// Summary description for Employeetc
    /// </summary>
     [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Employeetc : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,
        UseHttpGet = false, XmlSerializeString = false)]
        public String insertdata(String uName, String ugender, String uemail, String uphonenumber)
        {
            String reply = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("~/contact.xml"));
            XmlElement ParentElement = xmldoc.CreateElement("Contact");
            XmlElement lID = xmldoc.CreateElement("ID");
            lID.InnerText = "asas";
            XmlElement luname = xmldoc.CreateElement("Name");
            luname.InnerText = uName;
            XmlElement lfname = xmldoc.CreateElement("FatherName");
            lfname.InnerText = ugender;
            XmlElement ldob = xmldoc.CreateElement("dob");
            ldob.InnerText = "asas";
            XmlElement lmobile = xmldoc.CreateElement("mobile");
            lmobile.InnerText = "";
            XmlElement address = xmldoc.CreateElement("addrss");
            address.InnerText = "";
            ParentElement.AppendChild(lID);
            ParentElement.AppendChild(luname);
            ParentElement.AppendChild(lfname);
            ParentElement.AppendChild(ldob);
            ParentElement.AppendChild(lmobile);
            ParentElement.AppendChild(address);
            xmldoc.DocumentElement.AppendChild(ParentElement);
            xmldoc.Save(Server.MapPath("~/contact.xml"));
            return reply;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,
        UseHttpGet = false, XmlSerializeString = false)]
        public List<Record> getdata()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            String path = Server.MapPath(@"contact.xml");
            ds.ReadXml(path);
            dt = ds.Tables[0];
            List<Record> _records = new List<Record>();            
            for(Int32 i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                Record rec = new Record();
                rec.Sno = Convert.ToInt32(dt.Rows[i]["Sno"]);
                rec.Name = dt.Rows[i]["Name"].ToString();
                rec.Gender = dt.Rows[i]["Gender"].ToString();
                rec.Emailaddress = dt.Rows[i]["Emailaddress"].ToString();
                rec.Phonenumber = dt.Rows[i]["Phonenumber"].ToString();
                _records.Add(rec);   
            }
            return _records;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public List<Student> GetusersList(Int32 _dc, Int32 page, Int32 start, Int32 limit)
        {
            List<Student> obList = new List<Student>();
            Student ud = new Student();
            ud.Sno = 1;
            ud.FName = "John";
            ud.LName = "Doe";
            obList.Add(ud);
            ud.Sno = 2;
            ud.FName = "John";
            ud.LName = "Doe";
            obList.Add(ud);
            ud.Sno = 3;
            ud.FName = "Sodhi";
            ud.LName = "Doe";
            obList.Add(ud);
            return obList;
        }

        public class Student
        {
            public int Sno;
            public string FName;
            public string LName;            
        }
        
        public class Record
        {
            public int Sno;
            public string Name;
            public string Gender;
            public string Emailaddress;
            public string Phonenumber;            
        }

        public class Record2
        {
            public string FirstName;
            public string LastName;
            public string EmailAddress;
            public int Salary;
        }
        List<Record2> _record2s = new List<Record2>()
        {  
                    new Record2 { FirstName = "Palash", LastName = "Debnath", EmailAddress = "palash@yahoo.com", Salary = 100},  
                    new Record2 { FirstName = "Pritam", LastName = "Debnath", EmailAddress = "pritam@yahoo.com", Salary = 200}
        };

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet = false, XmlSerializeString = false)]
        public List<Record2> LoadRecords()
        {
            return _record2s;
        }


    }
}
