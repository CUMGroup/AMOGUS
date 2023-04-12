import {Component} from '@angular/core';
import {gameOption} from "../../../../core/interfaces/question";
import {Router} from "@angular/router";
import {GameService} from "../../../../core/services/game.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-game-selection',
  templateUrl: './game-selection.component.html',
  styleUrls: ['./game-selection.component.css']
})
export class GameSelectionComponent {
  gameOptions: gameOption[];
  constructor(
    public http: HttpClient,
    private router: Router,
    public gameService: GameService,
  ) {
    this.gameOptions = [
      {
        gameType: "Mental arithmetic",
      },
      {
        gameType: "Analysis",
      },
      {
        gameType: "Arithmetic",
      },
    ]
  }

  navigate(){
    this.http.get('https://example.com/api/things')
      .subscribe(
        data => console.log(data),
        err => console.log(err)
      );
    this.gameService.startNewGame();
    this.router.navigate(["user","game"])
  }

}
