import { Component, OnInit } from '@angular/core';
import { Packages_dataService } from '../../../../services/packages_data.service';

//This componet is the one caring the rows/list of packagaes as they car in cards templete
@Component({
  selector: 'app-property-bg-list',
  templateUrl: './property-bg-list.component.html',
  styleUrls: ['./property-bg-list.component.css']
})
export class PropertyBgListComponent implements OnInit {
 //property variable with type any as an array //to create an object we need {}
 //this is the packages that is on the UI
 //we weill pass this data to the package.json using http calls
 packages: any = [];

//we inject that services to fetch all out package data
  constructor(private packages_data: Packages_dataService) {
  }

  ngOnInit():void { // this method returns an observables we now need to use a subscirbe method for it to be excuted
   this.packages_data.getAllPackages().subscribe(
    data=>{ this.packages =data;//In-order to executed and put to use it needs the get method to be subscribed to it by the consumer.
  console.log(data)//data is just a name

   }
   )
 }
}



