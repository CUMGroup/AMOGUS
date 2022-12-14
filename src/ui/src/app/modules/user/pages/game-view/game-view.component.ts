import {Component, Inject, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {gsap} from "gsap";
import {question} from "../../../../core/interfaces/question";
import {GameService} from "../../../../core/services/game.service";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {Subscription, timer} from 'rxjs'


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.css']
})

export class GameViewComponent implements OnInit {

  currentQuestion: question;
  test:number;
  correctAnswers: Array<boolean> = [];
  gameProgress: Subscription;
  selectedAnswer: string;
  answers: string[] = [];

  constructor(
    private router: Router,
    public formBuilder:FormBuilder,
    public gameService:GameService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.newQuestion()
  }

  newQuestion(){
    this.currentQuestion = this.gameService.getQuestion();
    if(this.currentQuestion.finished){
      this.gameProgress.unsubscribe()
      const dialogRef = this.dialog.open(AnswerDialog, {
        data: {answers: this.correctAnswers},
      });

      dialogRef.afterClosed().subscribe(result => {
        this.router.navigate([""])
      });

    } else{
      this.animate();
      this.gameProgress = timer(this.currentQuestion.time * 1000).subscribe(() => this.submit())
    }
    switch(this.currentQuestion.type) {
      case "text": break;
      case "multipleChoice":
        this.answers = this.currentQuestion.multipleChoiceAnswers;
        break;
      default: console.log("error occurred");
    }
  }

  log(){
    console.log(this.correctAnswers)
  }

  animate(){
    // gsap.fromTo(".knife",{ rotate:0},{ rotate: 765, duration:this.time})
    gsap.fromTo(".progress",{width:"0%"},{width:"100%", duration:this.currentQuestion.time})

  }

  submit(){
    this.gameProgress.unsubscribe()
    if(this.currentQuestion.answer === this.selectedAnswer){
      this.correctAnswers.push(true);
    }else{
      this.correctAnswers.push(false);
    }
    this.newQuestion();
    this.animate();
  }

}

@Component({
  selector: 'answer-dialog',
  templateUrl: 'answer-dialog.html',
})

export class AnswerDialog {
  constructor(
    public dialogRef: MatDialogRef<AnswerDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { answers: Array<boolean> },
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
