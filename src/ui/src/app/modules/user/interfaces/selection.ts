import {gameOption} from "../../../core/interfaces/question";
import {CategoryType} from "../../../core/interfaces/game-session";

export class Constants{
  Difficulties: string[] = ["Easy","Medium","Hard"];
  Categories = ["Mental Arithmetic", "Analysis", "Geometry"]

  IndexedCategories: gameOption[] = [
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
