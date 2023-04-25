import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionEditViewComponent } from './question-edit-view.component';

describe('QuestionViewComponent', () => {
  let component: QuestionEditViewComponent;
  let fixture: ComponentFixture<QuestionEditViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionEditViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionEditViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
