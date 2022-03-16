import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './Components/header/header.component';
import { FooterComponent } from './Components/footer/footer.component';
import { HttpClientModule} from "@angular/common/http";
import { AppMaterialModule } from "./app.material-module";
import { EventsListComponent } from './Components/events-list/events-list.component';
import { LoginComponent } from './Components/login/login.component';
import { LogoutComponent } from './Components/logout/logout.component';
import { RegisterComponent } from './Components/register/register.component';
import { CustomerComponent } from './Components/customer/customer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuardGuard } from './Guards/auth-guard.guard';
import { AdminPageComponent } from './Components/admin-page/admin-page.component';
import { UserPageComponent } from './Components/user-page/user-page.component';
import { HomeComponent } from './Components/home/home.component';

export function tokenGetter() {
  return localStorage.getItem("webToken");
}



@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,   
    HeaderComponent,
    FooterComponent,
    EventsListComponent,
    LoginComponent,
    LogoutComponent,
    RegisterComponent,
    CustomerComponent,
    AdminPageComponent,
    UserPageComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    HttpClientModule,
    FormsModule, 
    ReactiveFormsModule,
   
    // ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
    
    JwtModule.forRoot({
      config: {       
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:57589"],
        blacklistedRoutes: []
      }
    })

  ],
  
  providers: [AuthGuardGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
