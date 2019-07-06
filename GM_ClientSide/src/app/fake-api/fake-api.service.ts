import { InMemoryDbService } from 'angular-in-memory-web-api';

import { ChatFakeDb } from 'app/fake-api/chat';
import { ContactsFakeDb } from 'app/fake-api/contacts';
import { ChatPanelFakeDb } from 'app/fake-api/chat-panel';

export class FakeDbService implements InMemoryDbService
{
    createDb(): any
    {
        return {

            // Chat
            'chat-contacts': ChatFakeDb.contacts,
            'chat-chats'   : ChatFakeDb.chats,
            'chat-user'    : ChatFakeDb.user,

            // Contacts
            'contacts-contacts': ContactsFakeDb.contacts,
            'contacts-user'    : ContactsFakeDb.user,

            // Chat Panel
            'chat-panel-contacts' : ChatPanelFakeDb.contacts,
            'chat-panel-chats': ChatPanelFakeDb.chats,
            'chat-panel-user': ChatPanelFakeDb.user,

        };
    }
}
