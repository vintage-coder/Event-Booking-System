import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {

  addEventForm:FormGroup;
  public selectedTransaction;
  constructor() { }

  ngOnInit(): void {
  }

  transaction=[
    {value:'internal',viewValue:'Internal'},
    {value:'external',viewValue:'External'}
  ];

  AddEvent()
  {
    console.log("add event was called....");
  }

}
