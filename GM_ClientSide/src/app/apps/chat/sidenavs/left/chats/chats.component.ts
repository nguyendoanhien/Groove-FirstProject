import { Component, OnDestroy, OnInit, ViewEncapsulation, ViewChildren, ChangeDetectorRef } from "@angular/core";
import { MediaObserver } from "@angular/flex-layout";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { fuseAnimations } from "@fuse/animations";
import { FuseMatSidenavHelperService } from "@fuse/directives/fuse-mat-sidenav/fuse-mat-sidenav.service";
import { ChatService } from "../../../chat.service";
import { UserProfileService } from "app/core/identity/userprofile.service";
import { FilterPipe } from "@fuse/pipes/filter.pipe";
import { UserInfoService } from "app/core/account/userInfo.service";
import { HubConnection } from "@aspnet/signalr";
import { UserInfo } from "../user/userInfo.model";
import { ProfileHubService } from "app/core/data-api/hubs/profile.hub";
import { MessageModel } from "app/models/message.model";
import { MessageService } from "app/core/data-api/services/message.service";
import { UnreadMessage } from "app/models/UnreadMessage.model";

@Component({
    selector: "chat-chats-sidenav",
    templateUrl: "./chats.component.html",
    styleUrls: ["./chats.component.scss"],
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

    @ViewChildren("someVar")
    filteredItems;

    currentSumLength: number;
    currentUnknownContactLength: number;

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
        public _userProfileService: UserProfileService,
        public _chatService: ChatService,
        private _fuseMatSidenavHelperService: FuseMatSidenavHelperService,
        public _mediaObserver: MediaObserver,
        public _userInfoService: UserInfoService,
        private profileHubService: ProfileHubService,
        private _messageService: MessageService,
        private cd: ChangeDetectorRef
    ) {
        // Set the defaults
        this.chatSearch = {
            name: ""
        };
        this.searchText = "";

        // Set the private defaults
        this._unsubscribeAll = new Subject();


    }

    changeProfile(user: UserInfo) {
        this._hubConnection.invoke("ChangeUserProfile", user).catch(function(err) {
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
        this.updateChangeProfileHub();

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
        this._chatService._messageHub.newChatMessage.subscribe((message: MessageModel) => {
            if (message) {
                const chatList = this.user.chatList as Array<any>;
                const chat = chatList.find(x => x.convId == message.fromConv);
                chat.message = message.payload;
                chat.lastMessage = message.payload;
                chat.lastMessageTime = message.time;
                this._chatService._messageHub.newChatMessage.next(null);
            }
        });

        this._chatService._messageHub.unreadMessage.subscribe((unreadMessage: UnreadMessage) => {
            if (unreadMessage) {
                console.log('Unread message amount:');
                console.log(unreadMessage);
                const chatList = this.user.chatList as Array<any>;
                const chat = chatList.find(x => x.convId == unreadMessage.conversationId);
                if (unreadMessage.amount > 100) {
                    chat.unread = "99+";
                } 
                else {
                    chat.unread = unreadMessage.amount;
                }
                this.cd.detectChanges();
                this._chatService._messageHub.unreadMessage.next(null);
            }
        });
    }


    initGetUserInfo() {
        this._userInfoService.getUserInfo().subscribe(res => {
            if (this._userInfoService.userInfo.status == "offline") {
                this._userInfoService.userInfo.status = "online";
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
        if (!this._mediaObserver.isActive("gt-md")) {
            this._fuseMatSidenavHelperService.getSidenav("chat-left-sidenav").toggle();
        }
    }

    async setValueSeenBy(conversationId) {
        await this._messageService.updateUnreadMessages(conversationId).subscribe(val => {
            },
            err => console.log(err));

        const chatList = this.user.chatList as Array<any>;
        const chat = chatList.find(x => x.convId == conversationId);
        chat.unread = "";
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
        if (status === "offline")
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
        this._userInfoService.userInfo.status = "offline";
        await this._userInfoService.changeDisplayName().subscribe();
        this._userProfileService.logOut();
        window.location.reload();
    }

    CountData() {

        this._chatService.getUnknownContacts(this.searchText).then(
            data => {
                if (!this.unknownContacts.equals(data)) this.unknownContacts = data;
            }
        );
        const pipe = new FilterPipe();
        // const unknownContactPipe = new UnknownContactFilterPipe();
        const arrayContact = pipe.transform(this.user.chatList, this.searchText, "") as Array<any>;
        const arrayChat = pipe.transform(this.contacts, this.searchText, "") as Array<any>;
        this.currentSumLength = arrayChat.length + arrayContact.length;
        // let arrayUnknownContact = unknownContactPipe.transform(this.unknownContacts, this.searchText, '') as Array<any>;
        // this.currentUnknownContactLength = arrayUnknownContact.length;

    }

    updateChangeProfileHub() {
        this.profileHubService.UserProfileChanged.subscribe(res => {
            if (res != null) {
                this.contacts.forEach(contact => {
                    if (contact.userId === res.userId) {
                        contact.mood = res.mood;
                        contact.avatar = res.avatar;
                        contact.status = res.status;
                    }
                });
                this.cd.detectChanges();
            }
        });
    }

}

//Global----------------------------------
Array.prototype.equals = function(array) {
    // if the other array is a falsy value, return
    if (!array)
        return false;

    // compare lengths - can save a lot of time
    if (this.length != array.length)
        return false;

    for (var i = 0, l = this.length; i < l; i++) {
        // Check if we have nested arrays
        if (this[i] instanceof Array && array[i] instanceof Array) {
            // recurse into the nested arrays
            if (!this[i].equals(array[i]))
                return false;
        } else if (JSON.stringify(this[i]) !== JSON.stringify(array[i])) {

            // Warning - two different object instances will never be equal: {x:20} != {x:20}
            return false;
        }
    }
    return true;
};
// Hide method from for-in loops
Object.defineProperty(Array.prototype, "equals", { enumerable: false });
declare global {
    interface Array<T> {
        equals: any;

        // }
    }
}