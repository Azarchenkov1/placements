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
    var pageQueryContract = { page: localStorage.getItem("page") };
    this.page = localStorage.getItem("page");
    console.log(this.page);
    
     //send query
     let url = "http://localhost:3000/api/home/placements";
     let pagequery = JSON.stringify(pageQueryContract);
     console.log(pagequery);
     const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

     this.http.post<PlacementContract[]>(url, pagequery, httpOptions)
     .subscribe(response => { this.placementlist = response; })
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
