// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const backendUrl = 'https://localhost:44383';
const authBaseUrl = backendUrl + '/Identity/ClientAccount';


export const environment = {
    production: false,
    hmr: false,
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
    apigetUserInfo: authBaseUrl + '/user',
    apiUrl: backendUrl + '/api/notes',
    applicationGoogle: {
        clientId: '687824117544-nvc2uojbm14hc330gl8qh3lsrtl3tc4a.apps.googleusercontent.com',
        clientSecret: 'ugBBsDjYS-Rz20RVlx9r7Blo'
      },
      applicationFacebook: {
        appId: '354060818601401',
        appSecreta: 'db1a4f4128e269fa6ca6d47e72cdd0a6'
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
