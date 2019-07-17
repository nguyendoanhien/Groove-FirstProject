export class User {
    id: string;
    userId: number;
    displayName: string;
    mood: string;
    status: Status;
    chatList: any[];
}
export enum Status {
    NotSet,
    Online,
    Offline
}