import { Component } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.css']
})
export class ImagetestComponent {
  public imageset;
  constructor(private http: HttpClient) { }
  submitData(mainphoto, secondphoto)
  {
    const photoform = new FormData();
    photoform.append(mainphoto[0].name, mainphoto[0]);
    photoform.append(secondphoto[0].name, secondphoto[0]);
    const photoFormSendRequest = new HttpRequest('POST', `api/home/returnimagelist` , photoform);
    this.http.request(photoFormSendRequest)
    .subscribe(response => 
    {
      console.log("photos was submited<---------------||");
      this.imageset = response;
      if(this.imageset != null)
      {
        console.log('string successfuly received');
        console.log(this.imageset);
      }
    });
  }
}