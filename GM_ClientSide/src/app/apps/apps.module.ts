import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import {AppsComponent} from './apps.component'
const routes = [
    {
        path        : 'chat',
        loadChildren: './chat/chat.module#ChatModule'
    },
];

@NgModule({
    declarations: [
       AppsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),
        FuseSharedModule
    ]
})
export class AppsModule
{
    
}
