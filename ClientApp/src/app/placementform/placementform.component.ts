import { Component } from '@angular/core';
import { HttpClient, HttpHeaders,HttpRequest} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from "@angular/router";

@Component({
  selector: 'app-placementform',
  templateUrl: './placementform.component.html',
  styleUrls: ['./placementform.component.css']
})
export class PlacementformComponent {
  
  constructor(private http: HttpClient, private router: Router) { }

  submitData(form: NgForm, mainphoto, photo2, photo3, photo4, photo5) {

    var token = localStorage.getItem("jwt");

    var newplacement = {
       id: 0,
   header: form.value.header,
mainphoto: mainphoto[0].name,
     type: form.value.type,
 location: form.value.location,
   entity: form.value.entity,
     size: form.value.size,
 fromDate: form.value.fromDate,
   toDate: form.value.toDate,
   photo2: photo2[0].name,
   photo3: photo3[0].name,
   photo4: photo4[0].name,
   photo5: photo5[0].name,
   jwt_token: token
}
console.log("newplacement initialized<---------------||");

//send data
let url = "http://localhost:3000/api/home/newplacement";
let data = JSON.stringify(newplacement);
const httpOptions = {
headers: new HttpHeaders({
'Content-Type': 'application/json'
})
};
this.http.post(url, data, httpOptions)
.subscribe(response => {
console.log("data was submited<---------------||");
var response_data = response;
console.log(response_data);
if(response_data == "successful response")
{
  this.router.navigate(["/"]);
}
});

const photoform = new FormData();
photoform.append(mainphoto[0].name, mainphoto[0]);
photoform.append(photo2[0].name, photo2[0]);
photoform.append(photo3[0].name, photo3[0]);
photoform.append(photo4[0].name, photo4[0]);
photoform.append(photo5[0].name, photo5[0]);
console.log("photoform initialized<---------------||");

//send photos
const photoFormSendRequest = new HttpRequest('POST', `api/home/uploadfile` , photoform);
this.http.request(photoFormSendRequest)
.subscribe(response => {
console.log("photos was submited<---------------||");
var response_data = response;
console.log(response_data);
});
 }
}