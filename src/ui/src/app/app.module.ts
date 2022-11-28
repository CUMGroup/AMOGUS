import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


import { AppComponent } from './app.component';
import { ParallaxDirective } from './parallax.directive';
import { AmongusParallaxComponent } from './amongus-parallax/amongus-parallax.component';
import { TextParallaxComponent } from './text-parallax/text-parallax.component';
import { SidewaysScrollComponent } from './sideways-scroll/sideways-scroll.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { GraphsComponent } from './graphs/graphs.component';

import { AppRoutingModule } from './app-routing.module';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';

import { NgxEchartsModule } from 'ngx-echarts';

import * as echarts from 'echarts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatExpansionModule} from "@angular/material/expansion";
import { LoginRegisterComponent } from './login-register/login-register.component';
import { MatFormFieldModule } from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatIconModule} from "@angular/material/icon";
import {MatSliderModule} from "@angular/material/slider";
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { GameViewComponent } from './game-view/game-view.component';
import {MatTabsModule} from "@angular/material/tabs";
import {MatCardModule} from "@angular/material/card";

@NgModule({
  declarations: [
    AppComponent,
    ParallaxDirective,
    AmongusParallaxComponent,
    TextParallaxComponent,
    SidewaysScrollComponent,
    LandingPageComponent,
    GraphsComponent,
    LoginRegisterComponent,
    NavBarComponent,
    GameViewComponent
  ],
  imports: [
    BrowserModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts')
    }),
    MatCheckboxModule,
    MatButtonModule,
    MatExpansionModule,
    MatFormFieldModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule,
    MatCardModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
