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
import { StatsGraphsComponent } from './components/stats-graphs/stats-graphs.component';
import { StatsTableComponent } from './components/stats-table/stats-table.component';
import { GameViewComponent } from "./pages/game-view/game-view.component";
import { MatCardModule } from "@angular/material/card";
import { LoginRegisterComponent } from "./pages/login-register/login-register.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSliderModule } from "@angular/material/slider";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";

@NgModule({
  declarations: [
    GraphsComponent,
    StatsComponent,
    LevelProgressComponent,
    StatsGraphsComponent,
    StatsTableComponent,
    GameViewComponent,
    LoginRegisterComponent
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
  ]
})
export class UserModule { }
