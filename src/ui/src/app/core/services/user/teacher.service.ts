import { Injectable } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import { ApiService } from '../api.service';
import { Observable, tap } from 'rxjs';
import { HttpStatusCode } from '@angular/common/http';
import { NewQuestion } from '../../interfaces/new-question';

@Injectable({
  providedIn: 'root'
})

export class TeacherService {

  questionArray: FormGroup[] = [];
  questions: NewQuestion[] = [];

  constructor(private apiService: ApiService, private formBuilder: FormBuilder,) {
  }

  remove(index:number){
    let fg = this.questionArray[index];
    let route = 'teachers/questions/' + fg.value['questionId'];
    return this.apiService.delete<HttpStatusCode>(route).subscribe(
      resp => {
        this.questionArray.splice(index);
      }
    );
  }

  add(questionGroup?:FormGroup){

    console.log(questionGroup);
    let question = this.parseQuestion(questionGroup['value']);
    console.log(question);

    return this.apiService.post<Observable<HttpStatusCode>>('/teachers/questions', question).subscribe(
      resp => {
        questionGroup.setValue(resp);
        this.questionArray.push(questionGroup);
      }
    );
  }

  getAllQuestions(){
    return this.apiService.get<NewQuestion[]>('/teachers/questions').subscribe(
      resp => {
        resp.forEach(question => {
          let schema = this.parseFormGroup();
          schema.patchValue(question);
          this.questionArray.push(schema);
        })
      }
    );
  }

  parseQuestion(questionValue?: FormGroup) {
    let newQuestion = new NewQuestion(
      '',
      questionValue['exercise'], 
      questionValue['answer'], 
      questionValue['wrongAnswers'],
      questionValue['help'],
      questionValue['category'], 
      questionValue['difficulty'], 
      questionValue['experiencePoints'], 
    );
    return newQuestion;
  }

  parseFormGroup() {
    return this.formBuilder.group({
        questionId: "",
        exercise: ["",Validators.required ],
        answer: ["", Validators.required ],
        wrongAnswers: this.formBuilder.array([
          this.formBuilder.control("",Validators.required),
          this.formBuilder.control("",Validators.required),
          this.formBuilder.control("",Validators.required),
        ]),
        help: ["", Validators.required ],
        difficulty: [0, Validators.required ],
        category: [0, Validators.required ],
        experiencePoints: [1, Validators.required],
      }
    )
  }

}
