import { Component, OnInit, Input  } from '@angular/core';

@Component({
  selector: 'app-property-pk-card',
  templateUrl: './property-pk-card.component.html',
  styleUrls: ['./property-pk-card.component.css']
})
export class PropertyPkCardComponent implements OnInit {
 //Angular uses this {@input()} decorator to bind a property as an input paramter
 // we do this to pass from one componet to another
 @Input() property_pk : any

 constructor() { }

  ngOnInit() {

  }

}
