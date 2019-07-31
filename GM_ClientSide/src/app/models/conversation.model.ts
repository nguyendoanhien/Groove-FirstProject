export interface ContactLatestChatListModel {
    ConvId: string;

    ContactId: string;

    DisplayName: string;

    Unread: string;

    LastMessage: string;

    LastMessageTime: Date | string | null;
}