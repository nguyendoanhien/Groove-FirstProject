import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseMatSidenavHelperService } from '@fuse/directives/fuse-mat-sidenav/fuse-mat-sidenav.service';
import { ChatService } from '../../../chat.service';
import { UserProfileService } from 'app/core/identity/userprofile.service';
let ChatChatsSidenavComponent = class ChatChatsSidenavComponent {
    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     * @param {FuseMatSidenavHelperService} _fuseMatSidenavHelperService
     * @param {MediaObserver} _mediaObserver
     */
    constructor(_userProfileService, _chatService, _fuseMatSidenavHelperService, _mediaObserver) {
        this._userProfileService = _userProfileService;
        this._chatService = _chatService;
        this._fuseMatSidenavHelperService = _fuseMatSidenavHelperService;
        this._mediaObserver = _mediaObserver;
        // Set the defaults
        this.chatSearch = {
            name: ''
        };
        this.searchText = '';
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * On init
     */
    ngOnInit() {
        this.user = this._chatService.user;
        this.chats = this._chatService.chats;
        this.contacts = this._chatService.contacts;
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
    /**
     * On destroy
     */
    ngOnDestroy() {
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
    getChat(contact) {
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
    setUserStatus(status) {
        this._chatService.setUserStatus(status);
    }
    /**
     * Change left sidenav view
     *
     * @param view
     */
    changeLeftSidenavView(view) {
        this._chatService.onLeftSidenavViewChanged.next(view);
    }
    /**
     * Logout
     */
    logout() {
        this._userProfileService.logOut();
    }
};
ChatChatsSidenavComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-chats-sidenav',
        templateUrl: './chats.component.html',
        styleUrls: ['./chats.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [UserProfileService,
        ChatService,
        FuseMatSidenavHelperService,
        MediaObserver])
], ChatChatsSidenavComponent);
export { ChatChatsSidenavComponent };
//# sourceMappingURL=chats.component.js.map