import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandingTextParallaxComponent } from './landing-text-parallax.component';

describe('LandingTextParallaxComponent', () => {
  let component: LandingTextParallaxComponent;
  let fixture: ComponentFixture<LandingTextParallaxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandingTextParallaxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandingTextParallaxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
