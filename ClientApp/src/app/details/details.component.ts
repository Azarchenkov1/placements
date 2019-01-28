import { Component } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent {

  public placement;

  constructor(private http: HttpClient, private router: Router) {
    var RequestedPlacement = { id: localStorage.getItem("placement_id") };
    let url = "http://localhost:3000/api/home/details";
    let RequestedPlacementJson = JSON.stringify(RequestedPlacement);
    console.log(RequestedPlacementJson);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

    this.http.post<PlacementContract>(url, RequestedPlacementJson, httpOptions)
    .subscribe(response => { this.placement = response });
  }

  isHaveEditRights()  
  {
    var user_id = localStorage.getItem("user_id");
    var isAdmin = localStorage.getItem("isAdmin");
    console.log("local user_id" + user_id);
    console.log("received user_id" + this.placement.userId);
    if(user_id == this.placement.userId || isAdmin == "True")
    {
      return true;
    }
  }

   deleteclick()
   {
     console.log("deleteclick");

     var data = {
      jwt_token: localStorage.getItem("jwt"),
      placement_id: localStorage.getItem("placement_id")
     }
     console.log(data.jwt_token);
     console.log(data.placement_id);

     //send query
     let url = "http://localhost:3000/api/home/deleteplacement";
     let deletequery = JSON.stringify(data);
     const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

     this.http.post(url, deletequery, httpOptions)
     .subscribe(response => {
      var response_data = response;
      console.log(response_data);
      if(response_data == "successful response")
      {
        this.router.navigate(["/"]);
      }
     })
    }
}