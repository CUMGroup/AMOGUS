import {Component} from '@angular/core';
import {TeacherService} from "../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionViewComponent} from "../question-view/question-view.component";

@Component({
  selector: 'app-teacher-view',
  templateUrl: './teacher-view.component.html',
  styleUrls: ['./teacher-view.component.css']
})
export class TeacherViewComponent {

  constructor(public teacherService:TeacherService, private dialog: MatDialog) {
  }
  createNewQuestion(){
    this.dialog.open(QuestionViewComponent, { width:"40rem", panelClass: 'mat-dialog-class'});
  }
}
