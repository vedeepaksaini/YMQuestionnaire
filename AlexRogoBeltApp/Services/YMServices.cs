using AlexRogoBeltApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMSDK;
using YMSDK.Entities;
using YMSDK.Providers;

namespace AlexRogoBeltApp.Services
{
    public class YMServices
    {
        private readonly TOCICOEntities db = new TOCICOEntities();

        public List<int> GetAllGuid()
        {

            List<int> objGuids = new List<int>();
            Dictionary<string, string> messageHeaders = new Dictionary<string, string>();

            XmlHttpProvider provider = new XmlHttpProvider("https://api.yourmembership.com/");

            YMSDK.ApiManager manager = new YMSDK.ApiManager(provider);
            manager.ApiKeyPublic = "9FB80E52-49C5-4B31-9AE6-08D83F065897";
            manager.ApiKeySa = "B4226439-485F-49EF-A183-37319519A0FA";
            manager.SaPasscode = "04iHL9sU5g24";
            manager.Version = "2.30";

            //Create session for the current user
            YMSDK.ApiResponse response = manager.SessionCreate();

            //Authenticate the user for the current session
            // response = manager.AuthAuthenticate("vijaysaini", "priyank1");
            response = manager.SaPeopleAllGetIDs(null, null);

            if (response.ErrorCode == YMSDK.ApiErrorCode.NoError)
            {
                //Optional, store the ID or MemberID of the Authenticated User for later use
                string memberGuid = response.MethodResults.GetNamedItem("ID").Value;
                string memberID = response.MethodResults.GetNamedItem("WebsiteID").Value;

                var MemberGuid = (from items in response.MethodResults.Items[2].Items select items)
                    .Select(x => new GuidMaster()
                    {
                        GUID = x.Value,
                        CreatedON = DateTime.Now,
                        UpdateOn = DateTime.Now
                    }).ToList();

                db.GuidMasters.AddRange(MemberGuid);
                db.SaveChanges();
            }
            return objGuids;
        }

        //public void GetMemberData()
        //{
        //    var MemberGuid = db.GuidMasters.ToList();
        //    foreach (var item in MemberGuid)
        //    {
        //        List<int> objGuids = new List<int>();
        //        Dictionary<string, string> messageHeaders = new Dictionary<string, string>();
        //        MemberProfile profile;
        //        XmlHttpProvider provider = new XmlHttpProvider("https://api.yourmembership.com/");

        //        YMSDK.ApiManager manager = new YMSDK.ApiManager(provider);
        //        manager.ApiKeyPublic = "9FB80E52-49C5-4B31-9AE6-08D83F065897";
        //        manager.ApiKeySa = "B4226439-485F-49EF-A183-37319519A0FA";
        //        manager.SaPasscode = "04iHL9sU5g24";
        //        manager.Version = "2.30";

        //        //Create session for the current user
        //        YMSDK.ApiResponse response = manager.SessionCreate();

        //        //Authenticate the user for the current session
        //        // response = manager.AuthAuthenticate("vijaysaini", "priyank1");

        //        response = manager.SaPeopleProfileGet(item.GUID);

        //        if (response.ErrorCode == ApiErrorCode.NoError)
        //        {
        //            //Get person profile
        //            profile = response.MethodResults.ConvertTo<MemberProfile>();
        //        }

        //    }
        //}
        public void GetYMMemberProfile()
        {


        }

        public void GetMemberData()
        {
            try
            {
                var MemberGuid = db.GuidMasters.ToList();
                foreach (var item in MemberGuid)
                {
                    List<int> objGuids = new List<int>();
                    Dictionary<string, string> messageHeaders = new Dictionary<string, string>();
                    MemberProfile profile;
                    //List<MemberMaster> memberData = new List<MemberMaster>();
                    XmlHttpProvider provider = new XmlHttpProvider("https://api.yourmembership.com/");

                    YMSDK.ApiManager manager = new YMSDK.ApiManager(provider);
                    manager.ApiKeyPublic = "9FB80E52-49C5-4B31-9AE6-08D83F065897";
                    manager.ApiKeySa = "B4226439-485F-49EF-A183-37319519A0FA";
                    manager.SaPasscode = "04iHL9sU5g24";
                    manager.Version = "2.30";

                    //Create session for the current user
                    YMSDK.ApiResponse response = manager.SessionCreate();

                    //Authenticate the user for the current session
                    // response = manager.AuthAuthenticate("vijaysaini", "priyank1");

                    response = manager.SaPeopleProfileGet(item.GUID);

                    if (response.ErrorCode == ApiErrorCode.NoError)
                    {

                        //Get person profile
                        profile = response.MethodResults.ConvertTo<MemberProfile>();
                        MemberMaster member = new MemberMaster();
                        member.WebsiteID = Convert.ToInt32(profile.WebsiteID);
                        member.GUID = profile.ID;
                        member.MemberUserName = profile.Username;
                        member.Email = profile.EmailAddr;
                        member.FirstName = profile.FirstName;
                        member.LastName = profile.LastName;
                        member.Company = profile.Employer;
                        member.MemberType = profile.EmpPhAreaCode;
                        member.HomeAddress1 = profile.EmpAddrLines;
                        member.City = profile.EmpCity;
                        member.Location = profile.EmpLocation;
                        member.Country = profile.EmpCountry;
                        member.Phone = profile.EmpPhone;
                        member.EmployeeName = profile.FirstName;
                        member.MemberSuspended = profile.Membership;
                        member.IsYBpaymentCompleted = false;
                        member.IsYBStepsCompleted = false;
                        member.MemberID = Convert.ToInt32(profile.WebsiteID);
                        // memberData.Add(member);
                        db.MemberMasters.Add(member);
                        db.SaveChanges();




                    }

                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
    }
}