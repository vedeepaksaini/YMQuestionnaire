using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools = YMSDK.Utilities.Toolkit;
using YMSDK.Utilities;

namespace YMSDK.Entities
{
	#region Feed

	public class Feed : Interfaces.IApiEntity
	{
		public Feed()
		{

		}

		public DataItem Serialize()
		{
			//Note: Read-only entities do not need to implement Serialize
			throw new System.NotImplementedException();
		}

		public void Deserialize(DataItem Source)
		{
			this.FeedID = Source["FeedID"].Value;
			this.Name = Source["Name"].Value;
			this.LastUpdated = Source["LastUpdated"].AsDateTime();
		}

		public string FeedID { get; set; }
		public string Name { get; set; }
		public DateTime LastUpdated { get; set; }
	}

	#endregion

	#region Group

	public class Group : Interfaces.IApiEntity
	{
		public Group()
		{}

		public DataItem Serialize()
		{
			DataItem group = new DataItem();

			if (this.Accessibility.HasValue) group.Items.Add(new DataItem("Accessibility", this.Accessibility.ToString()));
			if (this.Active.HasValue) group.Items.Add(new DataItem("Active", this.Active.ValueOrNull()));
			if (this.AdminCanExportMembers.HasValue) group.Items.Add(new DataItem("AdminCanExportMembers", this.AdminCanExportMembers.ValueOrNull()));
			if (this.EmailApproval.HasValue) group.Items.Add(new DataItem("EmailApproval", this.EmailApproval.ToString()));
			if (this.EnableFeed.HasValue) group.Items.Add(new DataItem("EnableFeed", this.EnableFeed.ValueOrNull()));
			if (this.GroupCode != null) group.Items.Add(new DataItem("GroupCode", this.GroupCode));
			if (this.GroupID.HasValue) group.Items.Add(new DataItem("GroupID", this.GroupID.ToString()));
			if (this.Hidden.HasValue) group.Items.Add(new DataItem("Hidden", this.Hidden.ValueOrNull()));
			if (this.Membership.HasValue) group.Items.Add(new DataItem("Membership", this.Membership.ToString()));
			if (this.Messaging.HasValue) group.Items.Add(new DataItem("Messaging", this.Messaging.ToString()));
			if (this.Name != null) group.Items.Add(new DataItem("Name", this.Name));
			if (this.PhotoApproval.HasValue) group.Items.Add(new DataItem("PhotoApproval", this.PhotoApproval.ValueOrNull()));
			if (this.SendNewsletter.HasValue) group.Items.Add(new DataItem("SendNewsletter", this.SendNewsletter.ValueOrNull()));
			if (this.ShortDescription != null) group.Items.Add(new DataItem("ShortDescription", this.ShortDescription));
			if (this.TypeID.HasValue) group.Items.Add(new DataItem("TypeID", this.TypeID.ToString()));
			if (this.WelcomeContent != null) group.Items.Add(new DataItem("WelcomeContent", this.WelcomeContent));

			return group;
		}

		public void Deserialize(DataItem Source)
		{
			this.Accessibility = Source["Accessibility"].AsInt32();
			this.Active = Source["Active"].AsBoolean();
			this.AdminCanExportMembers = Source["AdminCanExportMembers"].AsBoolean();
			this.EmailApproval = Source["EmailApproval"].AsInt32();
			this.EnableFeed = Source["EnableFeed"].AsBoolean();
			this.GroupCode = Source["GroupCode"].Value;
			this.GroupID = Source["GroupID"].AsInt32();
			this.Hidden = Source["Hidden"].AsBoolean();
			this.Membership = Source["Membership"].AsInt32();
			this.Messaging = Source["Messaging"].AsInt32();
			this.Name = Source["Name"].Value;
			this.PhotoApproval = Source["PhotoApproval"].AsBoolean();
			this.SendNewsletter = Source["SendNewsletter"].AsBoolean();
			this.ShortDescription = Source["ShortDescription"].Value;
			this.TypeID = Source["TypeID"].AsInt32();
			this.WelcomeContent = Source["WelcomeContent"].Value;
		}

		public int? Accessibility { get; set; }
		public bool? Active { get; set; }
		public bool? AdminCanExportMembers { get; set; }
		public int? EmailApproval { get; set; }
		public bool? EnableFeed { get; set; }
		public string GroupCode { get; set; }
		public int? GroupID { get; set; }
		public bool? Hidden { get; set; }
		public int? Membership { get; set; }
		public int? Messaging { get; set; }
		public string Name { get; set; }
		public bool? PhotoApproval { get; set; }
		public bool? SendNewsletter { get; set; }
		public string ShortDescription { get; set; }
		public int? TypeID { get; set; }
		public string WelcomeContent { get; set; }
	}

	#endregion

	#region InvoicePayment

	public class InvoicePayment : Interfaces.IApiEntity
	{
		public InvoicePayment()
		{}

		public DataItem Serialize()
		{
			DataItem payment = new DataItem();

			payment.Items.Add(new DataItem("InvoiceNo", this.InvoiceNo.ToString()));
			payment.Items.Add(new DataItem("Amount", this.Amount.ToString()));
			payment.Items.Add(new DataItem("Type", this.Type));
			payment.Items.Add(new DataItem("RefNo", this.RefNo));
			payment.Items.Add(new DataItem("PaymentDate", this.PaymentDate.DateOrNull()));
			payment.Items.Add(new DataItem("PayerName", this.PayerName));
			payment.Items.Add(new DataItem("PayerEmail", this.PayerEmail));
			payment.Items.Add(new DataItem("BillOrganization", this.BillOrganization));
			payment.Items.Add(new DataItem("UpdateInvoiceInfo", this.UpdateInvoiceInfo.ValueOrNull()));
			payment.Items.Add(new DataItem("AutoCloseInvoice", this.UpdateInvoiceInfo.ValueOrNull()));
			payment.Items.Add(new DataItem("UseInvoiceAddress", this.UpdateInvoiceInfo.ValueOrNull()));
			payment.Items.Add(new DataItem("BillAddress1", this.BillAddress1));
			payment.Items.Add(new DataItem("BillAddress2", this.BillAddress2));
			payment.Items.Add(new DataItem("BillCity", this.BillCity));
			payment.Items.Add(new DataItem("BillLocation", this.BillLocation));
			payment.Items.Add(new DataItem("BillPostalCode", this.BillPostalCode));
			payment.Items.Add(new DataItem("BillCountry", this.BillCountry));
			payment.Items.Add(new DataItem("BillPhone", this.BillPhone));
			payment.Items.Add(new DataItem("PaymentNotes", this.PaymentNotes));
			payment.Items.Add(new DataItem("InternalNotes", this.InternalNotes));

			return payment;
		}

		public void Deserialize(DataItem Source)
		{
			this.InvoiceNo = Source["InvoiceNo"].AsInt32();
			this.Amount = Source["Amount"].AsInt32();
			this.Type = Source["Type"].Value;
			this.RefNo = Source["RefNo"].Value;
			this.PaymentDate = Source["PaymentDate"].AsDateTime2();
			this.PayerName = Source["PayerName"].Value;
			this.PayerEmail = Source["PayerEmail"].Value;
			this.BillOrganization = Source["BillOrganization"].Value;
			this.UpdateInvoiceInfo = Source["UpdateInvoiceInfo"].AsBoolean();
			this.AutoCloseInvoice = Source["AutoCloseInvoice"].AsBoolean();
			this.UseInvoiceAddress = Source["UseInvoiceAddress"].AsBoolean();
			this.BillAddress1 = Source["BillAddress1"].Value;
			this.BillAddress2 = Source["BillAddress2"].Value;
			this.BillCity = Source["BillCity"].Value;
			this.BillLocation = Source["BillLocation"].Value;
			this.BillPostalCode = Source["BillPostalCode"].Value;
			this.BillCountry = Source["BillCountry"].Value;
			this.BillPhone = Source["BillPhone"].Value;
			this.PaymentNotes = Source["PaymentNotes"].Value;
			this.InternalNotes = Source["InternalNotes"].Value;
		}

		public int InvoiceNo { get; set; }
		public int Amount { get; set; }
		public string Type { get; set; }
		public string RefNo { get; set; }
		public DateTime? PaymentDate { get; set; }
		public string PayerName { get; set; }
		public string PayerEmail { get; set; }
		public string BillOrganization { get; set; }
		public bool? UpdateInvoiceInfo { get; set; }
		public bool? AutoCloseInvoice { get; set; }
		public bool? UseInvoiceAddress { get; set; }
		public string BillAddress1 { get; set; }
		public string BillAddress2 { get; set; }
		public string BillCity { get; set; }
		public string BillLocation { get; set; }
		public string BillPostalCode { get; set; }
		public string BillCountry { get; set; }
		public string BillPhone { get; set; }
		public string PaymentNotes { get; set; }
		public string InternalNotes { get; set; }
	}

	#endregion

	#region Message

	public class Message : Interfaces.IApiEntity
	{
		public Message()
		{

		}

		public DataItem Serialize()
		{
			//Note: Read-only entities do not need to implement Serialize
			throw new System.NotImplementedException();
		}

		public void Deserialize(DataItem Source)
		{
			this.MessageID = Source["MessageID"].AsInt32();
			this.From = Source["From"].Value;
			this.FromProfileID = Source["FromProfileID"].AsInt32();
			this.Subject = Source["Subject"].Value;
			this.Timestamp = Source["Timestamp"].AsDateTime();
			this.Unread = Source["Unread"].AsBoolean();
		}

		public int MessageID { get; set; }
		public string From { get; set; }
		public int FromProfileID { get; set; }
		public string Subject { get; set; }
		public DateTime Timestamp { get; set; }
		public bool Unread { get; set; }
	}

	#endregion

	#region MemberProfile (Full)

	public class MemberProfile : ProfileBase, Interfaces.IApiEntity
	{
		public MemberProfile()
		{
			this.CustomFieldResponses = new List<CustomFieldResponse>();
		}

		public void Deserialize(DataItem Source)
		{
			base.deserialize(Source);

			this.WebsiteID = Source["WebsiteID"].Value;
			this.MemberTypeCode = Source["MemberTypeCode"].Value;
			this.AltEmailAddr = Source["AltEmailAddr"].Value;
			this.EmailBounced = Source["EmailBounced"].AsBoolean();
			this.Username = Source["Username"].Value;
			this.Password = Source["Password"].Value;
			this.Suspended = Source["Suspended"].AsBoolean();
			this.ClassYear = Source["ClassYear"].Value;
			this.IsMember = Source["IsMember"].AsBoolean();
			this.IsNonMember = Source["IsNonMember"].AsBoolean();
		}

		public DataItem Serialize()
		{
			DataItem profile = base.serialize();

			profile.Items.Add(new DataItem("WebsiteID", this.WebsiteID));
			profile.Items.Add(new DataItem("MemberTypeCode", this.MemberTypeCode));
			profile.Items.Add(new DataItem("AltEmailAddr", this.AltEmailAddr));
			profile.Items.Add(new DataItem("Username", this.Username));
			profile.Items.Add(new DataItem("Password", this.Password));
			profile.Items.Add(new DataItem("Suspended", this.Suspended.ValueOrNull()));
			profile.Items.Add(new DataItem("ClassYear", this.ClassYear));
			profile.Items.Add(new DataItem("IsMember", this.IsMember.ValueOrNull()));
			profile.Items.Add(new DataItem("IsNonMember", this.IsNonMember.ValueOrNull()));

			return profile;
		}

		public string MemberTypeCode { get; set; }
		public string AltEmailAddr { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool? Suspended { get; set; }
		public string ClassYear { get; set; }
		public bool? IsMember { get; set; }
		public bool? IsNonMember { get; set; }
		public bool? EmailBounced { get; set; }
		public string WebsiteID { get; set; }
	}

	#endregion

	#region MemberProfile (Mini)

	public class MemberProfileMini : Interfaces.IApiEntity
	{
		public MemberProfileMini()
		{

		}

		public void Deserialize(DataItem Source)
		{
			this.ID = Source["ID"].Value;
			this.WebsiteID = Source["WebsiteID"].Value;
			this.EmailAddr = Source["EmailAddr"].Value;
			this.NamePrefix = Source["NamePrefix"].Value;
			this.FirstName = Source["FirstName"].Value;
			this.MiddleName = Source["MiddleName"].Value;
			this.LastName = Source["LastName"].Value;
			this.NameSuffix = Source["NameSuffix"].Value;
			this.Nickname = Source["Nickname"].Value;
			this.HeadshotImageURI = Source["HeadshotImageURI"].Value;
			this.AdditionalSeats = Source["AdditionalSeats"].AsInt32();
			this.HasAccessToGroups = Source["HasAccessToGroups"].AsBoolean();
			this.HasPersonalPage = Source["HasPersonalPage"].AsBoolean();
			this.HasPublicProfile = Source["HasPublicProfile"].AsBoolean();
			this.CanHaveConnections = Source["CanHaveConnections"].AsBoolean();
			this.CanHaveNetworks = Source["CanHaveNetworks"].AsBoolean();
			this.CanHavePhotoGallery = Source["CanHavePhotoGallery"].AsBoolean();
			this.CanHavePermanentEmail = Source["CanHavePermanentEmail"].AsBoolean();
			this.CanHavePersonalBlog = Source["CanHavePersonalBlog"].AsBoolean();
			this.CanHaveResumeCV = Source["CanHaveResumeCV"].AsBoolean();
			this.CanHaveSubscriptions = Source["CanHaveSubscriptions"].AsBoolean();
			this.CanPostCareerOpenings = Source["CanPostCareerOpenings"].AsBoolean();
			this.CanUseMessaging = Source["CanUseMessaging"].AsBoolean();
			this.CanMaintainWall = Source["CanMaintainWall"].AsBoolean();
			this.CanViewMembership = Source["CanViewMembership"].AsBoolean();
			this.PendingConnections = Source["PendingConnections"].AsInt32();
			this.UndreadMessages = Source["UndreadMessages"].AsInt32();
		}

		public DataItem Serialize()
		{
			DataItem profile = new DataItem();

			profile.Items.Add(new DataItem("ID", this.ID));
			profile.Items.Add(new DataItem("WebsiteID", this.WebsiteID));
			profile.Items.Add(new DataItem("EmailAddr", this.EmailAddr));
			profile.Items.Add(new DataItem("NamePrefix", this.NamePrefix));
			profile.Items.Add(new DataItem("FirstName", this.FirstName));
			profile.Items.Add(new DataItem("MiddleName", this.MiddleName));
			profile.Items.Add(new DataItem("LastName", this.LastName));
			profile.Items.Add(new DataItem("NameSuffix", this.NameSuffix));
			profile.Items.Add(new DataItem("Nickname", this.Nickname));
			profile.Items.Add(new DataItem("HeadshotImageURI", this.HeadshotImageURI));
			profile.Items.Add(new DataItem("AdditionalSeats", this.AdditionalSeats.ToString()));
			profile.Items.Add(new DataItem("HasAccessToGroups", this.HasAccessToGroups.ToString()));
			profile.Items.Add(new DataItem("HasPersonalPage", this.HasPersonalPage.ToString()));
			profile.Items.Add(new DataItem("HasPublicProfile", this.HasPublicProfile.ToString()));
			profile.Items.Add(new DataItem("CanHaveConnections", this.CanHaveConnections.ToString()));
			profile.Items.Add(new DataItem("CanHaveNetworks", this.CanHaveNetworks.ToString()));
			profile.Items.Add(new DataItem("CanHavePhotoGallery", this.CanHavePhotoGallery.ToString()));
			profile.Items.Add(new DataItem("CanHavePermanentEmail", this.CanHavePermanentEmail.ToString()));
			profile.Items.Add(new DataItem("CanHavePersonalBlog", this.CanHavePersonalBlog.ToString()));
			profile.Items.Add(new DataItem("CanHaveResumeCV", this.CanHaveResumeCV.ToString()));
			profile.Items.Add(new DataItem("CanHaveSubscriptions", this.CanHaveSubscriptions.ToString()));
			profile.Items.Add(new DataItem("CanPostCareerOpenings", this.CanPostCareerOpenings.ToString()));
			profile.Items.Add(new DataItem("CanUseMessaging", this.CanUseMessaging.ToString()));
			profile.Items.Add(new DataItem("CanMaintainWall", this.CanMaintainWall.ToString()));
			profile.Items.Add(new DataItem("CanViewMembership", this.CanViewMembership.ToString()));
			profile.Items.Add(new DataItem("PendingConnections", this.PendingConnections.ToString()));
			profile.Items.Add(new DataItem("UndreadMessages", this.UndreadMessages.ToString()));

			return profile;
		}

		public string ID { get; set; }
		public string WebsiteID { get; set; }
		public string EmailAddr { get; set; }
		public string NamePrefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string NameSuffix { get; set; }
		public string Nickname { get; set; }
		public string HeadshotImageURI { get; set; }
		public int AdditionalSeats { get; set; }
		public bool HasAccessToGroups { get; set; }
		public bool HasPersonalPage { get; set; }
		public bool HasPublicProfile { get; set; }
		public bool CanHaveConnections { get; set; }
		public bool CanHaveNetworks { get; set; }
		public bool CanHavePhotoGallery { get; set; }
		public bool CanHavePermanentEmail { get; set; }
		public bool CanHavePersonalBlog { get; set; }
		public bool CanHaveResumeCV { get; set; }
		public bool CanHaveSubscriptions { get; set; }
		public bool CanPostCareerOpenings { get; set; }
		public bool CanUseMessaging { get; set; }
		public bool CanMaintainWall { get; set; }
		public bool CanViewMembership { get; set; }
		public int PendingConnections { get; set; }
		public int UndreadMessages { get; set; }
	}

	#endregion

	#region ProfileBase

	public class ProfileBase
	{
		public ProfileBase()
		{
			this.CustomFieldResponses = new List<CustomFieldResponse>();
		}

		internal void deserialize(DataItem Source)
		{
			this.ID = Source["ID"].Value;
			this.PrimaryGroupCode = Source["PrimaryGroupCode"].Value;
			this.LastUpdated = Source["LastUpdated"].AsDateTime2();
			this.ImportID = Source["ImportID"].Value;
			this.ConstituentID = Source["ConstituentID"].Value;
			this.EmailAddr = Source["EmailAddr"].Value;
			this.NamePrefix = Source["NamePrefix"].Value;
			this.FirstName = Source["FirstName"].Value;
			this.MiddleName = Source["MiddleName"].Value;
			this.LastName = Source["LastName"].Value;
			this.NameSuffix = Source["NameSuffix"].Value;
			this.Nickname = Source["Nickname"].Value;
			this.Gender = Source["Gender"].Value;
			this.Birthdate = Source["Birthdate"].AsDateTime2();
			this.MaritalStatus = Source["MaritalStatus"].Value;
			this.MaidenName = Source["MaidenName"].Value;
			this.SpouseName = Source["SpouseName"].Value;
			this.AnniversaryDate = Source["AnniversaryDate"].AsDateTime2();
			this.Employer = Source["Employer"].Value;
			this.Title = Source["Title"].Value;
			this.Profession = Source["Profession"].Value;
			this.Membership = Source["Membership"].Value;
			this.MembershipExpiry = Source["MembershipExpiry"].AsDateTime2();
			this.HomeAddrLines = Source["HomeAddrLines"].Value;
			this.HomeCity = Source["HomeCity"].Value;
			this.HomeLocation = Source["HomeLocation"].Value;
			this.HomePostalCode = Source["HomePostalCode"].Value;
			this.HomeCountry = Source["HomeCountry"].Value;
			this.Website = Source["Website"].Value;
			this.HomePhAreaCode = Source["HomePhAreaCode"].Value;
			this.HomePhone = Source["HomePhone"].Value;
			this.MobileAreaCode = Source["MobileAreaCode"].Value;
			this.Mobile = Source["Mobile"].Value;
			this.EmpAddrLines = Source["EmpAddrLines"].Value;
			this.EmpCity = Source["EmpCity"].Value;
			this.EmpLocation = Source["EmpLocation"].Value;
			this.EmpPostalCode = Source["EmpPostalCode"].Value;
			this.EmpCountry = Source["EmpCountry"].Value;
			this.BusinessWebsite = Source["BusinessWebsite"].Value;
			this.EmpPhAreaCode = Source["EmpPhAreaCode"].Value;
			this.EmpPhone = Source["EmpPhone"].Value;
			this.EmpFaxAreaCode = Source["EmpFaxAreaCode"].Value;
			this.EmpFax = Source["EmpFax"].Value;
			this.Lost = Source["Lost"].AsBoolean();
			this.Deceased = Source["Deceased"].AsBoolean();
			this.LostMemNotes = Source["LostMemNotes"].Value;
			this.RegData1 = Source["RegData1"].Value;
			this.RegData2 = Source["RegData2"].Value;
			this.RegData3 = Source["RegData3"].Value;
			this.RegData4 = Source["RegData4"].Value;
			this.RegData5 = Source["RegData5"].Value;
			this.IsSyncCall = Source["IsSyncCall"].AsBoolean();

			this.CustomFieldResponses = Source.GetNamedItem("CustomFieldResponses").ConvertToListOf<CustomFieldResponse>();
		}

		internal DataItem serialize()
		{
			DataItem profile = new DataItem();

			profile.Items.Add(new DataItem("ID", this.ID));
			profile.Items.Add(new DataItem("PrimaryGroupCode", this.PrimaryGroupCode));
			profile.Items.Add(new DataItem("LastUpdated", this.LastUpdated.DateOrNull()));
			profile.Items.Add(new DataItem("ImportID", this.ImportID));
			profile.Items.Add(new DataItem("ConstituentID", this.ConstituentID));
			profile.Items.Add(new DataItem("EmailAddr", this.EmailAddr));
			profile.Items.Add(new DataItem("NamePrefix", this.NamePrefix));
			profile.Items.Add(new DataItem("FirstName", this.FirstName));
			profile.Items.Add(new DataItem("MiddleName", this.MiddleName));
			profile.Items.Add(new DataItem("LastName", this.LastName));
			profile.Items.Add(new DataItem("NameSuffix", this.NameSuffix));
			profile.Items.Add(new DataItem("Nickname", this.Nickname));
			profile.Items.Add(new DataItem("Gender", this.Gender));
			profile.Items.Add(new DataItem("Birthdate", this.Birthdate.DateOrNull()));
			profile.Items.Add(new DataItem("MaritalStatus", this.MaritalStatus));
			profile.Items.Add(new DataItem("MaidenName", this.MaidenName));
			profile.Items.Add(new DataItem("SpouseName", this.SpouseName));
			profile.Items.Add(new DataItem("AnniversaryDate", this.AnniversaryDate.DateOrNull()));
			profile.Items.Add(new DataItem("Employer", this.Employer));
			profile.Items.Add(new DataItem("Title", this.Title));
			profile.Items.Add(new DataItem("Profession", this.Profession));
			profile.Items.Add(new DataItem("Membership", this.Membership));
			profile.Items.Add(new DataItem("MembershipExpiry", this.MembershipExpiry.DateOrNull()));
			profile.Items.Add(new DataItem("HomeAddrLines", this.HomeAddrLines));
			profile.Items.Add(new DataItem("HomeCity", this.HomeCity));
			profile.Items.Add(new DataItem("HomeLocation", this.HomeLocation));
			profile.Items.Add(new DataItem("HomePostalCode", this.HomePostalCode));
			profile.Items.Add(new DataItem("HomeCountry", this.HomeCountry));
			profile.Items.Add(new DataItem("Website", this.Website));
			profile.Items.Add(new DataItem("HomePhAreaCode", this.HomePhAreaCode));
			profile.Items.Add(new DataItem("HomePhone", this.HomePhone));
			profile.Items.Add(new DataItem("MobileAreaCode", this.MobileAreaCode));
			profile.Items.Add(new DataItem("Mobile", this.Mobile));
			profile.Items.Add(new DataItem("EmpAddrLines", this.EmpAddrLines));
			profile.Items.Add(new DataItem("EmpCity", this.EmpCity));
			profile.Items.Add(new DataItem("EmpLocation", this.EmpLocation));
			profile.Items.Add(new DataItem("EmpPostalCode", this.EmpPostalCode));
			profile.Items.Add(new DataItem("EmpCountry", this.EmpCountry));
			profile.Items.Add(new DataItem("BusinessWebsite", this.BusinessWebsite));
			profile.Items.Add(new DataItem("EmpPhAreaCode", this.EmpPhAreaCode));
			profile.Items.Add(new DataItem("EmpPhone", this.EmpPhone));
			profile.Items.Add(new DataItem("EmpFaxAreaCode", this.EmpFaxAreaCode));
			profile.Items.Add(new DataItem("EmpFax", this.EmpFax));
			profile.Items.Add(new DataItem("Lost", this.Lost.ValueOrNull()));
			profile.Items.Add(new DataItem("Deceased", this.Deceased.ValueOrNull()));
			profile.Items.Add(new DataItem("LostMemNotes", this.LostMemNotes));
			profile.Items.Add(new DataItem("RegData1", this.RegData1));
			profile.Items.Add(new DataItem("RegData2", this.RegData2));
			profile.Items.Add(new DataItem("RegData3", this.RegData3));
			profile.Items.Add(new DataItem("RegData4", this.RegData4));
			profile.Items.Add(new DataItem("RegData5", this.RegData5));
			profile.Items.Add(new DataItem("IsSyncCall", this.IsSyncCall.ValueOrNull()));

			if (this.CustomFieldResponses.Count > 0)
			{
				DataItem customFieldResponses = new DataItem("CustomFieldResponses", string.Empty);

				foreach (CustomFieldResponse response in this.CustomFieldResponses)
				{
					customFieldResponses.Items.Add(response.Serialize());
				}

				profile.Items.Add(customFieldResponses);
			}

			return profile;
		}

		public string ID { get; set; }
		public string ImportID { get; set; }
		public string PrimaryGroupCode { get; set; }
		public DateTime? LastUpdated { get; set; }
		public string ConstituentID { get; set; }
		public string NamePrefix { get; set; }
		public string EmailAddr { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string NameSuffix { get; set; }
		public string Nickname { get; set; }
		public string Gender { get; set; }
		public DateTime? Birthdate { get; set; }
		public string MaritalStatus { get; set; }
		public string MaidenName { get; set; }
		public string SpouseName { get; set; }
		public DateTime? AnniversaryDate { get; set; }
		public string Employer { get; set; }
		public string Title { get; set; }
		public string Profession { get; set; }
		public string Membership { get; set; }
		public DateTime? MembershipExpiry { get; set; }
		public string HomeCity { get; set; }
		public string HomeLocation { get; set; }
		public string HomePostalCode { get; set; }
		public string HomeCountry { get; set; }
		public string Website { get; set; }
		public string HomeAddrLines { get; set; }
		public string HomePhAreaCode { get; set; }
		public string HomePhone { get; set; }
		public string MobileAreaCode { get; set; }
		public string Mobile { get; set; }
		public string EmpCity { get; set; }
		public string EmpLocation { get; set; }
		public string EmpPostalCode { get; set; }
		public string EmpCountry { get; set; }
		public string BusinessWebsite { get; set; }
		public string EmpAddrLines { get; set; }
		public string EmpPhAreaCode { get; set; }
		public string EmpPhone { get; set; }
		public string EmpFaxAreaCode { get; set; }
		public string EmpFax { get; set; }
		public bool? Lost { get; set; }
		public bool? Deceased { get; set; }
		public string LostMemNotes { get; set; }
		public string RegData1 { get; set; }
		public string RegData2 { get; set; }
		public string RegData3 { get; set; }
		public string RegData4 { get; set; }
		public string RegData5 { get; set; }
		public bool? IsSyncCall { get; set; }

		public List<CustomFieldResponse> CustomFieldResponses { get; set; }
	}

	#endregion

	#region Custom Fields

	public class CustomFieldResponse : Interfaces.IApiEntity
	{
		public CustomFieldResponse()
		{
			this.Values = new List<string>();
		}

		public DataItem Serialize()
		{
			DataItem customField;
			DataItem valuesItem;			
			
			customField = new DataItem();
			customField.Name = "CustomFieldResponse";
			customField.AddAttribute("FieldCode", this.FieldCode);

			valuesItem = new DataItem("Values", string.Empty);

			foreach (string value in this.Values)
			{
				valuesItem.AddChild("Value", value);
			}

			customField.Items.Add(valuesItem);

			return customField;
		}

		public void Deserialize(DataItem Source)
		{
			KeyValuePair<string, string> attr = Source.Attributes.SingleOrDefault(x => x.Key.Equals("FieldCode", StringComparison.OrdinalIgnoreCase));

			if (attr.Value.ContainsText())
			{
				this.FieldCode = attr.Value;

				DataItem valuesNode = Source.GetNamedItem("Values");

				this.Values = new List<string>();

				foreach (DataItem valueNode in valuesNode.Items)
				{
					this.Values.Add(valueNode.Value);
				}
			}
		}

		public string FieldCode { get; set; }
		public List<string> Values { get; set; }
	}

	#endregion

}
