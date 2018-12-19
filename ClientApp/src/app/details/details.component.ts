import { Component } from '@angular/core';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent {
  header: string;
  mainphoto: string;
  type: string;
  location: string;
  entity: string;
  size: string;
  fromDate: string;
  toDate: string;
  photo2: string;
  photo3: string;
  photo4: string;
  photo5: string;

  constructor() {
    this.header = sessionStorage.getItem("header");
    this.mainphoto = sessionStorage.getItem("mainphoto");
    this.type = sessionStorage.getItem("type");
    this.location = sessionStorage.getItem("location");
    this.entity = sessionStorage.getItem("entity");
    this.size = sessionStorage.getItem("size");
    this.fromDate = sessionStorage.getItem("fromDate");
    this.toDate = sessionStorage.getItem("toDate");
    this.photo2 = sessionStorage.getItem("photo2");
    this.photo3 = sessionStorage.getItem("photo3");
    this.photo4 = sessionStorage.getItem("photo4");
    this.photo5 = sessionStorage.getItem("photo5");
   }
}