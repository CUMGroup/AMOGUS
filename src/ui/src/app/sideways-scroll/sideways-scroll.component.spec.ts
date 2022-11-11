import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidewaysScrollComponent } from './sideways-scroll.component';

describe('SidewaysScrollComponent', () => {
  let component: SidewaysScrollComponent;
  let fixture: ComponentFixture<SidewaysScrollComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidewaysScrollComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SidewaysScrollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
