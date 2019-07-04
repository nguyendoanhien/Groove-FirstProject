import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';

const routes: Routes = [
  { path: 'chatbox', component: ChatboxComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatRoutingModule { }
