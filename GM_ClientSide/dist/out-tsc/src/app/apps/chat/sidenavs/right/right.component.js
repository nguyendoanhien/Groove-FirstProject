import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { ChatService } from '../../chat.service';
let ChatRightSidenavComponent = class ChatRightSidenavComponent {
    constructor(_chatService) {
        this._chatService = _chatService;
        // Set the defaults
        this.view = 'contact';
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
        this._chatService.onRightSidenavViewChanged
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
ChatRightSidenavComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-right-sidenav',
        templateUrl: './right.component.html',
        styleUrls: ['./right.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatRightSidenavComponent);
export { ChatRightSidenavComponent };
//# sourceMappingURL=right.component.js.map