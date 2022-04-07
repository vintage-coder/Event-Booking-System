export interface EventDto
{
    eventId:number;
    eventName:string;
    startDate:string;
    endDate:string;
    NoOfTickets:number;
    eventCategoryName:string;
    ticketCategoryName:string;
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