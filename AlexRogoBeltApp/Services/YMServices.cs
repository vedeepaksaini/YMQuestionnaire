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

        public void GetMemberData()
        {
            var MemberGuid = db.GuidMasters.ToList();
            foreach (var item in MemberGuid)
            {
                List<int> objGuids = new List<int>();
                Dictionary<string, string> messageHeaders = new Dictionary<string, string>();
                MemberProfile profile;
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
                }

            }
        }
        public void GetYMMemberProfile()
        {


        }
    }
}