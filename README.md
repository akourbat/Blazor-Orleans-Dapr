# BlazorServer
 Blazor project with Orleans Grains and Dapr Actors

 Orleans handles TODO page, with streaming updates to changes, Dapr actor manages the Weatherforecast page data.

 Based on BlazorServer sample project from Orleans repo https://github.com/dotnet/orleans/tree/main/samples/Blazor/BlazorServer

 To demonstrate real-time server updates, open multiple browser windows showing the server-side todo demo, and then proceed to perform changes to the todo list from any window. The other windows will mirror the update in real-time.