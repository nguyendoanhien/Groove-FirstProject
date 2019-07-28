import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { AppsComponent } from './apps.component'
import { ChatComponent } from './chat/chat.component';
import { ChatService } from './chat/chat.service';
import { ChatModule } from './chat/chat.module';
import { AuthRouteGuardService } from 'app/core/auth/authrouteguard.service';
import { MessageHubService } from 'app/core/data-api/hubs/message.hub';
import { ProfileHubService } from 'app/core/data-api/hubs/profile.hub';
import { ContactHubService } from 'app/core/data-api/hubs/contact.hub';
import { FacebookService } from 'ngx-facebook';
const routes = [
    {
        path: 'chat',
        component: ChatComponent,
        resolve: {
            chat: ChatService
        }
    }
];

@NgModule({
    declarations: [
        AppsComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        FuseSharedModule,
        ChatModule
    ],
    providers: [
        MessageHubService,
        ProfileHubService,
        ContactHubService
   
    ]
})
export class AppsModule {

}
