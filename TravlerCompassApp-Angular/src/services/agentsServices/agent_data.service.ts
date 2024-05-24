import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Agent_dataService {

constructor(private http:HttpClient) { }
getAllAgents(){
  return this.http.get('data/agents.json')
}
}
