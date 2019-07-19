import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { ChatService } from '../../../chat.service';

import { UserInfoService } from 'app/core/account/userInfo.service';
import { UserProfileService } from 'app/core/identity/userprofile.service';

@Component({
    selector: 'chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ChatUserSidenavComponent implements OnInit, OnDestroy {

    selectedFile:File = null;
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(
        private _chatService: ChatService,
        private _userInfoService: UserInfoService,
        private _userProfileService: UserProfileService
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
    ngOnInit() {
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    async changeDisplayName() {
        await this._userInfoService.changeDisplayName().subscribe();
        if(this._userInfoService.userInfo.status == 'offline')
            await this._userProfileService.logOut();

    }

    onUpload(event) {
        this.selectedFile = <File>event.target.files[0];
        var fd = new FormData();
        fd.append('file',this.selectedFile);
        this._userInfoService.onUpload(fd).subscribe();
        
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Change left sidenav view
     *
     * @param view
     */
    changeLeftSidenavView(view): void {
        this._chatService.onLeftSidenavViewChanged.next(view);
    }

}
