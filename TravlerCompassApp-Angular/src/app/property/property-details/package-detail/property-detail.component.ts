import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Packages_dataService } from '../../../../services/packages_data.service';
import { Modelpackage } from '../../../../model/modelPackage';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
  public packageId!: number; // To store the package ID from the route
  packages: Modelpackage = new Modelpackage();  // To store the package details
  galleryOptions!: NgxGalleryOptions[];
  galleryImages!: NgxGalleryImage[];
 
  // @Input() property_pk! : IPackage;

  
//Angular has an interface called ActivatedRoute that can be used to access
//an information from a actvie route
  constructor(private route: ActivatedRoute,
              private PackageService: Packages_dataService
  // Injecting necessary services
  ) {

}
  //using the OnInit to exctract the id we are getting in url
  ngOnInit() {
    this.packageId = +this.route.snapshot.params['id'];
    
  
 // Subscribing to route parameter changes
  this.route.params.subscribe(
    (params) => {
      this.packageId = +params['id'];// Converting the ID to a number
      this.PackageService.getPackage(this.packageId).subscribe(
        (data: Modelpackage | undefined) => {
          if (data) { // Check if data is not undefined

            this.packages = data;// Assigning the title of the package
          } else {
            console.error('Package not found'); // Handle the case where data is undefined
          }
        },
        error => {
          console.error('Error fetching package:', error); // Error handling for the HTTP request
        }
      );
    }
  );

  this.galleryOptions = [
    {
      width: '100%',
      height: '465px',
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      imageArrows: true,
      imageSwipe: true,
      thumbnailsSwipe: true,
      preview: true
    }
  ];

  this.galleryImages = [
    {
      small: 'assets/images/turkey1.jpeg',
      medium: 'assets/images/turkey1.jpeg',
      big: 'assets/images/turkey1.jpeg'
    },
    {
      small: 'assets/images/turkey2.jpg',
      medium: 'assets/images/turkey2.jpg',
      big: 'assets/images/turkey2.jpg'
    },
    {
      small: 'assets/images/turkey3.jpg',
      medium: 'assets/images/turkey3.jpg',
      big: 'assets/images/turkey3.jpg'
    },
    {
      small: 'assets/images/turkey4.jpg',
      medium: 'assets/images/turkey4.jpg',
      big: 'assets/images/turkey4.jpg'
    }
    
  ];

}

}