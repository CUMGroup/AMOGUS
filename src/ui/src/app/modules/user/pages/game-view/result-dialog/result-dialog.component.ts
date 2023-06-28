import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {question} from "../../../../../core/interfaces/question";
import {QuestionPreviewComponent} from "../../../components/question-preview/question-preview.component";

@Component({
  selector: 'app-result-dialog',
  templateUrl: './result-dialog.component.html',
  styleUrls: ['./result-dialog.component.scss']
})
export class ResultDialogComponent{

  constructor(
    public dialogRef: MatDialogRef<ResultDialogComponent>,
    public dialog: MatDialog,

    @Inject(MAT_DIALOG_DATA) public data: { answers: Array<boolean>; questions: Array<question> },
  ) {
  }

  showQuestion(index:number){
    this.dialog.open(QuestionPreviewComponent, { data: this.getQuestion(index), width:"40rem", panelClass: 'mat-dialog-class'});
  }

  getQuestion(index:number): question{
    return this.data.questions[index]
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
