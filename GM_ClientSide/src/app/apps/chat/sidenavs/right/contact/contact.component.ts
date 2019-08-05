import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { ChatService } from "../../../chat.service";
import { UserContactService } from "app/core/account/user-contact.service";
import { GroupService } from 'app/core/data-api/services/group.service';

@Component({
    selector: "chat-contact-sidenav",
    templateUrl: "./contact.component.html",
    styleUrls: ["./contact.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ChatContactSidenavComponent implements OnInit, OnDestroy {
    contact: any;
    chatGroup: any;
    contacts: any;
    selectedFile: File = null
    convGroup: any;
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(
        private _chatService: ChatService,
        private _userContactService: UserContactService,
        private _groupService: GroupService
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
        this._chatService.onContactSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(contact => {
                this.contact = contact;
            });
        this._chatService.onChatGroupSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatGroup => {
                this.chatGroup = chatGroup;
                this.showAllContactInGroup()
            });

        this._chatService._contactHub.editGroup.subscribe((edtgroup: any) => {
            if (edtgroup) {
                this._chatService.user.groupChatList.forEach(groupChat => {
                    if (groupChat.id === edtgroup.id) {
                        groupChat.name = edtgroup.name;
                        groupChat.avatar = edtgroup.avatar;
                    }
                })
            }
        });
    }

    showAllContactInGroup() {
        this.contacts = this._chatService.contacts;
        if (this.chatGroup) {
            this.convGroup = this._chatService.user.groupChatList.filter(group => group.id === this.chatGroup.id);
            console.log(this.convGroup)
            this.contacts.forEach(async contact => {
                contact.isMember = false;
                await this.convGroup[0].members.forEach(member => {
                    if (member.id === contact.userId) {
                        contact.isMember = true;
                    }
                })
            });
        }
    }

    // addMember(userId) {
    //     this._chatService.addMember(userId,this.chatGroup.id);

    // }

    editGroupChat() {
        var conv = { id: this.chatGroup.id, avatar: this.chatGroup.avatar, name: this.chatGroup.displayName, members: this.convGroup[0].members }
        this._groupService.editGroupChat(conv).subscribe(res => {
            this._chatService.user.groupChatList.forEach(groupChat => {
                if (groupChat.id === conv.id) {
                    groupChat.name = conv.name;
                    groupChat.avatar = conv.avatar;
                }
            })
        });
    }

    updateUpLoadAvatar(event) {
        this.selectedFile = (event.target.files[0] as File);
        const fd = new FormData();
        fd.append("file", this.selectedFile);
        this._groupService.updateUpLoadAvatar(fd).subscribe((res: any) => {
            this.chatGroup.avatar = res.url;
            this.editGroupChat();
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

    changeNickNameContact() {
        this._userContactService.changeNickNameContact(this.contact).subscribe();
    }
}