export class IndexMessageModel{
    id: string;
    conversationId: string;
    senderId: string;
    seenBy: string;
    content: string;
    type: string;
    createdOn : Date;
    public constructor(conversationId: string,senderId: string,seenBy: string,content: string,  type: string){
        this.conversationId = conversationId;
        this.senderId = senderId;
        this.seenBy = seenBy;
        this.content = content;
        this.type = type;
    }
}