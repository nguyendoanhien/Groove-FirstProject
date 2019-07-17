import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

import { ChatService } from '../../../chat.service';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { userInfo } from './userInfo.model';

@Component({
    selector: 'chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ChatUserSidenavComponent implements OnInit, OnDestroy {

    userInfo: userInfo
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(
        private _chatService: ChatService,
        private _userProfileService: UserProfileService
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
        this.userInfo = new userInfo();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit() {


        this._userProfileService.getUserInfo().subscribe(res => {
            this.userInfo = res as userInfo;
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

    changeName() {
        console.log(this.userInfo)
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
