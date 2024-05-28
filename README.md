# P6_NexaWorks
OCR P6 project NexaWorks

Outils nécessaires : 
- Installer Microsoft SQL Server ==> https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads. Le projet a été réalisé avec la version SQL Server 2022 Developer.
- Installer Microsoft Sql Server Management Studio (SMSS) ==> https://learn.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16. Le projet a été réalisé avec SMSS 19.2.56.2.
- Installer LINQPad ==> https://www.linqpad.net/.
Le projet a été réalisé avec LINQPad 8 (version gratuite).
- Git Hub
-- Créer un compte sur Git Hub.
Cet outil permet de gérer son dépôt distant Git. Le dépôt distant est une version dématérialisée du dépôt local, que ce soit sur Internet ou sur un réseau. Il permet de centraliser le travail des développeurs dans un projet collectif.
-- Installer Git Bash : https://git-scm.com/downloads
Note. Existe en version 32/64 bits, windows ou apple, et en version portable. Aide : https://git-scm.com/book/en/v2. Plusieurs GUI client : https://git-scm.com/downloads/guis.
Cet outil permet de gérer son dépôt local Git (copie locale sur votre ordinateur du dépôt distant contenant le projet). Un dépôt est comme un dossier qui conserve un historique des versions et des modifications d’un projet. 


==> Mode opératoire pour création de la base de données NexaWorks Produits logiciels, chargement des données de test et exécution des requêtes .linq :
===> Répertoire Git du projet dans le Dépôt Distant GitHub : 


===> Création de la base de données :
- Le Diagramme de Classe UML et Le Modèle Entité-Association fournissent une représentation modélisée des données, voir  "Moureu_Stephane_1_Modèle_entité-association_052024_vX.pdf".
- Le script de création de la base de données est basé sur ce modèle, voir fichier "Script-SQL_Create-DB_NexaWorks_VXX.sql".

===> Chargement des données : 
- Tickets constituants les exemples chargés en base de données, voir fichier "Moureu_Stephane_2_Exemples_de_tickets_052024_vX.pdf". Les exemples ont permis de constituer les fichiers plats (.CSV) servant à alimenter chaque table de la base de données par import de données dans SMSS. 				
- Pour importer les données des fichiers CSV dans SSMS, veuillez suivre le mode opératoire dans le fichier "Import-Donnees_mode-operatoire". Note. Les fichiers sont à importer dans l'ordre suivant : 1 Produit.csv // 2 Version.csv // 3 SystemeExploitation.csv // 4 Produit_Version.csv // 5 ProduitVersion_SystemeExploitation.csv // 6 Incident.csv.

==> Exécution des requètes : 
- Les requètes LINQ (4) produites pour le projet sont listées dans le document "Moureu_Stephane_4_Documentation_de_la_base_de_données_052024_vX.pdf".
- Les requêtes demandées (20) par l'équipe NexaWorks Produits logiciel sont listées dans le document "Liste_Requetes-demandées.pdf".
- Pour exécuter les requêtes dans LINQPad et extraire les résultats dans Microsft Excel, veuillez suivre le mode opératoire dans le fichier "LINQPad_mode-operatoire.pdf". 
