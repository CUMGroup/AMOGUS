import {AfterViewInit, Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {gsap} from "gsap";
import {question} from "../../../../core/interfaces/question";
import {GameService} from "../../../../core/services/game.service";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {Subject, Subscription, takeUntil, timer} from 'rxjs'
import {CategoryType, GameSession} from 'src/app/core/interfaces/game-session';
import {QuestionPreviewComponent} from "../../components/question-preview/question-preview.component";
import {ResultDialogComponent} from "./result-dialog/result-dialog.component";
import {BaseComponent} from "../../../../shared/components/base/base.component";


@Component({
  selector: 'app-game-view',
  templateUrl: './game-view.component.html',
  styleUrls: ['./game-view.component.scss'],
})

export class GameViewComponent extends BaseComponent implements OnInit, AfterViewInit, OnDestroy {

  currentQuestion: question;
  correctAnswers: Array<boolean> = [];
  gameProgress: Subscription;
  answers: string[] = [];
  questionType: CategoryType;
  selectedAnswer = this.formBuilder.control("")
  currentQuestionTimeStart: number;
  questionIndex: number = 0;

  constructor(
    private router: Router,
    public formBuilder: FormBuilder,
    public gameService: GameService,
    public dialog: MatDialog
  ) {
    super();
  }

  ngOnInit(): void {
    this.newQuestion()
  }

  ngAfterViewInit(): void {
    this.animate();
  }

  newQuestion() {
    this.currentQuestion = this.gameService.getQuestion();
    if (this.currentQuestion.finished) {
      const dialogRef = this.dialog.open(ResultDialogComponent, {
          data: {answers: this.correctAnswers, questions: this.gameService.questions}, panelClass: 'mat-dialog-class'
        }
      );

      dialogRef.afterClosed().pipe(takeUntil(this.componentDestroyed$)).subscribe(() => {
        this.router.navigate([""])
      });

      this.gameService.endGame().pipe(takeUntil(this.componentDestroyed$)).subscribe();
    } else {
      this.currentQuestionTimeStart = new Date().getTime();
      this.questionIndex++;
      this.animate();
      this.gameProgress = timer(this.currentQuestion.getTime() * 1000).pipe().subscribe(() => {
        this.submit()
      })
    }
    this.answers = this.currentQuestion.getMultipleChoiceAnswers();
  }

  animate() {
    gsap.fromTo(".progress", {width: "0%"}, {width: "100%", duration: this.currentQuestion.getTime()})
  }

  submit() {
    const session = this.gameService.getSession();
    this.gameProgress?.unsubscribe();
    const correctAnswer = this.currentQuestion.answer === this.selectedAnswer.value

    if (correctAnswer) {
      this.correctAnswers.push(true);
      session.correctAnswersCount++;
    } else {
      this.correctAnswers.push(false);
    }

    if (this.selectedAnswer) {
      session.givenAnswersCount++;
    }

    this.calculateQuestionStats(session,correctAnswer)

    this.currentQuestion.answer = this.selectedAnswer.value;

    this.selectedAnswer.reset();

    this.newQuestion();
  }

  calculateQuestionStats(session:GameSession, correctAnswer:boolean){
    const questionTime = new Date().getTime() - this.currentQuestionTimeStart;
    session.averageTimePerQuestion += (questionTime - session.averageTimePerQuestion) / Math.max(this.questionIndex, 1);
    if (questionTime < session.quickestAnswer && correctAnswer) {
      session.quickestAnswer = questionTime;
    }
    if (questionTime > session.slowestAnswer) {
      session.slowestAnswer = questionTime;
    }
  }

  getCategoryName() {
    switch (this.questionType) {
      case CategoryType.ANALYSIS:
        return "Analysis";
      case CategoryType.GEOMETRY:
        return "Geometry";
      case CategoryType.MENTAL:
        return "Mental";
      case CategoryType.RANDOMMENTAL:
        return "Random Mode";
      case CategoryType.RANDOMMENTAL_INSANE:
        return "Random Insane Mode";
    }
  }
}
