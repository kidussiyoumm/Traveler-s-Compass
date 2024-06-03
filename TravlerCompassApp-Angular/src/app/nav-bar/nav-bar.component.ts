import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../services/alertifyNotification/alertify.service';
import { pathConstants } from '../../assets/pathConstants/pathsConstants';
import { Router } from '@angular/router';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
loggInUserName: string | null = '';
menus: any[] = [];
filteredMenus: any[] = [];
role: string = '';
  constructor(private alertNotification: AlertifyService,
              private router: Router
  ) { }

  ngOnInit() {


}


//this function will login user
//it will return a token
loggedIn(){//if the user is logged in it will contain a value
  this.loggInUserName =  localStorage.getItem('token');//check this one
  return this.loggInUserName;
}

logOut(){//if the user is logged in it will contain a value and remove it from local storage
   localStorage.removeItem('token');
   this.alertNotification.success("You have logout out");
   this.router.navigate(['/user-login']); // Redirect to the login page

}

}
