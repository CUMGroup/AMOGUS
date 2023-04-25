import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateComponent } from './template.component';

describe('LandingComponent', () => {
  let component: TemplateComponent;;
  let fixture: ComponentFixture<TemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemplateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
