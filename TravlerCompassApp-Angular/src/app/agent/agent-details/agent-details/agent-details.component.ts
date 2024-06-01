import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';




@Component({
  selector: 'app-agent-details',
  templateUrl: './agent-details.component.html',
  styleUrls: ['./agent-details.component.css']
})
export class AgentDetailsComponent implements OnInit {
  public agentDetails!: number;
  constructor(
    private route: ActivatedRoute) { }

  ngOnInit():void {
    this.agentDetails = this.route.snapshot.params['Id'];

        }
}
