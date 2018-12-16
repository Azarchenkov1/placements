import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-placementform',
  templateUrl: './placementform.component.html',
  styleUrls: ['./placementform.component.css']
})
export class PlacementformComponent {
  
  constructor(private http: HttpClient) { }

  submitData(form: NgForm) {

    var newplacement = {
      id: 0,
      header: form.value.header,
      type: form.value.type,
      location: form.value.location,
      entity: form.value.entity,
      size: form.value.size,
      fromDate: form.value.fromDate,
      toDate: form.value.toDate
    }

    let url = "http://localhost:3000/api/home/newplacement";
    let data = JSON.stringify(newplacement);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this.http.post(url, data, httpOptions)
    .subscribe(response => {
      console.log("data was submited<---------------||");
      var response_data = response;
      console.log(response_data);
    });
  }
}