import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "../../pages/teacher-view/question-edit-view/question-edit-view.component";
import {question} from "../../../../core/interfaces/question";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-question-preview',
  templateUrl: './question-preview.component.html',
  styleUrls: ['./question-preview.component.css']
})
export class QuestionPreviewComponent{
  selectedAnswer = this.formBuilder.control(this.data.answer)
  constructor(
    public dialogRef: MatDialogRef<QuestionEditViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data:question,
    public formBuilder: FormBuilder,
  ) {

  }

  close(){
    this.dialogRef.close()
  }

}
