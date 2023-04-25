import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {gsap} from "gsap";
import {question} from "../../../../core/interfaces/question";
import {GameService} from "../../../../core/services/game.service";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {Subject, Subscription, takeUntil, timer} from 'rxjs'
import {ExerciseComponent} from "../shared/exercise/exercise.component";
import {QuestionPreviewComponent} from "../shared/question-preview/question-preview.component";


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.css']
})

export class GameViewComponent implements OnInit,OnDestroy {

  currentQuestion: question;
  correctAnswers: Array<boolean> = [];
  gameProgress: Subscription;
  answers: string[] = [];
  questionType: string = "Analysis"

  selectedAnswer = this.formBuilder.control("")

  protected componentDestroyed$: Subject<void> = new Subject<void>();

  constructor(
    private router: Router,
    public formBuilder: FormBuilder,
    public gameService: GameService,
    public dialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.newQuestion()
  }

  ngOnDestroy(){
    this.componentDestroyed$.next();
    this.componentDestroyed$.complete();
  }

  newQuestion() {
    this.currentQuestion = this.gameService.getQuestion();
    if (this.currentQuestion.finished) {
      const dialogRef = this.dialog.open(AnswerDialog, {
        data: {answers: this.correctAnswers, questions: this.gameService.questions}, panelClass: 'mat-dialog-class'}
      );

      dialogRef.afterClosed().pipe(takeUntil(this.componentDestroyed$)).subscribe(() => {
        this.router.navigate([""])
      });

    } else {
      this.animate();
      this.gameProgress = timer(this.currentQuestion.time * 1000).pipe(takeUntil(this.componentDestroyed$)).subscribe(() => this.submit())
    }
    this.answers = this.currentQuestion.multipleChoiceAnswers;
  }

  animate() {
    // gsap.fromTo(".knife",{ rotate:0},{ rotate: 765, duration:this.time})
    gsap.fromTo(".progress", {width: "0%"}, {width: "100%", duration: this.currentQuestion.time})

  }

  submit() {
    if (this.currentQuestion.answer === this.selectedAnswer.value) {
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
  styleUrls: ['./game-view.component.css']
})

export class AnswerDialog {
  constructor(
    public dialogRef: MatDialogRef<AnswerDialog>,
    public dialog: MatDialog,

  @Inject(MAT_DIALOG_DATA) public data: { answers: Array<boolean>; questions: Array<question> },
  ) {
  }

  showQuestion(index:number){
    this.dialog.open(QuestionPreviewComponent, { data: this.getQuestion(index), width:"40rem", panelClass: 'mat-dialog-class'});
  }

  getQuestion(index:number): question{
    return this.data.questions[index]
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
