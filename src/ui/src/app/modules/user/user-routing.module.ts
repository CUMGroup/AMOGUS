import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './pages/stats/stats.component';
import { GameViewComponent } from "./pages/game-view/game-view.component";

const routes: Routes = [
  { path: 'stats', component: StatsComponent },
  { path: 'game', component: GameViewComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
