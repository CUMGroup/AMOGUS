import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { GraphsComponent } from './graphs/graphs.component';
import { MatExpansionModule } from "@angular/material/expansion";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { NgxEchartsModule } from 'ngx-echarts';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StatsComponent } from './pages/stats/stats.component';
import { LevelProgressComponent } from './components/level-progress/level-progress.component';
import { StatsTableComponent } from './components/stats-table/stats-table.component';
import { AnswerDialog, GameViewComponent } from "./pages/game-view/game-view.component";
import { MatCardModule } from "@angular/material/card";
import { LoginRegisterComponent } from "./pages/login-register/login-register.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import {MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {MatRadioModule} from "@angular/material/radio";
import { GameSelectionComponent } from './pages/game-selection/game-selection.component';
import { TeacherViewComponent } from './pages/teacher-view/teacher-view.component';
import {MatSelectModule} from "@angular/material/select";
import { QuestionViewComponent } from './pages/question-view/question-view.component';
import {QuestionComponent, QuestionPreviewComponent} from './pages/question/question.component';
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { ExerciseComponent } from './pages/exercise/exercise.component';
import { PieGraphComponent } from './components/stats-graphs/pie-graph/pie-graph.component';
import { LineGraphComponent } from './components/stats-graphs/line-graph/line-graph.component';

@NgModule({
  declarations: [
    GraphsComponent,
    StatsComponent,
    LevelProgressComponent,
    StatsTableComponent,
    GameViewComponent,
    LoginRegisterComponent,
    AnswerDialog,
    GameSelectionComponent,
    TeacherViewComponent,
    QuestionViewComponent,
    QuestionComponent,
    ExerciseComponent,
    QuestionPreviewComponent,
    PieGraphComponent,
    LineGraphComponent,
  ],
    imports: [
        CommonModule,
        UserRoutingModule,
        MatExpansionModule,
        MatCheckboxModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        MatSlideToggleModule,
        NgxEchartsModule.forRoot({
            echarts: () => import('echarts')
        }),
        FormsModule,
        MatCardModule,
        ReactiveFormsModule,
        FormsModule,
        MatDialogModule,
        MatRadioModule,
        MatSelectModule,
        MatSnackBarModule,
    ]
})
export class UserModule { }
