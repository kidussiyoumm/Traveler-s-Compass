import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth.service';
import { AlertifyService } from '../../../services/alertifyNotification/alertify.service';
import { Router } from '@angular/router';
import { PathConstant } from '../../../services/PathConstants/pathConstant'; 
import e from 'express';
import { MenuServiceService } from '../../../services/menuServices/menu-service.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})

export class UserLoginComponent implements OnInit {


  loggInUserName: string | null = ''; 
  menus: any[] = [];
  filteredMenu : any [] = [];
  role: string = ''; 


  constructor(private authService: AuthService,
              private alertifyNotification: AlertifyService,
              private router: Router,
              private menuService: MenuServiceService
  ){}

  ngOnInit(): void {
    this.initializeMenu();
  }


  initializeMenu(): void {
  
    const storedRole = localStorage.getItem('role'); // Retrieve the role from localStorage
    this.menus = PathConstant.menus;
    const userData = localStorage.getItem('token');
    if (userData != null) {
      const parseObj = JSON.parse(userData);// we get the values passed from the token
      this.role = parseObj.role;//role is instanstiated 
    }else {
      if(storedRole) {
        this.role = storedRole;
      }
       // Reset role if no user data is found
  
}
    console.log('Menus:', this.menus); // Log the menus
    console.log('Role:', this.role); // Log the role

   
      
    if (this.menus.length && this.role) {
      this.filteredMenu = this.menus.filter((element: any) => element.roles.includes(this.role));
      this.menuService.setFilteredMenu(this.filteredMenu); // Update the shared service
    } else { 
      console.error('Menus or role are not defined.');
     
    }

    console.log('Filtered Menus:', this.filteredMenu); // Log the filtered menus

  }
    
  
    
  


  onLogin(loginForm: NgForm): void {
    if (loginForm.valid) { //we pass the authenticate by passing the loginForms value
      const email = loginForm.value.email;//this will get the email
      const password = loginForm.value.password;//this will grt the password form form
      const token = this.authService.authUser({ email, password });//we will use Auth services to authenticate the user and password from the local storage
      // Handle the login logic here
    
      if (token) {//token.userName
        localStorage.setItem('token', JSON.stringify(token));
        localStorage.setItem('role', token.role);
        localStorage.setItem('firstName', token.firstName); // Store the username 
       
        this.alertifyNotification.success("successfully logged in");
        this.initializeMenu(); // Update the menu after successful login
        this.router.navigate(['/home']);
        // Optionally, redirect the user or update the UI
      } else {
       
        this.alertifyNotification.error("Email or password missmacth. Try again");
        // Optionally, display an error message to the user
      }
    } else {
     
      this.alertifyNotification.warning("Error while trying to login");
    }
  }



}
