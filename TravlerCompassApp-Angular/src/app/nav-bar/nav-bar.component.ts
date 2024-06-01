import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../services/alertifyNotification/alertify.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
logginUserName: string | null = '';
  constructor(private alertNotification: AlertifyService) { }

  ngOnInit() {

  }


//this function will login user
//it will return a token
loggedIn(){//if the user is logged in it will contain a value
  this.logginUserName =  localStorage.getItem('token');
  return this.logginUserName;
}

logOut(){//if the user is logged in it will contain a value and remove it from local storage
   localStorage.removeItem('token');
   this.alertNotification.success("You have logout out");

}
}
