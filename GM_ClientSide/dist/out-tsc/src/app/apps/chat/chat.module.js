import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FuseSharedModule } from '@fuse/shared.module';
import { ChatService } from './chat.service';
import { ChatComponent } from './chat.component';
import { ChatStartComponent } from './chat-start/chat-start.component';
import { ChatViewComponent } from './chat-view/chat-view.component';
import { ChatChatsSidenavComponent } from './sidenavs/left/chats/chats.component';
import { ChatUserSidenavComponent } from './sidenavs/left/user/user.component';
import { ChatLeftSidenavComponent } from './sidenavs/left/left.component';
import { ChatRightSidenavComponent } from './sidenavs/right/right.component';
import { ChatContactSidenavComponent } from './sidenavs/right/contact/contact.component';
import { AuthRouteGuardService } from 'app/core/auth/authrouteguard.service';
const routes = [
    {
        path: '**',
        component: ChatComponent,
        resolve: {
            chat: ChatService
        },
        canActivate: [AuthRouteGuardService]
    }
];
let ChatModule = class ChatModule {
};
ChatModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            ChatComponent,
            ChatViewComponent,
            ChatStartComponent,
            ChatChatsSidenavComponent,
            ChatUserSidenavComponent,
            ChatLeftSidenavComponent,
            ChatRightSidenavComponent,
            ChatContactSidenavComponent
        ],
        imports: [
            RouterModule.forChild(routes),
            MatButtonModule,
            MatCardModule,
            MatFormFieldModule,
            MatIconModule,
            MatInputModule,
            MatListModule,
            MatMenuModule,
            MatRadioModule,
            MatSidenavModule,
            MatToolbarModule,
            FuseSharedModule
        ],
        providers: [
            ChatService
        ]
    })
], ChatModule);
export { ChatModule };
//# sourceMappingURL=chat.module.js.map