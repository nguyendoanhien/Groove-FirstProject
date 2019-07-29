import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { BehaviorSubject, Observable, Subject } from "rxjs";

import { User } from "../model/user.model";
import { environment } from "environments/environment";
import { UserInfoService } from "app/core/account/userInfo.service";
import { UserContactService } from "app/core/account/user-contact.service";
import { MessageHubService } from "../../core/data-api/hubs/message.hub";
import { UserProfileService } from "app/core/identity/userprofile.service";
import { ContactHubService } from "app/core/data-api/hubs/contact.hub";

@Injectable()
export class ChatService implements Resolve<any> {
    contacts: any[];
    unknownContacts: any[];
    chats: any[];
    user: User;
    onChatSelected: BehaviorSubject<any>;
    onContactSelected: BehaviorSubject<any>;
    onChatsUpdated: Subject<any>;
    onUserUpdated: Subject<any>;
    onLeftSidenavViewChanged: Subject<any>;
    onRightSidenavViewChanged: Subject<any>;
    _userContactService: UserContactService;
    _messageHub: MessageHubService;
    _contactHub: ContactHubService;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     * @param {UserInfoService} _userInformList
     * @param {MessageHubService} _messageHubService
     * @param {UserProfileService} _userProfileService
     */
    constructor(private _httpClient: HttpClient,
        userContactService: UserContactService,
        private _userInformList: UserInfoService,
        private _messageHubService: MessageHubService,
        private _userProfileService: UserProfileService,
        private _contactHubService: ContactHubService
    ) {
        // Set the defaults
        this.onChatSelected = new BehaviorSubject(null);
        this.onContactSelected = new BehaviorSubject(null);
        this.onChatsUpdated = new Subject();
        this.onUserUpdated = new Subject();
        this.onLeftSidenavViewChanged = new Subject();
        this.onRightSidenavViewChanged = new Subject();
        this.onRightSidenavViewChanged = new Subject();
        this._userContactService = userContactService;
        this._messageHub = _messageHubService;
        this._contactHub = _contactHubService;
        this._contactHub.newContact.subscribe((res: any) => {
            if (res) {
                this.chats.push(res.dialog);
                this.user.chatList.push(res.chatContact);
                this.contacts.push(res.contact);
            }
        });

    }

    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
        return new Promise((resolve, reject) => {
            Promise.all([
                this.getContacts(),
                this.getUnknownContacts(),
                this.getChats(),
                this.getUser(),
                this.getChatList()
            ]).then(
                ([contacts, unknownContacts, chats, user, chatList]) => {
                    this.contacts = contacts;
                    this.unknownContacts = unknownContacts;
                    this.chats = (chats === null ? [] : chats);
                    this.user = user;
                    this.user.chatList = chatList;
                    resolve();
                },
                reject
            );
        });
    }

    /**
     * Get chat
     *
     * @param contactId
     * @returns {Promise<any>}
     */
    getChat(contactId): Promise<any> {
        const chatItem = this.user.chatList.find((item) => {
            return item.contactId === contactId;
        });

        return new Promise((resolve, reject) => {
            if (!chatItem) {
                const unknowContact = this.unknownContacts.find((unknowContact) => {
                    return unknowContact.userId === contactId;
                });
                const chatData = {
                    chatId: unknowContact.userId, // this is not id of conversation
                    dialog: null,
                    contact: unknowContact
                };

                this.onChatSelected.next({ ...chatData });
            } else {
                this._httpClient.get(environment.apiGetChatListByConvId + chatItem.convId)
                    .subscribe((response: any) => {
                            const chat = response;
                            const chatContact = this.contacts.concat(this.unknownContacts).find((contact) => {
                                return contact.userId === contactId;
                            });

                            const chatData = {
                                // <<<<<<< HEAD
                                //                         chatId: chat.userId,
                                // =======
                                chatId: chat.id, // This is id of conversation
                                dialog: chat.dialog,
                                contact: chatContact
                            };

                            this.onChatSelected.next({ ...chatData });

                        },
                        reject);
            }
        });

    }

    /**
     * Select contact
     *
     * @param contact
     */
    selectContact(contact): void {
        this.onContactSelected.next(contact);
    }

    /**
     * Set user status
     *
     * @param status
     */
    setUserStatus(status): void {
        this.user.status = status;
    }

    /**
     * Update user data
     *
     * @param userData
     */
    updateUserData(userData): void {
        this._httpClient.post(`api/chat-user/${this.user.id}`, userData)
            .subscribe((response: any) => {
                    this.user = userData;
                }
            );
    }

    /**
     * Update the chat dialog
     *
     * @param chatId
     * @param dialog
     * @returns {Promise<any>}
     */
    updateDialog(chatId, dialog): Promise<any> {
        return new Promise((resolve, reject) => {

            const newData = {
                id: chatId,
                dialog: dialog
            };

            this._httpClient.post(`api/chat-chats/${chatId}`, newData)
                .subscribe(updatedChat => {
                        resolve(updatedChat);
                    },
                    reject);
        });
    }

    /**
     * Get contacts
     *
     * @returns {Promise<any>}
     */
    getContacts(): Promise<any> {
        return this._userContactService.getContacts().toPromise();


    }

    getUnknownContacts(displayNameSearch?: string): Promise<any> {
        return this._userContactService.getUnknownContacts(displayNameSearch).toPromise();

    }


    /**
     * Get chats
     *
     * @returns {Promise<any>}
     */
    getChats(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._httpClient
                .get(environment.apiGetChatListByUserId +
                    this._userProfileService.userProfile.UserId) // using static user id to test
                .subscribe((response: any) => {
                        resolve(response);
                    },
                    reject);
        });
    }

    /**
     * Get user
     *
     * @returns {Promise<any>}
     */
    getUser(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._httpClient.get(environment.apiUserUrl)
                .subscribe((response: any) => {
                        resolve(response);
                    },
                    reject);
        });
    }

    /**
     * Get chat list
     *
     * @returns {Promise<any>}
     */
    getChatList(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._httpClient.get(environment.apiGetContactChatList)
                .subscribe((response: any) => {
                        resolve(response);
                    },
                    reject);
        });
    }
}