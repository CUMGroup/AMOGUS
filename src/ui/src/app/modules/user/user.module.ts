import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { GraphsComponent } from './graphs/graphs.component';
import { MatExpansionModule } from "@angular/material/expansion";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { NgxEchartsModule } from 'ngx-echarts';
import { FormsModule } from '@angular/forms';
import { StatsComponent } from './pages/stats/stats.component';
import { LevelProgressComponent } from './components/level-progress/level-progress.component';
import { StatsGraphsComponent } from './components/stats-graphs/stats-graphs.component';
import { StatsTableComponent } from './components/stats-table/stats-table.component';


@NgModule({
  declarations: [
    GraphsComponent,
    StatsComponent,
    LevelProgressComponent,
    StatsGraphsComponent,
    StatsTableComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MatExpansionModule,
    MatCheckboxModule,
    MatButtonModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts')
    }),
    FormsModule,
  ]
})
export class UserModule { }
