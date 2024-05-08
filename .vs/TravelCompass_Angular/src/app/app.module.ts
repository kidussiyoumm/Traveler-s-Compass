import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PackagesComponent } from './packages/packages.component';
import { PackageListComponent } from './package-list/package-list.component';
import { OfferCollectionService } from './package-list/services/offer-collection.service';
import { AddPackageComponent } from './add-package/add-package.component';
import { AgentComponent } from './agent/agent.component';
import { ItineraryComponent } from './itinerary/itinerary.component';
import { BookPackageComponent } from './book-package/book-package.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { UserLoginComponent } from './User/user-login/user-login.component';
import { UserRegisterComponent } from './User/user-register/user-register.component';
import { UserServicesService } from './package-list/user-services.service';
import { ProfileComponent } from './profile/profile.component';
import { ResumeComponent } from './resume/resume.component';

//we need to map different componets
const appRoutes : Routes = [
  {path: '' , component: HomePageComponent},
  {path: '' , component: PackageListComponent},
  {path: 'add-package' , component: AddPackageComponent},
  {path: 'agents' , component: AgentComponent},
  {path: 'itineraries' , component: ItineraryComponent},
  {path: 'book-package/:id' , component: BookPackageComponent},
  {path: 'contact-us' , component: ContactUsComponent},
  {path: 'user-login' , component: UserLoginComponent},
  {path: 'user-register' , component: UserRegisterComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'resume', component: ResumeComponent},
  {path: '**' , component: HomePageComponent}//Page not found is used as **
]


@NgModule({
  declarations: [ //All our components must be declared 
    AppComponent,
    HomePageComponent,
    NavBarComponent,
    PackagesComponent,
    PackageListComponent,
    AddPackageComponent,
    AgentComponent,
    ItineraryComponent,
    BookPackageComponent,
    ContactUsComponent,
    UserLoginComponent,
    UserRegisterComponent,
    ProfileComponent,
    ResumeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, //db injection
    FormsModule, //For Binding like ngModule
    ReactiveFormsModule, // For the UI being reactive instead of temple form
    RouterModule.forRoot(appRoutes) //so the app knows routes from one to another
  
  ],
  providers: [
    OfferCollectionService,
    UserServicesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
