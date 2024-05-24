/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { Agent_dataService } from './agent_data.service';

describe('Service: Agent_data', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Agent_dataService]
    });
  });

  it('should ...', inject([Agent_dataService], (service: Agent_dataService) => {
    expect(service).toBeTruthy();
  }));
});
