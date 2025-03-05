# inss-fip - Find an Insolvency Practitioner (FIP)

## Introduction

This solution allows users to find an Insolvency Practitioner, using a web app front end, with a function app backend. 

The web app allows the user to search and navigate search results to find and view Insolvency Proactitioners by making API calls to the function app.

The function app provides an API to search and retrieve Insolvency Practitioner details.

## Documentation

Confluence - [FIP](https://inssdigital.atlassian.net/wiki/spaces/ADS/pages/2805661711/Find+an+Insolvency+Practitioner+FIP)

## Getting started

To run this solution, the web app and function app both require app settings to be created. These may be copied from the app setting templates files and edited to suit your environment.

## Configuration 

To run this solution, set the web app and function app as startup projects, and create app settings files for both and set the app settings to suit your environment.

## App settings

### Web app

Copy the <i>appsettings-template.json</i> name and name it <i>appsettings.json</i>, then edit to set the following app settings to suit your environment.

|App setting name|Sample value|Description|
|----------------|------------|-----------|
|PhaseBanner|ALPHA, BETA or null|Service phase banner|
|EnvironmentBanner|local-dev|One of local-dev, dev, sit, pp or prod|
|EnvironmentBannerText|Local Development Environment - Do Not Enter Personal Data|Environment display message|
|FipApiConnectorClientOptions__BaseAddress|http://localhost:7071/|Base address URL for function app|


### Function app

Copy the <i>local.settings-template.json</i> name and name it <i>local.settings.json</i>, then edit to set the following app settings to suit your environment.

|App setting name|Sample value|Description|
|----------------|------------|-----------|
|iirwebdbContextConnectionString|Data Source=.;Initial Catalog=iirwebdb;Integrated Security=True|SQL Server database connecion string|
|OpenApi__Version|v3|Swagger version|
|OpenApi__DocVersion|1.0.0|Document version for Swagger document|
|OpenApi__DocTitle|Swagger FIP Services|Title for Swagger document|

## 3rd party tools

* Entity Framework
* SQL Server database

## Built With


* Microsoft Visual Studio 2022
* .NET 6