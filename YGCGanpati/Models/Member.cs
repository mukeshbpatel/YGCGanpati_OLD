using System;   
using System.Collections.Generic;   
using System.Linq;   
using System.Text;   
using System.Threading.Tasks;   
using System.Xml.Serialization; //Add   
using System.Data;
using System.Web;

namespace YGCGanpati.Models
{   
      /// <summary>   
      /// This class is being serialized to XML.   
      /// </summary>   
      [Serializable]
      [XmlRoot("Members"), XmlType("Members")]
    public class Members   
      {
          public string Mobile { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string FlatNo { get; set; }
          public string Email { get; set; }
          public string Role { get; set; }

      } 
  
    public class XMLReader   
   {   
         /// <summary>   
         /// Return list of products from XML.   
         /// </summary>   
         /// <returns>List of products</returns>   
        public List<Members> GetListOfMembers()   
         {   
               string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/Members.xml");//Path of the xml script   
               DataSet ds = new DataSet();//Using dataset to read xml file   
               ds.ReadXml(xmlData);
               var members = new List<Members>();
               members = (from rows in ds.Tables[0].AsEnumerable()
                           select new Members   
               {   
                     Mobile = rows[0].ToString(), //Convert row to int   
                     FirstName = rows[1].ToString(),   
                     LastName = rows[2].ToString(),
                     FlatNo = rows[3].ToString(),
                     Email = rows[4].ToString(),
                     Role = rows[5].ToString(),   
               }).ToList();
               return members;   
         }   
      } 

}  
