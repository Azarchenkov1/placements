import { Component } from '@angular/core';
import { HttpClient, HttpHeaders,HttpRequest, HttpEventType } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-placementform',
  templateUrl: './placementform.component.html',
  styleUrls: ['./placementform.component.css']
})
export class PlacementformComponent {
  
  constructor(private http: HttpClient) { }

  submitData(form: NgForm, mainphoto, photo2, photo3, photo4, photo5) {

    var newplacement = {
       id: 0,
   header: form.value.header,
mainphoto: '../../assets/photos/' + mainphoto[0].name,
     type: form.value.type,
 location: form.value.location,
   entity: form.value.entity,
     size: form.value.size,
 fromDate: form.value.fromDate,
   toDate: form.value.toDate,
   photo2: '../../assets/photos/' + photo2[0].name,
   photo3: '../../assets/photos/' + photo3[0].name,
   photo4: '../../assets/photos/' + photo4[0].name,
   photo5: '../../assets/photos/' + photo5[0].name,
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