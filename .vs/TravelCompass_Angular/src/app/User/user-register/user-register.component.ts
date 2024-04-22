import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/model/user';
import { UserServicesService } from 'src/app/package-list/user-services.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {
  userRegistrationForm!: FormGroup;
  user! : User; //user array 
  userSubmitted: boolean = true; //This boolean will alert user that they can't send data with no input

  constructor(private fb: FormBuilder, private userService : UserServicesService ) { //we inject a formbuilder to the constructor.
    this.createRegisterationForm();                                                  //injecting the user as a services 
  }

  ngOnInit(): void {
  
    this.createRegisterationForm();
 
  
  }

 
 createRegisterationForm(){
  this.userRegistrationForm = this.fb.group({
  name:[null, Validators.required],
  email:[ null, [Validators.required, Validators.email]],
  address: [null, [Validators.required]],
  password: [null, [Validators.required, Validators.minLength(8)]],
  confirmPassword: [null, [Validators.required, Validators.minLength(8)]],
  mobile: [null, [Validators.required, Validators.minLength(10)]],
  
  });

 }


 //This is getting the values from user interface(model class) only way to relate to specific data models 
userData(): User{
  return this.user = {
    name : this.name.value,
    email : this.email.value,
    address : this.address.value,
    mobile : this.mobile.value,
    password : this.password.value
  }

}

 
 //Getter methods for form controlls 
  get name() {
    return this.userRegistrationForm.get('name') as FormControl;
  }
  get email() {
    return this.userRegistrationForm.get('email') as FormControl;
  }
  get address() {
    return this.userRegistrationForm.get('address') as FormControl;
  }
  get password() {
    return this.userRegistrationForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.userRegistrationForm.get('confirmPassword') as FormControl;
  }
  get mobile() {
    return this.userRegistrationForm.get('mobile') as FormControl;
  }


  onSubmit() {
   
    console.log(this.userRegistrationForm.value);
    this.userSubmitted = true; 

    if(this.userRegistrationForm.valid){//checks at componet level if user form is valid to send data to local storage
    //this.user = Object.assign(this.user, this.userRegistrationForm.value);//using theobject.assign to assign two objects together  // Not the correct way 
   // localStorage.setItem('Users', JSON.stringify(this.user));//Here we are storing user in local storage with key and value setItems(); 
    this.userService.addUser(this.userData());//to use the addUser to store to local storage by calling the service method 
    this.userRegistrationForm.reset();
    //this.userSubmitted = false; 
    }
  }

 
    
}

  

