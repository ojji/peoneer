#Peoneer CI project
Pet project to learn about CI build server infrastructure & methodology.
The project will consist a configurable build agent and a web dashboard to create and control builds.

## Some references:
[Continuous Delivery Maturity Model](https://developer.ibm.com/urbancode/docs/continuous-delivery-maturity-model/)

##0.01 goals:
- Able to create a build agent as a WCF service hosted as a windows service.
- Able to connect to the build agent.
- Issue an MSBuild command through the WCF service client.
- Get the build output summary from the build agent through the service.