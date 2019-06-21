using System;
using System.Collections.Generic;
using System.Linq;
using YMSDK;
using YMSDK.Providers;
using YMSDK.Entities;

namespace SampleApp
{
	class ImplementationSample
	{
		//TODO: Update constants here with your values.
		private const string _ApiKeyPublic = "TBD";
		private const string _ApiKeySA = "TBD";
		private const string _ApiSAPasscode = "TBD";
		private const string _ApiEndpoint = "https://api.yourmembership.com/";
		private const string _ApiVersion = "2.30";
		private const string _ApiCallOrigin = "YMSDK Test App";

		XmlHttpProvider provider;
		ApiManager manager;

		private bool ApiSessionInitialized = false;

		/******************************************************************************************
		*   SyncMemberData - Sample API Implementation
		*  
		*  Description: This sample demonstrates a basic framework for developing an application
		*  that interacts with the YourMembership.com API system in real-time by way of the YMSDK.
		*  
		*  The SDK provides all of the functionality needed to compose API calls, receive responses
		*  and parse return data using standard .NET application logic.
		********************************************************************************************/
		public void SyncMemberData()
		{
			InitializeProvider();

			InitializeAPISession();

			UpdateMembers();

			AbandonSession();
		}

		#region UpdateMembers method

		public void UpdateMembers()
		{
			string internalValue = string.Empty;
			string guid;
			CustomFieldResponse testCustomField;
			string strCustomFieldCode = "testcustomfield";

			//Get a few Member IDs (added since the first of this year)
			ApiResponse response = manager.SaPeopleAllGetIDs(new DateTime(DateTime.Now.Year, 1, 1), null);

			if (response.ErrorCode != ApiErrorCode.NoError)
			{
				//Error fetching IDs. Check your configuration and try again.
				Console.WriteLine("Unable to fetch MemberIDs. Check your configuration values and try again.");
				return;
			}

			List<DataItem> peopleIDs = response.MethodResults.GetNamedItem("People").Items;
			int peopleIDCount = peopleIDs.Count;
			MemberProfile profile;
			List<CustomFieldResponse> customFields;

			if (peopleIDs.Count > 50)
			{
				//Take X people off the top (rather than process them all and hog resources)
				peopleIDs = peopleIDs.Take(50).ToList();
			}

			foreach (DataItem item in peopleIDs)
			{
				internalValue = string.Empty;
				guid = item.Value;

				Console.WriteLine("Processing member guid: " + guid);

				//Get Profile information for this Person
				response = manager.SaPeopleProfileGet(guid);

				if (response.ErrorCode == ApiErrorCode.NoError)
				{
					//Get person profile
					profile = response.MethodResults.ConvertTo<MemberProfile>();

					//Get an internal value for this member (some arbirary data from Client system)
					internalValue = GetInternalValueForMember(profile.ConstituentID);

					//Make sure we have an internal value to update with
					if (!string.IsNullOrEmpty(internalValue))
					{
						//From there, get CustomFieldResponses
						customFields = profile.CustomFieldResponses;

						//Optional: update standard profile fields here
						profile.LastUpdated = DateTime.Now;

						//Check for existence of a given custom field response, by FieldCode
						testCustomField = customFields.SingleOrDefault(x => x.FieldCode != null && x.FieldCode.Equals(strCustomFieldCode));

						if (testCustomField == null)
						{
							//User has no response for this field....create one
							testCustomField = new CustomFieldResponse();
							testCustomField.FieldCode = strCustomFieldCode;
							testCustomField.Values.Add(internalValue);
							customFields.Add(testCustomField);
						}
						else
						{
							//User already has a value in this field...update it
							testCustomField.Values[0] = internalValue;
						}

						//Update this person profile
						response = manager.SaPeopleProfileUpdate(profile.Serialize());

						if (response.ErrorCode != ApiErrorCode.NoError)
						{
							Console.WriteLine(string.Format("ERROR: SaPeopleProfileUpdate call failed. Details: {0} - {1}", response.ErrorCode, response.ErrorMessage));
						}

					}
				}
			}
		}

		#endregion

		//Example method which should be replaced with your functionality for retrieving data for a given member.
		private string GetInternalValueForMemberGuid(string Guid)
		{
			DateTime now = DateTime.Now;
			return string.Format("Custom1Ü: {0} {1}", now.ToShortDateString(), now.ToShortTimeString());
		}

		private string GetInternalValueForMember(string InternalID)
		{
			DateTime now = DateTime.Now;
			return string.Format("Custom1Ü: {0} {1}", now.ToShortDateString(), now.ToShortTimeString());
		}

		#region API Helper Methods

		private void InitializeProvider()
		{
			provider = new XmlHttpProvider(_ApiEndpoint);
			manager = new ApiManager(provider);

			manager.Version = _ApiVersion;
			manager.ApiKeyPublic = _ApiKeyPublic;
			manager.ApiKeySa = _ApiKeySA;
			manager.SaPasscode = _ApiSAPasscode;
			manager.CallOrigin = _ApiCallOrigin;
		}

		private void InitializeAPISession()
		{
			if (!ApiSessionInitialized)
			{
				ApiResponse response = manager.SessionCreate();
				ApiSessionInitialized = true;
			}
		}

		private void AbandonSession()
		{
			ApiResponse response = manager.SessionAbandon();
			ApiSessionInitialized = false;
		}

		#endregion
	}
}
