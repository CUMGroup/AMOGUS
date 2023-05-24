import {Component} from '@angular/core';
import {TeacherService} from "../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "./question-edit-view/question-edit-view.component";
import {Constants} from "../../interfaces/selection";
import { FormBuilder, FormControl } from '@angular/forms';
import { BaseComponent } from 'src/app/shared/components/base/base.component';

@Component({
  selector: 'app-teacher-view',
  templateUrl: './teacher-view.component.html',
  styleUrls: ['./teacher-view.component.css']
})
export class TeacherViewComponent extends BaseComponent{

  noSelectCategory:string[] = [undefined]
  noSelectDifficulty:string[] = [undefined]

  categoryForm: FormControl = this.formBuilder.control(undefined)
  difficultyForm: FormControl = this.formBuilder.control(undefined)

  constructor(private formBuilder: FormBuilder,
              public teacherService:TeacherService,
              private readonly dialog: MatDialog,
              public readonly constants: Constants
  ) {
    super()
    this.noSelectCategory = this.noSelectCategory.concat(constants.Categories)
    this.noSelectDifficulty = this.noSelectDifficulty.concat(constants.Difficulties)
  }
  createNewQuestion(){
    this.dialog.open(QuestionEditViewComponent, { width:"40rem", panelClass: 'mat-dialog-class'});
  }
  ngOnInit(){
    this.teacherService.getAllQuestions();
  }
}
