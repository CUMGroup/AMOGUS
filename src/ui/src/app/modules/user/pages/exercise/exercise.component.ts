import {Component, Input} from '@angular/core';
import {question} from "../../../../core/interfaces/question";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.scss']
})
export class ExerciseComponent {

  @Input() currentQuestion: question| undefined;
  @Input() selectedAnswer?: FormControl;
  constructor(
  ) {
  }

}
