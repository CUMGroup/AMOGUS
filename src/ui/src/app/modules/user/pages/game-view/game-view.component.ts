import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { gsap } from "gsap";
import { question } from "../../../../core/interfaces/question";
import { GameService } from "../../../../core/services/game.service";
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { Subscription, timer } from 'rxjs'


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.css']
})

export class GameViewComponent implements OnInit {

  currentQuestion: question;
  solutionControl: FormArray;
  checkboxGroup: FormGroup;
  test: number;
  correctAnswers: Array<boolean> = [];
  gameProgress: Subscription;

  constructor(
    private router: Router,
    public formBuilder: FormBuilder,
    public gameService: GameService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.newQuestion()
  }

  newQuestion() {
    this.currentQuestion = this.gameService.getQuestion();
    if (this.currentQuestion.finished) {
      this.gameProgress.unsubscribe()
      const dialogRef = this.dialog.open(AnswerDialog, {
        data: { answers: this.correctAnswers },
      });

      dialogRef.afterClosed().subscribe(result => {
        this.router.navigate([""])
      });
    } else {
      this.animate();
      this.gameProgress = timer(this.currentQuestion.time * 1000).subscribe(() => this.submit())
    }
    this.solutionControl = this.formBuilder.array([])
    switch (this.currentQuestion.type) {
      case "text": break;
      case "multipleChoice":
        for (let i = 0; i < this.currentQuestion.multipleChoiceAnswers.length; i++) {
          (this.solutionControl as FormArray).push(new FormControl(false))
        }
        this.checkboxGroup = this.formBuilder.group({
          values: this.solutionControl
        })
        break;
      default: console.log("error occurred");
    }
    // this.delay(this.currentQuestion.time * 1000).then(() => this.submit())
  }

  log() {
    console.log(this.correctAnswers)
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  animate() {
    // gsap.fromTo(".knife",{ rotate:0},{ rotate: 765, duration:this.time})
    gsap.fromTo(".progress", { width: "0%" }, { width: "100%", duration: this.currentQuestion.time })

  }

  submit() {
    this.gameProgress.unsubscribe()
    let answer = "";
    for (let i = 0; i < this.solutionControl.value.length; i++) {
      // only works properly if one question is correct
      if (this.solutionControl.value[i]) {
        answer = this.currentQuestion.multipleChoiceAnswers[i]
      }
    }
    if (this.currentQuestion.answer === answer) {
      this.correctAnswers.push(true);
    } else {
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
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
