import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {TeacherService} from "../../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "../question-edit-view/question-edit-view.component";
import {QuestionPreviewComponent} from "../../shared/question-preview/question-preview.component";
import {question} from "../../../../../core/interfaces/question";
import {Constants} from "../../../interfaces/selection";

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {

  @Input() question: FormGroup;
  @Input() index: number;

  constructor(public teacherService:TeacherService, private dialog: MatDialog, private constants: Constants) { }

  ngOnInit(): void {

  }

  edit(){
    this.dialog.open(QuestionEditViewComponent, { data: this.question, width:"40rem", panelClass: 'mat-dialog-class'});
  }

  getStringValue(question: FormGroup, key: string): string {
    let numValue = question.get(key).value;
    if (key === "difficulty") {
      return this.constants.Difficulties[numValue];
    }
    if (key === "category") {
      return this.constants.Categories[numValue];
    }
    return 'unknown';
  }

  preview(){
    let quest = this.question.value;
    let questionData = new question(quest.answer, quest.category, quest.difficulty, quest.exercise, quest.experiencePoints, quest.help, quest.questionId, quest.wrongAnswers, false)
    this.dialog.open(QuestionPreviewComponent, { data: questionData, width:"40rem", panelClass: 'mat-dialog-class'});
  }
}

