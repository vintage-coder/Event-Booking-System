import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Student} from "./../../Models/student";
import {StudentService} from "./../../Services/student.service";

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {

  public displayedColumns = ['firstName', 'lastName', 'studentEmail', 'yearOfStudy', 'registrationNumber', 'course' ];
  //the source where we will get the data
  public dataSource = new MatTableDataSource<Student>();

  //dependency injection
  constructor(private studentService: StudentService) {
  }

  ngOnInit(){
    //call this method on component load
    this.getStudentsInformation();
  }
  /**
   * This method returns students details
   */
  getStudentsInformation(){
    this.studentService.getStudentsInformation()
      .subscribe((res)=>{
        console.log(res);
        this.dataSource.data = res;
      })
  }

}
