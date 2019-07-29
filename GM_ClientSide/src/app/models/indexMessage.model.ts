export class IndexMessageModel {
    id: string;
    conversationId: string;
    senderId: string;
    seenBy: string;
    content: string;
    type: string;
    createdOn: Date;
    receiver: string;

    public constructor(conversationId: string,
        senderId: string,
        seenBy: string,
        content: string,
        type: string,
        receiver: string) {
        this.conversationId = conversationId;
        this.senderId = senderId;
        this.seenBy = seenBy;
        this.content = content;
        this.type = type;
        this.receiver = receiver;
    }
}