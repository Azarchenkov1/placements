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

  details(event)
  {
    console.log("details");
    var target = event.target || event.srcElement || event.currentTarget;
    var attribute = target.attributes.id;
    var idvalue = attribute.nodeValue;
    idvalue = idvalue - 1;
    console.log('target element have id ' + idvalue + ', initializing details component');
    //index must have -1 correction couse of call placement INDEX,
    //but not element PROPERTY

    sessionStorage.setItem("header", this.placementlist[idvalue].header);
    sessionStorage.setItem("mainphoto", this.placementlist[idvalue].mainphoto);
    sessionStorage.setItem("type", this.placementlist[idvalue].type);
    sessionStorage.setItem("location", this.placementlist[idvalue].location);
    sessionStorage.setItem("entity", this.placementlist[idvalue].entity);
    sessionStorage.setItem("size", this.placementlist[idvalue].size);
    sessionStorage.setItem("fromDate", this.placementlist[idvalue].fromDate);
    sessionStorage.setItem("toDate", this.placementlist[idvalue].toDate);
    sessionStorage.setItem("photo2", this.placementlist[idvalue].photo2);
    sessionStorage.setItem("photo3", this.placementlist[idvalue].photo3);
    sessionStorage.setItem("photo4", this.placementlist[idvalue].photo4);
    sessionStorage.setItem("photo5", this.placementlist[idvalue].photo5);
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