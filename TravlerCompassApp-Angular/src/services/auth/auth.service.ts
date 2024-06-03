import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor() { }

//this function will Authenticate the user
authUser(user: any){//passes the user info in this function
  let usersArray: any[] = []; //for users since our local storage is in an array
  let agentsArray: any[] = [];

  // check if the 'Users' key exists in local storage
const usersData = localStorage.getItem('Users');//getItem has a key to pass, we are passing the User key we used to store the local storage data
if (usersData) {
  usersArray = JSON.parse(usersData); //Populate usersArray
}

const agentsData = localStorage.getItem('Agents');
    if (agentsData) {
      agentsArray = JSON.parse(agentsData); // Populate agentsArray
    }


 // Find and return the user if it exists in either Users or Agents
 const foundUser = usersArray.find(x => x.email === user.email && x.password === user.password);
 if (foundUser) {
   return { ...foundUser, role: 'User' };
 }

 const foundAgent = agentsArray.find(x => x.email === user.email && x.password === user.password);
 if (foundAgent) {
   return { ...foundAgent, role: 'Agent' };
 }

 return null;


}
}


// Return null if no user is found
 //we first check if the user exists first
 //if(usersData){//only parse if usersData is not null
 //UserArray = JSON.parse(usersData);//we are populating the UserArray chaging the data to string
       //we use JSON.parse to access the value since it was in json format
 //return UserArray.find(x => x.email === user.email && x.password === user.password);
    // Find and return the user object if it exists



