/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { Packages_dataService } from './packages_data.service';

describe('Service: Packages_data', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Packages_dataService]
    });
  });

  it('should ...', inject([Packages_dataService], (service: Packages_dataService) => {
    expect(service).toBeTruthy();
  }));
});
