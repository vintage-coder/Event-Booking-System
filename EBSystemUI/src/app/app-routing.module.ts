import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HeaderComponent} from './Components/header/header.component';
import {FooterComponent } from './Components/footer/footer.component';
import {EventsListComponent } from './Components/events-list/events-list.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { LoginComponent } from './Components/login/login.component';
import { LogoutComponent } from './Components/logout/logout.component';
import { RegisterComponent } from './Components/register/register.component';
import { CustomerComponent } from './Components/customer/customer.component';
import { AuthGuardGuard } from './Guards/auth-guard.guard';
import { HomeComponent } from './Components/home/home.component';
import { AddEventComponent } from './Components/add-event/add-event.component';
import { AdminPageComponent } from './Components/admin-page/admin-page.component';
import { UserPageComponent } from './Components/user-page/user-page.component';


const routes: Routes = [
   {path:'', redirectTo:'admin',pathMatch:'full'},
   {path:'login',component:LoginComponent},
   {path:'register',component:RegisterComponent},
   {path:'customers', component:CustomerComponent,canActivate: [AuthGuardGuard] },
   {path:'dashboard', component:DashboardComponent},
   {path:'home', component:HomeComponent},
   {path:'addevent',component:AddEventComponent},
   {path:'admin',component:AdminPageComponent},
   {path:'user',component:UserPageComponent}
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
