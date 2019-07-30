import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewChildren, ViewEncapsulation } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Subject } from "rxjs";
import { takeUntil, take } from "rxjs/operators";

import { FusePerfectScrollbarDirective } from
    "@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive";

import { ChatService } from "../chat.service";
import { MessageModel } from 'app/models/message.model';
import { MessageService } from 'app/core/data-api/services/message.service';
import { IndexMessageModel } from 'app/models/indexMessage.model';
import { UserContactService } from 'app/core/account/user-contact.service';
import { RxSpeechRecognitionService, resultList } from '@kamiazya/ngx-speech-recognition';
import { OpenGrapthService } from 'app/core/data-api/services/open-grapth.service';
import { AppHelperService } from 'app/core/utilities/app-helper.service';
import * as $ from 'jquery/dist/jquery.min.js';
import * as ts from "typescript";
import { Meta } from '@angular/platform-browser';
import { FacebookService } from 'ngx-facebook';
import { ApiMethod } from 'ngx-facebook/dist/esm/providers/facebook';
import { Cloudinary } from '@cloudinary/angular-5.x';
declare var window: any;
@Component({
    selector: "chat-view",
    templateUrl: "./chat-view.component.html",
    styleUrls: ["./chat-view.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ChatViewComponent implements OnInit, OnDestroy, AfterViewInit {
    user: any;
    chat: any;
    dialog: any;
    chatId: string; // conversation id
    contact: any;
    replyInput: any;
    selectedChat: any;
    FB: any;

    numbers = Array(5).fill(0).map((x, i) => i);
    @ViewChild(FusePerfectScrollbarDirective, { static: false })
    directiveScroll: FusePerfectScrollbarDirective;

    @ViewChildren("replyInput")
    replyInputField;

    @ViewChild("replyForm", { static: false })
    replyForm: NgForm;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     * @param {MessageService} _messageService
     */
    constructor(
        private _chatService: ChatService,
        private _messageService: MessageService,
        private _userContactService: UserContactService,
        public _rxSpeechRecognitionService: RxSpeechRecognitionService,
        private _openGrapthService: OpenGrapthService,
        public _appHelperService: AppHelperService,
        private fbk: FacebookService,
        private cloudinary: Cloudinary
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        console.log('ak is' + this.fbk.getAuthResponse()['accessToken']);

        this.fbk.api(
            '/',
            "post",
            { "scrape": "true", "id": "https://www.skype.com/en/" }
        ).then(function (response) {
            console.log(response.image[0].url);
        });

        // this.fbk.init({
        //     appId: '354060818601401', cookie: true, status: true, xfbml: true, version: 'v3.3'
        // });



        // (function (d, s, id) {
        //     var js, fjs = d.getElementsByTagName(s)[0];
        //     if (d.getElementById(id))
        //         return;
        //     js = d.createElement(s);
        //     js.id = id;
        //     js.src = "//connect.facebook.net/en_US/all.js";
        //     fjs.parentNode.insertBefore(js, fjs);
        // }(document, 'script', 'facebook-jssdk'));

        this.user = this._chatService.user;
        this._chatService.onChatSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatData => {
                if (chatData) {
                    this.selectedChat = chatData;
                    this.contact = chatData.contact;
                    this.dialog = chatData.dialog;
                    this.chatId = chatData.chatId; // current conversation id
                    this.readyToReply();
                }
            });

        this._chatService._messageHub.newChatMessage.subscribe((message: MessageModel) => {
            if (message) {
                if (this.chatId === message.fromConv) {
                    this.dialog.push({ who: message.fromSender, message: message.payload, time: message.time });
                }
            }
        })
    }

    /**
     * After view init
     */
    ngAfterViewInit(): void {
        this.replyInput = this.replyInputField.first.nativeElement;
        this.readyToReply();

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
     * Decide whether to show or not the contact's avatar in the message row
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    shouldShowContactAvatar(message, i): boolean {

        return (
            message.who === this.contact.id &&
            ((this.dialog[i + 1] && this.dialog[i + 1].who !== this.contact.id) || !this.dialog[i + 1])
        );
    }

    /**
     * Check if the given message is the first message of a group
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    isFirstMessageOfGroup(message, i): boolean {
        return (i === 0 || this.dialog[i - 1] && this.dialog[i - 1].who !== message.who);
    }

    /**
     * Check if the given message is the last message of a group
     *
     * @param message
     * @param i
     * @returns {boolean}
     */
    isLastMessageOfGroup(message, i): boolean {
        return (i === this.dialog.length - 1 || this.dialog[i + 1] && this.dialog[i + 1].who !== message.who);
    }

    /**
     * Select contact
     */
    selectContact(): void {
        this._chatService.selectContact(this.contact);
    }

    /**
     * Ready to reply
     */
    readyToReply(): void {
        setTimeout(() => {
            this.focusReplyInput();
            this.scrollToBottom();
        });
    }

    /**
     * Focus to the reply input
     */
    focusReplyInput(): void {
        setTimeout(() => {
            this.replyInput.focus();
        });
    }

    /**
     * Scroll to the bottom
     *
     * @param {number} speed
     */
    scrollToBottom(speed?: number): void {
        speed = speed || 400;
        if (this.directiveScroll) {
            this.directiveScroll.update();

            setTimeout(() => {
                this.directiveScroll.scrollToBottom(0, speed);
            });
        }
        this._messageService.sendUnreadMessages(this.user.chatList[0].convId)
            .subscribe(val => { }, error => { console.log(error); }
            );
    }

    /**
     * Reply
     */

    async getOgImage(urlPath: string) {
        let imageUrl = '';
        var apiMethod: ApiMethod = "post";

        await this.fbk.api(
            '/',
            apiMethod,
            { "scrape": "true", "id": "https://www.skype.com/en/" }
        ).then(function (response) {
            imageUrl = response.image[0].url;
            console.log(imageUrl)
        }
        );
        return imageUrl;
    }
    async reply(event) {


        this.getOgImage('abc');
        // console.log(await this.getOgImage(this.replyForm.form.value.message));
        event.preventDefault();

        if (!this.replyForm.form.value.message) {
            return;
        }

        // Message
        const message = {
            who: this.user.userId,
            message: this.replyForm.form.value.message,
            time: new Date().toISOString()
        };
        var newMessage: IndexMessageModel = new IndexMessageModel(this.chatId, this.user.userId, null, message.message, 'Text', this.contact.userId);
        await this._messageService.addMessage(newMessage).subscribe(success => {
        }, err => console.log("send fail"));
        // Add the message to the chat
        //this.dialog.push(message); //Truc: don't need because broadcast to user + contact
        // Reset the reply form
        this.replyForm.reset();

        // Update the server
        this._chatService.updateDialog(this.selectedChat.chatId, this.dialog).then(response => {
            this.readyToReply();
        });
        // Truc: Call count unread messages in controller backend
        this._messageService.sendUnreadMessages(this.user.chatList[0].convId)
            .subscribe(val => { },
                error => { console.log(error); }
            );


        this._messageService.updateUnreadMessages(this.chatId)
            .subscribe(val => {
                var chatList = this.user.chatList as Array<any>;
                var chat = chatList.find(x => x.convId == this.chatId);
                chat.unread = val;
            },
                err => console.log(err));

    }

    SayHi(contact: any) {
        this._userContactService.SayHi(contact).subscribe(
            (res: any) => {

                this._chatService.contacts.push(res.contact);
                this._chatService.user.chatList.push(res.chatContact);
                this._chatService.chats.push(res.diaglog);
                console.log(res.contact);
                this._chatService.unknownContacts = this._chatService.unknownContacts.filter(item => item.userId !== res.contact.userId);
                const chatData = {
                    chatId: res.dialog.id, // This is id of conversation
                    dialog: res.dialog.dialog,
                    contact: res.contact
                };
                this._chatService.onChatSelected.next({ ...chatData });
            }
        );
    }


    listen() {

        this._rxSpeechRecognitionService
            .listen()
            .pipe(resultList, take(1))
            .subscribe((list: SpeechRecognitionResultList) => {
                // console.log('chat voice' + list.item(0).item(0).transcript);
                this.replyInput.value += list.item(0).item(0).transcript + ' ';
            });
    }

    Say() {
        alert(123);
    }

}
