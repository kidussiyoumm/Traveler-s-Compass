import { Injectable } from '@angular/core';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserServicesService {

  constructor() { }

  addUser(user: User){//to add more users to the local storage
    let users = []; //using an array to store multiple users 
    
     // Check if the item exists in local storage and is not null
    const storedUsers = localStorage.getItem('Users');
      if (storedUsers) {
      users = JSON.parse(storedUsers);
      users = [user, ...users];
    }else {
      users = [user];
    }

    localStorage.setItem('Users', JSON.stringify(users));
  }


}
