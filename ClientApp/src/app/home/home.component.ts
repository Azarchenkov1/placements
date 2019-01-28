import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public placementlist = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    localStorage.setItem("page", "1");

    console.log("send initializing request<---------------||");
    http.get<Placement[]>(baseUrl + 'api/home/main').subscribe(result => {
      this.placementlist = result;
      console.log("response received");
    }, error => console.error(error));
  }
}

interface Placement {
  id: number,
  header: string,
  mainphoto: any,
  type: string,
  location: string,
  entity: string,
  size: string,
  fromDate: string,
  toDate: string,
  owner_credentials: string
}