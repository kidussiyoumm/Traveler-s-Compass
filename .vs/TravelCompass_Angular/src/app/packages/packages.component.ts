import { Component } from '@angular/core';

@Component({
  selector: 'app-packages',
  templateUrl: './packages.component.html',
  styleUrls: ['./packages.component.css']
})
export class PackagesComponent {

  Package: any = {
    "Id": 1,
    "Type" : "Bali Package",
    "Price" : 3500 
  }
}
