import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { Agent_dataService } from '../../services/agentsServices/agent_data.service';
import { IAgent } from '../../Interfaces/IAgent.interface';


@Component({
  selector: 'app-agent',
  templateUrl: './agent.component.html',
  styleUrls: ['./agent.component.css']
})
export class AgentComponent implements OnInit {

  @Input() Agent_Ag! : IAgent;

  agents: IAgent[] = [];

  constructor(private agent_data:Agent_dataService) {}

//this one is a void methods that gets all agents
ngOnInit():void {// this method returns an observables we now need to use a subscirbe method for it to be excuted
 this.agent_data.getAllAgents().subscribe( //gets our agents from packages services
 data=>{ this.agents =data; //we are setting the data to the egent array then printing console.log
  console.log(data)//data is just a name
  //first call back is to get the observable second call back is to hanlde the error
 },
error => {//second call back is error handling
  console.log(error);
 }//third call back is for completion
 );


}



}


