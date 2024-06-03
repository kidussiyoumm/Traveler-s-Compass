import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../../services/alertifyNotification/alertify.service';
import { FormGroup, Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { IAgent } from '../../../Interfaces/IAgent.interface';
import { Agent_dataService } from '../../../services/agentsServices/agent_data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agent-registeration',
  templateUrl: './agent-registeration.component.html',
  styleUrls: ['./agent-registeration.component.css']
})
export class AgentRegisterationComponent implements OnInit {


registerAgentForm!: FormGroup;
agent!: IAgent;//we will store form information in here
agentSubmitted!: boolean; //condition to check if a form is sumbitted no null values can pass as sumbisson

constructor(private alertNotification: AlertifyService,
              private agentService: Agent_dataService,
              private router: Router

  ) { }

  ngOnInit() {
    this.registerAgentForm = new FormGroup({
      firstName: new FormControl(null, Validators.required), // we need to bind this in the view model //Tests the value using a regular expression pattern suitable for common use cases.
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.required, Validators.email]),//In form control we can pass vaildators as an argument
      password: new FormControl(null, [Validators.required , Validators.minLength(7)]),//We can pass an array of
      confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(7)]),
      address: new FormControl(null, [Validators.required]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.minLength(10)]),
      companyTitle: new FormControl(null, [Validators.required] ),
      gender: new FormControl(null, [Validators.required])

    });
  }

//Getter Methods for all form controls
//Angular providers getter methods we can use to expose values as properties
get firstName() { //no arguments and must return a value
  return this.registerAgentForm.get('firstName') as FormControl;
 }

 get lastName() { //no arguments and must return a value
  return this.registerAgentForm.get('lastName') as FormControl;
 }

 get email() {
   return this.registerAgentForm.get('email') as FormControl;
 }
 get address() {
   return this.registerAgentForm.get('address') as FormControl;
 }
 get password() {
   return this.registerAgentForm.get('password') as FormControl;
 }
 get confirmPassword() {
   return this.registerAgentForm.get('confirmPassword') as FormControl;
 }
 get phoneNumber() {
   return this.registerAgentForm.get('phoneNumber') as FormControl;
 }
 get companyTitle() { //no arguments and must return a value
  return this.registerAgentForm.get('companyTitle') as FormControl;
 }

 get gender() { //no arguments and must return a value
  return this.registerAgentForm.get('gender') as FormControl;
 }


  onSubmit(){
    console.log(this.registerAgentForm);
    this.agentSubmitted =true; //we set this to true since it has been submitted


    if (this.registerAgentForm.valid) {
      // Ensure this.user is initialized
      if (!this.agent) {
        this.agent = {} as IAgent; // this is using the interface as a array
      }


    this.agent = Object.assign(this.agent, this.registerAgentForm.value);//With the help of object.assign we assign the value of one object to another object
    this.agentService.addAgent(this.agent);//using our userservice to use addUser method
    this.registerAgentForm.reset(); //this will reset the form
    this.agentSubmitted =false;//we set it back to false
    this.alertNotification.success("Congrats, you are successfully registered"); //this is checking to see if the form we submmited is valid or not and returning a notification
    this.router.navigate(['/user-login']);
  }else{
      this.alertNotification.error('Kindy resubmit form with valid input');
  }


}


//This method will map the form values to the user interface in the domain model
//this will get the user object containg the value we would like to save in broswers stroage
AagentData(): IAgent{
  return this.agent = {
firstName: this.firstName.value,
lastName: this.lastName.value, //this.user is from the getter method we are fecthing the cuurent value
email: this.email.value,
mobile: this.phoneNumber.value,
address: this.address.value,
companyName: this.companyTitle.value,


  };
}



}



