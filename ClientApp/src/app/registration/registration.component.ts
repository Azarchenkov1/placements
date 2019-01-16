import { Component } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  successfulResponse: boolean;
  constructor(private router: Router, private http: HttpClient) { }

  login(form: NgForm) {
    console.log("login<---------------||");
    let credentials = JSON.stringify(form.value);
    this.http.post("http://localhost:3000/api/home/registration", credentials, { 
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
     }).subscribe(response => {
       this.successfulResponse = true;
       console.log("successful  response<---------------||");
     }, err => {
      console.log("error response<---------------||");
      this.successfulResponse = false;
     });
    }
}
