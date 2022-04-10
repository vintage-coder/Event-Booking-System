import { Component, OnInit,AfterViewInit, ViewChild } from '@angular/core';
import { DateAdapter, NativeDateModule } from '@angular/material/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { EventDto } from 'src/app/Dtos/EventDto';
import {MatDialog} from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../Pop-Ups/confirm-dialog/confirm-dialog.component';



@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent implements OnInit {


  displayedColumns: string[] = ['eventId', 'eventName', 'NoOfTickets',
  'eventCategoryName', 'ticketCategoryName','startDate','endDate'];


  

  dataSource = new MatTableDataSource<EventDto>(EVENT_DATA);

  @ViewChild(MatPaginator) paginator: MatPaginator;

 

  ngAfterViewInit() {

    this.dataSource.paginator = this.paginator;
  
  }
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {


  }

  ViewOrders()
  {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
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

