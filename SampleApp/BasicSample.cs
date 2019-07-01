using System;
using System.Collections.Generic;
using YMSDK;
using YMSDK.Providers;

namespace SampleApp
{
    public class BasicSample
    {
        public void GetMemberInbox()
        {
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
            response = manager.AuthAuthenticate("vijaysaini", "priyank1");
            //response = manager.AuthAuthenticate("priyankmittal", "priyank1");

            if (response.ErrorCode == YMSDK.ApiErrorCode.NoError)
            {
                //Optional, store the ID or MemberID of the Authenticated User for later use
                string memberGuid = response.MethodResults.GetNamedItem("ID").Value;
                string memberID = response.MethodResults.GetNamedItem("WebsiteID").Value;

                //Get Inbox messages for this user
                //response = manager.MemberProfileGet();
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH mm ss");
                //2008 - 01 - 01 00:00:00
                response = manager.SaPeopleAllGetIDs(DateTime.Now, Convert.ToInt32(memberID));
                if (response.ErrorCode == YMSDK.ApiErrorCode.NoError)
                {
                    //Display the messages to the user
                    ApiMethodResults results = response.MethodResults;

                    if (results.Items.Count > 0)
                    {
                        foreach (DataItem message in results.Items)
                        {
                            messageHeaders.Add(
                                message.GetNamedItem("MessageID").Value,
                                message.GetNamedItem("Subject").Value
                            );
                        }
                    }
                }
            }

            //Console.WriteLine(string.Format("Fetched {0} messages.", messageHeaders.Count));
            Console.WriteLine(response);
            Console.ReadLine();
        }

    }
}