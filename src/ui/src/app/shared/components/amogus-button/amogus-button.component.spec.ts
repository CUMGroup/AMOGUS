import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmogusButtonComponent } from './amogus-button.component';

describe('AmogusButtonComponent', () => {
  let component: AmogusButtonComponent;
  let fixture: ComponentFixture<AmogusButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmogusButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AmogusButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
