import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../services/alertifyNotification/alertify.service';
import { PathConstant } from '../../services/PathConstants/pathConstant';
import { Router } from '@angular/router';
import { MenuServiceService } from '../../services/menuServices/menu-service.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {



filteredMenu: any[] = [];
firstName: string | null = null;

  constructor(private alertNotification: AlertifyService,
              private router: Router,
              private menuServices: MenuServiceService

  ) { 

  
  }

  ngOnInit(): void{
    this.menuServices.filteredMenu$.subscribe(menu => {
      this.filteredMenu = menu;
      
    });

    // Retrieve the username from local storage
    this.firstName = localStorage.getItem('firstName');
}


//this function will login user
//it will return a token
loggedIn(){ 
     // Check if the token exists
     return !!localStorage.getItem('token');
}

logOut(): void {//if the user is logged in it will contain a value and remove it from local storage
   localStorage.removeItem('token');
   localStorage.removeItem('firstName'); // Optional: Remove UserName as well
   this.alertNotification.success("You have logout out");
   this.router.navigate(['/user-login']); // Redirect to the login page

}

}
