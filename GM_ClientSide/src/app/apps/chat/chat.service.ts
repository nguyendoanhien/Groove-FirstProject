import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { FuseUtils } from '@fuse/utils';
import { User } from '../model/user.model';
import { environment } from 'environments/environment';
import { UserInfoService } from 'app/core/account/userInfo.service';
import { UserContactService } from 'app/core/account/user-contact.service';
import { MessageHubService } from '../../core/data-api/hubs/message.hub';
import { MessageModel } from 'app/models/message.model';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { ProfileHubService } from 'app/core/data-api/hubs/profile.hub';

@Injectable()
export class ChatService implements Resolve<any>
{
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
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     * @param {UserInfoService} _userInformList
     * @param {MessageHubService} _messageHubService
     * @param {UserProfileService} _userProfileService
     */

    constructor(private _httpClient: HttpClient, userContactService: UserContactService, private _userInformList: UserInfoService, private _messageHubService: MessageHubService) {

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
                //this.getUnknownContacts(),
                this.getChats(),
                this.getUser(),
                this.getChatList()
            ]).then(
                ([contacts, chats, user, chatList]) => {
                    this.contacts = contacts;
                    console.log(contacts);
                    //this.unknownContacts = unknownContacts;
                    this.chats = chats;
                    this.user = user;
                    this.user.chatList = chatList
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
        console.log(contactId);
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
            this._httpClient.get(environment.apiGetChatListByConvId + chatItem.convId)
                .subscribe((response: any) => {
                    const chat = response;
                    const chatContact = this.contacts.concat(this.unknownContacts).find((contact) => {
                        return contact.userId === contactId;
                    });
                    console.log(chatContact);
                    const chatData = {
                        chatId: chat.userId,
                        dialog: chat.dialog,
                        contact: chatContact
                    };
                    console.log(chatData);
                    this.onChatSelected.next({ ...chatData });

                }, reject);

        });

    }

    /**
     * Create new chat
     *
     * @param contactId
     * @returns {Promise<any>}
     */
    createNewChat(contactId): Promise<any> {

        return new Promise((resolve, reject) => {

            // const contact = this.unknownContacts.concat(this.contacts).find((item) => {
            //     return item.userId === contactId;
            // });

            const chatId = FuseUtils.generateGUID();

            const chat = {
                id: chatId,
                dialog: []
            };

            const chatListItem = {
                contactId: contactId,
                id: chatId,
                lastMessageTime: '2017-02-18T10:30:18.931Z',
                //name: contact.displayName,
                unread: null
            };

            // Add new chat list item to the user's chat list
            this.user.chatList.push(chatListItem);

            // Post the created chat
            this._httpClient.post('api/chat-chats', { ...chat })
                .subscribe((response: any) => {

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
        this._httpClient.post('api/chat-user/' + this.user.id, userData)
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
    getContacts(): Promise<any> {
        return this._userContactService.getContacts().toPromise();


    }
    getUnknownContacts(displayNameSearch?: string): Promise<any> {
        return this._userContactService.getUnknownContacts(displayNameSearch).toPromise();

    }

    // getUnknownContacts(displayNameSearch?: string): Promise<any> {
    //     //return this._userContactService.getUnknownContacts(displayNameSearch).toPromise();
    // }



    /**
     * Get chats
     *
     * @returns {Promise<any>}
     */
    getChats(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._httpClient.get(environment.apiGetChatListByUserId + this._userProfileService.userProfile.UserId) // using static user id to test
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
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
                }, reject);
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
                // =======
                //             this._httpClient.get('https://localhost:44383/api/contact/getchatlistsp')
                // >>>>>>> 485cad35367b9bf806ff66315b8edb404a3033ad
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }
}
