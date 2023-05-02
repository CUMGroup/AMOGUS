import {AfterViewInit, Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {gsap} from "gsap";
import {question} from "../../../../core/interfaces/question";
import {GameService} from "../../../../core/services/game.service";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {Subject, Subscription, takeUntil, timer} from 'rxjs'
import { CategoryType } from 'src/app/core/interfaces/game-session';
import {ExerciseComponent} from "../shared/exercise/exercise.component";
import {QuestionPreviewComponent} from "../shared/question-preview/question-preview.component";


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.css'],
})

export class GameViewComponent implements OnInit, AfterViewInit, OnDestroy {

  currentQuestion: question;
  correctAnswers: Array<boolean> = [];
  gameProgress: Subscription;
  answers: string[] = [];
  questionType: CategoryType;

  selectedAnswer = this.formBuilder.control("")

  protected componentDestroyed$: Subject<void> = new Subject<void>();

  newGameSubscription$ : Subscription;
  endGameSubscription$ : Subscription;
  routerSubscription$ : Subscription;

  currentQuestionTimeStart: number;
  questionIndex : number = 0;

  constructor(
    private router: Router,
    public formBuilder: FormBuilder,
    public gameService: GameService,
    public dialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    //this.newGameSubscription$ = this.gameService.startNewGame(this.questionType).subscribe(e => {
      this.newQuestion()
      // });
    }

  ngAfterViewInit(): void {
      this.animate();
  }

  ngOnDestroy(){
    this.componentDestroyed$.next();
    this.componentDestroyed$.complete();
    this.newGameSubscription$?.unsubscribe();
    this.routerSubscription$?.unsubscribe();
    this.gameProgress?.unsubscribe();
    // TODO: Analyse if memory leak problem could arise
    //this.endGameSubscription$?.unsubscribe();
  }

  newQuestion() {
    this.currentQuestion = this.gameService.getQuestion();
    if (this.currentQuestion.finished) {
      const dialogRef = this.dialog.open(AnswerDialog, {
        data: {answers: this.correctAnswers, questions: this.gameService.questions}, panelClass: 'mat-dialog-class'}
      );

      this.routerSubscription$ = dialogRef.afterClosed().pipe(takeUntil(this.componentDestroyed$)).subscribe(() => {
        this.router.navigate([""])
      });

      this.endGameSubscription$ = this.gameService.endGame().subscribe();

    } else {
      this.currentQuestionTimeStart = new Date().getTime();
      this.questionIndex++;
      this.animate();
      this.gameProgress = timer(this.currentQuestion.getTime() * 1000).pipe(takeUntil(this.componentDestroyed$)).subscribe(() => {
        this.submit()
      })
    }
    this.answers = this.currentQuestion.getMultipleChoiceAnswers();
  }

  animate() {
    // gsap.fromTo(".knife",{ rotate:0},{ rotate: 765, duration:this.time})
    gsap.fromTo(".progress", {width: "0%"}, {width: "100%", duration: this.currentQuestion.getTime()})

  }

  submit() {
    const session = this.gameService.getSession();
    this.gameProgress?.unsubscribe();
    if (this.currentQuestion.answer === this.selectedAnswer.value) {
      this.correctAnswers.push(true);
      session.correctAnswersCount++;
    } else {
      this.correctAnswers.push(false);
    }
    if(this.selectedAnswer) {
      session.givenAnswersCount++;
    }
    const questionTime = new Date().getTime() - this.currentQuestionTimeStart;
    session.averageTimePerQuestion += (questionTime - session.averageTimePerQuestion) / Math.max(this.questionIndex, 1);
    if(questionTime < session.quickestAnswer) {
      session.quickestAnswer = questionTime;
    }
    if(questionTime > session.slowestAnswer) {
      session.slowestAnswer = questionTime;
    }
    this.currentQuestion.answer = this.selectedAnswer.value;
    this.newQuestion();
    this.animate();
  }

  getCategoryName() {
    switch(this.questionType) {
      case CategoryType.ANALYSIS:
        return "Analysis";
      case CategoryType.GEOMETRY:
        return "Geometry";
      case CategoryType.MENTAL:
        return "Mental";
    }
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
