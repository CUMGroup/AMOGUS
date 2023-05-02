import {Component, OnDestroy} from '@angular/core';
import {gameOption} from "../../../../core/interfaces/question";
import {Router} from "@angular/router";
import {GameService} from "../../../../core/services/game.service";
import {HttpClient} from "@angular/common/http";
import { CategoryType } from 'src/app/core/interfaces/game-session';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-game-selection',
  templateUrl: './game-selection.component.html',
  styleUrls: ['./game-selection.component.css']
})
export class GameSelectionComponent implements OnDestroy {

  gameOptions: gameOption[];
  
  newGameSub$ : Subscription;
  
  constructor(
    public http: HttpClient,
    private router: Router,
    public gameService: GameService,
  ) {
    this.gameOptions = [
      {
        category: CategoryType.MENTAL,
        gameType: "Mental arithmetic",
      },
      {
        category: CategoryType.ANALYSIS,
        gameType: "Analysis",
      },
      {
        category: CategoryType.GEOMETRY,
        gameType: "Geometry",
      },
      {
        category: CategoryType.RANDOMMENTAL,
        gameType: "Randomized Mental Mode",
      },
      {
        category: CategoryType.RANDOMMENTAL_INSANE,
        gameType: "Randomized Mental Insane Mode",
      },
    ]
  }
  ngOnDestroy(): void {
    this.newGameSub$?.unsubscribe();
  }

  navigate(category : CategoryType){
    this.gameService.startNewGame(category).subscribe(e => {
      this.router.navigate(["user","game"]);
    });
  }

}
