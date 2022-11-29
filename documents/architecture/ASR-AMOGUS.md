# Summary of architectural decisions and design patters for AMOGUS

## 1. Tactics AMOGUS will practice
### 1.1 Quality attribute scenarios: Modifiability

| Source | Stimulus | Artifact | Environment | Response | Response measure |
| - | - | - | - | - | - |
| Developer | Modify functionality | Code | Build time | Make & test modification | Extent to which this modification affects other functions or quality attributes |
| System administrator | Deploy new version | Code & Data | Run time | Deployment | Downtime, effort |

### 1.2 Tactics
- __Modularization__ → Split application in many modules to have clear boundaries.
- __Increase cohesion__ → Only store related functionalities in one module.
- __Reduce coupling__ → Communication between modules should be abstracted into interfaces and kept at a minimum to easily modify a single module.
- __Choice of technology__ → The chosen technologies should always reflect the architectural needs to enforce it more easily.

## 2. Architecture decisions and concrete design patterns AMOGUS will follow
### 2.1 Architecture decisions
- AMOGUS will follow a service based architecture to enforce modularization and increase cohesion, hence making modifications more centralized and independent.
- Coupling will be reduced by following the [ABBA-Scheme](https://github.com/CUMGroup/AMOGUS/blob/main/documents/architecture/BackendArchitecture.pdf) and only allowing communication via well defined interfaces.
- Backend and frontend will be completely seperated (only communicating through a REST-API) to further decouple the application.
- Backend and frontend will be deployed in different docker containers therefore making deployment easier.

### 2.2 Design patterns
- The backend will be using the MVC pattern to further encapsulate the behaviors (with the View being the REST-API layer).
- The frontend will implement the MVVM pattern to ensure a responsive feel. 
