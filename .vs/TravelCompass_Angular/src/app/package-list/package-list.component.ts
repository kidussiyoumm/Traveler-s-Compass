
import { Component, OnInit } from '@angular/core';
import { OfferCollectionService } from './services/offer-collection.service';
import { IPackage } from '../Interfaces/IPackages.interface';

@Component({
  selector: 'app-package-list',
  templateUrl: './package-list.component.html',
  styleUrls: ['./package-list.component.css']
})
export class PackageListComponent implements OnInit{

  Packages!: Array<IPackage>; // we can't asign an array to an object 

  constructor(private offer:OfferCollectionService) { //db injection in the constructor of the componet 

  }

  ngOnInit(): void {
    this.offer.getAllPackages().subscribe( //we have to use subscribe for the get method used
    data=>{this.Packages = data; //to get the result
    console.log(data);//this one is used to log it in the dev ops tool
    },error => {
      console.log('httperror:');
      console.log(error)
    } 
    );
  } 
    

   
   
  
}
