import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

constructor() { }


addUser(user: any){//this method is to add multiple users
  let users: any[] =[];//making an array of users to be added and set inside of it
  if (localStorage.getItem('Users')){ //But first we check using a if condition if there is a key 'users' already
    users = JSON.parse(localStorage.getItem('Users')!);//If the key is present then assign that value to that key //Added ! after localStorage.getItem('Users') to assert that the result is not null.
     //parse is to convert a string into a json object
     users = [user, ...users];//we are adding that user to users array using this ... dots its called a spread operator
  } else{
    users = [user]; // incase there is no key then simply just assign the key to the array
  }
     localStorage.setItem('Users' ,JSON.stringify(users)); //this is a key : value pair // the key is to store multipe users
}                               //this.user is returning an json object so we have to use the Json.strringf method to chage it to a string

}
