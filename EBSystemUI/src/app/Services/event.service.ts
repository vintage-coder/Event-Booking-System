import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Event} from './../Models/event';
import { Observable } from 'rxjs';

import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private router:Router, private http:HttpClient) { }

  getAllEvents():Observable<Event[]>
  {
    return this.http.get<Event[]>(`${environment.apiURL}`+"/ebs/v1/Event/getallevents");
  }

}
