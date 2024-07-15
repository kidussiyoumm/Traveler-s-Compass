import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
//Angular now shares a single instance of services throughout the full application
import { Routes, RouterModule } from '@angular/router';
//Angular uses routes to navigate through different componets in single page application
import { FormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { PropertyBgListComponent } from './property/property-bg-list/property-bg-list/property-bg-list.component';
import { PropertyPkCardComponent } from './property/property-pk-card/property-pk-card/property-pk-card.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BackgroundComponent } from './background/background.component';
import { Packages_dataService } from '../services/packages_data.service';
import { ItineraryComponent } from './Itinerary/Itinerary.component';
import { AgentComponent } from './agent/agent.component';
import { ResumeComponent } from './resume/resume.component';
import { HomeComponent } from './home/home.component';
import { Agent_dataService } from '../services/agentsServices/agent_data.service';
import { PropertyDetailComponent } from './property/property-details/package-detail/property-detail.component';
import { AgentDetailsComponent } from './agent/agent-details/agent-details/agent-details.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { AccountComponent } from './account/account.component';
import { AgentRegisterationComponent } from './agent/agent-registeration/agent-registeration.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { UserServiceService } from '../services/userServices/user-service.service';
import { AlertifyService } from '../services/alertifyNotification/alertify.service';
import { AuthService } from '../services/auth/auth.service';
import { AddPackageComponent } from './agent/add-package/add-package/add-package.component';
import { AddItineraryComponent } from './agent/add-itinerary/add-itinerary/add-itinerary.component';
import { BookPackageComponent } from './Booking/book-package/book-package.component';
import { BookItineraryComponent } from './Booking/book-itinerary/book-itinerary.component';
import { MenuServiceService } from '../services/menuServices/menu-service.service';



//To define mapping in different components we create a const with an array
 const appRoutes : Routes =[ //each route in this array is a Javascript object
{ path: '', redirectTo: '/user-login', pathMatch: 'full'},
{ path: 'user-login', component: UserLoginComponent },
{ path: 'resume' , component : ResumeComponent},
{ path: 'register-agent', component: AgentRegisterationComponent},
{ path: 'contact-us', component: ContactUsComponent},
{ path:  'agents' , component : AgentComponent},
{ path: 'agent-details/:id', component : AgentDetailsComponent},
{ path: 'packageDetails/:id', component: PropertyDetailComponent },
{ path: 'home', component: HomeComponent }, // Use HomeComponent for the home route
{ path: 'user-register', component: UserRegisterComponent}, 
{ path: 'user-account', component: AccountComponent},
{ path: 'add-package', component: AddPackageComponent},
{ path: 'book-package', component: BookPackageComponent},
{ path: 'book-itinerary', component: BookItineraryComponent},
{ path: 'add-itinerary', component: AddItineraryComponent}
 ]
/**
 *  path: '',
  component: HomeComponent,
  children:[
 { path: 'agent' , component : AgentComponent},
 { path: 'agent-details/:id', component : AgentDetailsComponent},
 { path: 'itinerary' , component : ItineraryComponent},
 { path: 'packageList' , component : PackageComponent},
 { path: 'packageDetails/:id', component: PropertyDetailComponent },
 { path: 'home', component: HomeComponent }, // Use HomeComponent for the home route
 { path: 'user-register', component: UserRegisterComponent},
 { path: 'user-account', component: AccountComponent},
 { path: 'add-package', component: AddPackageComponent},
 { path: 'book-package', component: BookPackageComponent},
 { path: 'book-itinerary', component: BookItineraryComponent},
 { path: 'add-itinerary', component: AddItineraryComponent}
 */





@NgModule({
  declarations: [
    AppComponent,
    PropertyBgListComponent,
    PropertyPkCardComponent,
    NavBarComponent,
    BackgroundComponent,
    ItineraryComponent,
    AgentComponent,
    ResumeComponent,
    HomeComponent,
    PropertyDetailComponent,
    AgentDetailsComponent,
    UserLoginComponent,
    UserRegisterComponent,
    AccountComponent,
    AgentRegisterationComponent,
    ContactUsComponent,
    AddPackageComponent,
    AddItineraryComponent

   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, //Create Services to be consumed
    RouterModule.forRoot(appRoutes), //This will tell Angluar that routes exist in this application
    ReactiveFormsModule, //Adding reactive form module to be used
    FormsModule, //this is for templete driven forms
    TabsModule.forRoot(), //this is installed to create tabs in the application
    ButtonsModule.forRoot() //this is installed to create buttons in the application
  ],
  providers: [
    provideClientHydration(),
    Packages_dataService, //regestering it here in the 'root' level from the services class
    Agent_dataService,
    UserServiceService,
    AlertifyService,
    AuthService,
    MenuServiceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
