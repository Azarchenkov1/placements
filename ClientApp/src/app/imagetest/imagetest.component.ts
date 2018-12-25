import { Component, Inject } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.css']
})
export class ImagetestComponent {
  public str;
  constructor(private http: HttpClient) { }
  submitData(mainphoto)
  {
    const photoform = new FormData();
    photoform.append(mainphoto[0].name, mainphoto[0]);
    const photoFormSendRequest = new HttpRequest('POST', `api/home/returnimagelist` , photoform);
this.http.request(photoFormSendRequest)
.subscribe(response => {
console.log("photos was submited<---------------||");
var response_data = response;
if(response_data != null)
{
         console.log('string successfuly received');
         console.log(response_data);
         this.str = response_data;
}
});
  }
}