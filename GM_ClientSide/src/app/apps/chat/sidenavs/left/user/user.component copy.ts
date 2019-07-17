import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { ChatService } from '../../../chat.service';
declare var jquery: any;
declare var $: any;
import { User } from 'app/apps/model/user.model';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { UserProfileModel } from 'app/account/user-profile/user-profile.model';
@Component({
    selector: 'chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ChatUserSidenavComponent implements OnInit, OnDestroy {
    user: User;
    beforeUser: any;
    userForm: FormGroup;
    contenteditable = false;
    _userProfileModel: UserProfileModel;

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
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this._userProfileModel = this._userProfileService.CurrentUserProfileModel();
        this._userProfileService.getUserById(this._userProfileModel.UserName).subscribe((data) => {
            console.log(this.user);
            this.user = data
            console.log(this.user);
            this.beforeUser = Object.assign({}, this.user);
            this.userForm = new FormGroup({
                // mood: new FormControl(this.user.mood),
                // status: new FormControl(this.user.status)
                mood: new FormControl(this.user.displayName),
                status: new FormControl(this.user.displayName)
            });

            this.userForm.valueChanges
                .pipe(
                    takeUntil(this._unsubscribeAll),
                    debounceTime(500),
                    distinctUntilChanged()
                )
                .subscribe(data => {
                    // this.user.mood = data.mood;
                    // this.user.status = data.status;
                    console.log(data);
                    this.user.mood = data.mood;
                    this.user.status = data.status;
                    this._chatService.updateUserData(this.user);
                });
        }


        );

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
     * Change left sidenav view
     *
     * @param view
     */

    changeLeftSidenavView(view): void {
        this._chatService.onLeftSidenavViewChanged.next(view);
    }

    focusFunction() {
        this.contenteditable = true;
    }
    focusOutFunction() {
        this.contenteditable = false;
        debugger;
        this._userProfileService.editUser(this._userProfileModel.UserName, this.user).subscribe((data) => {
            console.log(data)
        });
    }
}