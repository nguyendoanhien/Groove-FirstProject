import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { ChatService } from '../../../chat.service';
let ChatUserSidenavComponent = class ChatUserSidenavComponent {
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
        this.user = this._chatService.user;
        this.userForm = new FormGroup({
            mood: new FormControl(this.user.mood),
            status: new FormControl(this.user.status)
        });
        this.userForm.valueChanges
            .pipe(takeUntil(this._unsubscribeAll), debounceTime(500), distinctUntilChanged())
            .subscribe(data => {
            this.user.mood = data.mood;
            this.user.status = data.status;
            this._chatService.updateUserData(this.user);
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
     * Change left sidenav view
     *
     * @param view
     */
    changeLeftSidenavView(view) {
        this._chatService.onLeftSidenavViewChanged.next(view);
    }
};
ChatUserSidenavComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-user-sidenav',
        templateUrl: './user.component.html',
        styleUrls: ['./user.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatUserSidenavComponent);
export { ChatUserSidenavComponent };
//# sourceMappingURL=user.component.js.map