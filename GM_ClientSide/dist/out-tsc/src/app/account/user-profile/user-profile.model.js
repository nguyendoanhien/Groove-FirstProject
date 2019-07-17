export class UserProfileModel {
    isLogged() {
        return this.SecurityAccessToken.length > 0;
    }
    constructor() {
        this.DisplayName = 'Guest';
        this.UserName = 'Guest';
        this.SecurityAccessToken = '';
    }
}
//# sourceMappingURL=user-profile.model.js.map