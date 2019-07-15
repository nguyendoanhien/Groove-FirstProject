import { Component, OnDestroy, OnInit, ViewEncapsulation, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { ChatService } from '../../../chat.service';
import { read } from 'fs';
declare var jquery: any;
declare var $: any;
import { User } from 'app/apps/model/user.model';
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


    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(
        private _chatService: ChatService
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
        debugger;
        debugger;
        this._chatService.getUserById("toilati123vn@gmail.com").subscribe((data) => {
            debugger;
            this.user = data
        }
        );
        this.beforeUser = Object.assign({}, this.user);
        this.userForm = new FormGroup({
            mood: new FormControl(this.user.mood),
            status: new FormControl(this.user.status)
        });

        this.userForm.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(500),
                distinctUntilChanged()
            )
            .subscribe(data => {
                this.user.mood = data.mood;
                this.user.status = data.status;
                this._chatService.updateUserData(this.user);
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
    // Edit() {
    //     this.contenteditable = !this.contenteditable; //`this.` was missing in later assignment
    //     this.beforeUser = Object.assign({}, this.user)
    //     // console.log("bfr" + this.beforeUser.name + "_" + "after" + this.user.name);
    // }



    // AcceptChange() {
    //     this.contenteditable = !this.contenteditable;
    //     console.log("bfr" + this.beforeUser.name + "_" + "after" + this.user.name);
    //     this.beforeUser = Object.assign({}, this.user);

    // }
    // RejectChange() {
    //     debugger;
    //     this.contenteditable = !this.contenteditable;
    //     console.log("bfr" + this.beforeUser.name + "_" + "after" + this.user.name);
    //     this.user = Object.assign({}, this.beforeUser);
    // }
    focusFunction() {
        this.contenteditable = true;



    }
    focusOutFunction() {
        this.contenteditable = false;

    }
}
