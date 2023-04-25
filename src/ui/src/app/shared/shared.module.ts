import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from "./components/nav-bar/nav-bar.component";
import {RouterModule} from "@angular/router";
import { AmogusButtonComponent } from './components/amogus-button/amogus-button.component';



@NgModule({
  declarations: [
    NavBarComponent,
    AmogusButtonComponent
  ],
  exports: [
    NavBarComponent,
    AmogusButtonComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
  ]
})
export class SharedModule { }
