# RealWorldSharp - RealWorld Example App built with Fullstack Sharp Framework 

> ### Fullstack Sharp Framework codebase containing RealWorld example (CRUD, auth, advanced patterns, etc) that adheres to the [RealWorld](https://github.com/gothinkster/realworld) spec and API.


### [Demo](realworldsharp-a3fqa4fqffehfnh8.canadacentral-01.azurewebsites.net)&nbsp;&nbsp;&nbsp;&nbsp;[RealWorld](https://github.com/gothinkster/realworld)

This codebase was created to demonstrate a fully fledged fullstack application built with **Fullstack Sharp Framework** including CRUD operations, authentication, routing, pagination, and more.

We've gone to great lengths to adhere to the **RealWorld** community styleguides & best practices.

For more information on how to this works with other frontends/backends, head over to the [RealWorld](https://github.com/gothinkster/realworld) repo.


# How it works

Whole application is built in C# but is not using Blazor or razor pages.
Frontend is created using C# HtmlSharp library and C# wrappers for HTMX and AlpineJs. 
Backend is using ASP.Net MinimalApi and EFCore.
Details about implementation and Fullstack Sharp Framework here: link

# Getting started

RealWorldSharp is a C# VS2022 solution with 5 projects:
- RealWorldSharp - C# project
- RealWorldSharp.Test - C# testing project
- HtmlSharp library
- HtmlConverter - a Windows utility that converts HTML to C#
- Lorem(https://github.com/dochoffiday/Lorem.NET) - a helper class library used for this demo to generate random data, upgraded to Net.8

Web pages generated have links to HTMX and AjaxJs libraries in the HTML document HEAD.

DB is InMemoryDatabase so nothing is persisted. Data is randomly generated except for the initial users.

To run the app, clone or download the solution and open in VS2022.
There is also a running instance [here](realworldsharp-a3fqa4fqffehfnh8.canadacentral-01.azurewebsites.net)

