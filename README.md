# Fougasse Lover - ASP.NET Core

## Bienvenue sur le projet Fougasse Lover (ASP.NET Core MVC + Identity).
Ce projet a été réalisé dans le cadre de la procédure dérogatoire pour accéder à la formation Développeur d'applications back-end .NET d'Open Classrooms.

### Lancer le projet en local

#### Préparer la base de données
Dans le Gestionnaire de package NuGet (Menu Outils > Gestionnaire de package NuGet > Console du gestionnaire de package) :

#### Créer la migration :
Add-Migration InitialCreate

#### Mettre à jour la base de données :
Update-Database

#### Démarrer l’application
Lancez l’app avec Visual Studio

#### Créer un compte utilisateur
Cliquez sur S’enregistrer / Register
Renseignez l'email de l'admin (disponible dans appsettings.json) : admin@fougasse-lover.fr
Renseigner un mot de passe respectant les standards d'Identity

#### Relancer l’application
Fermez puis relancez l’application afin de bien affecter le rôle admin au compte

#### Se connecter
Cliquez sur Se connecter / Login
Entrez vos identifiants créés à l’étape précédente

Vous avez désormais accès aux fonctionnalités de l'administrateur, afin de créer un article, l'éditer ou le supprimer 
