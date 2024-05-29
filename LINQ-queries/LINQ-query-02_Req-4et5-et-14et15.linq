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
 4: Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (toutes les versions).
 5: Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (une seule version).
 14: Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (toutes les versions).
 15: Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (une seule version). */
var dataContext = this; // Reference to 'dataContext' variable. It allows to change dataContext from LINQ-to-SQL (case used here (see above "Connection")), to EF Core (Visual Studio) for SQL Server querying.

// Input parameters.
string isInProgressInput = Util.ReadLine("Incident en cours ? (true / false / valeur nulle acceptée (= tickets en cours ou résolus))");
string softwareName = Util.ReadLine("Produit ? (saisie obligatoire)");
string softwareVersion = Util.ReadLine("Version ? (valeur nulle acceptée = toutes les versions)");
string startPeriodInput = Util.ReadLine("Début période ? (format : AAAA-MM-JJ)");
string endPeriodInput = Util.ReadLine("Fin période ? (format : AAAA-MM-JJ)");
bool isInProgress;
bool allState = false;
DateTime StartPeriod, EndPeriod;

// Check parameters values.
if (isInProgressInput == "")
{
	allState = true;
	Console.WriteLine("isInProgressInput == null");
	isInProgressInput = "false";
}

if (!bool.TryParse(isInProgressInput, out isInProgress))
{
    Console.WriteLine("La valeur pour incident en cours doit être : true ou false !");
}
else if (!DateTime.TryParse(startPeriodInput, out StartPeriod))
{
    Console.WriteLine("Format de date début de période incorrect ("+startPeriodInput+") : AAAA-MM-JJ attendu !");
}
else if (!DateTime.TryParse(endPeriodInput, out EndPeriod))
{
    Console.WriteLine("Format de date fin de période incorrect ("+endPeriodInput+") : AAAA-MM-JJ attendu !");
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
				 && Incident.DateCreation >= StartPeriod && Incident.DateCreation <= EndPeriod 
				 && Incident.ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit == softwareName
				 && (Incident.ProduitVersion_SystemeExploitation.Produit_Version.Version.NomVersion == softwareVersion || softwareVersion == string.Empty)
				 // Fields to display in Results.
	             select new
	             {	                 
					 Incident.Id,
					 ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit,
	                 Incident.ProduitVersion_SystemeExploitation.Produit_Version.Version.NomVersion,
	                 Incident.ProduitVersion_SystemeExploitation.SystemeExploitation.NomSystemeExploitation,
	                 Incident.DateCreation,
	                 Incident.Probleme,
	                 Incident.EnCours,
	                 Incident.DateResolution,
	                 Incident.Resolution
	             };
	result.Dump();
}
