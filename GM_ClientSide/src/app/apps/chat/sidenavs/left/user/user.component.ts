import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { ChatService } from '../../../chat.service';
import { userInfo } from './userInfo.model';
import { UserInfoService } from 'app/core/account/userInfo.service';

@Component({
    selector: 'chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ChatUserSidenavComponent implements OnInit, OnDestroy {

    userInfo: userInfo
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
        this._userInfoService.getUserInfo().subscribe(res => {
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

    changeDisplayName() {
        this._userInfoService.changeDisplayName(this.userInfo).subscribe(res => this.userInfo = res as userInfo)
    }

    onUpload(event) {
        this.selectedFile = <File>event.target.files[0];
        var fd = new FormData();
        fd.append('file',this.selectedFile);
        this._userInfoService.onUpload(fd).subscribe((res: any) => {
            this.userInfo.avatar = res.url;
            this.changeDisplayName();
        });
        
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
