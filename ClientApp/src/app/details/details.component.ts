import { Component } from '@angular/core';
import { HttpClient, HttpHeaders,HttpRequest} from '@angular/common/http';
import { Router } from "@angular/router";



@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent {
  header: string;
  image_1: any;
  type: string;
  location: string;
  entity: string;
  size: string;
  fromDate: string;
  toDate: string;

  owner_credentials: string;
  user_id: string;
  isAdmin: string;

  constructor(private http: HttpClient, private router: Router) {
    this.header = sessionStorage.getItem("header");
    this.image_1 = sessionStorage.getItem("image_1");
    this.type = sessionStorage.getItem("type");
    this.location = sessionStorage.getItem("location");
    this.entity = sessionStorage.getItem("entity");
    this.size = sessionStorage.getItem("size");
    this.fromDate = sessionStorage.getItem("fromDate");
    this.toDate = sessionStorage.getItem("toDate");

    this.owner_credentials = sessionStorage.getItem("owner_credentials");
    this.user_id = localStorage.getItem("user_id");
    this.isAdmin = localStorage.getItem("isAdmin"); 
   }

   isHaveEditRights()
   {
    if(this.owner_credentials == this.user_id || this.isAdmin == "True")
    {
      return true;
    }
   }

   deleteclick()
   {
     console.log("deleteclick");

     var data = {
      jwt_token:  localStorage.getItem("jwt"),
      placement_id:  sessionStorage.getItem("id")
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