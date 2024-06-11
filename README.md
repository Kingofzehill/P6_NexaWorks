# P6_NexaWorks
OCR P6 project NexaWorks

Outils : 
- Microsoft SQL Server 
https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads. 
Le projet a été réalisé avec la version SQL Server 2022 Developer.
- Microsoft Sql Server Management Studio (SMSS) 
https://learn.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16. 
Le projet a été réalisé avec SMSS 19.2.56.2.  
- LINQPad 
https://www.linqpad.net/.
Le projet a été réalisé avec LINQPad 8 (version gratuite).
- GitHub / GitHub Desktop (ou Git Bash)
https://github.com/.
https://desktop.github.com/.
Dupliquer (fork) le dépôt distant du projet (voir url communiquée) dans votre GitHub. Cloner le projet localement (sur votre machine) avec GitHub Desktop (ou GitBash). Vous disposez localement des sources du projet.

    
Projet et instructions de démarrage :
- Création de la base de données :
    - Note. Diagramme de Classe UML et Modèle Entité-Association fournissent une représentation modélisée des données, voir  "..\Moureu_Stephane_1_Modèle_entité-association_052024_vX.pdf".
    - Ouvrir SMSS et connectez-vous au moteur de base de données.
    - Créer une nouvelle requête SQL
    - Copier le contenu du script SQL de la base de données, fichier : "..\SQL\Script-SQL_Create-DB_NexaWorks_VXX.sql".
    - Coller le script dans la requête SQL et exécutez la. Si la création s'est passée correctement le message "Commandes réussies" est affiché. 
    - Actualiser les bases de données, une base de données "P6NexaWorks_01" est affichée. 
      
- Chargement des données : 
    - Des jeux de données (fichiers .csv) de test ont été constitués (voir dossier "..\Data_import\CSV") à partir des 25 tickets du fichier "..\Moureu_Stephane_2_Exemples_de_tickets_052024_vX.pdf". 
Ces fichiers de données sont à importer dans la base de données pour charger les données de test.		
    - Pour importer les données dans la base de données "P6NexaWorks_01", veuillez suivre le mode opératoire, fichier "..\Data_import\Import-Donnees_mode-operatoire.pdf". 
Note. Les fichiers .csv sont à importer dans l'ordre suivant : 1 Produit.csv // 2 Version.csv // 3 SystemeExploitation.csv // 4 Produit_Version.csv // 5 ProduitVersion_SystemeExploitation.csv // 6 Incident.csv.
  
- Exécution des requètes : 
    - Les requètes LINQ (4) produites pour le projet sont disponibles dans le dossier "..\LINQ-queries\Queries\" 
Elles sont documentées dans le fichier "..\Moureu_Stephane_4_Documentation_de_la_base_de_données_052024_vX.pdf".
Elles couvrent les 20 requêtes demandées par l'équipe NexaWorks Produits logiciel listées dans le document "..\LINQ-queries\Liste_Requetes-demandées.pdf".
    - Ouvrir LINQPad.
    - Ajouter une connexion à la base de données :
En haut à gaucher cliquer sur "Add connection" pour ajouter une connexion à une base de données.

    - Sélectionner le LINQPad driver "LINQ to sql (optimized for SQL SERVER)" puis next.
        - Provider : SQL Server
        - Server : (voir le nom du serveur dans SMSS)
        - Log on details : (sélectionner le mode d'authentification au serveur).
        - Database : Display all in a TreeView, et cocher Populate on startup. 
        - Data context options, cocher : 
        - Allow DateOnly and TimeOnly types.
        - Pluralize EntitySet and Table properties.
        - Capitalize property names.
        - Include Stored Procedure and Functions. 
        - cocher "Remember this connection"
        - Utiliser le bouton "Test" pour vérifier la connexion à la base de données", si message "Connection successful", cliquer sur OK. 

    - Ajouter le répertoire des requêtes .linq :
En bas à gauche dans l'onglet My Queries, cliquer sur "Set Folder".
        - Dans la partie "My Queries", sélectionner Custom Location et à l'aide du lien "Browse" positionnez vous sur le répertoire contenant les requêtes .linq, puis ok. 
        - Les requêtes .linq sont à présent affichées.
        - Ouvrez les requêtes en cliquant dessus. 
        - Puis F5 pour les exécuter (ou cliquer sur le symbole play de couleur verte).
        - LINQPad vous demande de saisir les paramètres attendus (en bas de l'écran). 
            - Veuillez vous référer à la documentation de chaque requête "..\Moureu_Stephane_4_Documentation_de_la_base_de_données_052024_vX.pdf".
            - Le dossier "..\LINQ-queries\excel_extracts\" fournit un extract excel pour chacune des 20 requêtes demandées par NexaWorks (voir colonne Commentaires de le documentation de la base de données).
        - les résultats sont affichés dans l'onglet "Results". 
        - Vous pouvez ouvrir (et sauvegarder) ces résultats dans Excel, via les "..." affichés à côté de la mention "IOrderedQueryable".      
