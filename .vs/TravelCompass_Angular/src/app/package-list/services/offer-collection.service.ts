import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';//allows passing some data to a function and returns a new data 
import { IPackage } from 'src/app/Interfaces/IPackages.interface';
import { Observable } from 'rxjs';

//this ts class is used to create sa service for other class/commponets to share all the packages objects
//rather than having all of them use db injection on each compomnets.
@Injectable({ // we can inject services here
  providedIn: 'root' // here shows where we want to provide that service location 'root' mean app.module level in the providers level
  
})

export class OfferCollectionService {

  constructor(private http: HttpClient) { }

  getAllPackages(): Observable<IPackage[]> {
    return this.http.get<{ [key: string]: IPackage }>('data/properties.json').pipe(
      map((data) => {
        const packagesArray: IPackage[] = Object.values(data);
        return packagesArray;
      })
    );
  }
} 