import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './pages/stats/stats.component';
import { LoginRegisterComponent } from "./pages/login-register/login-register.component";

const routes: Routes = [
  { path: 'stats', component: StatsComponent },
  { path: 'login', component: LoginRegisterComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
