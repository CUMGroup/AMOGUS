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
    {
      category: CategoryType.RANDOMMENTAL,
      gameType: "Randomized Mental Mode",
    },
    {
      category: CategoryType.RANDOMMENTAL_INSANE,
      gameType: "Randomized Mental Insane Mode",
    },
  ];
}
