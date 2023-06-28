import {Component, OnDestroy} from '@angular/core';
import {gameOption} from "../../../../core/interfaces/question";
import {Router} from "@angular/router";
import {GameService} from "../../../../core/services/game.service";
import {HttpClient} from "@angular/common/http";
import { CategoryType } from 'src/app/core/interfaces/game-session';
import { Subscription } from 'rxjs';
import {Constants} from "../../interfaces/selection";

@Component({
  selector: 'app-game-selection',
  templateUrl: './game-selection.component.html',
  styleUrls: ['./game-selection.component.css']
})
export class GameSelectionComponent implements OnDestroy {

  newGameSub$ : Subscription;

  constructor(
    public http: HttpClient,
    private router: Router,
    public gameService: GameService,
    public constants: Constants,
  ) { }
  ngOnDestroy(): void {
    this.newGameSub$?.unsubscribe();
  }

  navigate(category : CategoryType){
    console.log("reached")
    this.gameService.startNewGame(category).subscribe(e => {
      console.log("reached sub")
      this.router.navigate(["user","game"]);
    });
  }

}
