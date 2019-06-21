using System.Collections.Specialized;
using System;

namespace YMSDK
{
    public partial class ApiManager
    {
        #region Constants
        public const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private const string API_METHOD_AUTH_AUTHENTICATE = "Auth.Authenticate";
        private const string API_METHOD_AUTH_CREATE_TOKEN = "Auth.CreateToken";
        private const string API_METHOD_CONVERT_TOEASTERNTIME = "Convert.ToEasternTime";
        private const string API_METHOD_EVENTS_ALL_SEARCH = "Events.All.Search";
        private const string API_METHOD_EVENTS_EVENT_GET = "Events.Event.Get";
        private const string API_METHOD_EVENTS_EVENT_ATTENDEES_GET = "Events.Event.Attendees.Get";
        private const string API_METHOD_FEEDS_FEED_GET = "Feeds.Feed.Get";
        private const string API_METHOD_FEEDS_GET = "Feeds.Get";
        private const string API_METHOD_MEMBER_CERTIFICATIONS_GET = "Member.Certifications.Get";
        private const string API_METHOD_MEMBER_CERTIFICATIONS_JOURNAL_GET = "Member.Certifications.Journal.Get";
        private const string API_METHOD_MEMBER_COMMERCE_STORE_ORDER_GET = "Member.Commerce.Store.Order.Get";
        private const string API_METHOD_MEMBER_COMMERCE_STORE_GETORDERIDS = "Member.Commerce.Store.GetOrderIDs";
        private const string API_METHOD_MEMBER_CONNECTION_APPROVE = "Member.Connection.Approve";
        private const string API_METHOD_MEMBER_ISAUTHENTICATED = "Member.IsAuthenticated";
        private const string API_METHOD_MEMBER_MEDIAGALLERY_UPLOAD = "Member.MediaGallery.Upload";
        private const string API_METHOD_MEMBER_MESSAGES_GETINBOX = "Member.Messages.GetInbox";
        private const string API_METHOD_MEMBER_MESSAGES_GETSENT = "Member.Messages.GetSent";
        private const string API_METHOD_MEMBER_MESSAGES_MESSAGE_READ = "Member.Messages.Message.Read";
        private const string API_METHOD_MEMBER_MESSAGES_MESSAGE_SEND = "Member.Messages.Message.Send";
        private const string API_METHOD_MEMBER_PASSWORD_INITIALIZE_RESET = "Member.Password.InitializeReset";
        private const string API_METHOD_MEMBER_PASSWORD_UPDATE = "Member.Password.Update";
        private const string API_METHOD_MEMBER_PROFILE_GET = "Member.Profile.Get";
        private const string API_METHOD_MEMBER_PROFILE_GETMINI = "Member.Profile.GetMini";
        private const string API_METHOD_MEMBER_WALL_POST = "Member.Wall.Post";
        private const string API_METHOD_MEMBERS_CONNECTIONS_CATEGORIES_GET = "Members.Connections.Categories.Get";
        private const string API_METHOD_MEMBERS_CONNECTIONS_GET = "Members.Connections.Get";
        private const string API_METHOD_MEMBERS_MEDIAGALLERY_ALBUMS_GET = "Members.MediaGallery.Albums.Get";
        private const string API_METHOD_MEMBERS_MEDIAGALLERY_GET = "Members.MediaGallery.Get";
        private const string API_METHOD_MEMBERS_MEDIAGALLERY_ITEM_GET = "Members.MediaGallery.Item.Get";
        private const string API_METHOD_MEMBERS_WALL_GET = "Members.Wall.Get";
        private const string API_METHOD_PEOPLE_ALL_SEARCH = "People.All.Search";
        private const string API_METHOD_PEOPLE_PROFILE_GET = "People.Profile.Get";
        private const string API_METHOD_SA_AUTH_AUTHENTICATE = "Sa.Auth.Authenticate";
        private const string API_METHOD_SA_CERTIFICATIONS_ALL_GET = "Sa.Certifications.All.Get";
        private const string API_METHOD_SA_COMMERCE_STORE_ORDER_GET = "Sa.Commerce.Store.Order.Get";
        private const string API_METHOD_SA_COMMERCE_PRODUCTS_ALL_GETIDS = "Sa.Commerce.Products.All.GetIDs";
        private const string API_METHOD_SA_COMMERCE_PRODUCT_GET = "Sa.Commerce.Product.Get";
        private const string API_METHOD_SA_EVENT_GetIDS = "Sa.Event.GetIDs";
        private const string API_METHOD_SA_EVENTS_ALL_GETIDS = "Sa.Events.All.GetIDs";
		private const string API_METHOD_SA_EVENTS_EVENT_GET = "Sa.Events.Event.Get";
        private const string API_METHOD_SA_EVENTS_EVENT_REGISTRATION_GET = "Sa.Events.Event.Registration.Get";
        private const string API_METHOD_SA_EVENTS_EVENT_REGISTRATIONS_GETIDS = "Sa.Events.Event.Registrations.GetIDs";
        private const string API_METHOD_SA_EXPORT_ALL_INVOICEITEMS = "Sa.Export.All.InvoiceItems";
        private const string API_METHOD_SA_EXPORT_CAREER_OPENINGS = "Sa.Export.Career.Openings";
        private const string API_METHOD_SA_EXPORT_DONATIONS_TRANSACTIONS = "Sa.Export.Donations.Transactions";
        private const string API_METHOD_SA_EXPORT_DONATIONS_INVOICEITEMS = "Sa.Export.Donations.InvoiceItems";
        private const string API_METHOD_SA_EXPORT_DUES_TRANSACTIONS = "Sa.Export.Dues.Transactions";
        private const string API_METHOD_SA_EXPORT_DUES_INVOICEITEMS = "Sa.Export.Dues.InvoiceItems";
		private const string API_METHOD_SA_EXPORT_EVENT_REGISTRATIONS = "Sa.Export.Event.Registrations";
		private const string API_METHOD_SA_EXPORT_FINANCE_BATCH = "Sa.Export.Finance.Batch";
        private const string API_METHOD_SA_EXPORT_MEMBERS = "Sa.Export.Members";
		private const string API_METHOD_SA_EXPORT_MEMBERS_GROUPS = "Sa.Export.Members.Groups";
        private const string API_METHOD_SA_EXPORT_STORE_ORDERS = "Sa.Export.Store.Orders";
        private const string API_METHOD_SA_EXPORT_STORE_INOICEITEMS = "Sa.Export.Store.InvoiceItems";
		private const string API_METHOD_SA_EXPORT_STATUS = "Sa.Export.Status";
		private const string API_METHOD_SA_FINANCE_BATCH_CREATE = "Sa.Finance.Batch.Create";
		private const string API_METHOD_SA_FINANCE_BATCHES_GET = "Sa.Finance.Batches.Get";
		private const string API_METHOD_SA_FINANCE_INVOICE_PAYMENT_CREATE = "Sa.Finance.Invoice.Payment.Create";
		private const string API_METHOD_SA_GROUPS_GROUP_CREATE = "Sa.Groups.Group.Create";
		private const string API_METHOD_SA_GROUPS_GROUP_UPDATE = "Sa.Groups.Group.Update";
		private const string API_METHOD_SA_GROUPS_GROUP_GETMEMBERSHIPLOG = "Sa.Groups.Group.GetMembershipLog";
		private const string API_METHOD_SA_GROUPS_GROUPTYPES_GET = "Sa.Groups.GroupTypes.Get";
        private const string API_METHOD_SA_MEMBER_CERTIFICATIONS_GET = "Sa.Member.Certifications.Get";
        private const string API_METHOD_SA_MEMBER_CERTIFICATIONS_JOURNAL_GET = "Sa.Member.Certifications.Journal.Get";
        private const string API_METHOD_SA_MEMBERS_ALL_GETIDS = "Sa.Members.All.GetIDs";
		private const string API_METHOD_SA_MEMBERS_ALL_MEMBERTYPES_GET = "Sa.Members.All.MemberTypes.Get";
        private const string API_METHOD_SA_MEMBERS_ALL_RECENTACTIVITY = "Sa.Members.All.RecentActivity";
        private const string API_METHOD_SA_MEMBERS_CERTIFICATIONS_JOURNALENTRY_CREATE = "Sa.Members.Certifications.JournalEntry.Create";
        private const string API_METHOD_SA_MEMBERS_COMMERCE_STORE_GETORDERIDS = "Sa.Members.Commerce.Store.GetOrderIDs";
        private const string API_METHOD_SA_MEMBERS_EVENTS_EVENT_REGISTRATION_GET = "Sa.Members.Events.Event.Registration.Get";
		private const string API_METHOD_SA_MEMBERS_GROUPS_ADD = "Sa.Members.Groups.Add";
		private const string API_METHOD_SA_MEMBERS_GROUPS_REMOVE = "Sa.Members.Groups.Remove";
        private const string API_METHOD_SA_MEMBERS_PROFILE_CREATE = "Sa.Members.Profile.Create";
        private const string API_METHOD_SA_MEMBERS_REFERRALS_GET = "Sa.Members.Referrals.Get";
        private const string API_METHOD_SA_MEMBERS_SUBACCOUNT_GET = "Sa.Members.SubAccounts.Get";
        private const string API_METHOD_SA_NONMEMBERS_ALL_GETIDS = "Sa.NonMembers.All.GetIDs";
        private const string API_METHOD_SA_NONMEMBERS_PROFILE_CREATE = "Sa.NonMembers.Profile.Create";
        private const string API_METHOD_SA_PEOPLE_ALL_GETIDS = "Sa.People.All.GetIDs";
        private const string API_METHOD_SA_PEOPLE_PROFILE_FINDID = "Sa.People.Profile.FindID";
        private const string API_METHOD_SA_PEOPLE_PROFILE_GET = "Sa.People.Profile.Get";
        private const string API_METHOD_SA_PEOPLE_GROUPS_GET = "Sa.People.Profile.Groups.Get";
        private const string API_METHOD_SA_PEOPLE_PROFILE_UPDATE = "Sa.People.Profile.Update";
        private const string API_METHOD_SESSION_ABANDON = "Session.Abandon";
        private const string API_METHOD_SESSION_CREATE = "Session.Create";
        private const string API_METHOD_SESSION_PING = "Session.Ping";

        #endregion

        #region Authenticate

        public ApiResponse AuthAuthenticate(string Username, string Password)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Username", Username));
            payload.Add(new DataItem("Password", Password));

            ApiResponse response = ExecuteMethod(API_METHOD_AUTH_AUTHENTICATE, payload);

            if (response.ErrorCode.Equals(ApiErrorCode.NoError))
            {
                this.AuthenticatedMemberGUID = response.MethodResults.GetNamedItem("ID").Value;
                this.AuthenticatedMemberID = response.MethodResults.GetNamedItem("WebsiteID").Value;
            }

            return response;
        }

        public ApiResponse AuthCreateToken(string RetUrl, string Username, string Password, bool Persist)
        {
            DataItemCollection payload = new DataItemCollection();

            if (!string.IsNullOrEmpty(RetUrl))
                payload.Add(new DataItem("RetUrl", RetUrl));

            if (!string.IsNullOrEmpty(Username))
                payload.Add(new DataItem("Username", Username));

            if (!string.IsNullOrEmpty(Password))
                payload.Add(new DataItem("Password", Password));

            payload.Add(new DataItem("Persist", Persist.ToString()));

            return ExecuteMethod(API_METHOD_AUTH_CREATE_TOKEN, payload);
        }

        #endregion

        #region Convert

        public ApiResponse ConvertToEasternTime(System.DateTime LocalTime, int LocalGmtBias)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("LocalTime", LocalTime.ToShortDateString() + " " + LocalTime.ToLongTimeString()));
            payload.Add(new DataItem("LocalGmtBias", LocalGmtBias.ToString()));

            ApiResponse response = ExecuteMethodSA(API_METHOD_CONVERT_TOEASTERNTIME, payload);

            return response;
        }

        #endregion

        #region Events

        public ApiResponse EventsAllSearch(string SearchText)
        {
            return EventsAllSearch(SearchText, null, null);
        }

        public ApiResponse EventsAllSearch(string SearchText, int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();

            if (!string.IsNullOrEmpty(SearchText))
            {
                payload.Add(new DataItem("SearchText", SearchText));
            }

            if (PageSize.HasValue)
            {
                payload.Add(new DataItem("PageSize", PageSize.ToString()));
            }

            if (StartRecord.HasValue)
            {
                payload.Add(new DataItem("StartRecord", StartRecord.ToString()));
            }

            return ExecuteMethod(API_METHOD_EVENTS_ALL_SEARCH, payload);
        }

        public ApiResponse EventsEventGet(int EventID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("EventID", EventID.ToString()));

            return ExecuteMethod(API_METHOD_EVENTS_EVENT_GET, payload);
        }

        public ApiResponse EventsEventAttendeesGet(int EventID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("EventID", EventID.ToString()));

            return ExecuteMethod(API_METHOD_EVENTS_EVENT_ATTENDEES_GET, payload);
        }

        #endregion

        #region Feeds

        public ApiResponse FeedsFeedGet(string FeedID, int PageSize, int StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("FeedID", FeedID));
            payload.Add(new DataItem("PageSize", PageSize.ToString()));
            payload.Add(new DataItem("StartRecord", StartRecord.ToString()));

            return ExecuteMethod(API_METHOD_FEEDS_FEED_GET, payload);
        }

        public ApiResponse FeedsGet()
        {
            return ExecuteMethod(API_METHOD_FEEDS_GET);
        }

        #endregion

        #region Member
        public ApiResponse MemberCertificationsGet(bool IsArchived)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("IsArchived", IsArchived == true ? "1" : "0"));

            return ExecuteMethod(API_METHOD_MEMBER_CERTIFICATIONS_GET, payload);
        }

        public ApiResponse MemberCertificationsJournalGet(bool? ShowExpired, System.DateTime? StartDate, int? EntryID, string CertificationID, int? PageSize, int? PageNumber)
        {
            DataItemCollection payload = new DataItemCollection();
            if (ShowExpired.HasValue)
                payload.Add(new DataItem("ShowExpired", ShowExpired == true ? "1" : "0"));

            if (StartDate.HasValue)
                payload.Add(new DataItem("StartDate", StartDate.Value.ToString(SQL_DATETIME_FORMAT)));

            if (EntryID.HasValue)
                payload.Add(new DataItem("EntryID", EntryID.Value.ToString()));

            if (!string.IsNullOrEmpty(CertificationID))
                payload.Add(new DataItem("CertificationID", CertificationID));

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.Value.ToString()));

            if (PageNumber.HasValue)
                payload.Add(new DataItem("PageNumber", PageNumber.Value.ToString()));


            return ExecuteMethod(API_METHOD_MEMBER_CERTIFICATIONS_JOURNAL_GET, payload);
        }

        public ApiResponse MemberCommerceStoreOrderGet(string InvoiceGuid)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("InvoiceID", InvoiceGuid));

            return ExecuteMethod(API_METHOD_MEMBER_COMMERCE_STORE_ORDER_GET, payload);
        }

        public ApiResponse MemberCommerceStoreGetOrderIDs(System.DateTime? Timestamp, int? Status)
        {
            DataItemCollection payload = new DataItemCollection();

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            if (Status.HasValue)
                payload.Add(new DataItem("Status", Status.Value.ToString()));

            return ExecuteMethod(API_METHOD_MEMBER_COMMERCE_STORE_GETORDERIDS, payload);
        }

        public ApiResponse MemberConnectionApprove(string GuidOrProfileID, bool Approve)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));
            payload.Add(new DataItem("Approve", Approve == true ? "1" : "0"));

            return ExecuteMethod(API_METHOD_MEMBER_CONNECTION_APPROVE, payload);
        }

        public ApiResponse MemberMediaGalleryUpload(int? AlbumID, string Caption, string AllowComments, string UploadFileName, string UploadContentType)
        {
            DataItemCollection payload = new DataItemCollection();
            if (AlbumID.HasValue)
                payload.Add(new DataItem("AlbumID", AlbumID.Value.ToString()));

            payload.Add(new DataItem("Caption", Caption));
            payload.Add(new DataItem("AllowComments", AllowComments));

            return ExecuteMethodWithUpload(API_METHOD_MEMBER_MEDIAGALLERY_UPLOAD, false, payload, "Image1", UploadFileName, UploadContentType);
        }

        public ApiResponse MemberMessagesGetInbox(int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.ToString()));

            if (StartRecord.HasValue)
                payload.Add(new DataItem("StartRecord", StartRecord.ToString()));

            return ExecuteMethod(API_METHOD_MEMBER_MESSAGES_GETINBOX, payload);
        }

        public ApiResponse MemberMessagesGetSent(int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.ToString()));

            if (StartRecord.HasValue)
                payload.Add(new DataItem("StartRecord", StartRecord.ToString()));

            return ExecuteMethod(API_METHOD_MEMBER_MESSAGES_GETSENT, payload);
        }

        public ApiResponse MemberMessagesMessageRead(int MessageID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("MessageID", MessageID.ToString()));

            return ExecuteMethod(API_METHOD_MEMBER_MESSAGES_MESSAGE_READ, payload);
        }

        public ApiResponse MemberMessagesMessageSend(string GuidOrProfileID, string Subject, string Body)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));
            payload.Add(new DataItem("Subject", Subject));
            payload.Add(new DataItem("Body", Body));

            return ExecuteMethod(API_METHOD_MEMBER_MESSAGES_MESSAGE_SEND, payload);
        }

        public ApiResponse MemberIsAuthenticated()
        {
            return ExecuteMethod(API_METHOD_MEMBER_ISAUTHENTICATED);
        }

        public ApiResponse MemberPasswordInitializeReset(string UserName, string EmailAddress)
        {
            DataItemCollection payload = new DataItemCollection();

            payload.Add(new DataItem("Username", UserName));
            payload.Add(new DataItem("EmailAddress", EmailAddress));

            return ExecuteMethod(API_METHOD_MEMBER_PASSWORD_INITIALIZE_RESET, payload);
        }

        public ApiResponse MemberPasswordUpdate(string ResetToken, string CurrentPassword, string NewPassword)
        {
            DataItemCollection payload = new DataItemCollection();

            payload.Add(new DataItem("ResetToken", ResetToken));
            payload.Add(new DataItem("CurrentPassword", CurrentPassword));
            payload.Add(new DataItem("NewPassword", NewPassword));

            return ExecuteMethod(API_METHOD_MEMBER_PASSWORD_UPDATE, payload);
        }

        public ApiResponse MemberProfileGet()
        {
            return ExecuteMethod(API_METHOD_MEMBER_PROFILE_GET);
        }

        public ApiResponse MemberProfileGetMini()
        {
            return ExecuteMethod(API_METHOD_MEMBER_PROFILE_GETMINI);
        }

        public ApiResponse MemberWallPost(string PostText)
        {
            return MemberWallPost(string.Empty, PostText);
        }

        public ApiResponse MemberWallPost(string GuidOrProfileID, string PostText)
        {
            DataItemCollection payload = new DataItemCollection();

            if (!string.IsNullOrEmpty(GuidOrProfileID))
                payload.Add(new DataItem("ID", GuidOrProfileID));

            payload.Add(new DataItem("PostText", PostText));

            return ExecuteMethod(API_METHOD_MEMBER_WALL_POST, payload);
        }

        #endregion

        #region Members

        public ApiResponse MembersConnectionsCategoriesGet(string GuidOrProfileID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            return ExecuteMethod(API_METHOD_MEMBERS_CONNECTIONS_CATEGORIES_GET, payload);
        }

        public ApiResponse MembersConnectionsGet(string GuidOrProfileID, int? CategoryID, int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            if (CategoryID.HasValue)
                payload.Add(new DataItem("CategoryID", CategoryID.Value.ToString()));

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.Value.ToString()));

            if (StartRecord.HasValue)
                payload.Add(new DataItem("StartRecord", StartRecord.Value.ToString()));

            return ExecuteMethod(API_METHOD_MEMBERS_CONNECTIONS_GET, payload);
        }

        public ApiResponse MembersMediaGalleryAlbumsGet(string GuidOrProfileID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            return ExecuteMethod(API_METHOD_MEMBERS_MEDIAGALLERY_ALBUMS_GET, payload);
        }

        public ApiResponse MembersMediaGalleryGet(string GuidOrProfileID, int? AlbumID, int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            if (AlbumID.HasValue)
                payload.Add(new DataItem("AlbumID", AlbumID.Value.ToString()));

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.Value.ToString()));

            if (StartRecord.HasValue)
                payload.Add(new DataItem("StartRecord", StartRecord.Value.ToString()));

            return ExecuteMethod(API_METHOD_MEMBERS_MEDIAGALLERY_GET, payload);
        }

        public ApiResponse MembersMediaGalleryItemGet(string GuidOrProfileID, int AlbumID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));
            payload.Add(new DataItem("ItemID", AlbumID.ToString()));

            return ExecuteMethod(API_METHOD_MEMBERS_MEDIAGALLERY_ITEM_GET, payload);
        }

        public ApiResponse MembersWallGet(string GuidOrProfileID, int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.Value.ToString()));

            if (StartRecord.HasValue)
                payload.Add(new DataItem("StartRecord", StartRecord.Value.ToString()));

            return ExecuteMethod(API_METHOD_MEMBERS_WALL_GET, payload);
        }

        #endregion

        #region People

        public ApiResponse PeopleAllSearch()
        {
            return PeopleAllSearch2(string.Empty, null, null);
        }

        public ApiResponse PeopleAllSearch(string SearchText)
        {
            return PeopleAllSearch2(SearchText, null, null);
        }

        public ApiResponse PeopleAllSearch(string SearchText, int PageSize, int StartRecord)
        {
            return PeopleAllSearch2(SearchText, PageSize, StartRecord);
        }

        internal ApiResponse PeopleAllSearch2(string SearchText, int? PageSize, int? StartRecord)
        {
            DataItemCollection payload = new DataItemCollection();

            if (!string.IsNullOrEmpty(SearchText))
            {
                payload.Add(new DataItem("SearchText", SearchText));
            }

            if (PageSize.HasValue)
            {
                payload.Add(new DataItem("PageSize", PageSize.ToString()));
            }

            if (StartRecord.HasValue)
            {
                payload.Add(new DataItem("StartRecord", StartRecord.ToString()));
            }

            return ExecuteMethod(API_METHOD_PEOPLE_ALL_SEARCH, payload);
        }

        public ApiResponse PeopleProfileGet(string GuidOrProfileID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", GuidOrProfileID));

            return ExecuteMethod(API_METHOD_PEOPLE_PROFILE_GET, payload);
        }

        #endregion

        #region Sa.Authenticate

        public ApiResponse SaAuthAuthenticate(string Username, string Password, string PasswordHash)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Username", Username));
            payload.Add(new DataItem("Password", Password));
            payload.Add(new DataItem("PasswordHash", PasswordHash));

            ApiResponse response = ExecuteMethodSA(API_METHOD_SA_AUTH_AUTHENTICATE, payload);

            if (response.ErrorCode.Equals(ApiErrorCode.NoError))
            {
                this.AuthenticatedMemberGUID = response.MethodResults.GetNamedItem("ID").Value;
                this.AuthenticatedMemberID = response.MethodResults.GetNamedItem("WebsiteID").Value;
            }

            return response;
        }

        #endregion

        #region Sa.Certifications

        public ApiResponse SaCertificationsAllGet(bool? IsActive)
        {
            DataItemCollection payload = new DataItemCollection();
            if (IsActive.HasValue)
                payload.Add(new DataItem("IsActive", IsActive == true ? "1" : "0"));

            return ExecuteMethodSA(API_METHOD_SA_CERTIFICATIONS_ALL_GET, payload);
        }

        #endregion

        #region Sa.Commerce
        public ApiResponse SaCommerceStoreOrderGet(string InvoiceGuid)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("InvoiceID", InvoiceGuid));

            return ExecuteMethodSA(API_METHOD_SA_COMMERCE_STORE_ORDER_GET, payload);
        }

        public ApiResponse SaCommerceProductsAllGetIDs(string Name, int? ProductType)
        {
            DataItemCollection payload = new DataItemCollection();
            if (!string.IsNullOrEmpty(Name)) payload.Add(new DataItem("Name", Name));
            if (ProductType.HasValue) payload.Add(new DataItem("ProductType", ProductType.Value.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_COMMERCE_PRODUCTS_ALL_GETIDS, payload);
        }

        public ApiResponse SaCommerceProductGet(string ProductID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ProductID", ProductID));

            return ExecuteMethodSA(API_METHOD_SA_COMMERCE_PRODUCT_GET, payload);
        }

        #endregion

        #region Sa.Event

        public ApiResponse SaEventGetIDs(DateTime? StartDate, DateTime? EndDate, string Name)
        {
            /*******************************************************************
             * DEPRECIATED - Replaced by SaEventsAllGetIDs in version 2.00
             *******************************************************************/
            return SaEventsAllGetIDs(StartDate, EndDate, Name);
        }

        #endregion

        #region Sa.Events

		public ApiResponse SaEventsEventGet(int eventId)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("EventID", eventId.ToString()));

			return ExecuteMethodSA(API_METHOD_SA_EVENTS_EVENT_GET, payload);
		}

        public ApiResponse SaEventsAllGetIDs(DateTime? StartDate, DateTime? EndDate, string Name)
        {
            DataItemCollection payload = new DataItemCollection();
            if (StartDate.HasValue) payload.Add(new DataItem("StartDate", StartDate.Value.ToString()));
            if (EndDate.HasValue) payload.Add(new DataItem("EndDate", EndDate.Value.ToString()));
            if (!string.IsNullOrEmpty(Name)) payload.Add(new DataItem("Name", Name));

            return ExecuteMethodSA(API_METHOD_SA_EVENTS_ALL_GETIDS, payload);
        }

        public ApiResponse SaEventsEventRegistrationsGetIDs(int EventID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("EventID", EventID.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_EVENTS_EVENT_REGISTRATIONS_GETIDS, payload);
        }

        public ApiResponse SaEventsEventRegistrationGet(int EventID, string RegistrationID, int? BadgeNumber)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("EventID", EventID.ToString()));
            if (!string.IsNullOrEmpty(RegistrationID)) payload.Add(new DataItem("RegistrationID", RegistrationID));
            if (BadgeNumber.HasValue) payload.Add(new DataItem("BadgeNumber", BadgeNumber.Value.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_EVENTS_EVENT_REGISTRATION_GET, payload);
        }

        #endregion
		
        #region Sa.Export

        public ApiResponse SaExportCareerOpenings(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_CAREER_OPENINGS, payload);
        }

        public ApiResponse SaExportDonationsTransactions(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_DONATIONS_TRANSACTIONS, payload);
        }

        public ApiResponse SaExportDonationsInvoiceItems(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_DONATIONS_INVOICEITEMS, payload);
        }

        public ApiResponse SaExportDuesTransactions(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_DUES_TRANSACTIONS, payload);
        }

        public ApiResponse SaExportEventRegistrations(bool Unicode, int EventID, string SessionIDs, int? ProductID, int? Processed, string LastName, bool? AttendedEvent)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("EventID", EventID.ToString()));
            if (!string.IsNullOrEmpty(SessionIDs)) payload.Add(new DataItem("SessionIDs", SessionIDs));
            if (ProductID.HasValue) payload.Add(new DataItem("ProductID", ProductID.Value.ToString()));
            if (Processed.HasValue) payload.Add(new DataItem("Processed", Processed.Value.ToString()));
            if (!string.IsNullOrEmpty(LastName)) payload.Add(new DataItem("LastName", LastName));
            if (AttendedEvent.HasValue) payload.Add(new DataItem("AttendedEvent", AttendedEvent.Value == true ? "1" : "0"));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_EVENT_REGISTRATIONS, payload);
        }

		public ApiResponse SaExportFinanceBatch(bool Unicode, string BatchID)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
			payload.Add(new DataItem("BatchID", BatchID));

			return ExecuteMethodSA(API_METHOD_SA_EXPORT_FINANCE_BATCH, payload);
		}

        public ApiResponse SaExportDuesInvoiceItems(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_DUES_INVOICEITEMS, payload);
        }

        public ApiResponse SaExportMembers(bool Unicode, bool IncludeCustomFields)
        {
			return SaExportMembers(Unicode, IncludeCustomFields, null);
        }

		public ApiResponse SaExportMembers(bool Unicode, bool IncludeCustomFields, System.DateTime? Timestamp)
		{
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("CustomFields", IncludeCustomFields == true ? "1" : "0"));

			if (Timestamp.HasValue)
				payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_MEMBERS, payload);
        }

		public ApiResponse SaExportMembersGroups(bool Unicode, System.DateTime? Timestamp)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
			
			if (Timestamp.HasValue)
				payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

			return ExecuteMethodSA(API_METHOD_SA_EXPORT_MEMBERS_GROUPS, payload);
		}

        public ApiResponse SaExportStoreOrders(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_STORE_ORDERS, payload);
        }

        public ApiResponse SaExportStoreInvoiceItems(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_STORE_INOICEITEMS, payload);
        }

        public ApiResponse SaExportAllInvoiceItems(bool Unicode, System.DateTime StartDate)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Unicode", Unicode == true ? "1" : "0"));
            payload.Add(new DataItem("Date", StartDate.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_ALL_INVOICEITEMS, payload);
        }

        public ApiResponse SaExportStatus(string ExportID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ExportID", ExportID));

            return ExecuteMethodSA(API_METHOD_SA_EXPORT_STATUS, payload);
        }

        #endregion

		#region Sa.Finance

		public ApiResponse SaFinanceBatchCreate(string CommerceType, DateTime ClosingDate, bool ClosedOnly)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("CommerceType", CommerceType));
			payload.Add(new DataItem("ClosingDate", ClosingDate.ToString(SQL_DATETIME_FORMAT)));
			payload.Add(new DataItem("ClosedOnly", ClosedOnly == true ? "1" : "0"));

			return ExecuteMethodSA(API_METHOD_SA_FINANCE_BATCH_CREATE, payload);
		}

		public ApiResponse SaFinanceBatchesGet(DateTime Timestamp, string StartBatchID)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("Timestamp", Timestamp.ToString(SQL_DATETIME_FORMAT)));
			payload.Add(new DataItem("StartBatchID", StartBatchID));

			return ExecuteMethodSA(API_METHOD_SA_FINANCE_BATCHES_GET, payload);
		}

		public ApiResponse SaFinanceInvoicePaymentCreate(YMSDK.Entities.InvoicePayment invoicePayment)
		{
			return SaFinanceInvoicePaymentCreate(invoicePayment.Serialize());
		}

		public ApiResponse SaFinanceInvoicePaymentCreate(DataItem invoicePayment)
		{
			DataItemCollection payload = new DataItemCollection();

			foreach (DataItem item in invoicePayment.Items)
            {
                payload.Add(item);
            }

			return ExecuteMethodSA(API_METHOD_SA_FINANCE_INVOICE_PAYMENT_CREATE, payload);
		}

		#endregion

		#region Sa.Groups

		public ApiResponse SaGroupsGroupCreate(YMSDK.Entities.Group group)
		{
			return SaGroupsGroupCreate(group.Serialize());
		}

		public ApiResponse SaGroupsGroupCreate(DataItem group)
		{
			DataItemCollection payload = new DataItemCollection();

			foreach (DataItem item in group.Items)
			{
				payload.Add(item);
			}

			return ExecuteMethodSA(API_METHOD_SA_GROUPS_GROUP_CREATE, payload);
		}

		public ApiResponse SaGroupsGroupUpdate(YMSDK.Entities.Group group)
		{
			return SaGroupsGroupUpdate(group.Serialize());
		}

		public ApiResponse SaGroupsGroupUpdate(DataItem group)
		{
			DataItemCollection payload = new DataItemCollection();

			foreach (DataItem item in group.Items)
			{
				payload.Add(item);
			}

			return ExecuteMethodSA(API_METHOD_SA_GROUPS_GROUP_UPDATE, payload);
		}

		public ApiResponse SaGroupsGroupGetMembershipLog(int eventId, int? itemId, DateTime? StartDate)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("EventID", eventId.ToString()));
			if (itemId.HasValue) payload.Add(new DataItem("ItemID", itemId.ToString()));
			if (StartDate.HasValue) payload.Add(new DataItem("StartDate", StartDate.Value.ToString()));
			
			return ExecuteMethodSA(API_METHOD_SA_GROUPS_GROUP_GETMEMBERSHIPLOG, payload);
		}

		public ApiResponse SaGroupsGroupTypesGet()
		{
			return ExecuteMethodSA(API_METHOD_SA_GROUPS_GROUPTYPES_GET);
		}

        #endregion

        #region Sa.Member

        public ApiResponse SaMemberCertificationsGet(string ID, bool? IsArchived)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", ID));

            if (IsArchived.HasValue)
                payload.Add(new DataItem("IsArchived", IsArchived == true ? "1" : "0"));

            return ExecuteMethodSA(API_METHOD_SA_MEMBER_CERTIFICATIONS_GET, payload);
        }

        public ApiResponse SaMemberCertificationsJournalGet(string ID, bool? ShowExpired, System.DateTime? StartDate, int? EntryID, string CertificationID, int? PageSize, int? PageNumber)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", ID));

            if (ShowExpired.HasValue)
                payload.Add(new DataItem("ShowExpired", ShowExpired == true ? "1" : "0"));

            if (StartDate.HasValue)
                payload.Add(new DataItem("StartDate", StartDate.Value.ToString(SQL_DATETIME_FORMAT)));

            if (EntryID.HasValue)
                payload.Add(new DataItem("EntryID", EntryID.Value.ToString()));

            if (!string.IsNullOrEmpty(CertificationID))
                payload.Add(new DataItem("CertificationID", CertificationID));

            if (PageSize.HasValue)
                payload.Add(new DataItem("PageSize", PageSize.Value.ToString()));

            if (PageNumber.HasValue)
                payload.Add(new DataItem("PageNumber", PageNumber.Value.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_MEMBER_CERTIFICATIONS_JOURNAL_GET, payload);
        }


        #endregion

        #region Sa.Members
		
        public ApiResponse SaMembersAllGetIDs()
        {
            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_ALL_GETIDS);
        }

        /// <summary>
        /// Returns a list of member IDs.
        /// </summary>
        /// <param name="Timestamp">DateTime</param>
        /// <param name="WebsiteID">Int - Member's Website ID</param>
        /// <param name="GroupList">Comma Delimited List of Group Codes and/or Group Names (i.e. "1998,1999,Athletics: Baseball")</param>
        /// <returns></returns>
        public ApiResponse SaMembersAllGetIDs(System.DateTime? Timestamp, int? WebsiteID, string GroupList)
        {
            //build a DataItemCollection based on a CDL
            DataItemCollection groups = new DataItemCollection();

            if (!string.IsNullOrEmpty(GroupList))
            {
                string[] groupsarray = GroupList.Split(new char[] { ',' });
                foreach (string group in groupsarray)
                {
                    if (group.Contains(": "))
                    {
                        //group name
                        groups.Add(new DataItem("Name", group.Trim()));
                    }
                    else
                    {
                        //group code
                        groups.Add(new DataItem("Code", group.Trim()));
                    }
                }
            }

            return SaMembersAllGetIDs(Timestamp, WebsiteID, groups);
        }

        public ApiResponse SaMembersAllGetIDs(System.DateTime? Timestamp, int? WebsiteID, DataItemCollection Groups)
        {
            DataItemCollection payload = new DataItemCollection();

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            if (WebsiteID.HasValue)
                payload.Add(new DataItem("WebsiteID", WebsiteID.Value.ToString()));

            if (Groups != null && Groups.Count > 0)
            {
                DataItem g = new DataItem();
                g.Name = "Groups";
                g.Items = Groups;
                payload.Add(g);
            }

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_ALL_GETIDS, payload);
        }

		public ApiResponse SaMembersAllMemberTypesGet()
		{
			return ExecuteMethodSA(API_METHOD_SA_MEMBERS_ALL_MEMBERTYPES_GET);
		}

        public ApiResponse SaMembersAllRecentActivity()
        {
            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_ALL_RECENTACTIVITY);
        }

        public ApiResponse SaMembersCertificationsJournalEntryCreate(string ID, int CEUsEarned, System.DateTime CEUsExpireDate, string CertificationID, string CertificationName, string Description, System.DateTime? EntryDate, decimal? ScorePercent)
        {
            return SaMembersCertificationsJournalEntryCreate(ID, CEUsEarned, CEUsExpireDate, CertificationID, CertificationName, Description, EntryDate, ScorePercent, string.Empty);
        }

        public ApiResponse SaMembersCertificationsJournalEntryCreate(string ID, int CEUsEarned, System.DateTime CEUsExpireDate, string CertificationID, string CertificationName, string Description, System.DateTime? EntryDate, decimal? ScorePercent, string CreditTypeCode)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", ID));

            payload.Add(new DataItem("CEUsEarned", CEUsEarned.ToString()));

            payload.Add(new DataItem("CEUsExpireDate", CEUsExpireDate.ToString(SQL_DATETIME_FORMAT)));

            if (!string.IsNullOrEmpty(CertificationID))
                payload.Add(new DataItem("CertificationID", CertificationID));

            if (!string.IsNullOrEmpty(CertificationName))
                payload.Add(new DataItem("CertificationName", CertificationName));

            payload.Add(new DataItem("Description", Description));

            if (EntryDate.HasValue)
                payload.Add(new DataItem("EntryDate", EntryDate.Value.ToString(SQL_DATETIME_FORMAT)));

            if (ScorePercent.HasValue)
                payload.Add(new DataItem("ScorePercent", ScorePercent.Value.ToString()));

            payload.Add(new DataItem("CreditTypeCode", CreditTypeCode));

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_CERTIFICATIONS_JOURNALENTRY_CREATE, payload);
        }

        public ApiResponse SaMembersCommerceStoreGetOrderIDs(string MemberGUID, System.DateTime? Timestamp, int? Status)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", MemberGUID));

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            if (Status.HasValue)
                payload.Add(new DataItem("Status", Status.Value.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_COMMERCE_STORE_GETORDERIDS, payload);
        }

        public ApiResponse SaMembersEventsEventRegistrationGet(string MemberGuid, int EventID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", MemberGuid));
            payload.Add(new DataItem("EventID", EventID.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_EVENTS_EVENT_REGISTRATION_GET, payload);
        }

		public ApiResponse SaMembersGroupsAdd(string memberGuid, string groupCode, int? addAsMember, int? isPrimaryGroup, int? addAsGroupAdmin, string addGroupRepTitle)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("ID", memberGuid));
			payload.Add(new DataItem("GroupCode", groupCode));

			if (addAsMember.HasValue)
			{
				payload.Add(new DataItem("AddAsMember", addAsMember.ToString()));

				if (isPrimaryGroup.HasValue)
					payload.Add(new DataItem("IsPrimaryGroup", isPrimaryGroup.ToString()));
			}

			if (addAsGroupAdmin.HasValue)
				payload.Add(new DataItem("AddAsGroupAdmin", addAsGroupAdmin.ToString()));

			if (!string.IsNullOrEmpty(addGroupRepTitle))
				payload.Add(new DataItem("AddGroupRepTitle", addGroupRepTitle));

			return ExecuteMethodSA(API_METHOD_SA_MEMBERS_GROUPS_ADD, payload);
		}

		public ApiResponse SaMembersGroupsRemove(string memberGuid, string groupCode, int? removeAsMember, int? removeAsGroupAdmin, string removeGroupRepTitle)
		{
			DataItemCollection payload = new DataItemCollection();
			payload.Add(new DataItem("ID", memberGuid));
			payload.Add(new DataItem("GroupCode", groupCode));

			if (removeAsMember.HasValue)
				payload.Add(new DataItem("RemoveAsMember", removeAsMember.ToString()));

			if (removeAsGroupAdmin.HasValue)
				payload.Add(new DataItem("RemoveAsGroupAdmin", removeAsGroupAdmin.ToString()));

			if (!string.IsNullOrEmpty(removeGroupRepTitle)) 
				payload.Add(new DataItem("RemoveGroupRepTitle", removeGroupRepTitle));

			return ExecuteMethodSA(API_METHOD_SA_MEMBERS_GROUPS_REMOVE, payload);
		}

        public ApiResponse SaMembersProfileCreate(YMSDK.Entities.MemberProfile Person)
        {
            return SaMembersProfileCreate(Person.Serialize());
        }

        public ApiResponse SaMembersProfileCreate(DataItem Person)
        {
            DataItemCollection payload = new DataItemCollection();

            foreach (DataItem item in Person.Items)
            {
                payload.Add(item);
            }

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_PROFILE_CREATE, payload);
        }

        public ApiResponse SaMembersReferralsGet(string MemberGuid, System.DateTime? Timestamp)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", MemberGuid));

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_REFERRALS_GET, payload);
        }

        public ApiResponse SaMembersSubAccountsGet(string MemberGuid, System.DateTime? Timestamp)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", MemberGuid));

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            return ExecuteMethodSA(API_METHOD_SA_MEMBERS_SUBACCOUNT_GET, payload);
        }

        #endregion

        #region Sa.NonMembers

        public ApiResponse SaNonMembersAllGetIDs(System.DateTime Timestamp, int WebsiteID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("Timestamp", Timestamp.ToString(SQL_DATETIME_FORMAT)));
            payload.Add(new DataItem("WebsiteID", WebsiteID.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_NONMEMBERS_ALL_GETIDS, payload);
        }

        public ApiResponse SaNonMembersProfileCreate(DataItem Nonmember)
        {
            DataItemCollection payload = new DataItemCollection();

            foreach (DataItem item in Nonmember.Items)
            {
                payload.Add(item);
            }

            return ExecuteMethodSA(API_METHOD_SA_NONMEMBERS_PROFILE_CREATE, payload);
        }

        #endregion

        #region Sa.People

        public ApiResponse SaPeopleAllGetIDs(System.DateTime? Timestamp, int? WebsiteID)
        {
            return SaPeopleAllGetIDs(Timestamp, WebsiteID, string.Empty);
        }

        /// <summary>
        /// Returns a list of member and non-member IDs that may be optionally filtered by timestamp.
        /// </summary>
        /// <param name="Timestamp">DateTime</param>
        /// <param name="WebsiteID">Int - Member's Website ID</param>
        /// <param name="GroupList">Comma Delimited List of Group Codes and/or Group Names (i.e. "1998,1999,Athletics: Baseball")</param>
        /// <returns></returns>
        public ApiResponse SaPeopleAllGetIDs(System.DateTime? Timestamp, int? WebsiteID, string GroupList)
        {
            //build a DataItemCollection based on a CDL
            DataItemCollection groups = new DataItemCollection();

            if (!string.IsNullOrEmpty(GroupList))
            {
                string[] groupsarray = GroupList.Split(new char[] { ',' });
                foreach (string group in groupsarray)
                {
                    if (group.Contains(": "))
                    {
                        //group name
                        groups.Add(new DataItem("Name", group.Trim()));
                    }
                    else
                    {
                        //group code
                        groups.Add(new DataItem("Code", group.Trim()));
                    }
                }
            }

            return SaPeopleAllGetIDs(Timestamp, WebsiteID, groups);
        }

        public ApiResponse SaPeopleAllGetIDs(System.DateTime? Timestamp, int? WebsiteID, DataItemCollection Groups)
        {
            DataItemCollection payload = new DataItemCollection();

            if (Timestamp.HasValue)
                payload.Add(new DataItem("Timestamp", Timestamp.Value.ToString(SQL_DATETIME_FORMAT)));

            if (WebsiteID.HasValue)
                payload.Add(new DataItem("WebsiteID", WebsiteID.Value.ToString()));

            if (Groups != null && Groups.Count > 0)
            {
                DataItem g = new DataItem();
                g.Name = "Groups";
                g.Items = Groups;
                payload.Add(g);
            }

            return ExecuteMethodSA(API_METHOD_SA_PEOPLE_ALL_GETIDS, payload);
        }

        public ApiResponse SaPeopleProfileFindID(string ImportID, string ConstituentID, int? WebsiteID)
        {
            return SaPeopleProfileFindID(ImportID, ConstituentID, WebsiteID, "", "");
        }

        public ApiResponse SaPeopleProfileFindID(string ImportID, string ConstituentID, int? WebsiteID, string Username, string Email)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ImportID", ImportID.ToString()));
            payload.Add(new DataItem("ConstituentID", ConstituentID));
            if (WebsiteID.HasValue)
                payload.Add(new DataItem("WebsiteID", WebsiteID.Value.ToString()));
            payload.Add(new DataItem("Username", Username.ToString()));
            payload.Add(new DataItem("Email", Email.ToString()));

            return ExecuteMethodSA(API_METHOD_SA_PEOPLE_PROFILE_FINDID, payload);
        }

        public ApiResponse SaPeopleProfileGet(string ID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", ID));

            return ExecuteMethodSA(API_METHOD_SA_PEOPLE_PROFILE_GET, payload);
        }

        public ApiResponse SaPeopleProfileGroupsGet(string ID)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.Add(new DataItem("ID", ID));

            return ExecuteMethodSA(API_METHOD_SA_PEOPLE_GROUPS_GET, payload);
        }

        public ApiResponse SaPeopleProfileUpdate(DataItem person)
        {
            DataItemCollection payload = new DataItemCollection();
            payload.AddRange(person.Items);

            return ExecuteMethodSA(API_METHOD_SA_PEOPLE_PROFILE_UPDATE, payload);
        }

        #endregion

        #region Session

        public ApiResponse SessionCreate()
        {
            ApiResponse response = ExecuteMethod(API_METHOD_SESSION_CREATE);

            if (response.ErrorCode.Equals(ApiErrorCode.NoError))
            {
                this.sessionID = response.MethodResults.Items[0].Value;
            }

            return response;
        }

        public ApiResponse SessionPing()
        {
            return ExecuteMethod(API_METHOD_SESSION_PING);
        }

        public ApiResponse SessionAbandon()
        {
            ApiResponse response = ExecuteMethod(API_METHOD_SESSION_ABANDON);

            //Clear local SessionID var
            this.sessionID = string.Empty;

            return response;
        }

        #endregion
    }
}
