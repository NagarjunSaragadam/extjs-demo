using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Web;
using System.Web.Mvc;

namespace webapi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        public class Employee
        {
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
        }
      
    }
}
