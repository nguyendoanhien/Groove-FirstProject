const backendUrl = "https://groovemessengerapi.azurewebsites.net";
const authBaseUrl = backendUrl + "/Identity/ClientAccount";
const apiContactUrl = backendUrl + "/api/contact";
const apiConvUrl = backendUrl + "/api/conversation";
const apiBaseUrl = backendUrl + "/api";
const apiMessageUrl = backendUrl + "/api/message";
const apiConversationtUrl = backendUrl + "/api/Conversation";

export const environment = {
    production: true,
    hmr: false,
    appName: "Groove Messenger Client",
    backendUrl: backendUrl,
    authBaseUrl: authBaseUrl,
    apiContactUrl: apiContactUrl,
    authLoginUrl: authBaseUrl + "/login",
    authRegisterUrl: authBaseUrl + "/register",
    authConfirmEmailUrl: authBaseUrl + "/confirmemail",
    authGoogleUrl: authBaseUrl + "/logingoogle",
    authFacebookUrl: authBaseUrl + "/loginfacebook",
    authResetPasswordUrl: authBaseUrl + "/resetpassword",
    authForgotPasswordUrl: authBaseUrl + "/forgotpassword",
    authNotificationUrl: authBaseUrl + "/notification",
    apigetUserInfo: authBaseUrl + "/user",
    apiContactGetAllInformUrl: apiContactUrl + "/getallcontactinform",
    apiContactGetAllUnknownInformUrl: apiContactUrl + "/getallunknowncontactinform",
    apiGetContactChatList: apiContactUrl + "/getchatlist",
    apiGetChatListByConvId: apiConvUrl + "/dialog/",
    apiGetChatListByUserId: apiConvUrl + "/dialogs/",
    apiConvUrl: apiConvUrl,
    apiMessageUrl: apiMessageUrl,
    apiCreateNewConversationUrl: apiConversationtUrl,
    apiUnreadMessage: apiMessageUrl + "/unread/",
    apiReadMessage: apiMessageUrl + "/read/",
    applicationGoogle: {
        clientId: "790976332784-alnl6bf2ofphpi9t6elcuhbkebifo787.apps.googleusercontent.com",
        clientSecret: "vIlgfzy_LuUUn6I73QSYp1Cf"
    },
    applicationFacebook: {
        appId: "459900294839907",
        appSecreta: "b473ce1cdf6e6b9b4f8ed69087885175",
        access_token:
            "EAAFCBDVSVbkBAEmGKIgPfakhNnfXnM42ug4xgLN6uGGyIx8OItdoGlrmHouagaZBI8e5ekzr7Dq2y2BMupi3jrxZAJlksFn85GUN7tQjWIQq1F6JpBehiB7niISRHdZBDRd8akzqsmxcXHcNy5qzLTzsIAwbgZAUZBikTItDYmaGTUvWSC5nY"
    },
    openGraph:
    {
        appId: "1cc1af51-54ef-4c2f-a54d-5d2c3b96d7cb",
        url: "https://opengraph.io/api/1.1/site"
    },
    apiUserUrl: apiBaseUrl + "/user",
    apiUserContactUrl: apiBaseUrl + "/usercontact",
    apiUrl: apiBaseUrl + "/notes",
    cloudinary: {
        url: "https://api.cloudinary.com/v1_1/groovemessenger/upload",
        upload_preset: "qlbjv3if"
    },
    hub: {
        profileUrl: backendUrl + "/profilehub",
        messageUrl: backendUrl + "/messagehub",
        contactUrl: backendUrl + "/contacthub",
        serverTimeoutInSeconds: 60
    }
};