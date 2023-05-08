rm -rf coverage
rm -rf TestResults

dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test --logger "console;verbosity=detailed" --collect:"XPlat Code Coverage"

reportgenerator "-reports:TestResults/*/coverage*" "-targetdir:coverage" "-reporttypes:TextSummary" "-classfilters:-AMOGUS.Core.Common*;-AMOGUS.Core.DataTransferObjects*;-AMOGUS.*.DependencyInjection;-AMOGUS.Core.Domain*;-AMOGUS.Infrastructure.Identity*;-AMOGUS.Infrastructure.Persistence*"
cat coverage/Summary.txt