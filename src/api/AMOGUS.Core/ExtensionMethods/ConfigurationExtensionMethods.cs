using Microsoft.Extensions.Configuration;

namespace AMOGUS.Core.ExtensionMethods {
    public static class ConfigurationExtensionMethods {

        public static string GetExercisePathString(this IConfiguration configuration) {
            return configuration?["ExercisePath"];
        }
    }
}
