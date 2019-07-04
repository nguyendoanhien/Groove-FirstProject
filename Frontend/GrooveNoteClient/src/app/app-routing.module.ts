import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthGuardService } from './core/authguard.service';

const routes: Routes = [
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
  },
  {
    path: 'notebook',
    loadChildren: () => import('./notebook/notebook.module').then(mod => mod.NotebookModule)
  },
  {
    path: 'chat',
    loadChildren: () => import('./chat/chat.module').then(mod => mod.ChatModule)
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
