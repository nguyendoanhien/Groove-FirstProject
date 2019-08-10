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
import { GroupService } from 'app/core/data-api/services/group.service';

@Injectable()
export class ChatService implements Resolve<any> {
    contacts: any[];
    unknownContacts: any[];
    chats: any[];
    user: User;
    isGroup: boolean;
    onChatSelected: BehaviorSubject<any>;
    onContactSelected: BehaviorSubject<any>;
    onChatGroupSelected: BehaviorSubject<any>;
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
        private _contactHubService: ContactHubService,
        private _groupService: GroupService
    ) {
        // Set the defaults
        this.onChatSelected = new BehaviorSubject(null);
        this.onContactSelected = new BehaviorSubject(null);
        this.onChatGroupSelected = new BehaviorSubject(null);

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
                this.getChatList(),
                this.getGroupChat()
            ]).then(
                ([contacts, unknownContacts, chats, user, chatList, groupChatList]) => {
                    this.contacts = contacts;
                    this.unknownContacts = unknownContacts;
                    this.chats = (chats === null ? [] : chats);
                    this.user = user;
                    this.user.chatList = chatList;
                    this.user.groupChatList = groupChatList;
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
        this.isGroup = false;
        return new Promise((resolve, reject) => {
            {
                if (!chatItem) {
                    const unknowContact = this.unknownContacts.find((unknowContact) => {
                        return unknowContact.userId === contactId;
                    });
                    const chatData = {
                        chatId: unknowContact.userId,
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
                                chatId: chat.id,
                                dialog: chat.dialog,
                                contact: chatContact
                            };
    
                            this.onChatSelected.next({ ...chatData });
    
                        },
                            reject);
                }
            }
        });

    }

    getChatOfGroupChat(groupchat: any) {

        var contact = { displayName: groupchat.name, avatar: groupchat.avatar, id: groupchat.id, members: groupchat.members };
        this._httpClient.get(environment.apiGetChatListByConvId + groupchat.id).subscribe((res: any) => {
            const chatData = {
                chatId: groupchat.id,
                dialog: res.dialog,
                contact: contact,
                isGroup: true
            }

            this.isGroup = true;
            this.onChatSelected.next({ ...chatData });
        })
    }

    /**
     * Select contact
     *
     * @param contact
     */
    getMoreChat(convId, createdOn: Date): any {
        
        const queryPath = createdOn === undefined ? null : `?CreatedOn=${createdOn}`;
        return this._httpClient.get(environment.apiGetChatListByConvId + convId + queryPath)
    }
    selectContact(contact): void {
        this.onContactSelected.next(contact);
    }

    selectChatGroup(contact): void {
        this.onChatGroupSelected.next(contact);
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

    getGroupChat(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._groupService.getGroupChat().subscribe((response: any) => {
                response.forEach(element => {
                    element.unreadMessage = element.unreadMessage > 99 ? '99+' : element.unreadMessage;
                });
                resolve(response);
            },
                reject);
        });
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