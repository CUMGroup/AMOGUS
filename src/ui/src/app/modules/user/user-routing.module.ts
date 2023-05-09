import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './pages/stats/stats.component';
import { GameViewComponent } from "./pages/game-view/game-view.component";
import { LoginRegisterComponent } from "./pages/login-register/login-register.component";
import {GameSelectionComponent} from "./pages/game-selection/game-selection.component";
import {TeacherViewComponent} from "./pages/teacher-view/teacher-view.component";
import { AuthGuardService } from 'src/app/core/services/authentication/auth-guard.service';
import { RoleGuardService } from 'src/app/core/services/authentication/role-guard.service';

const routes: Routes = [
  { 
    path: 'stats', 
    component: StatsComponent,
    canActivate: [AuthGuardService]
  },
  { 
    path: 'game', 
    component: GameViewComponent,
    canActivate: [AuthGuardService]
  },
  { 
    path: 'login', 
    component: LoginRegisterComponent,
  },
  { 
    path: 'game-selection', 
    component: GameSelectionComponent,
    canActivate: [AuthGuardService]
  },
  { 
    path: 'teacher-view', 
    component: TeacherViewComponent, 
    canActivate: [RoleGuardService]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
