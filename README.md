# parcel-signalr-react-recipe
A recipe for setting up Parcel with SignalR and React. This nodejs project uses yarn. It can be set up using NPM.

## Prerequisites

* Visual Studio Code => https://code.visualstudio.com/
* nodejs / npm=> https://nodejs.org/en/download/
* NET5.0 SDK => https://dotnet.microsoft.com/download/dotnet/5.0
* Parcel Bundler

### Parcel Installation

```
npm install -g parcel-bundler
```

## Features

* Server Side SignalR Hub transport backend
* Client Side React / .tsx
* Parcel
* Dev and Build (Minification)
* Compound Debug Startup process

## Why Parcel?

Parcel offers easy configurations for setting up a broad range of front end technologies, such as (P)React/Svelte and tooling, such as:

* Minification for CSS
* Compiling SASS
* Typescript integration for various extensions, such as .ts and .tsx

`Parcel` has an easy to use recipe, setting up react through the `parcel bundler`. This project has been setup as follows:

```
npm init -y
npm install --save react
npm install --save react-dom
npm install --save-dev parcel-bundler
npm install --save-dev tslint typescript
```

## Setup .tsx / jsx.

Tell typescript to use the ~ module loading convention is used for Parcel.
The .ts / .tsx sources are assumed to be in the ./src folder.

in `tsconfig.json`:

```json
{
    "jsx": "react",
    "compilerOptions": {
      "jsx": "react",
      "baseUrl": "./src",
      "paths": {
        "~*": ["./*"]
      },
    },
    "include": ["src/**/*"]
  }
```

## Enable Debugging

1. Install de debugger for Chrome available as a vscode extension from:
https://marketplace.visualstudio.com/items?itemName=msjsdiag.debugger-for-chrome

2. Add a launch.json to your .vscode folder

```json
{
  // Use IntelliSense to learn about possible attributes.
  // Hover to view descriptions of existing attributes.
  // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
  "version": "0.2.0",
  "configurations": [
    {
      "type": "chrome",
      "request": "launch",
      "name": "Launch Chrome against localhost",
      "url": "http://localhost:1234",
      "webRoot": "${workspaceFolder}",
      "breakOnLoad": true,
      "sourceMapPathOverrides": {
        "../*": "${webRoot}/*"
      }
    }
  ]
}
```

3. Start the parcel dev server

The following additions were made to package.json to startup parcel:

start script for `parcel`
```json
"scripts": {
  ...
  "dev": "parcel ./src/index.html"
  ...
}
```

Start parcel with:

```
npm run dev
```

4. Start debugging using `F5` from within  vscode.

Your session now debuggs the application through .map files and is served from the ./dist folder

## Building production version

1. Add a build script to package.json. This builds the project with minification enabled.

```json
  "scripts": {
    ...
    "build": "parcel build ./src/index.html -d build/output"
    ...
  },
``` 

## Installing SignalR

```
npm install @microsoft/signalr
```

## Create a SignalR Server

1. Create an empty website

```
dotnet new web
```

2. Setup SignalR in Startup.cs with endpoint to `/telemetry`

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSignalR();
}
```

and here:

```csharp
public void Configure(IApplicationBuilder app)
{
...
    app.UseEndpoints(endpoints => 
    {
        endpoints.MapHub<Telemetry>("/telemetry");
    });
}
```

## Compound launching

Both the client and server need to be started when debugging. For this we add a compound section to `launch.json`. You can start a debugging session and it spins up everything.

```json
"compounds": [
    {
        "name": "SignalR dotnet core server/ React client",
        "configurations": ["Server", "Client", "Parcel"]
    }
]
```

