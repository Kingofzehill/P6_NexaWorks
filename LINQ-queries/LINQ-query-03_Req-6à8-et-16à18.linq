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
6: Obtenir tous les problèmes en cours contenant une liste de mots-clés (tous les produits).
7: Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (toutes les versions).
8: Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (une seule version).
16: Obtenir tous les problèmes résolus contenant une liste de mots-clés (tous les produits).
17: Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (toutes les versions).
18: Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (une seule version).*/
var dataContext = this; // Reference to 'dataContext' variable. It allows to change dataContext from LINQ-to-SQL (case used here (see above "Connection")), to EF Core (Visual Studio) for SQL Server querying.

// Input parameters.
string isInProgressInput = Util.ReadLine("Incident en cours ? (true ou false)");;
string productName = Util.ReadLine("Produit ? (valeur nulle acceptée = toutes les produits)");
string versionNumber = Util.ReadLine("Version ? (valeur nulle acceptée = toutes les versions)");
string keyword1 = Util.ReadLine("Premier mot-clé ?");
string keyword2 = Util.ReadLine("Deuxième mot-clé ? (facultatif, valeur nulle acceptée)");
string keyword3 = Util.ReadLine("Troisième mot-clé ? (facultatif, valeur nulle acceptée)");
bool isInProgress;

// Check parameters values.
if (!bool.TryParse(isInProgressInput, out isInProgress))
{
    Console.WriteLine("La valeur pour incident en cours doit être : true ou false !");
}
else if (keyword1 == "")
{
    Console.WriteLine("Au moins un mot-clé est attendu !");
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
            && (Produit.NomProduit == productName || string.IsNullOrEmpty(productName))
            && (Version.NomVersion == versionNumber || string.IsNullOrEmpty(versionNumber))
			&& (string.IsNullOrEmpty(keyword1) || Incident.Probleme.Contains(keyword1))
            && (string.IsNullOrEmpty(keyword2) || Incident.Probleme.Contains(keyword2))
            && (string.IsNullOrEmpty(keyword3) || Incident.Probleme.Contains(keyword3))
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

/*
	// Test: syntax Join  method (.Join()) https://learn.microsoft.com/fr-fr/dotnet/csharp/linq/standard-query-operators/join-operations#single-key-join.
	var result = dataContext.Incidents	
    .Join(dataContext.ProduitVersion_SystemeExploitations, Incident => Incident.ProduitVersion_SystemeExploitationId, ProduitVersion_SystemeExploitation => ProduitVersion_SystemeExploitation.Id, (Incident, ProduitVersion_SystemeExploitation) => new { Incident, ProduitVersion_SystemeExploitation })
	.Join(dataContext.SystemeExploitations, x => x.ProduitVersion_SystemeExploitation.SystemeExploitationId, SystemeExploitation => SystemeExploitation.Id, (x, SystemeExploitation) => new { x.Incident, x.ProduitVersion_SystemeExploitation, SystemeExploitation })
    .Join(dataContext.Produit_Versions, x => x.ProduitVersion_SystemeExploitation.Produit_VersionId, Produit_Version => Produit_Version.Id, (x, Produit_Version) => new { x.Incident, x.ProduitVersion_SystemeExploitation, x.SystemeExploitation, Produit_Version })
	.Join(dataContext.Produits, x => x.Produit_Version.ProduitId, Produit => Produit.Id, (x, Produit) => new { x.Incident, x.ProduitVersion_SystemeExploitation, x.SystemeExploitation, x.Produit_Version, Produit })
    .Join(dataContext.Versions, x => x.Produit_Version.VersionId, Version => Version.Id, (x, Version) => new { x.Incident, x.ProduitVersion_SystemeExploitation, x.SystemeExploitation, x.Produit_Version, x.Produit, Version })    
	.Where(x => x.Incident.EnCours == isInProgress
                && (string.IsNullOrEmpty(keyword1) || x.Incident.Probleme.Contains(keyword1))
                && (string.IsNullOrEmpty(keyword2) || x.Incident.Probleme.Contains(keyword2))
                && (string.IsNullOrEmpty(keyword3) || x.Incident.Probleme.Contains(keyword3))
                && (x.Produit.NomProduit == productName || string.IsNullOrEmpty(productName))
                && (x.Version.NomVersion == versionNumber || string.IsNullOrEmpty(versionNumber)))
	// Fields to display in Results.
    .Select(x => new
    {
		x.Incident.Id,
		x.ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit,
        x.Incident.ProduitVersion_SystemeExploitation.Produit_Version.Version.NomVersion,
        x.Incident.ProduitVersion_SystemeExploitation.SystemeExploitation.NomSystemeExploitation,
        x.Incident.DateCreation,
        x.Incident.Probleme,
        x.Incident.EnCours,
        x.Incident.DateResolution,
        x.Incident.Resolution
    });
*/
