import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
import { HomeComponent } from './Components/customer/home/home.component';
import { FacebookLoginProvider, GoogleLoginProvider, SocialLoginModule} from 'angularx-social-login';
import { AddEventComponent } from './Components/add-event/add-event.component';
import { SidenavListComponent } from './Components/navigation/sidenav-list/sidenav-list.component';
import { CheckOutComponent } from './Components/check-out/check-out.component';
import { ToastrModule } from 'ngx-toastr';
import { FlexLayoutModule } from "@angular/flex-layout";




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
    HomeComponent,
    AddEventComponent,
    SidenavListComponent,
    CheckOutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SocialLoginModule,
    FlexLayoutModule,
    // ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:57589"],
        blacklistedRoutes: []
      }
    })

  ],

  providers: [
    AuthGuardGuard,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('473645421288-192r2fjahgshbp2s4isqta4m59jrlp3j.apps.googleusercontent.com')
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(
              '300303215559380'
            )
          }
        ]
      }
    },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
