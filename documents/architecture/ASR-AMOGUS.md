# Summary of architectural decisions and design patters for AMOGUS

## 1. Tactics AMOGUS will practice
### 1.1 Quality attribute scenarios: Modifiability

| Source | Stimulus | Artifact | Environment | Response | Response measure |
| - | - | - | - | - | - |
| Developer | Modify functionality | Code | Build time | Make & test modification | Extent to which this modification affects other functions or quality attributes |
| System administrator | Deploy new version | Code & Data | Run time | Deployment | Downtime, effort |

### 1.2 Tactics
Modularization → Split application in many modules to have clear boundaries.
Increase cohesion → Only store related functionalities in one module.
Reduce coupling → Communication between modules should be abstracted into interfaces and kept at a minimum to easily modify a single module.
Choice of technology →The chosen technologies should always reflect the architectural needs to enforce it more easily.

## 2. Architecture decisions and concrete design patterns AMOGUS will follow
- AMOGUS will follow a service based architecture to enforce modularization and increase cohesion, hence making modifications more centralized and independent
- Coupling 
