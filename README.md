# MovieAdmin
#2 Setup
To get the app initiate a pull request or download the .zip version.
Once you have the app on your machine, follow these steps:
1. Open the project in Visual studio.
2. Build the project.
3. Open the NuGet Package manager console. The console can be found in Tools > NuGet Package Manager > Package Manager Console.
4. Type in the following commands:
   * `Add-Migration InitialCreate`
   * `Update-Database` (if you get an error `The term 'add-migration' is not recognized as the name of a cmdlet`) reopen Visual Studio.
5. Run the app with ISS Express.
