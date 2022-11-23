import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LevelProgressComponent } from './level-progress.component';

describe('LevelProgressComponent', () => {
  let component: LevelProgressComponent;
  let fixture: ComponentFixture<LevelProgressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LevelProgressComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LevelProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
