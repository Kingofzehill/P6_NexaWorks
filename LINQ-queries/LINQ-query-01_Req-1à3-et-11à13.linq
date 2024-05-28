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

/* Requêtes prises en charge
 1: Obtenir tous les problèmes en cours (tous les produits)
 2: Obtenir tous les problèmes en cours pour un produit (toutes les versions)
 3: Obtenir tous les problèmes en cours pour un produit (une seule version)
 11: Obtenir tous les problèmes résolus (tous les produits)
 12: Obtenir tous les problèmes résolus pour un produit (toutes les versions)
 13: Obtenir tous les problèmes résolus pour un produit (une seule version) */

var dataContext = this; // use this for reference to 'dataContext' variable. It allows to change dataContext from LINQ-to-SQL (in your LINQPad queries (see above "Connection")), to EF Core (Visual Studio) for SQL Server querying.

string isInProgressInput = Util.ReadLine("Incident en cours ? (true / false)");
string softwareName = Util.ReadLine("Produit ?"); // example Produit.NomProduit: Trader en Herbe
string softwareVersion = Util.ReadLine("Version ?"); // example Version.NomVersion: 1.0
bool isInProgress;
if (!bool.TryParse(isInProgressInput, out isInProgress))
{
    Console.WriteLine("Incident en cours doit être TRUE ou FALSE !");
}
else
{
	var result = from Incident in Incidents
	             join ProduitVersion_SystemeExploitation in ProduitVersion_SystemeExploitations on Incident.ProduitVersion_SystemeExploitationId equals ProduitVersion_SystemeExploitation.Id
	             join SystemeExploitation in SystemeExploitations on ProduitVersion_SystemeExploitation.SystemeExploitationId equals SystemeExploitation.Id
				 join Produit_Version in Produit_Versions on ProduitVersion_SystemeExploitation.Produit_VersionId equals Produit_Version.Id
				 join Produit in Produits on Produit_Version.ProduitId equals Produit.Id
				 join Version in Versions on Produit_Version.VersionId equals Version.Id
				 where Incident.EnCours == isInProgress 
				 && (Incident.ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit == softwareName || softwareName == string.Empty)
				 && (Incident.ProduitVersion_SystemeExploitation.Produit_Version.Version.NomVersion == softwareVersion || softwareVersion == string.Empty)
				 orderby Incident.EnCours, Incident.ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit, 
				 Incident.ProduitVersion_SystemeExploitation.Produit_Version.Version.NomVersion, Incident.ProduitVersion_SystemeExploitation.SystemeExploitation.NomSystemeExploitation,
				 Incident.DateCreation				 
	             select new
	             {
	                 Incident.ProduitVersion_SystemeExploitation.Produit_Version.Produit.NomProduit,
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
