import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './Components/header/header.component';
import { FooterComponent } from './Components/footer/footer.component';
// import {MatToolbarModule} from '@angular/material/toolbar'; 
import { HttpClientModule} from "@angular/common/http";
import { AppMaterialModule } from "./app.material-module";
import { EventsListComponent } from './Components/events-list/events-list.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
   
    HeaderComponent,
    FooterComponent,
    EventsListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    HttpClientModule
    // ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
