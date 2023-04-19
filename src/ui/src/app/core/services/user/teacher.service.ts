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
}
