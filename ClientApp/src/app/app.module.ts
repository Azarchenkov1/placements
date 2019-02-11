import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PlacementformComponent } from './placementform/placementform.component';
import { DetailsComponent } from './details/details.component';
import { ImagetestComponent } from './imagetest/imagetest.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { PlacementsComponent } from './placements/placements.component';
import { EditplacementComponent } from './editplacement/editplacement.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PlacementformComponent,
    DetailsComponent,
    ImagetestComponent,
    LoginComponent,
    RegistrationComponent,
    PlacementsComponent,
    EditplacementComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'placementform', component: PlacementformComponent },
      { path: 'details', component: DetailsComponent },
      { path: 'imagetest', component: ImagetestComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'placements', component: PlacementsComponent },
      { path: 'editplacement', component: EditplacementComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
