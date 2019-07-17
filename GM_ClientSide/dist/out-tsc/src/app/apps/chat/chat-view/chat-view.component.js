import * as tslib_1 from "tslib";
import { Component, ViewChild, ViewChildren, ViewEncapsulation } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FusePerfectScrollbarDirective } from '@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import { ChatService } from '../chat.service';
let ChatViewComponent = class ChatViewComponent {
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
        this._chatService.onChatSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatData => {
            if (chatData) {
                this.selectedChat = chatData;
                this.contact = chatData.contact;
                this.dialog = chatData.dialog;
                this.readyToReply();
            }
        });
    }
    /**
     * After view init
     */
    ngAfterViewInit() {
        this.replyInput = this.replyInputField.first.nativeElement;
        this.readyToReply();
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
     * Decide whether to show or not the contact's avatar in the message row
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    shouldShowContactAvatar(message, i) {
        return (message.who === this.contact.id &&
            ((this.dialog[i + 1] && this.dialog[i + 1].who !== this.contact.id) || !this.dialog[i + 1]));
    }
    /**
     * Check if the given message is the first message of a group
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    isFirstMessageOfGroup(message, i) {
        return (i === 0 || this.dialog[i - 1] && this.dialog[i - 1].who !== message.who);
    }
    /**
     * Check if the given message is the last message of a group
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    isLastMessageOfGroup(message, i) {
        return (i === this.dialog.length - 1 || this.dialog[i + 1] && this.dialog[i + 1].who !== message.who);
    }
    /**
     * Select contact
     */
    selectContact() {
        this._chatService.selectContact(this.contact);
    }
    /**
     * Ready to reply
     */
    readyToReply() {
        setTimeout(() => {
            this.focusReplyInput();
            this.scrollToBottom();
        });
    }
    /**
     * Focus to the reply input
     */
    focusReplyInput() {
        setTimeout(() => {
            this.replyInput.focus();
        });
    }
    /**
     * Scroll to the bottom
     *
     * @param {number} speed
     */
    scrollToBottom(speed) {
        speed = speed || 400;
        if (this.directiveScroll) {
            this.directiveScroll.update();
            setTimeout(() => {
                this.directiveScroll.scrollToBottom(0, speed);
            });
        }
    }
    /**
     * Reply
     */
    reply(event) {
        event.preventDefault();
        if (!this.replyForm.form.value.message) {
            return;
        }
        // Message
        const message = {
            who: this.user.id,
            message: this.replyForm.form.value.message,
            time: new Date().toISOString()
        };
        // Add the message to the chat
        this.dialog.push(message);
        // Reset the reply form
        this.replyForm.reset();
        // Update the server
        this._chatService.updateDialog(this.selectedChat.chatId, this.dialog).then(response => {
            this.readyToReply();
        });
    }
};
tslib_1.__decorate([
    ViewChild(FusePerfectScrollbarDirective, { static: false }),
    tslib_1.__metadata("design:type", FusePerfectScrollbarDirective)
], ChatViewComponent.prototype, "directiveScroll", void 0);
tslib_1.__decorate([
    ViewChildren('replyInput'),
    tslib_1.__metadata("design:type", Object)
], ChatViewComponent.prototype, "replyInputField", void 0);
tslib_1.__decorate([
    ViewChild('replyForm', { static: false }),
    tslib_1.__metadata("design:type", NgForm)
], ChatViewComponent.prototype, "replyForm", void 0);
ChatViewComponent = tslib_1.__decorate([
    Component({
        selector: 'chat-view',
        templateUrl: './chat-view.component.html',
        styleUrls: ['./chat-view.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [ChatService])
], ChatViewComponent);
export { ChatViewComponent };
//# sourceMappingURL=chat-view.component.js.map