import {Component} from '@angular/core';
import {TeacherService} from "../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "./question-edit-view/question-edit-view.component";
import {Constants} from "../../interfaces/selection";

@Component({
  selector: 'app-teacher-view',
  templateUrl: './teacher-view.component.html',
  styleUrls: ['./teacher-view.component.css']
})
export class TeacherViewComponent {

  constructor(public teacherService:TeacherService,
              private dialog: MatDialog,
              public constants: Constants
  ) {
  }
  createNewQuestion(){
    this.dialog.open(QuestionEditViewComponent, { width:"40rem", panelClass: 'mat-dialog-class'});
  }
}
