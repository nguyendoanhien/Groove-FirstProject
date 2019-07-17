import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ChatService } from '../../../chat.service';
let ChatContactSidenavComponent = class ChatContactSidenavComponent {
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
        this._chatService.onContactSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(contact => {
            this.contact = contact;
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
ChatContactSidenavComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-contact-sidenav',
        templateUrl: './contact.component.html',
        styleUrls: ['./contact.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatContactSidenavComponent);
export { ChatContactSidenavComponent };
//# sourceMappingURL=contact.component.js.map