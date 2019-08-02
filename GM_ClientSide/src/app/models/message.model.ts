export class MessageModel {
    fromConv: string;
    fromSender: string;
    messageId: string;
    payload: string;
    senderAvatar: string;
    senderName: string;
    time: Date;

    constructor(fromConv: string, fromSender: string, messageId: string, payload: string, time: Date, senderAvatar: string, senderName: string) {
        this.fromConv = fromConv;
        this.fromSender = fromSender;
        this.messageId = messageId;
        this.payload = payload;
        this.time = time;
        this.senderAvatar = senderAvatar;
        this.senderName = senderName;
    }
}