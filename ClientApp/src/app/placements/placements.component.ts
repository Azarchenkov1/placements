import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-placements',
  templateUrl: './placements.component.html',
  styleUrls: ['./placements.component.css']
})
export class PlacementsComponent {

  public placementlist = [];
  public page;

  constructor(private http: HttpClient) {
    var QueryContract = { page: localStorage.getItem("page"),
    header: localStorage.getItem("header"),
    type: localStorage.getItem("type"),
    location: localStorage.getItem("location")
  };
    this.page = localStorage.getItem("page");
    console.log(this.page);
    
     //send query
     let url = "http://localhost:3000/api/home/mainquery";
     let pagequery = JSON.stringify(QueryContract);
     console.log(pagequery);
     const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

     this.http.post<PlacementContract[]>(url, pagequery, httpOptions)
     .subscribe(response => { this.placementlist = response; })
   }

   details(event)
   {
    console.log("details");
    var target = event.target || event.srcElement || event.currentTarget;
    var attribute = target.attributes.id;
    var idvalue = attribute.nodeValue;
    console.log('target element have id ' + idvalue + ', initializing details component');
    localStorage.setItem("placement_id", idvalue);
   }
   
   go_left()
   {
    this.page = parseInt(this.page, 10) - 1;
    localStorage.setItem("page", this.page.toString());
    location.reload(); 
   }
   go_right()
   {
     this.page = parseInt(this.page, 10) + 1;
     localStorage.setItem("page", this.page.toString());
     location.reload(); 
   }

}
