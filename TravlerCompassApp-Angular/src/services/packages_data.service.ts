import { Injectable } from '@angular/core'; //Services are using Injectable decorator
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'; //map operators allows passing data to a function and returning a new data as a observable and to be subscribed to
import { Observable } from 'rxjs';
import { IPackage } from '../Interfaces/IPackage.interface';
import { Modelpackage } from '../model/modelPackage';
@Injectable({//Services are using Injectable decorator
  providedIn: 'root' //provide this in app.root(app.modules) level/we are injecting this serivces in the root
})//This services class will share the package data throughout components
export class Packages_dataService {

 //we add/inject it our constructors componet
 constructor(private http:HttpClient) {}
//The getAllPackages method fetches all packages from a JSON file and returns an observable of an array of IPackage objects.
getAllPackages(): Observable<IPackage[]> {
  return this.http.get<{ [key: string]: IPackage }>('data/packages.json').pipe(//This line makes an HTTP GET request to the URL src/data/packages.json.
    map(data => { //The map operator transforms the data received from the HTTP GET request.
      const packagesArray: IPackage[] = []; //An empty array packagesArray of type IPackage[] is initialized.
      for (const id in data) { //The code iterates over each key (id) in the data object.
        if (data.hasOwnProperty(id)) {
           packagesArray.push(data[id]); //If the data object has the property (id), it pushes the value (data[id]), which is of type IPackage, into the packagesArray.
        }
      }
      return packagesArray;//returing/sending an observale
  })
  );
 }



 addPackage(packages : Modelpackage){
  localStorage.setItem('newPackage', JSON.stringify(packages));
}
}
