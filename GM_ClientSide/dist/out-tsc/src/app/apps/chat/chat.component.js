import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { ChatService } from './chat.service';
let ChatComponent = class ChatComponent {
    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(_chatService) {
        this._chatService = _chatService;
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
        this._chatService.onChatSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatData => {
            this.selectedChat = chatData;
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
};
ChatComponent = tslib_1.__decorate([
    Component({
        selector: 'chat',
        templateUrl: './chat.component.html',
        styleUrls: ['./chat.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatComponent);
export { ChatComponent };
//# sourceMappingURL=chat.component.js.map