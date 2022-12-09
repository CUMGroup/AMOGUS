import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { LandingTextParallaxComponent } from './landing-text-parallax/landing-text-parallax.component';
import { TextParallaxComponent } from 'src/app/shared/components/text-parallax/text-parallax.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AmongusParallaxComponent } from './components/amongus-parallax/amongus-parallax.component';
import { LandingComponent } from './pages/landing/landing.component';


@NgModule({
  declarations: [
    LandingTextParallaxComponent,
    TextParallaxComponent,
    LandingPageComponent,
    AmongusParallaxComponent,
    LandingComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
