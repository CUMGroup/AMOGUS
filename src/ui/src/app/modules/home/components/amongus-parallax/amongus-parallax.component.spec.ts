import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmongusParallaxComponent } from './amongus-parallax.component';

describe('ParallaxComponent', () => {
  let component: AmongusParallaxComponent;
  let fixture: ComponentFixture<AmongusParallaxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmongusParallaxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AmongusParallaxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
