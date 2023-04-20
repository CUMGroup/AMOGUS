import {Component, Inject, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {TeacherService} from "../../../../core/services/user/teacher.service";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {QuestionViewComponent} from "../question-view/question-view.component";
import {question} from "../../../../core/interfaces/question";

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {

  @Input() question: FormGroup;
  @Input() index: number;

  constructor(public teacherService:TeacherService, private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  edit(){
    this.dialog.open(QuestionViewComponent, { data: this.question, width:"40rem", panelClass: 'mat-dialog-class'});
  }

  preview(){
    this.dialog.open(QuestionPreviewComponent, { data: this.question.value, width:"40rem", height:"40rem", panelClass: 'mat-dialog-class'});
  }
}

@Component({
  selector: 'app-question-preview',
  templateUrl: './question-preview.component.html',
  styleUrls: ['../exercise/exercise.component.scss']
})
export class QuestionPreviewComponent{
  selectedAnswer = this.formBuilder.control("")
  constructor(
    public dialogRef: MatDialogRef<QuestionViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data:question,
    public formBuilder: FormBuilder,
  ) {
    this.data.multipleChoiceAnswers = [];
    this.data.multipleChoiceAnswers.push(this.data.answer)
    this.data.wrongAnswers.forEach((answer) => this.data.multipleChoiceAnswers.push(answer))
  }

  close(){
    this.dialogRef.close()
  }

}
