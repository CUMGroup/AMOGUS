## Risk Management

| Risk ID |	Category | Risk Description |	Probability |	Impact |	Risk Score | Mitigation Strategy |	Indicator |	Contingency Plan	| Responsible |	Status |	Last modifed date |
|-|-|-|-|-|-|-|-|-|-|-|-|
|1	|Organisation|	Drop out of Students	|3|	4|	12|	Healthy Lifestyle, Benefits|	Longer absence|	Higher Workload, cancel unimportant featuers|	Alex|	Closed|	04.06.2023|
|2	|Technical|	Server down	|1	|8	|8|	Backup plan for deployment, Backup (Code, Data, etc.)| 	Email from provider, Health Monitoring|	Change to Azure|	Alex	|Open	|12.04.2023|
|3	|Technical|	Appsettings leaked in production (private Tokens)|3	|10	|30	|DO NOT PUSH THE REAL APPSETTINGS.JSON, if edited check git status|	Code Review, Pull Request |	Change secrets|	Pusher	|Closed	|04.06.2023|
|4	|Technical	|Accidential main branch wrecking|	3	|8	|24	|Git main branch protection |	Warning in git bash and force push|	Revert branch, force push recent main branch|	Pusher|	Closed	|12.04.2023|
|5	|Technical	|End of support for third party libraries	|4|	6|	24|	News monitoring on used 3rd party libraries|	Dependabot warning on github, Owasp Zap Warning|	Switch to own implementations, use old versions, use alternative libraries|	Anna|	Open	|12.04.2023|

