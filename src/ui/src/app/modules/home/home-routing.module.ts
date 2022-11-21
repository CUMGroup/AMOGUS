import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AmongusParallaxComponent } from './amongus-parallax/amongus-parallax.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LandingTextParallaxComponent } from './landing-text-parallax/landing-text-parallax.component';

const routes: Routes = [
  { path: 'parallax', component: AmongusParallaxComponent },
  { path: 'landing', component: LandingPageComponent },
  { path: 'text', component: LandingTextParallaxComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
