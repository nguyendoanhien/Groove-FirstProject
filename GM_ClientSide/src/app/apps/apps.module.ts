import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import {AppsComponent} from './apps.component'
import { ChatComponent } from './chat/chat.component';
import { ChatService } from './chat/chat.service';
import {ChatModule} from './chat/chat.module';
import { AuthRouteGuardService } from 'app/core/auth/authrouteguard.service';
const routes = [
    {
        path        : 'chat',
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
    imports     : [
        RouterModule.forChild(routes),
        FuseSharedModule,
        ChatModule
    ]
})
export class AppsModule
{
    
}
