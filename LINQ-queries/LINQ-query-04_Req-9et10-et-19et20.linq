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
 9: Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés (toutes les versions)
 10: Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version)
 19 : Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots-clés (toutes les versions)
 20 : Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version) */
var dataContext = this; // Reference to 'dataContext' variable. It allows to change dataContext from LINQ-to-SQL (case used here (see above "Connection")), to EF Core (Visual Studio) for SQL Server querying.

// Input parameters.
string isInProgressInput = Util.ReadLine("Incident en cours ? (true ou false / valeur nulle acceptée (= tickets en cours et résolus))");;
string productName = Util.ReadLine("Produit ? (valeur nulle acceptée = toutes les produits)");
string versionNumber = Util.ReadLine("Version ? (valeur nulle acceptée = toutes les versions)");
string keyword1 = Util.ReadLine("Premier mot-clé ?");
string keyword2 = Util.ReadLine("Deuxième mot-clé ? (facultatif, valeur nulle acceptée)");
string keyword3 = Util.ReadLine("Troisième mot-clé ? (facultatif, valeur nulle acceptée)");
string startPeriodInput = Util.ReadLine("Début période ? (format : AAAA-MM-JJ)");
string endPeriodInput = Util.ReadLine("Fin période ? (format : AAAA-MM-JJ)");
bool isInProgress;
bool allState = false;
DateTime StartPeriod, EndPeriod;

// Check parameters values.
if (isInProgressInput == "")
{
	allState = true;	
	isInProgressInput = "false"; // allow to search for Incident.EnCours == true && false.
}

if (!bool.TryParse(isInProgressInput, out isInProgress))
{
    Console.WriteLine("Incident en cours doit être TRUE ou FALSE !");
}
else if (keyword1 == "")
{
    Console.WriteLine("Au moins un mot-clé est attendu !");
}
else if (!DateTime.TryParse(startPeriodInput, out StartPeriod))
{
    Console.WriteLine("Format incorrect de la date de début de période ("+startPeriodInput+") : AAAA-MM-JJ attendu !");
}
else if (!DateTime.TryParse(endPeriodInput, out EndPeriod))
{
    Console.WriteLine("Format incorrect de la date de fin de période ("+endPeriodInput+") : AAAA-MM-JJ attendu !");
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
		where (Incident.EnCours == isInProgress || Incident.EnCours == allState)			
			&& (Produit.NomProduit == productName || productName == string.Empty)
			&& (Version.NomVersion == versionNumber || versionNumber == string.Empty)
			&& Incident.Probleme.Contains(keyword1)
			&& (Incident.Probleme.Contains(keyword2) || string.IsNullOrEmpty(keyword2))
			&& (Incident.Probleme.Contains(keyword3) || string.IsNullOrEmpty(keyword3))		
			&& Incident.DateCreation >= StartPeriod && Incident.DateCreation <= EndPeriod
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
