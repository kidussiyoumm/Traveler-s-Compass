import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-property-pk-card',
  templateUrl: './property-pk-card.component.html',
  styleUrls: ['./property-pk-card.component.css']
})
export class PropertyPkCardComponent implements OnInit {
  //property variable with type any
  property: any = { //to create an object we need {}
    "Id" : 1,
    "Tour":"Bali",
    "Price": 4000
  }
  constructor() { }

  ngOnInit() {
  }

}
