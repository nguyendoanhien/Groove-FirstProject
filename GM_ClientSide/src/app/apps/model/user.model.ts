export class User {
    id: string;
    userId: number;
    displayName: string;
    mood: Mood;
    status: Status;
    chatList: any[];
}
enum Mood {
    Up = 1,
    Down,
    Left,
    Right,
}
enum Status {
    Up = 1,
    Down,
    Left,
    Right,
}