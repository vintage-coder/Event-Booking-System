import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  eventlist:any=["Dance","Super Singer","Cricket","Pro Kabadi"];

  ngOnInit(): void {
  }

}
