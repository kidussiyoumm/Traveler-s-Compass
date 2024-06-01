import { Component, OnInit, Inject, PLATFORM_ID  } from '@angular/core';
import { FormControl, FormGroup, Validators ,ValidatorFn, AbstractControl } from '@angular/forms';
import { UserServiceService } from '../../../services/userServices/user-service.service';
import { UserInterface } from '../../model/user-interface';
import { AlertifyService } from '../../../services/alertifyNotification/alertify.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  constructor(private userService: UserServiceService,
              private alertNotification: AlertifyService
  ) { }
//This formGroup is a class to organize the related form control
//acts as a wraper around the collection of the form controlls
  registrationForm! : FormGroup;
  user!: UserInterface;//user is implementing user-interface
  userSubmitted!: boolean; //condition to check if a form is sumbitted no null values can pass as sumbisson




  ngOnInit() {
    this.registrationForm = new FormGroup({
      userName: new FormControl(null, Validators.required), // we need to bind this in the view model //Tests the value using a regular expression pattern suitable for common use cases.
      email: new FormControl(null, [Validators.required, Validators.email]),//In form control we can pass vaildators as an argument
      password: new FormControl(null, [Validators.required , Validators.minLength(7)]),//We can pass an array of
      confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(7)]),
      address: new FormControl(null, [Validators.required]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.minLength(10)])

    }); //custom valdations , { validators: this.passwordMatchingValidator }



  }    //this is called checking at control level

  //we need to make a custome cross field validation to compare the the passwords
  //we can't fo this in the formControl level there is no inbuild Validators for it
  //fg is just a name and the type is FormGroup
    passwordMatchingValidator: ValidatorFn = (fg: AbstractControl): { [key: string]: boolean } | null => {
      const password = fg.get('password')!.value; //taking the value of password and confirmPassword and comparing them in the if statment
      const confirmPassword = fg.get('confirmPassword')?.value;
      if (password !== confirmPassword) {
        return { notMatched: true }; // key and value of it as true if not matched
      }
      return null; //if condiditons is valid validators should return null
    }
//ValidatorFn and AbstractControl are part of the forms module used to handle form validation.
//ValidatorFn is a type alias for a function that takes an AbstractControl as an argument and returns either
//a validation error object or null. It's used to define custom validators for Angular forms.

//AbstractControl is a base class for form controls, form groups, and form arrays.
// It provides common functionality and properties for handling the value, validation, status, and state of these controls.





//Getter Methods for all form controls
//Angular providers getter methods we can use to expose values as properties
get userName() { //no arguments and must return a value
 return this.registrationForm.get('userName') as FormControl;
}

get email() {
  return this.registrationForm.get('email') as FormControl;
}
get address() {
  return this.registrationForm.get('address') as FormControl;
}
get password() {
  return this.registrationForm.get('password') as FormControl;
}
get confirmPassword() {
  return this.registrationForm.get('confirmPassword') as FormControl;
}
get phoneNumber() {
  return this.registrationForm.get('phoneNumber') as FormControl;
}



  //the view uses a event to call this function
  //this will return the registrationform we have created
  onSubmit(){

    console.log(this.registrationForm);
    this.userSubmitted =true; //we set this to true since it has been submitted


    if (this.registrationForm.valid) {
      // Ensure this.user is initialized
      if (!this.user) {
        this.user = {} as UserInterface; // this is using the interface as a array
      }


    this.user = Object.assign(this.user, this.registrationForm.value);//With the help of object.assign we assign the value of one object to another object
    this.userService.addUser(this.user);//using our userservice to use addUser method
    this.registrationForm.reset(); //this will reset the form
    this.userSubmitted =false;//we set it back to false
    this.alertNotification.success("Congrats, you are successfully registered");
    //this is checking to see if the form we submmited is valid or not and returning a notification
    }else{
      this.alertNotification.error('Kindy resubmit form with valid input');
    }


}






//This method will map the form values to the user interface in the domain model
//this will get the user object containg the value we would like to save in broswers stroage
userData(): UserInterface{
  return this.user = {
userName: this.userName.value, //this.user is from the getter method we are fecthing the cuurent value
email: this.email.value,
password: this.password.value,
mobile: this.phoneNumber.value,
address: this.address.value
  };
}

}
