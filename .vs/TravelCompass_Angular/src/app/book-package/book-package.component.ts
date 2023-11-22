import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-book-package',
  templateUrl: './book-package.component.html',
  styleUrls: ['./book-package.component.css']
})
export class BookPackageComponent implements OnInit {

public packageId: number; 

  constructor(private route: ActivatedRoute, private router: Router){ //Angular provides an interface called ActivatedRoute to acess an information to an sctive route 
    this.packageId = 0;
  }

  ngOnInit(){
    this.packageId = Number(this.route.snapshot.params['id']);//using the number function to incerement the Id number 
  }


  onSelectNext() {
    this.packageId += 1;
    this.router.navigate(['book-package/',  this.packageId])
  }
}
