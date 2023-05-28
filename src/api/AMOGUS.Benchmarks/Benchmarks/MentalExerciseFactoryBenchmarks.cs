using AMOGUS.Core.Factories;
using BenchmarkDotNet.Attributes;

namespace AMOGUS.Benchmarks.Benchmarks {
    [MemoryDiagnoser(false)]
    public class MentalExerciseFactoryBenchmarks {

        private MentalExerciseFactory _mentalExerciseFactory;

        [Params(1, 10, 100, 1_000)]
        public int Amount { get; set; }

        [GlobalSetup]
        public void GlobalSetup() {
            _mentalExerciseFactory = new MentalExerciseFactory();
        }
        [Benchmark]
        public void GenerateRandomExercises() {
            for (int i = 0; i < Amount; ++i) {
                _mentalExerciseFactory.GenerateRandomQuestion(false);
            }
        }

        [Benchmark]
        public void GenerateRandomExercises_Insane() {
            for (int i = 0; i < Amount; ++i) {
                _mentalExerciseFactory.GenerateRandomQuestion(true);
            }
        }

    }
}
