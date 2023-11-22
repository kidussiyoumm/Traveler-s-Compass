import { TestBed } from '@angular/core/testing';

import { OfferCollectionService } from './offer-collection.service';

describe('OfferCollectionService', () => {
  let service: OfferCollectionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfferCollectionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
