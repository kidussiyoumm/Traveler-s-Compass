import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'; //map operators allows passing data to a function and returning a new data as a observable and to be subscribed to
import { IAgent } from '../../Interfaces/IAgent.interface';

@Injectable({
  providedIn: 'root'//provide this service at the root level then all component can inject it in there components
})

export class Agent_dataService {

constructor(private http:HttpClient) { }

getAllAgents(): Observable<IAgent[]> {
    return this.http.get<{ [key: string]: IAgent }>('data/agents.json').pipe(//This line makes an HTTP GET request to the URL src/data/packages.json.
      map(data => { //The map operator transforms the data received from the HTTP GET request.
        const AgentArray: IAgent[] = []; //An empty array packagesArray of type IPackage[] is initialized.
        for (const id in data) { //The code iterates over each key (id) in the data object.
          if (data.hasOwnProperty(id)) {
            AgentArray.push(data[id]); //If the data object has the property (id), it pushes the value (data[id]), which is of type IPackage, into the packagesArray.
          }
        }
        return AgentArray;//returing/sending an observale
    })
    );



   }

   //this is the agent beign passed
   addAgent(agent: any){//this method is to add multiple agents
    let agentsArray: any[] = [];//making an array of users to be added and set inside of it
    if (localStorage.getItem('Agents')){ //But first we check using a if condition if there is a key 'Agents' already
      agentsArray = JSON.parse(localStorage.getItem('Agents')!);//If they key is present then assign that value to that key //Added ! after localStorage.getItem('Users') to assert that the result is not null.
       //parse is to convert a string into a json object
       agentsArray = [agent, ...agentsArray];//we are adding that agents to agentsArray array using this ... dots meaning its trailing and we are adding in front of the array
    } else{
      agentsArray = [agentsArray]; // incase there is no key then simply just assign the key to the array
    }
       localStorage.setItem('Agents' ,JSON.stringify(agentsArray)); //this is a key : value pair // the key is to store multipe agents
  }                               //this.user is returning an json object so we have to use the Json.strringf method to chage it to a string

}
