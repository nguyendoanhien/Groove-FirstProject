import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { ChatService } from '../../chat.service';
let ChatLeftSidenavComponent = class ChatLeftSidenavComponent {
    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(_chatService) {
        this._chatService = _chatService;
        // Set the defaults
        this.view = 'chats';
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
        this._chatService.onLeftSidenavViewChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(view => {
            this.view = view;
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
ChatLeftSidenavComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-left-sidenav',
        templateUrl: './left.component.html',
        styleUrls: ['./left.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatLeftSidenavComponent);
export { ChatLeftSidenavComponent };
//# sourceMappingURL=left.component.js.map