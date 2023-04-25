import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './pages/stats/stats.component';
import { GameViewComponent } from "./pages/game-view/game-view.component";
import { LoginRegisterComponent } from "./pages/login-register/login-register.component";
import {GameSelectionComponent} from "./pages/game-selection/game-selection.component";
import {TeacherViewComponent} from "./pages/teacher-view/teacher-view.component";

const routes: Routes = [
  { path: 'stats', component: StatsComponent },
  { path: 'game', component: GameViewComponent },
  { path: 'login', component: LoginRegisterComponent },
  { path: 'game-selection', component: GameSelectionComponent },
  { path: 'teacher-view', component: TeacherViewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
