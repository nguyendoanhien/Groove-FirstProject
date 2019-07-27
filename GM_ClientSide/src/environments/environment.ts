// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
'https://localhost:44383/api/Conversation/dialogs/4f592118-ed4e-432f-bc7c-6bb3b4ff299a'
const backendUrl = 'https://localhost:44383';
const authBaseUrl = backendUrl + '/Identity/ClientAccount';
const apiContactUrl = backendUrl + '/api/contact';
const apiConvUrl = backendUrl + '/api/conversation';
const apiMessageUrl = backendUrl + '/api/message';
const apiUserUrl = backendUrl + '/api/user'
const apiBaseUrl = backendUrl + '/api';
const messageHubUrl = backendUrl + '/messagehub';
export const environment = {
    production: false,
    hmr: false,
    appName: 'Groove Messenger Client',
    backendUrl: backendUrl,
    authBaseUrl: authBaseUrl,
    apiContactUrl: apiContactUrl,
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
    apiGetContactChatList: apiContactUrl + '/getchatlist',
    apiGetChatListByConvId: apiConvUrl + '/dialog/',
    apiGetChatListByUserId: apiConvUrl + '/dialogs/',
    apiConvUrl: apiConvUrl,
    apiGetUser: apiUserUrl,
    apiMessageUrl: apiMessageUrl,
    messageHubUrl: messageHubUrl,
    apiUnreadMessage: apiMessageUrl + '/unread/',
    apiReadMessage: apiMessageUrl + '/read/',
    applicationGoogle: {
        clientId: '687824117544-nvc2uojbm14hc330gl8qh3lsrtl3tc4a.apps.googleusercontent.com',
        clientSecret: 'ugBBsDjYS-Rz20RVlx9r7Blo'
    },
    applicationFacebook: {
        appId: '354060818601401',
        appSecreta: 'db1a4f4128e269fa6ca6d47e72cdd0a6'
    },
    openGrapth:
    {
        appId: '1cc1af51-54ef-4c2f-a54d-5d2c3b96d7cb'
    },
    apiOpenGrapthUrl: 'https://opengraph.io/api/1.1/site',
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
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
