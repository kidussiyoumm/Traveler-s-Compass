import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-property-bg-list',
  templateUrl: './property-bg-list.component.html',
  styleUrls: ['./property-bg-list.component.css']
})
export class PropertyBgListComponent implements OnInit {
 //property variable with type any
 packages: Array <any> = [
  { //to create an object we need {}
  "Id" : 1,
  "Tour":"Bali",
  "description" : "2 Weeks trip in bali",
  "Price": 4000
  },
  {
  "Id" : 2,
  "Tour":"Thaliland",
  "description" : "1 Weeks trip in Thailand",
  "Price": 4000
  },
  {
    "Id" : 3,
    "Tour": "Turkey",
    "description" : "5 days in Turkey",
    "Price": 4000
    },
    {
      "Id" : 4,
      "Tour":"Japan",
      "description" : "2 Weeks trip in Japan",
      "Price": 4000
      },
      {
        "Id" : 5,
        "Tour":"Zanzibar",
        "description" : "2 Weeks trip in Zanzibar",
        "Price": 4000
        },
        {
          "Id" : 6,
          "Tour":"Athens",
          "description" : "9 days in Athens",
          "Price": 4000
          }
  ]

  constructor() { }

  ngOnInit() {
  }

}
