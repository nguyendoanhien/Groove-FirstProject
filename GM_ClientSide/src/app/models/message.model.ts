export class MessageModel {
    fromConv: string;
    fromSender: string;
    messageId: string;
    payload: string;
    time: Date;

    constructor(fromConv: string, fromSender: string, messageId: string, payload: string, time: Date) {
        this.fromConv = fromConv;
        this.fromSender = fromSender;
        this.messageId = messageId;
        this.payload = payload;
        this.time = time;
    }
}