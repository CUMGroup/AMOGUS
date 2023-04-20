import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingTextParallaxComponent } from './landing-text-parallax/landing-text-parallax.component';
import { LandingComponent } from './pages/landing/landing.component';
import {HowToPageComponent} from "./pages/how-to-page/how-to-page.component";

const routes: Routes = [
  { path: 'landing', component: LandingComponent },
  { path: 'how-to', component: HowToPageComponent },
  { path: 'text', component: LandingTextParallaxComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
