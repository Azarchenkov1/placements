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

    sessionStorage.setItem("id", this.placementlist[idvalue].id);
    sessionStorage.setItem("header", this.placementlist[idvalue].header);
    sessionStorage.setItem("image_1", this.placementlist[idvalue].image_1);
    sessionStorage.setItem("type", this.placementlist[idvalue].type);
    sessionStorage.setItem("location", this.placementlist[idvalue].location);
    sessionStorage.setItem("entity", this.placementlist[idvalue].entity);
    sessionStorage.setItem("size", this.placementlist[idvalue].size);
    sessionStorage.setItem("fromDate", this.placementlist[idvalue].fromDate);
    sessionStorage.setItem("toDate", this.placementlist[idvalue].toDate);
    sessionStorage.setItem("owner_credentials", this.placementlist[idvalue].owner_credentials);
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