import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  onLogin(loginForm: NgForm){
    if (loginForm.valid) {
      console.log('Form Value:', loginForm.value); // This will log the form values
      const email = loginForm.value.email;
      const password = loginForm.value.password;
      // Handle the login logic here
    } else {
      console.log('Form is invalid');
    }
  }
}
