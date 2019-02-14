using rpl_registrations.Models;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.SqlClient;
using Property.Models;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace RplWebsite.Controllers
{
    public class HomeController : Controller
    {//Constr
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
       
        public ActionResult index(string AlertMessage)
        {


            TrackUsers();
           
          
                if(Session["IsModalOpen"]!=null)
                {
                    var checkvalue = Session["IsModalOpen"].ToString();
                    if (Session["IsModalOpen"].ToString() == "Yes" || Session["IsModalOpen"].ToString() == "No")
                    {
                        Session["IsModalOpen"] = "No";
                    }
                   
                }
                else
                {
                    Session["IsModalOpen"] = "Yes";
                    //Session["SessionCount"] = userCount + 1;
                }
            ViewBag.AlertMessage = AlertMessage;
            return View();
        }

        public ActionResult gallery4()
        {

            return View();
        }
        public ActionResult gallery5()
        {

            return View();
        }
        
        public ActionResult about_us()
        {
            
            return View();
        }

        public ActionResult contact_us()
        {
            
            return View();
        }
        public ActionResult gallery2()
        {
            
            return View();
        }
        public ActionResult gallery6()
        {

            return View();
        }
        public ActionResult gallery3()
        {
            
            return View();
        }
        public ActionResult main_gallery()
        {           
            return View();
        }
        public ActionResult match_result()
        {   
            return View();
        }
        public ActionResult match_scedule()
        {
            
            return View();
        }
        public ActionResult opening_gallery()
        {
            
            return View();
        }
        public ActionResult points_table()
        {
            
            return View();
        }
        public ActionResult points_table_2()
        {

            return View();
        }
        public ActionResult team1()
        {
           
            return View();
        }
        public ActionResult team2()
        {
            
            return View();
        }
        public ActionResult team3()
        {
            
            return View();
        }
            public ActionResult team4()
        {
            
            return View();
        }
        public ActionResult team5()
        {
           
            return View();
        }
        public ActionResult team6()
        {
            
            return View();
        }
          public ActionResult team7()
        {
           
            return View();
        }
          public ActionResult team8()
        {
            

            return View();
        }
          public ActionResult team9()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult OfferForm()
        {
            return View();
        }

          [HttpPost]
          public ActionResult ContactUs(PersonModel person)
          {
              System.Threading.Thread.Sleep(2000);  /*simulating slow connection*/

              /*Do something with object person*/
              if (person != null)
              {
                 var status= SendEmail(person);
                  if(status=="success")
                  {
                      return Json(new { msg = "Successfully send email.Please check your email." });
                  }
                  else
                  {
                      return Json(new { msg = "unable to send email." });
                  }
                  

              }
              else
              {
                  return Json(new { msg = "model empty " });
              }


          }

        [HttpPost]
        public ActionResult RegisterForOffer(PersonModel person)
        {
            System.Threading.Thread.Sleep(2000);  /*simulating slow connection*/

            /*Do something with object person*/
            if (person != null)
            {
                var qry = "insert into [dbo].[RegisterForOffer] values('" + person.FirstName + "','" + person.Email + "','" + person.PhoneNo + "','"+ person .Brokerage+ "')";
                ExecuteNonQuery(qry);
                var status = SendEmail(person);
                if (status == "success")
                {
                    return Json(new { msg = "Successfully send email.Please check your email." });
                }
                else
                {
                    return Json(new { msg = "unable to send email." });
                }


            }
            else
            {
                return Json(new { msg = "model empty " });
            }


        }

        [HttpPost]
        public ActionResult Testimonial(PersonModel person, HttpPostedFileBase file)
        {

            if (person != null)
            {
                if(file!=null&& file.ContentLength>0)
                {
                    person.Image = Savefile(file);
                }
                
                var status = SendEmail(person);
                if (status == "success")
                {
                    return RedirectToAction("index", new { AlertMessage = "Mail send successfully" });

                }
                else
                {
                    return RedirectToAction("index", new { AlertMessage = "Mail not send successfully" });
                }


            }
            else
            {
                return RedirectToAction("index");
            }

        }

        public string SendEmail(PersonModel model)
          {
            var subject = "";
            if (model.Brokerage != "" && model.Brokerage != null && (model.Image == "" && model.Image == null))
            {
                subject = "Niagra Fall Offer";
            }
            else if (model.Image != "" && model.Image != null)
            {
                subject = "New Testimonial send by Client.";
            }
            else
            {
                subject = "Client contact mail.";
            }


            var EmailId = "rajpal@rplcanada.com";// "rajpal@rplcanada.com";
              var Status = "";
              try
              {
                  //Send mail
                  MailMessage mail = new MailMessage();
                  string FromEmailID = WebConfigurationManager.AppSettings["FromEmailID"];
                  string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                  SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
                  int _Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString());
                  Boolean _UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseDefaultCredentials"].ToString());
                  Boolean _EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"].ToString());
                  mail.To.Add(new MailAddress(EmailId));
                  mail.From = new MailAddress(FromEmailID);
                  mail.Subject = subject;

                  string msgbody = "";
                  msgbody += "<b>Name</b>    : " + model.FirstName + "<br>";
                  msgbody += "<b>Email</b>   : " + model.Email + "<br>";
                  msgbody += "<b>Phone No</b>: " + model.PhoneNo + "<br>";
                if(model.Brokerage!= "" && model.Brokerage != null &&( model.Image==null && model.Image==""))
                {
                    msgbody += "<b>Brokerage</b> : " + model.Brokerage + "<br>";
                }
                else
                if (model.Image != "" && model.Image != null)
                {
                    msgbody += "<b>Brokerage</b> : " + model.Brokerage + "<br>";
                    msgbody += "<b>Description</b> : " + model.Remarks + "<br>";
                    msgbody += "<b>ImagePath</b> : " + model.Image + "<br>";
                }
                else
                {
                    msgbody += "<b>Comments</b> : " + model.Remarks + "<br>";
                }

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                  mail.SubjectEncoding = System.Text.Encoding.UTF8;
                  System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(msgbody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                  System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msgbody, null, "text/html");

                  mail.AlternateViews.Add(plainView);
                  mail.AlternateViews.Add(htmlView);
                  // mail.Body = msgbody;
                  mail.IsBodyHtml = true;
                  SmtpClient smtp = new SmtpClient();
                  smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                  smtp.Host = "smtp.gmail.com"; //_Host;
                  smtp.Port = _Port;
                  //smtp.UseDefaultCredentials = _UseDefaultCredentials;
                  smtp.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);// Enter senders User name and password
                  smtp.EnableSsl = _EnableSsl;
                  smtp.Send(mail);
                  Status = "success";
              }
              catch
              {
                  Status = "fail";
              }

              return Status;
          }

        public bool TrackUsers()
        {
            bool result = false;

            var locationService = new GoogleMaps.LocationServices.GoogleLocationService();
            var ip = Request.ServerVariables["REMOTE_ADDR"]; ;
            IpInfo ipinfo = GetUserCountryByIp(ip);
            if (ipinfo.Loc != null)
            {

                string[] latlong = ipinfo.Loc.Split(',');
                ipinfo.Loc = GetAddressFromLatLong(Convert.ToDouble(latlong[0]), Convert.ToDouble(latlong[1]));

            }

            var Visitors = GetdataTable("select * from [dbo].[TrackRecord]");
            var IsFound= Visitors.AsEnumerable().Where(a => a["Ip"].ToString() == ipinfo.Ip);
            if(IsFound.Count()==0)
            {
                result = true;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertTrackRecord";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Ip", ipinfo.Ip == null ? "" : ipinfo.Ip);
                cmd.Parameters.AddWithValue("@Hostname", ipinfo.Hostname == null ? "" : ipinfo.Hostname);
                cmd.Parameters.AddWithValue("@City", ipinfo.City == null ? "" : ipinfo.City);
                cmd.Parameters.AddWithValue("@Region", ipinfo.Region == null ? "" : ipinfo.Region);
                cmd.Parameters.AddWithValue("@Country", ipinfo.Country == null ? "" : ipinfo.Country);
                cmd.Parameters.AddWithValue("@Loc", ipinfo.Loc == null ? "" : ipinfo.Loc);
                cmd.Parameters.AddWithValue("@Org", ipinfo.Org == null ? "" : ipinfo.Org);
                cmd.Parameters.AddWithValue("@Postal", ipinfo.Postal == null ? "" : ipinfo.Postal);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            return result;
            
        }
        public string GetAddressFromLatLong(double Latitute, double Longitute)
        {
            string Address = "";
            try
            {
                string url = "https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&Key={2}&sensor=false";
                url = string.Format(url, Latitute, Longitute, "AIzaSyDxw4-H4Y1ig9brfwS9Qwt8wCNE96ueVhE");
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);
                        Address = dsResult.Tables["result"].Rows[0]["formatted_address"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message.ToString();

            }
            return Address;
        }
        public static IpInfo GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo;
        }
        public DataTable GetdataTable(string qry)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter NewsLetter_Adp = new SqlDataAdapter(qry, con);
            NewsLetter_Adp.Fill(dt);

            return dt;

        }
        public string ExecuteNonQuery(string QStr)
        {
            string ErrorMessage = "";
            SqlCommand cmd = null;
            try
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd = new SqlCommand(QStr, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                ErrorMessage = "success";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    ErrorMessage = "FK";
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (con != null)
                {
                    con.Close();
                }

            }

            return ErrorMessage;
        }

        public string Savefile(HttpPostedFileBase file)
        {
            //Save the photo in Folder
            var fileExt = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid() + fileExt;
            var subPath = Server.MapPath("~/TestimonialImages");

            //Check SubPath Exist or Not
            if (!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }
            //End : Check SubPath Exist or Not

            var path = Path.Combine(subPath, fileName);
            file.SaveAs(path);

            return "http://rplcanada.com" + "/TestimonialImages/" + fileName;

        }

    }
    }

