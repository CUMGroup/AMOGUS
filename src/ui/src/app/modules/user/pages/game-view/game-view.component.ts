import { Component, OnInit } from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {gsap} from "gsap";
import {question} from "../../../../core/interfaces/question";
import {GameService} from "../../../../core/services/game.service";


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.css']
})
export class GameViewComponent implements OnInit {

  currentQuestion: question;
  solutionControl: FormArray;
  checkboxGroup: FormGroup;
  test:number;

  constructor(public formBuilder:FormBuilder, public gameService:GameService) { }

  ngOnInit(): void {
    this.newQuestion()
  }

  newQuestion(){
    this.currentQuestion = this.gameService.getQuestion();
    this.solutionControl = this.formBuilder.array([])
    switch(this.currentQuestion.type) {
      case "text": break;
      case "multipleChoice":
        for(let i = 0; i<this.currentQuestion.multipleChoiceAnswers.length; i++){
          (this.solutionControl as FormArray).push(new FormControl(false))
        }
        this.checkboxGroup = this.formBuilder.group({
          values: this.solutionControl
        })
        break;
      default: console.log("error occurred");
    }
    this.animate();
    this.timeout().then(() => console.log("GameOver"))
  }

  log(){
    console.log(this.checkboxGroup.controls['values'].value)
    this.test = 10;
    while(this.test === 10) {
      this.test = this.test + 1
    }
  }

  async timeout(){
    await this.delay(this.currentQuestion.time * 1000)
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  animate(){
    // gsap.fromTo(".knife",{ rotate:0},{ rotate: 765, duration:this.time})
    gsap.fromTo(".progress",{width:"0%"},{width:"100%", duration:this.currentQuestion.time})

  }

  submit(){
    let answer = "";
    for (let i = 0; i<this.solutionControl.value.length; i++) {
      if(this.solutionControl.value[i]){
        answer = this.currentQuestion.multipleChoiceAnswers[i]
      }
    }
    if(this.currentQuestion.answer === answer){
      console.log("right")
    }else{
      console.log("wrong")
    }
    this.newQuestion();
    this.animate();
  }

}
