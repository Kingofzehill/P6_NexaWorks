<Query Kind="Statements">
  <Connection>
    <ID>5a736ac5-94c0-417f-b837-c8090bd26cbc</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>LAPTOP-T4HGN9GO</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>P6NexaWorks_01</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
  <Reference>C:\Repos\OCR\P6V1\bin\Debug\net6.0\P6V1.dll</Reference>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

/* Requêtes NexaWorks prises en charge
 1: Obtenir tous les problèmes en cours (tous les produits).
 2: Obtenir tous les problèmes en cours pour un produit (toutes les versions).
 3: Obtenir tous les problèmes en cours pour un produit (une seule version).
 11: Obtenir tous les problèmes résolus (tous les produits).
 12: Obtenir tous les problèmes résolus pour un produit (toutes les versions).
 13: Obtenir tous les problèmes résolus pour un produit (une seule version). */
var dataContext = this; // Reference to 'dataContext' variable. It allows to change dataContext from LINQ-to-SQL (case used here (see above "Connection")), to EF Core (Visual Studio) for SQL Server querying.

// Input parameters.
string isInProgressInput = Util.ReadLine("Incident en cours ? (true / false)");
string productName = Util.ReadLine("Produit ? (valeur nulle acceptée = tous les produits)"); // example Produit.NomProduit: Trader en Herbe
string versionNumber = Util.ReadLine("Version ? (valeur nulle acceptée = toutes les versions)"); // example Version.NomVersion: 1.0
bool isInProgress;

// Check parameters values.
if (!bool.TryParse(isInProgressInput, out isInProgress))
{
    Console.WriteLine("La valeur pour incident en cours doit être : true ou false !");
}
else
{
	// Query.
	var result = from Incident in dataContext.Incidents
        join ProduitVersion_SystemeExploitation in dataContext.ProduitVersion_SystemeExploitations on Incident.ProduitVersion_SystemeExploitationId equals ProduitVersion_SystemeExploitation.Id
        join SystemeExploitation in dataContext.SystemeExploitations on ProduitVersion_SystemeExploitation.SystemeExploitationId equals SystemeExploitation.Id
		join Produit_Version in dataContext.Produit_Versions on ProduitVersion_SystemeExploitation.Produit_VersionId equals Produit_Version.Id
		join Produit in dataContext.Produits on Produit_Version.ProduitId equals Produit.Id
		join Version in dataContext.Versions on Produit_Version.VersionId equals Version.Id
		where Incident.EnCours == isInProgress 
			&& (Produit.NomProduit == productName || productName == string.Empty)
			&& (Version.NomVersion == versionNumber || versionNumber == string.Empty)
		// Descending order : [table.field] descending.
		orderby Produit.NomProduit, Version.NomVersion, 
			SystemeExploitation.NomSystemeExploitation, Incident.DateCreation	
		// Data fields displayed in Results.
        select new
        {
            Incident.Id,
			Produit.NomProduit,
            Version.NomVersion,
            SystemeExploitation.NomSystemeExploitation,
            Incident.DateCreation,
            Incident.Probleme,
            Incident.EnCours,
            Incident.DateResolution,
            Incident.Resolution
        };
	result.Dump();
}
