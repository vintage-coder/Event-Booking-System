import { Component } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Student} from "./Models/student";
import {StudentService} from "./Services/student.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EBSystemUI';


  constructor() {
  }

  ngOnInit(){
   
  }
 

}
