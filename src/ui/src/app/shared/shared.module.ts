import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from "./components/nav-bar/nav-bar.component";



@NgModule({
  declarations: [
    NavBarComponent
  ],
  exports: [
    NavBarComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
