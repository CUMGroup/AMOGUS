import {Component, Input, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {question} from "../../../../core/interfaces/question";

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.scss']
})
export class ExerciseComponent {

  @Input() currentQuestion: question;
  @Input() selectedAnswer?: FormControl;
  constructor(
  ) {
  }

}
