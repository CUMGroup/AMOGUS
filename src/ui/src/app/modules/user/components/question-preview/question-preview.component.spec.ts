import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionPreviewComponent } from './question-preview.component';

describe('QuestionPreviewComponent', () => {
  let component: QuestionPreviewComponent;
  let fixture: ComponentFixture<QuestionPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionPreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
