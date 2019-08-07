import { Component, ViewEncapsulation } from "@angular/core";

import { fuseAnimations } from "@fuse/animations";
import { ChatService } from '../chat.service';

@Component({
    selector: "chat-start",
    templateUrl: "./chat-start.component.html",
    styleUrls: ["./chat-start.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ChatStartComponent {
    constructor(private _chatService: ChatService) {

    }
    ngOnInit(): void {
        this._chatService.onChatSelected.next(null);

    }
}