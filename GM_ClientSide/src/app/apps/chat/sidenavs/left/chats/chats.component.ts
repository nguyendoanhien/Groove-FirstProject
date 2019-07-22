import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';
import { FuseMatSidenavHelperService } from '@fuse/directives/fuse-mat-sidenav/fuse-mat-sidenav.service';

import { ChatService } from '../../../chat.service';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { UserInfoService } from 'app/core/account/userInfo.service';
import { HubConnection } from '@aspnet/signalr';
import { UserInfo } from '../user/userInfo.model';
@Component({
    selector: 'chat-chats-sidenav',
    templateUrl: './chats.component.html',
    styleUrls: ['./chats.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ChatChatsSidenavComponent implements OnInit, OnDestroy {
    chats: any[];
    chatSearch: any;
    contacts: any[];
    unknownContacts: any[];
    searchText: string;
    user: any;


    // Private
    private _unsubscribeAll: Subject<any>;
    private _hubConnection: HubConnection | undefined;
    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     * @param {FuseMatSidenavHelperService} _fuseMatSidenavHelperService
     * @param {MediaObserver} _mediaObserver
     */
    constructor(
        private _userProfileService: UserProfileService,
        private _chatService: ChatService,
        private _fuseMatSidenavHelperService: FuseMatSidenavHelperService,
        public _mediaObserver: MediaObserver,
        private _userInfoService: UserInfoService,
    ) {
        // Set the defaults
        this.chatSearch = {
            name: ''
        };
        this.searchText = '';

        // Set the private defaults
        this._unsubscribeAll = new Subject();



    }

    public changeProfile(user: UserInfo) {
        this._hubConnection.invoke("ChangeUserProfile", user).catch(function (err) {
            return console.error(err.toString());
        });
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {


        this.initGetUserInfo();


        this.user = this._chatService.user;
        this.chats = this._chatService.chats;
        this.contacts = this._chatService.contacts;

        this.unknownContacts = this._chatService.unknownContacts;

        this._chatService.onChatsUpdated
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(updatedChats => {
                this.chats = updatedChats;
            });

        this._chatService.onUserUpdated
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(updatedUser => {
                this.user = updatedUser;
            });
    }




    initGetUserInfo() {
        this._userInfoService.getUserInfo().subscribe(res => {
            if (this._userInfoService.userInfo.status == 'offline') {
                this._userInfoService.userInfo.status = 'online';
                this._userInfoService.changeDisplayName().subscribe();
            }
        });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get chat
     *
     * @param contact
     */
    getChat(contact): void {

        this._chatService.getChat(contact);
        if (!this._mediaObserver.isActive('gt-md')) {
            this._fuseMatSidenavHelperService.getSidenav('chat-left-sidenav').toggle();
        }
    }

    /**
     * Set user status
     *
     * @param status
     */
    async setUserStatus(status) {
        this._userInfoService.userInfo.status = status;
        await this._userInfoService.changeDisplayName().subscribe(
        );
        if (status === 'offline')
            await this._userProfileService.logOut();
    }

    /**
     * Change left sidenav view
     *
     * @param view
     */
    changeLeftSidenavView(view): void {
        this._chatService.onLeftSidenavViewChanged.next(view);
    }

    /**
     * Logout
     */
    async logout() {
        this._userInfoService.userInfo.status = 'offline';
        await this._userInfoService.changeDisplayName().subscribe()
        this._userProfileService.logOut();

    }
}
