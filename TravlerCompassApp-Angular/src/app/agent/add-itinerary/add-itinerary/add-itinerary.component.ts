import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {  NgForm } from '@angular/forms';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { IItinerary } from '../../../../Interfaces/IItinerary,interface';

@Component({
  selector: 'app-add-itinerary',
  templateUrl: './add-itinerary.component.html',
  styleUrls: ['./add-itinerary.component.css']
})
export class AddItineraryComponent implements OnInit {
  @ViewChild('Form') addItineraryForm!: NgForm;
  @ViewChild('formTabs') formTabs!: TabsetComponent;
  @Input() property_pk! : IItinerary;
  SellRent = '1';


  //Will come from Database later
  titleName: Array<string> = ['Thailand', 'Athens']; 
  
//We are using two way binding
  ItineraryView : IItinerary = {
  Id: 1,
  Tour: '',
  Price: null,
  Discription: '',
  Title: ''

  };
  

  constructor(private router: Router) { }

  ngOnInit() {


  }


  onBack(){
    this.router.navigate(['/']);
  }

  onSubmit(){
    console.log('Congrats, Form Submitted');
    console.log(this.addItineraryForm); 
  }

  selectTab(tabId: number) {
    this.formTabs.tabs[tabId].active = true;
  }
}


