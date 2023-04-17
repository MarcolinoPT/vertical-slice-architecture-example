# Vertical Slice Architecture example

### 1. Introduction

A simple example of Vertical Slice Architecture using an SPA + Web API. The FE and BE and used the following technologies:
- Angular 14 on the FE
- ASP .NET 7 on the BE
  - data is not persisted in database but in memory
  - this is based on a boilerplate solution for Angular + ASP .NET 7 to be run from Visual Studio
  - there are no tests implemented. The ones that exist are from the boilerplate solution.

### 2. How to run

To run the application open the solution in Visual Studio and run it. The FE is served after the BE has started and can be accessed at runtime automatically through the predefined web browser. There is also a swagger explorer available by adding `/swagger` to the URL.

### 3. Overview of the solution

The solution is composed of 1 project and the client app is under the folder 'ClientApp' in the project root.
The BE is designed using Vertical Slice Architecture. This is based on the principle of designing your endpoints through features, thus the features folder, and keeping your code coherent and easy to maintain. To this end I've also applied CQRS to separate commands from queries.

The folder structure is as follows:
- `Pages`
  - default from the template using MVC
- `Models`
  - To store the domain models that represent your data structures
- `Features`
  - API endpoints are grouped by feature and any related code is grouped in the same folder
- `Data`
  - The mocked data access layer is stubbed here. Nothing fancy, kept it simple
- `ClientApp`
  - source code for the Angular app
    - `proxy.conf.js` configure the proxy to the BE API
    - `app` folder contains the Angular app
      - `_components`, `_helpers`, `_models`, `_services` folders  are shared/common code such as components, services, models, helpers and etc. They are placed in folders prefixed with an underscore _ to easily differentiate them from features, when used, and group them together at the top of the folder structure.
        - In this case I did not create features folders since this was a small app and I did not want to over complicate it, so I kept the components in folders and used the `AppComponent` directly.
