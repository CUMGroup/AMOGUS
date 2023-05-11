import { Injectable } from '@angular/core';
import {FormGroup} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})

export class TeacherService {

  questionArray:FormGroup[] = [];
  constructor() {
  }

  remove(index:number){
    this.questionArray.splice(index);
  }

  add(question?:FormGroup){
    this.questionArray.push(question)
  }

  filteredQuestionArray(category,difficulty): FormGroup[]{
    return this.questionArray.filter(value => {
      if(category == null && !difficulty){
        return true;
      }
      if(category == null && difficulty != null){
        return value.value.difficulty === difficulty
      }
      if(category != null && difficulty == null){
        return value.value.category === category
      }
      return value.value.category === category && value.value.difficulty === difficulty

    })
  }
}
