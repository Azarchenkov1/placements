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
    http.get<Placement[]>(baseUrl + 'api/home/main').subscribe(result => {
      this.placementlist = result;
    }, error => console.error(error));
  }
}

interface Placement {
  id: number,
  header: string,
  mainphoto: string,
  type: string,
  location: string,
  entity: string,
  size: string,
  fromDate: string,
  toDate: string,
  photo2: string,
  photo3: string,
  photo4: string,
  photo5: string,
}