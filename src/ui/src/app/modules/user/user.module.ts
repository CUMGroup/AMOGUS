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
import { GameViewComponent } from "./pages/game-view/game-view.component";
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
import { QuestionEditViewComponent } from './pages/teacher-view/question-edit-view/question-edit-view.component';
import {QuestionComponent} from './pages/teacher-view/question/question.component';
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { ExerciseComponent } from './components/exercise/exercise.component';
import { PieGraphComponent } from './components/stats-graphs/pie-graph/pie-graph.component';
import { LineGraphComponent } from './components/stats-graphs/line-graph/line-graph.component';
import {QuestionPreviewComponent} from "./components/question-preview/question-preview.component";
import {Constants} from "./interfaces/selection";
import { SharedModule } from 'src/app/shared/shared.module';
import {MatTooltipModule} from '@angular/material/tooltip';
import { ResultDialogComponent } from './pages/game-view/result-dialog/result-dialog.component';

@NgModule({
  declarations: [
    GraphsComponent,
    StatsComponent,
    LevelProgressComponent,
    StatsTableComponent,
    GameViewComponent,
    LoginRegisterComponent,
    GameSelectionComponent,
    TeacherViewComponent,
    QuestionEditViewComponent,
    QuestionComponent,
    ExerciseComponent,
    QuestionPreviewComponent,
    PieGraphComponent,
    LineGraphComponent,
    ResultDialogComponent,
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
        SharedModule,
        MatTooltipModule
    ],
  providers:[
    Constants
  ]
})
export class UserModule { }
