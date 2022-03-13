import {Student} from "../Models/student";

import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

import {environment} from "../../environments/environment";

import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
export class StudentService {

  constructor(private httpClient: HttpClient) { }

 
  getStudentsInformation(): Observable<Student[]>{
    return this.httpClient.get<Student[]>(`${environment.baseURL}students.json`);
  }
}