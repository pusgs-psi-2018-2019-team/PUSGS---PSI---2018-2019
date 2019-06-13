import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketPriceEditComponent } from './ticket-price-edit.component';

describe('TicketPriceEditComponent', () => {
  let component: TicketPriceEditComponent;
  let fixture: ComponentFixture<TicketPriceEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketPriceEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketPriceEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
