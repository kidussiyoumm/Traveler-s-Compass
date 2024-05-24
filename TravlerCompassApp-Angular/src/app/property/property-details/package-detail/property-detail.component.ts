import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
  public packageId!: number;
//Angular has an interface called ActivatedRoute that can be used to access
//an information from a actvie route
  constructor(private route: ActivatedRoute) {

}
  //using the OnInit to exctract the id we are getting in url
  ngOnInit() {
    this.packageId = this.route.snapshot.params['id'];
  }

}
