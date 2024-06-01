import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth.service';
import { AlertifyService } from '../../../services/alertifyNotification/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(private authService: AuthService,
              private alertifyNotification: AlertifyService,
              private router: Router
  ){

  }

  ngOnInit(){
  }


  onLogin(loginForm: NgForm){
    if (loginForm.valid) { //we pass the authenticate by passing the loginForms value
      console.log('Form Value:', loginForm.value); //This will log the form values
      const email = loginForm.value.email;
      const password = loginForm.value.password;
      const token = this.authService.authUser({ email, password });//we will use Auth services to authenticate the user and password from the local storage
      // Handle the login logic here

      if (token) {
        localStorage.setItem('token', token.userName)
        console.log('Login Successful');
        this.alertifyNotification.success("successfully logged in");
        this.router.navigate(['/home']);
        // Optionally, redirect the user or update the UI
      } else {
        console.log('Login not Successful');
        this.alertifyNotification.error("Email or password missmacth. Try again");
        // Optionally, display an error message to the user
      }
    } else {
      console.log('Form is invalid'); //this will
      this.alertifyNotification.warning("Error while trying to login");
    }
  }



}
