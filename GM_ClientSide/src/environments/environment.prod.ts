const backendUrl = 'https://groovemessengerapi.azurewebsites.net';
const authBaseUrl = backendUrl + '/Identity/ClientAccount';

export const environment = {
    production: true,
    hmr       : false,
    appName: 'Groove Messenger Client',
    backendUrl: backendUrl,
    authBaseUrl: authBaseUrl,
    authLoginUrl: authBaseUrl + '/login',
    authRegisterUrl: authBaseUrl + '/register',
    authConfirmEmailUrl: authBaseUrl + '/confirmemail',
    authGoogleUrl: authBaseUrl + '/logingoogle',
    authFacebookUrl: authBaseUrl + '/loginfacebook',
    authResetPasswordUrl: authBaseUrl + '/resetpassword',
    authForgotPasswordUrl: authBaseUrl + '/forgotpassword',
    apiUrl: backendUrl + '/api/notes',
    applicationGoogle: {
        clientId: '790976332784-alnl6bf2ofphpi9t6elcuhbkebifo787.apps.googleusercontent.com',
        clientSecret: 'vIlgfzy_LuUUn6I73QSYp1Cf'
      },
      applicationFacebook: {
        appId: '459900294839907',
        appSecreta: 'b473ce1cdf6e6b9b4f8ed69087885175'
      }
};

