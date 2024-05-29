import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

constructor() { }

//this function will Authenticate the user
authUser(user: any){//passes the user info in this function
  let UserArray: any[] = []; //since our local storage is in an array

  // check if the 'Users' key exists in local storage
const usersData = localStorage.getItem('Users');//getItem has a key to pass we are passing the User key we used to store the local storage data

 //we first check if the user exists first
 if(usersData){//only parse if usersData is not null
 UserArray = JSON.parse(usersData);//we are populating the UserArray chaging the data to string
 }          //we use JSON.parse to access the value since it was in json format
 return UserArray.find(x => x.email === user.email && x.password === user.password);
}

}
