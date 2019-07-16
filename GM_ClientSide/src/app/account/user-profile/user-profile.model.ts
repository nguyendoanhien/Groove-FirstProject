export class UserProfileModel {
    UserName: string;
    DisplayName: string;
    SecurityAccessToken: string;

    isLogged(): boolean {
        return this.SecurityAccessToken.length > 0;
    }

    constructor() {
        this.DisplayName = 'Guest';
        this.UserName = 'Guest';
        this.SecurityAccessToken = '';
    }
}


