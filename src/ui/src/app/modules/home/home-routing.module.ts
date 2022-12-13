import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AmongusParallaxComponent } from './components/amongus-parallax/amongus-parallax.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LandingTextParallaxComponent } from './landing-text-parallax/landing-text-parallax.component';
import { LandingComponent } from './pages/landing/landing.component';

const routes: Routes = [
  { path: 'landing', component: LandingComponent },
  { path: 'text', component: LandingTextParallaxComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
