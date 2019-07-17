import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Subject } from 'rxjs';
import { FuseUtils } from '@fuse/utils';
let ChatService = class ChatService {
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(_httpClient) {
        this._httpClient = _httpClient;
        // Set the defaults
        this.onChatSelected = new BehaviorSubject(null);
        this.onContactSelected = new BehaviorSubject(null);
        this.onChatsUpdated = new Subject();
        this.onUserUpdated = new Subject();
        this.onLeftSidenavViewChanged = new Subject();
        this.onRightSidenavViewChanged = new Subject();
    }
    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route, state) {
        return new Promise((resolve, reject) => {
            Promise.all([
                this.getContacts(),
                this.getChats(),
                this.getUser()
            ]).then(([contacts, chats, user]) => {
                this.contacts = contacts;
                this.chats = chats;
                this.user = user;
                resolve();
            }, reject);
        });
    }
    /**
     * Get chat
     *
     * @param contactId
     * @returns {Promise<any>}
     */
    getChat(contactId) {
        const chatItem = this.user.chatList.find((item) => {
            return item.contactId === contactId;
        });
        // Create new chat, if it's not created yet.
        if (!chatItem) {
            this.createNewChat(contactId).then((newChats) => {
                this.getChat(contactId);
            });
            return;
        }
        return new Promise((resolve, reject) => {
            this._httpClient.get('api/chat-chats/' + chatItem.id)
                .subscribe((response) => {
                const chat = response;
                const chatContact = this.contacts.find((contact) => {
                    return contact.id === contactId;
                });
                const chatData = {
                    chatId: chat.id,
                    dialog: chat.dialog,
                    contact: chatContact
                };
                this.onChatSelected.next(Object.assign({}, chatData));
            }, reject);
        });
    }
    /**
     * Create new chat
     *
     * @param contactId
     * @returns {Promise<any>}
     */
    createNewChat(contactId) {
        return new Promise((resolve, reject) => {
            const contact = this.contacts.find((item) => {
                return item.id === contactId;
            });
            const chatId = FuseUtils.generateGUID();
            const chat = {
                id: chatId,
                dialog: []
            };
            const chatListItem = {
                contactId: contactId,
                id: chatId,
                lastMessageTime: '2017-02-18T10:30:18.931Z',
                name: contact.name,
                unread: null
            };
            // Add new chat list item to the user's chat list
            this.user.chatList.push(chatListItem);
            // Post the created chat
            this._httpClient.post('api/chat-chats', Object.assign({}, chat))
                .subscribe((response) => {
                // Post the new the user data
                this._httpClient.post('api/chat-user/' + this.user.id, this.user)
                    .subscribe(newUserData => {
                    // Update the user data from server
                    this.getUser().then(updatedUser => {
                        this.onUserUpdated.next(updatedUser);
                        resolve(updatedUser);
                    });
                });
            }, reject);
        });
    }
    /**
     * Select contact
     *
     * @param contact
     */
    selectContact(contact) {
        this.onContactSelected.next(contact);
    }
    /**
     * Set user status
     *
     * @param status
     */
    setUserStatus(status) {
        this.user.status = status;
    }
    /**
     * Update user data
     *
     * @param userData
     */
    updateUserData(userData) {
        this._httpClient.post('api/chat-user/' + this.user.id, userData)
            .subscribe((response) => {
            this.user = userData;
        });
    }
    /**
     * Update the chat dialog
     *
     * @param chatId
     * @param dialog
     * @returns {Promise<any>}
     */
    updateDialog(chatId, dialog) {
        return new Promise((resolve, reject) => {
            const newData = {
                id: chatId,
                dialog: dialog
            };
            this._httpClient.post('api/chat-chats/' + chatId, newData)
                .subscribe(updatedChat => {
                resolve(updatedChat);
            }, reject);
        });
    }
    /**
     * Get contacts
     *
     * @returns {Promise<any>}
     */
    getContacts() {
        return new Promise((resolve, reject) => {
            this._httpClient.get('api/chat-contacts')
                .subscribe((response) => {
                resolve(response);
            }, reject);
        });
    }
    /**
     * Get chats
     *
     * @returns {Promise<any>}
     */
    getChats() {
        return new Promise((resolve, reject) => {
            this._httpClient.get('api/chat-chats')
                .subscribe((response) => {
                resolve(response);
            }, reject);
        });
    }
    /**
     * Get user
     *
     * @returns {Promise<any>}
     */
    getUser() {
        return new Promise((resolve, reject) => {
            this._httpClient.get('api/chat-user')
                .subscribe((response) => {
                resolve(response[0]);
            }, reject);
        });
    }
};
ChatService = tslib_1.__decorate([
    Injectable(),
    tslib_1.__metadata("design:paramtypes", [HttpClient])
], ChatService);
export { ChatService };
//# sourceMappingURL=chat.service.js.map