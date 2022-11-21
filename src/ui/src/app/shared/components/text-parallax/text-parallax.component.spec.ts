import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextParallaxComponent } from './text-parallax.component';

describe('TextParallaxComponent', () => {
  let component: TextParallaxComponent;
  let fixture: ComponentFixture<TextParallaxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TextParallaxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TextParallaxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
