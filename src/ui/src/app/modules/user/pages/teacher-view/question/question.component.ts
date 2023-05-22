import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {TeacherService} from "../../../../../core/services/user/teacher.service";
import {MatDialog} from "@angular/material/dialog";
import {QuestionEditViewComponent} from "../question-edit-view/question-edit-view.component";
import {QuestionPreviewComponent} from "../../shared/question-preview/question-preview.component";
import {question} from "../../../../../core/interfaces/question";
import { CategoryTypeNewQuestion, DifficultyTypeNewQuestion } from 'src/app/core/interfaces/new-question';

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

  getStringValue(question: FormGroup, key: string): string {
    let numValue = question.get(key).value;
    if (key === "difficulty") {
      return DifficultyTypeNewQuestion[numValue].toLowerCase();
    }
    if (key === "category") {
      return CategoryTypeNewQuestion[numValue].toLowerCase();
    }
    return 'unknown';
  }

  preview(){
    let quest = this.question.value;
    let questionData = new question(quest.answer, quest.category, quest.difficulty, quest.exercise, quest.experiencePoints, quest.help, quest.questionId, quest.wrongAnswers, false)
    this.dialog.open(QuestionPreviewComponent, { data: questionData, width:"40rem", panelClass: 'mat-dialog-class'});
  }
}

