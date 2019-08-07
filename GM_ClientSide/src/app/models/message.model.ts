export class MessageModel {
    fromConv: string;
    fromSender: string;
    messageId: string;
    payload: string;
    time: Date;
    senderAvatar: string;
    senderName: string;
    type: string;

    constructor(fromConv: string, fromSender: string, messageId: string, payload: string, time: Date, senderAvatar: string, senderName: string, type: string) {
        this.fromConv = fromConv;
        this.fromSender = fromSender;
        this.messageId = messageId;
        this.payload = payload;
        this.time = time;
        this.senderAvatar = senderAvatar;
        this.senderName = senderName;
        this.type = type;
    }
}