import {CategoryType} from "../../../core/interfaces/game-session";
import {gameOption} from "../../../core/interfaces/question";

export class Constants{
  Difficulties:string[] = ["Easy","Medium","Hard"];
  Categories: gameOption[] = [
    {
      category: CategoryType.MENTAL,
      gameType: "Mental Arithmetic",
    },
    {
      category: CategoryType.ANALYSIS,
      gameType: "Analysis",
    },
    {
      category: CategoryType.GEOMETRY,
      gameType: "Geometry",
    },
  ];
}
