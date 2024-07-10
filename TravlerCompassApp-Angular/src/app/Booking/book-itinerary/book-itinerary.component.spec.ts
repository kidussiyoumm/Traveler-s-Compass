import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookItineraryComponent } from './book-itinerary.component';

describe('BookItineraryComponent', () => {
  let component: BookItineraryComponent;
  let fixture: ComponentFixture<BookItineraryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BookItineraryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookItineraryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
