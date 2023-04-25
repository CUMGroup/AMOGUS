import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {TeacherService} from "../../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "../question-edit-view/question-edit-view.component";
import {QuestionPreviewComponent} from "../../shared/question-preview/question-preview.component";

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
    this.dialog.open(QuestionEditViewComponent, { data: this.question, width:"40rem", panelClass: 'mat-dialog-class'});
  }

  preview(){
    this.dialog.open(QuestionPreviewComponent, { data: this.question.value, width:"40rem", panelClass: 'mat-dialog-class'});
  }
}

