import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderLogedInComponent } from './header-loged-in.component';

describe('HeaderLogedInComponent', () => {
  let component: HeaderLogedInComponent;
  let fixture: ComponentFixture<HeaderLogedInComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeaderLogedInComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderLogedInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
