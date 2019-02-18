import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from "@angular/router";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  constructor( private router: Router ) { }

  submitData(form: NgForm)
  {
    if(form.value.header != null)
    {
      localStorage.setItem("header",form.value.header);
    } else { localStorage.setItem("header",null); }
    if(form.value.type != null)
    {
      localStorage.setItem("type",form.value.type);
    } else { localStorage.setItem("type", null); }
    if(form.value.location != null)
    {
      localStorage.setItem("location",form.value.location);
    } else { localStorage.setItem("location", null); }

    this.router.navigate(["/placements"]);
  }
}
