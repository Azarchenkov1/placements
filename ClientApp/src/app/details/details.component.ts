import { Component } from '@angular/core';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent {
  header: string;
  image_1: any;
  type: string;
  location: string;
  entity: string;
  size: string;
  fromDate: string;
  toDate: string;
  image_2: any;
  image_3: any;
  image_4: any;
  image_5: any;

  constructor() {
    this.header = sessionStorage.getItem("header");
    this.image_1 = sessionStorage.getItem("image_1");
    this.type = sessionStorage.getItem("type");
    this.location = sessionStorage.getItem("location");
    this.entity = sessionStorage.getItem("entity");
    this.size = sessionStorage.getItem("size");
    this.fromDate = sessionStorage.getItem("fromDate");
    this.toDate = sessionStorage.getItem("toDate");
    this.image_2 = sessionStorage.getItem("image_2");
    this.image_3 = sessionStorage.getItem("image_3");
    this.image_4 = sessionStorage.getItem("image_4");
    this.image_5 = sessionStorage.getItem("image_5");
   }
}