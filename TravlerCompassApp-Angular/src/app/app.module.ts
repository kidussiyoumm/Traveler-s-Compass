import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PropertyBgListComponent } from './property/property-bg-list/property-bg-list/property-bg-list.component';
import { PropertyPkCardComponent } from './property/property-pk-card/property-pk-card/property-pk-card.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BackgroundComponent } from './background/background.component';
import { Packages_dataService } from '../services/packages_data.service';
//Angular now shares a single instance of services throughout the full application
@NgModule({
  declarations: [
    AppComponent,
    PropertyBgListComponent,
    PropertyPkCardComponent,
      NavBarComponent,
      BackgroundComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration(),
    Packages_dataService //regestering it here in the 'root' level from the services class
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
