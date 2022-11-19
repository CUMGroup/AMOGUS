import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { GraphsComponent } from './graphs/graphs.component';
import { MatExpansionModule } from "@angular/material/expansion";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { NgxEchartsModule } from 'ngx-echarts';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    GraphsComponent
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
