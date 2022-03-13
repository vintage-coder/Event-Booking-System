import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HeaderComponent} from './Components/header/header.component';

import {FooterComponent } from './Components/footer/footer.component';

import { EventsListComponent } from './Components/events-list/events-list.component';

const routes: Routes = [
  {path:'event',component:EventsListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
