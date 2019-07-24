import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { AuthRouteGuardService } from './core/auth/authrouteguard.service';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthRouteGuardService],
    loadChildren: () => import('./apps/apps.module').then(mod => mod.AppsModule),
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
