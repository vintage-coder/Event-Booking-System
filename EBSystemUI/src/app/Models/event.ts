import { NumberValueAccessor } from "@angular/forms";

export interface Event {
    eventId:number;
    eventName:string;
    startDate:string;
    endDate:string;
    eventCategoryId:number;
    ticketCategoryId:number;
    noOfTickets:number;
    promoCode:string;
}

// "eventId": 1,
// "eventName": "IPL",
// "startDate": "2022-03-09T00:00:00",
// "endDate": "2022-03-22T00:00:00",
// "eventCategoryId": 1,
// "ticketCategoryId": 1,
// "noOfTickets": 1000,
// "promoCode": "IPL0ABT090",
// "event": null,
// "ticketCategory": null

