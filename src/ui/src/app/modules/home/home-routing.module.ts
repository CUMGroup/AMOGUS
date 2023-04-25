import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './pages/landing/landing.component';
import {HowToPageComponent} from "./pages/how-to-page/how-to-page.component";

const routes: Routes = [
  { path: 'landing', component: LandingComponent },
  { path: 'how-to', component: HowToPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
