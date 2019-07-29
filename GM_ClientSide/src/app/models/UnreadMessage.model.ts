export class UnreadMessage {
    conversationId: string;
    amount: number;

    constructor(conversationId: string, amount: number) {
        this.conversationId = conversationId;
        this.amount = amount;
    }
}