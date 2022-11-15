import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ParallaxDirective } from './parallax.directive';
import { AmongusParallaxComponent } from './amongus-parallax/amongus-parallax.component';
import { TextParallaxComponent } from './text-parallax/text-parallax.component';
import { SidewaysScrollComponent } from './sideways-scroll/sideways-scroll.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { GraphsComponent } from './graphs/graphs.component';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';

import { NgxEchartsModule } from 'ngx-echarts';

import * as echarts from 'echarts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatExpansionModule} from "@angular/material/expansion";

@NgModule({
  declarations: [
    AppComponent,
    ParallaxDirective,
    AmongusParallaxComponent,
    TextParallaxComponent,
    SidewaysScrollComponent,
    LandingPageComponent,
    GraphsComponent
  ],
  imports: [
    BrowserModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts')
    }),
    MatCheckboxModule,
    MatButtonModule,
    MatExpansionModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
