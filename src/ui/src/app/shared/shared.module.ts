import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from "./components/nav-bar/nav-bar.component";
import {RouterModule} from "@angular/router";



@NgModule({
  declarations: [
    NavBarComponent
  ],
  exports: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class SharedModule { }
