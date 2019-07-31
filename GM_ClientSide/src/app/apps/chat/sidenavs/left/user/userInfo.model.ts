import { environment } from "environments/environment";

export class UserInfo {
    id: string;
    userId: string;
    displayName: string;
    mood: string;
    status: string;
    avatar: string;

    constructor() {
        this.avatar = environment.backendUrl + "/Images/avatar.png";
    }
}