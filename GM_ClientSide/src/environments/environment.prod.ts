const backendUrl = 'https://groovemessengerapi.azurewebsites.net';
const authBaseUrl = backendUrl + '/Identity/ClientAccount';
const apiContactUrl = backendUrl + '/api/contact';
const apiConvUrl = backendUrl + '/api/conversation';
const apiBaseUrl = backendUrl + '/api';
const apiMessageUrl = backendUrl + '/api/message';
const messageHubUrl = backendUrl + '/messagehub';
const apiConversationtUrl = backendUrl + '/api/Conversation';

export const environment = {
  production: true,
  hmr: false,
  appName: 'Groove Messenger Client',
  backendUrl: backendUrl,
  authBaseUrl: authBaseUrl,
  apiContactUrl:apiContactUrl,
  authLoginUrl: authBaseUrl + '/login',
  authRegisterUrl: authBaseUrl + '/register',
  authConfirmEmailUrl: authBaseUrl + '/confirmemail',
  authGoogleUrl: authBaseUrl + '/logingoogle',
  authFacebookUrl: authBaseUrl + '/loginfacebook',
  authResetPasswordUrl: authBaseUrl + '/resetpassword',
  authForgotPasswordUrl: authBaseUrl + '/forgotpassword',
  apigetUserInfo: authBaseUrl + '/user',
  apiContactGetAllInformUrl: apiContactUrl + '/getallcontactinform',
  apiContactGetAllUnknownInformUrl: apiContactUrl + '/getallunknowncontactinform',
  apiGetContactChatList: apiContactUrl+'/getchatlist',
  apiGetChatListByConvId: apiConvUrl +'/dialog/',
  apiGetChatListByUserId:apiConvUrl+'/dialogs/',
  apiConvUrl:apiConvUrl,
  apiMessageUrl: apiMessageUrl,
  messageHubUrl : messageHubUrl,
  apiCreateNewConversationUrl:apiConversationtUrl,

  applicationGoogle: {
    clientId: '790976332784-alnl6bf2ofphpi9t6elcuhbkebifo787.apps.googleusercontent.com',
    clientSecret: 'vIlgfzy_LuUUn6I73QSYp1Cf'
  },
  applicationFacebook: {
    appId: '459900294839907',
    appSecreta: 'b473ce1cdf6e6b9b4f8ed69087885175'
  },
  apiUserUrl: apiBaseUrl + '/user',
  apiUserContactUrl: apiBaseUrl + '/usercontact',
  apiUrl: apiBaseUrl + '/notes',
  cloudinary: {
    url: 'https://api.cloudinary.com/v1_1/groovemessenger/upload',
    upload_preset: 'qlbjv3if'
  },
  hub: {
    profileUrl: backendUrl + '/profileHub'
  }
};

