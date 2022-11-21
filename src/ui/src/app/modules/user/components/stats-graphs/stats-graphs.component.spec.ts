import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatsGraphsComponent } from './stats-graphs.component';

describe('StatsGraphsComponent', () => {
  let component: StatsGraphsComponent;
  let fixture: ComponentFixture<StatsGraphsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatsGraphsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatsGraphsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
