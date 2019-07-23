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

        public string GetAllGuid()
        {
            //XmlHttpProvider provider = new XmlHttpProvider("https://api.yourmembership.com/");

            //YMSDK.ApiManager manager = new YMSDK.ApiManager(provider);
            //manager.ApiKeyPublic = "9FB80E52-49C5-4B31-9AE6-08D83F065897";
            //manager.ApiKeySa = "B4226439-485F-49EF-A183-37319519A0FA";
            //manager.SaPasscode = "04iHL9sU5g24";
            //manager.Version = "2.30";

            ////Create session for the current user
            //YMSDK.ApiResponse response = manager.SessionCreate();

            ////Authenticate the user for the current session
            //response = manager.AuthAuthenticate("vijaysaini", "priyank1");

            //response = manager.SaPeopleAllGetIDs(null, null);

            //if (response.ErrorCode == YMSDK.ApiErrorCode.NoError)
            //{
            //    IEnumerable<string> apiGuids = (from items in response.MethodResults.Items[2].Items select items).Select(x => x.Value);
            //    IEnumerable<string> localGuid = db.GuidMasters.Select(x => x.GUID);
            //    apiGuids = apiGuids.Except(localGuid).ToList();

            //    var MemberGuid = apiGuids.Select(x => new GuidMaster()
            //    {
            //        GUID = x,
            //        CreatedON = DateTime.Now,
            //        UpdateOn = DateTime.Now
            //    }).ToList();

            //    db.GuidMasters.AddRange(MemberGuid);
            //    db.SaveChanges();

            //    GetMemberData(response, manager, MemberGuid.Select(x => x.GUID).ToList());
           // }
            return "Data updated successfully";
        }

        public void GetMemberData(YMSDK.ApiResponse response, YMSDK.ApiManager manager, IEnumerable<string> newGuids)
        {
            MemberProfile profile = null;
            List<MemberMaster> newMembers = new List<MemberMaster>();

            foreach (var item in newGuids)
            {
                response = manager.SaPeopleProfileGet(item);

                if (response.ErrorCode == ApiErrorCode.NoError)
                {
                    //Get person profile
                    profile = response.MethodResults.ConvertTo<MemberProfile>();
                    newMembers.Add(new MemberMaster()
                    {
                        MemberID = Convert.ToInt32(profile.WebsiteID),
                        WebsiteID = Convert.ToInt32(profile.WebsiteID),
                        GUID = profile.ID,
                        MemberUserName = profile.Username,
                        Email = profile.EmailAddr,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Company = profile.Employer,
                        MemberType = profile.EmpPhAreaCode,
                        HomeAddress1 = profile.EmpAddrLines,
                        City = profile.EmpCity,
                        Location = profile.EmpLocation,
                        Country = profile.EmpCountry,
                        Phone = profile.EmpPhone,
                        EmployeeName = profile.FirstName,
                        MemberSuspended = profile.Membership,
                        IsYBpaymentCompleted = false,
                        IsYBStepsCompleted = false,
                        IsGBpaymentCompleted = false,
                        IsGBStepsCompleted=false,
                        IsBBpaymentCompleted=false,
                        IsBBStepsCompleted=false,
                        IsTOIpaymentCompleted=false,
                        IsTOIStepsCompleted=false
                    });
                }
            }
            db.MemberMasters.AddRange(newMembers);
            db.SaveChanges();
        }
    }


    //public class YMServices
    //{
    //    private readonly TOCICOEntities db = new TOCICOEntities();

    //    

    //    public void GetMemberData()
    //    {
    //        try
    //        {
    //            var MemberGuid = db.GuidMasters.ToList();
    //            foreach (var item in MemberGuid)
    //            {
    //                List<int> objGuids = new List<int>();
    //                Dictionary<string, string> messageHeaders = new Dictionary<string, string>();
    //                MemberProfile profile;

    //                XmlHttpProvider provider = new XmlHttpProvider("https://api.yourmembership.com/");

    //                YMSDK.ApiManager manager = new YMSDK.ApiManager(provider);
    //                manager.ApiKeyPublic = "9FB80E52-49C5-4B31-9AE6-08D83F065897";
    //                manager.ApiKeySa = "B4226439-485F-49EF-A183-37319519A0FA";
    //                manager.SaPasscode = "04iHL9sU5g24";
    //                manager.Version = "2.30";

    //                //Create session for the current user
    //                YMSDK.ApiResponse response = manager.SessionCreate();

    //                //Authenticate the user for the current session
    //                // response = manager.AuthAuthenticate("vijaysaini", "priyank1");

    //                response = manager.SaPeopleProfileGet(item.GUID);

    //                if (response.ErrorCode == ApiErrorCode.NoError)
    //                {

    //                    //Get person profile
    //                    profile = response.MethodResults.ConvertTo<MemberProfile>();
    //                    MemberMaster member = new MemberMaster();
    //                    member.WebsiteID = Convert.ToInt32(profile.WebsiteID);
    //                    member.GUID = profile.ID;
    //                    member.MemberUserName = profile.Username;
    //                    member.Email = profile.EmailAddr;
    //                    member.FirstName = profile.FirstName;
    //                    member.LastName = profile.LastName;
    //                    member.Company = profile.Employer;
    //                    member.MemberType = profile.EmpPhAreaCode;
    //                    member.HomeAddress1 = profile.EmpAddrLines;
    //                    member.City = profile.EmpCity;
    //                    member.Location = profile.EmpLocation;
    //                    member.Country = profile.EmpCountry;
    //                    member.Phone = profile.EmpPhone;
    //                    member.EmployeeName = profile.FirstName;
    //                    member.MemberSuspended = profile.Membership;
    //                    member.IsYBpaymentCompleted = false;
    //                    member.IsYBStepsCompleted = false;
    //                    member.MemberID = Convert.ToInt32(profile.WebsiteID);
    //                    // memberData.Add(member);
    //                    db.MemberMasters.Add(member);
    //                    db.SaveChanges();

    //                }

    //            }
    //        }
    //        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
    //        {
    //            Exception raise = dbEx;
    //            foreach (var validationErrors in dbEx.EntityValidationErrors)
    //            {
    //                foreach (var validationError in validationErrors.ValidationErrors)
    //                {
    //                    string message = string.Format("{0}:{1}",
    //                        validationErrors.Entry.Entity.ToString(),
    //                        validationError.ErrorMessage);
    //                    // raise a new exception nesting  
    //                    // the current instance as InnerException  
    //                    raise = new InvalidOperationException(message, raise);
    //                }
    //            }
    //            throw raise;
    //        }

    //    }

    //    public List<int> GetAllGuidFromTable()
    //    {
    //        var objLocalGuids = (from guid in db.GuidMasters select guid).ToList();
    //        return objLocalGuids;
    //    }
    //}
}