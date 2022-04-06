import { Component, OnInit,AfterViewInit, ViewChild } from '@angular/core';
import { DateAdapter, NativeDateModule } from '@angular/material/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { EventDto } from 'src/app/Dtos/EventDto';




@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent implements OnInit {


  displayedColumns1: string[] = ['eventId', 'eventName', 'NoOfTickets',
  'eventCategoryName', 'ticketCategoryName','startDate','endDate'];

  displayedColumns: string[] = ['position', 'name', 'weight',
  'symbol'];


  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  dataSource1 = new MatTableDataSource<EventDto>(EVENT_DATA);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatPaginator) paginator1: MatPaginator;

  ngAfterViewInit() {

    this.dataSource.paginator = this.paginator;
    this.dataSource1.paginator = this.paginator;
  }
  constructor() { }

  ngOnInit(): void {


  }



}

const EVENT_DATA:EventDto[]=[
  { eventId:1, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:2, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:3, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:4, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:5, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:6, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:7, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:8, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:9, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
  { eventId:10, eventName:"IPL",NoOfTickets:5000,
  eventCategoryName:"Sports",ticketCategoryName:"Platinum",
  startDate:"2022-03-23", endDate:"2022-04-23" },
];


const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
  {position: 11, name: 'Sodium', weight: 22.9897, symbol: 'Na'},
  {position: 12, name: 'Magnesium', weight: 24.305, symbol: 'Mg'},
  {position: 13, name: 'Aluminum', weight: 26.9815, symbol: 'Al'},
  {position: 14, name: 'Silicon', weight: 28.0855, symbol: 'Si'},
  {position: 15, name: 'Phosphorus', weight: 30.9738, symbol: 'P'},
  {position: 16, name: 'Sulfur', weight: 32.065, symbol: 'S'},
  {position: 17, name: 'Chlorine', weight: 35.453, symbol: 'Cl'},
  {position: 18, name: 'Argon', weight: 39.948, symbol: 'Ar'},
  {position: 19, name: 'Potassium', weight: 39.0983, symbol: 'K'},
  {position: 20, name: 'Calcium', weight: 40.078, symbol: 'Ca'},
];
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}


// {
// 	"eventId": 1,
// 	"eventName":"IPL",
// 	"startDate":"2022-03-23",
// 	"endDate":"2022-04-30",
// 	"NoOfTickets":5000,
// 	"eventCategoryId":1,
// 	"ticketCategoryId":1,
// 	"eventCategory":
// 	{
// 		"eventCategoryId":1,
// 		"eventCategoryName:"Sports"
// 	},
// 	"ticketCategory":
// 	{
// 		"ticketCategoryId":1,
// 		"ticketCategoryName":"Premium"
// 	}
// }
