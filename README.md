# CustomCultureExample
Demonstrates the issue of trying to localize for custom cultures in .NET Core/.NET 5+ projects.

Open the solution in Visual Studio 2022 and run it. You should see a page that looks something like the image below. Clicking the culture names will reload the page with that culture specified. When attempting to load the page with the custom culture (en-MX) you can see the localized resources (the page heading) are not being loaded.

![image](https://user-images.githubusercontent.com/249088/140424548-34b673d8-faf2-43c9-9841-86b9258967cd.png)
