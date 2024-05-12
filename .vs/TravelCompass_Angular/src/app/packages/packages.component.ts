import { Component, Input, OnInit } from '@angular/core';
import { IPackage } from '../Interfaces/IPackages.interface';

@Component({
  selector: 'app-packages',
  templateUrl: './packages.component.html',
  styleUrls: ['./packages.component.css']
})
export class PackagesComponent{
  @Input() package: IPackage = {} as IPackage; ; 


}
