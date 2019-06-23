import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotebookListComponent } from './notebook-list/notebook-list.component';
import { AuthGuardService } from '../core/authguard.service';

const routes: Routes = [
  {
    path: '', component: NotebookListComponent,
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotebookRoutingModule { }
