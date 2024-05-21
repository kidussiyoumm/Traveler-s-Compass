import { Injectable } from '@angular/core'; //Services are using Injectable decorator
import { HttpClient } from '@angular/common/http';

@Injectable({//Services are using Injectable decorator
  providedIn: 'root' //provide this in app.root(app.modules) level/we are injecting this serivces in the root
})//This services class will share the package data throughout components
export class Packages_dataService {

 //we add/inject it our constructors componet
 constructor(private http:HttpClient) {}
//we use a property name/method getAllPackages to fetch all packages
getAllPackages(){  //we use this method to fetch and return all our package data
  return this.http.get('data/packages.json') // using the http get method we can pass any url insde this method and return a json object
 } //returing/sending an observale

}

