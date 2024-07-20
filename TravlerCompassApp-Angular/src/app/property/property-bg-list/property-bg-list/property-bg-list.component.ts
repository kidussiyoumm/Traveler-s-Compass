import { Component, OnInit } from '@angular/core';
import { Packages_dataService } from '../../../../services/packages_data.service';
import { Agent_dataService } from '../../../../services/agentsServices/agent_data.service';
import { IPackage } from '../../../../Interfaces/IPackage.interface';
import { IAgent } from '../../../../Interfaces/IAgent.interface';
//This componet is the one caring the rows/list of packagaes as they car in cards templete
@Component({
  selector: 'app-property-bg-list',
  templateUrl: './property-bg-list.component.html',
  styleUrls: ['./property-bg-list.component.css']
})
export class PropertyBgListComponent implements OnInit {

//property variable with type any as an array //to create an object we need {}
 //this is the packages that is on the UI
 //we will pass this data to the package.json using http calls

 packages: IPackage[] = [];
 agents: IAgent[] =[];
 Tour: string = ''; // Variable to hold the filter input
 TourSearch = ''; //component fetches the packages and uses the Tour variable for filtering
 SortbyParam = '';
 SortDirection = 'asc'; //To change to asc order



//we inject that services to fetch all out package data
  constructor(private packages_data: Packages_dataService,
               private agent_details: Agent_dataService) { }

  ngOnInit():void { // this method returns an observables we now need to use a subscirbe method for it to be excuted
  
    this.packages_data.getAllPackages().subscribe(//gets our packages from packages services
      data=>{ 
        this.packages =data;//In-order to executed and put to use it needs the get method to be subscribed to it by the consumer.
       // Fetching packages from HTTP service
            
       console.log("Fetched packages from JSON:", data);//data is just a name
       console.log("Combined packages:", this.packages);
           
            
      }, //first call back is to get the observable second call back is to hanlde the error
     
     error => {//second call back is error handling
      console.log(error);
     }//third call back is for completion
     );

   this.agent_details.getAllAgents().subscribe(
    data=>{this.agents = data;
      console.log(data)//we are mapping agents to Json data and returning it
      //first call back is to get the observable second call back is to hanlde the error
        },
        error => {
         console.log(error);
        }//third call back is for completion
      );
    }



    onTourFilter(){
      this.TourSearch = this.Tour; //Two way data binding 

    }

    Clear(){
      this.Tour = '';
      this.TourSearch = '';
    }

    onSortDirection(){
      if(this.SortDirection === 'desc'){
        this.SortDirection = 'asc';
      } else{
        this.SortDirection = 'desc';
      }
    }
}
    
  




