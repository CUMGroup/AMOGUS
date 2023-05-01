namespace AMOGUS.Core.Domain.Models.Generators {
    public class MentalExerciseModel {

        public string Expression { get; set; } = string.Empty;

        public int[] Operands { get; set; } = Array.Empty<int>();
        public int[] Operators { get; set; } = Array.Empty<int>();

        public int MinOperand { get; set; }
        public int MaxOperand { get; set; }

        public int? Answer { get; set; }

        public int? Difficulty { get; set; }
        public int? Xp { get; set; }

    }
}
