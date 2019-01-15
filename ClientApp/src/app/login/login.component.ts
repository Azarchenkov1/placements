import { Component } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  invalidLogin: boolean;
  constructor(private router: Router, private http: HttpClient) { }

login(form: NgForm) {
  console.log("login<---------------||");
  let credentials = JSON.stringify(form.value);
  this.http.post("http://localhost:3000/api/home/login", credentials, { 
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
   }).subscribe(response => {
     let token = (<any>response).token;
     localStorage.setItem("jwt", token);
     localStorage.setItem("userLogin", form.value.userLogin);
     this.invalidLogin = false;
     this.router.navigate(["/"]);
     console.log("successful  response<---------------||");
   }, err => {
    console.log("error response<---------------||");
     this.invalidLogin = true;
   });
  }
}