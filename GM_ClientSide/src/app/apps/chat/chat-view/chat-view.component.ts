import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewChildren, ViewEncapsulation, ElementRef, HostListener, ApplicationRef } from "@angular/core";
import { NgForm, FormControl } from "@angular/forms";
import { Subject, BehaviorSubject, Observable, Subscription } from "rxjs";
import { takeUntil, take, debounceTime, last } from "rxjs/operators";

import { FusePerfectScrollbarDirective } from
    "@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive";

import { ChatService } from "../chat.service";
import { MessageModel } from "app/models/message.model";
import { MessageService } from "app/core/data-api/services/message.service";
import { IndexMessageModel } from "app/models/indexMessage.model";
import { UserContactService } from "app/core/account/user-contact.service";
import { RxSpeechRecognitionService, resultList, SpeechRecognitionService } from "@kamiazya/ngx-speech-recognition";
import { ApiMethod, FacebookService } from "ngx-facebook/dist/esm/providers/facebook";
import { WindowRef } from '@fuse/services/window-ref';


export class DialogModel {
    id: string;
    who: string;
    message: string;
    time: Date;
    nickName: string;
    avatar: string;
}
declare var jquery: any;
declare var $: any;
@Component({
    selector: "chat-view",
    templateUrl: "./chat-view.component.html",
    styleUrls: ["./chat-view.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ChatViewComponent implements OnInit, OnDestroy, AfterViewInit {
    @ViewChild('file', { static: false }) myFile: ElementRef;

    user: any;
    chat: any;
    dialog: any[];
    chatId: string; // conversation id
    groupId: string;
    contact: any;
    replyInput: any;
    selectedChat: any;
    isHide: any = true;
    selectedFile: any = null;
    isGroup: boolean;
    numberOfMembers: number;
    clicked = false;

    @ViewChild('vcChatContent', { static: false }) vcChatContent: ElementRef;
    @ViewChild(FusePerfectScrollbarDirective, { static: false })
    directiveScroll: FusePerfectScrollbarDirective;
    isOver = false;
    lastClicked: Date = new Date();

    @ViewChildren("replyInput")
    replyInputField;

    @ViewChild("replyForm", { static: false })
    replyForm: NgForm;
    @HostListener('window:scroll', ['$event']) // for window scroll events
    onScroll(event) {

        this.LoadMoreMessage();
    }

    LoadMoreMessage() {
        if (this.vcChatContent.nativeElement.scrollTop == 0) {
            var now = new Date();


            if (now.getTime() - this.lastClicked.getTime() >= 300) {
                this.lastClicked = now;
                let CreatedOn = this.dialog[0].time;

                this._chatService.getMoreChat(this.chatId, CreatedOn).pipe(
                    debounceTime(5000)
                ).subscribe(
                    (res: any) => {
                        var beforeScrollHeight = this.vcChatContent.nativeElement.scrollHeight;
                        console.log('before' + beforeScrollHeight)
                        let dialogs = res.dialog as DialogModel[];
                        if (dialogs.length == 0) this.isOver = true;

                        Array.prototype.forEach.call(dialogs.reverse(), child => {
                            this.dialog.unshift(child);
                        });



                        setTimeout(() => {
                            var afterScrollHeight = this.vcChatContent.nativeElement.scrollHeight;
                            this.vcChatContent.nativeElement.scrollTop = afterScrollHeight - beforeScrollHeight;
                            // if ($('.message-row div').is(':empty')) {
                            //     $('.message-row div').remove();
                            // }
                        }, 0)
                    }
                )
            }


        }
    }

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     * @param {MessageService} _messageService
     */
    constructor(
        public _chatService: ChatService,
        private _messageService: MessageService,
        private _userContactService: UserContactService,
        public _rxSpeechRecognitionService: RxSpeechRecognitionService,
        private fbk: FacebookService,
        private _windowRef: WindowRef
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

    requestPermission() {
        // Let's check if the browser supports notifications
        if (!("Notification" in window)) {
            alert("This browser does not support system notifications");
            // This is not how you would really do things if they aren't supported. :)
        }

        // Let's check whether notification permissions have already been granted
        else if (Notification.permission === "granted") {
            // If it's okay let's create a notification
            new Notification("Hi there!");
        }

        // Otherwise, we need to ask the user for permission
        else if (Notification.permission !== 'denied') {
            Notification.requestPermission(function (permission) {
                // If the user accepts, let's create a notification
                if (permission === "granted") {
                    var notification = new Notification("Hi there!");
                }
            });
        }

        // Finally, if the user has denied notifications and you
        // want to be respectful there is no need to bother them any more.
    }
    displayNotification() {
        if (Notification.permission == 'granted') {
            navigator.serviceWorker.getRegistration().then(function (reg) {
                var options = {
                    body: 'Here is a notification body!',
                    icon: 'images/example.png',
                    vibrate: [100, 50, 100],
                    data: {
                        dateOfArrival: Date.now(),
                        primaryKey: 1
                    }
                };
                reg.showNotification('Hello world!', options);
            });
        }
    }
    ngOnInit(): void {
        this.selectedChat = null;
        this.isOver = false;
        this.lastClicked = new Date();
        this.user = this._chatService.user;
        this._chatService.onChatSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatData => {
                if (chatData) {
                    this.clicked = false;
                    this.isOver = false;
                    this.selectedChat = chatData;
                    this.contact = chatData.contact;
                    this.dialog = chatData.dialog;
                    this.chatId = chatData.chatId; // current conversation id
                    this.isGroup = chatData.isGroup ? chatData.isGroup : false;
                    if (this.isGroup === true) {
                        this.numberOfMembers = chatData.contact.members.length;
                    }
                    this.readyToReply();
                }
            });

        this._chatService._messageHub.newChatMessage.subscribe((message: MessageModel) => {
            if (message) {
                if (this.chatId === message.fromConv) {
                    this.dialog.push({ who: message.fromSender, message: message.payload, time: message.time });
                }
            }
        });
        this._chatService._messageHub.newGroupChatMessage.subscribe((message: MessageModel) => {
            if (message) {
                var lastItem = this.dialog[this.dialog.length - 1];
                if (this.chatId === message.fromConv && lastItem.id !== message.messageId) {
                    this.dialog.push({ who: message.fromSender, message: message.payload, time: message.time, avatar: message.senderAvatar, nickName: message.senderName });
                }
            }
        });

        this._chatService._contactHub.editGroup.subscribe((edtgroup: any) => {
            if (edtgroup) {
                this.contact.avatar = edtgroup.avatar;
                this.contact.displayName = edtgroup.name;
            }
        });
    }

    /**
     * After view init
     */
    ngAfterViewInit(): void {
        this.replyInput = this.replyInputField.first.nativeElement;
        this.readyToReply();

        let CreatedOn = this.dialog[0].time;
        console.log(CreatedOn);
        this._chatService.getMoreChat(this.chatId, CreatedOn).pipe(
            debounceTime(5000)
        ).subscribe(
            (res: any) => {
                var beforeScrollHeight = this.vcChatContent.nativeElement.scrollHeight;
                let dialogs = res.dialog as DialogModel[];
                console.log(dialogs);
                Array.prototype.forEach.call(dialogs.reverse(), child => {
                    this.dialog.unshift(child);
                });
                this.scrollToBottom();
            }
        )


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

    selectChatGroup(): void {

        this._chatService.selectChatGroup(this.contact);
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
    }

    /**
     * Reply
     */
    async getOgImage(urlPath: string) {
        let imageUrl = "";
        const apiMethod: ApiMethod = "post";

        await this.fbk.api(
            "/",
            apiMethod,
            { "scrape": "true", "id": "https://www.skype.com/en/" }
        ).then(function (response) {
            imageUrl = response.image[0].url;

        }
        );
        return imageUrl;
    }
    async reply(event) {

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

        const newMessage = new IndexMessageModel(this.chatId,
            this.user.userId,
            null,
            message.message,
            "Text",
            this.contact.userId);
        // Truc> Check if exists all spaces
        const urlRegex = /^(?!\s*$).+/g;
        const isMatch: boolean = urlRegex.test(this.replyForm.form.value.message);
        if (isMatch) {
            // Truc> Add the message and broadcast unread message amount
            if (this.isGroup === false) {
                this._messageService.addMessage(newMessage).subscribe(success => {
                    this._messageService.sendUnreadMessages(this.user.chatList[0].convId)
                        .subscribe();
                    this._messageService.updateUnreadMessages(this.chatId)
                        .subscribe(val => {
                            var chatList = this.user.chatList as Array<any>;
                            var chat = chatList.find(x => x.convId == this.chatId);
                            chat.unread = val;
                        },
                            err => console.log(err));
                    //Truc> Chats of sender
                    var chatList = this.user.chatList as Array<any>;
                    var chat = chatList.find(x => x.convId == this.chatId);
                    chat.lastMessage = message.message;
                    chat.lastMessageTime = message.time;
                    console.log("Sent successfully");
                },
                    err => console.log("Sent failed"));
            } else { // Isgroup
                this._messageService.addMessageToFroup(newMessage).subscribe(success => {
                    this._messageService.sendUnreadMessages(this.chatId)
                        .subscribe();
                    this._messageService.updateUnreadMessages(this.chatId)
                        .subscribe(val => {
                            var groupChatList = this.user.groupChatList as Array<any>;
                            var groupChat = groupChatList.find(x => x.id == this.chatId);
                            groupChat.unreadMessage = val;
                        },
                            err => console.log(err));
                    //Truc> Chats of sender        
                    var groupChatList = this.user.groupChatList as Array<any>;
                    var groupChat = groupChatList.find(x => x.id == this.chatId);
                    groupChat.lastestMessage = message.message;
                    groupChat.lastestMessageTime = message.time;
                    console.log("Chat group: Sent successfully");
                },
                    err => console.log("Chat group: Sent failed"));
            }
            this.dialog.push(message);
        }

        // Reset the reply form
        this.replyForm.reset();

        // Update the server
        this._chatService.updateDialog(this.selectedChat.chatId, this.dialog).then(response => {
            this.readyToReply();
        });

        // Truc: Call count unread messages in controller backend
        this.messageInput = ''; //reset
        //Hide emoji table
        this.isHide = true;
    }

    SayHi(contact: any) {
        this._userContactService.SayHi(contact).subscribe(
            (res: any) => {
                this._chatService.contacts.push(res.contact);
                this._chatService.user.chatList.push(res.chatContact);
                this._chatService.chats.push(res.diaglog);
                this._chatService.unknownContacts =
                    this._chatService.unknownContacts.filter(item => item.userId !== res.contact.userId);
                const chatData = {
                    chatId: res.dialog.id, // This is id of conversation
                    dialog: res.dialog.dialog,
                    contact: res.contact
                };
                this._chatService.onChatSelected.next({ ...chatData });
            }
        );
    }
    
    recognition = new SpeechRecognition();
    listenSwitch = false;
    listen() {
        if (!this.listenSwitch) {
            this.listenSwitch = true;
            console.log('on')
            //recognition.continuous = false;
            this.recognition.lang = 'en-US';
            this.recognition.interimResults = false;
            this.recognition.maxAlternatives = 1;
            //clicked
            this.recognition.start();
            console.log('Ready to receive a color command.');

            this.recognition.onresult = (event) => {
                var text = event.results[0][0].transcript;
                this.messageInput += text;
                
            }
        } else {
            console.log('off');
            this.listenSwitch = false;
            this.recognition.stop();
        }

    }


    onUpload(event) {
        console.log(event.target.files);
        if (!this.Validate(event.target.files)) { this.myFile.nativeElement.value = ''; return; }
        this.selectedFile = (event.target.files[0] as File);
        const fd = new FormData();
        fd.append("file", this.selectedFile);

        const message = {
            who: this.user.userId,
            message: "",
            time: new Date().toISOString()
        };
        const newMessage = new IndexMessageModel(this.chatId,
            this.user.userId,
            null,
            message.message,
            "Image",
            this.contact.userId);

        this._messageService.onUpload(fd, newMessage, this.isGroup).subscribe(data => {
            this.replyImage(data);
            this.myFile.nativeElement.value = '';
            if (this.isGroup == false) {
                var chatList = this.user.chatList as Array<any>;
                var chat = chatList.find(x => x.convId == this.chatId);
                chat.lastMessage = '[Image]';
                chat.lastMessageTime = message.time;
            }
            else {
                var groupChatList = this.user.groupChatList as Array<any>;
                var groupChat = groupChatList.find(x => x.id == this.chatId);
                groupChat.lastestMessage = '[Image]';
                groupChat.lastestMessageTime = message.time;
            }
        });
    }

    messageInput: string = '';
    addEmoji(event) {
        if (this.messageInput == null) this.messageInput = '';
        this.messageInput += event.emoji.native;
    }
    ShowEmoji() {
        this.isHide = !this.isHide;
    }
    Say() {
        alert(123);
    }

    //---Tam thoi
    replyImage(imageUrl: string) {



        if (!imageUrl) {
            return;
        }
        // Message
        const message = {
            who: this.user.userId,
            message: imageUrl,
            time: new Date().toISOString()
        };

        const newMessage = new IndexMessageModel(this.chatId,
            this.user.userId,
            null,
            message.message,
            "Image",
            this.contact.userId);
        // Truc> Check if exists all spaces
        const urlRegex = /^(?!\s*$).+/g;
        const isMatch: boolean = urlRegex.test(imageUrl);
        if (isMatch) {
            // Truc> Add the message and broadcast unread message amount

            this.dialog.push(message);
        }

        // Reset the reply form
        this.replyForm.reset();

        // Update the server
        this._chatService.updateDialog(this.selectedChat.chatId, this.dialog).then(response => {
            this.readyToReply();
        });

        // Truc: Call count unread messages in controller backend
        this.messageInput = ''; //reset
        //Hide emoji table
        this.isHide = true;
    }

    _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    Validate(files: any[]) {

        var arrInputs = files;
        for (var i = 0; i < arrInputs.length; i++) {
            var oInput = arrInputs[i];

            var sFileName = oInput.name;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < this._validFileExtensions.length; j++) {
                    var sCurExtension = this._validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                if (!blnValid) {
                    alert("Sorry, " + sFileName + " is invalid, allowed extensions are: " + this._validFileExtensions.join(", "));
                    return false;
                }
            }

        }

        return true;
    }
    imgError(image) {
        image.onerror = "";
        image.src = "/images/noimage.gif";
        return true;
    }
    errorHandler(event, el) {
        $(event.target).remove();
        event.target.src = "https://cdn.browshot.com/static/images/not-found.png";
    }
}