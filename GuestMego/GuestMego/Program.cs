using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;

namespace GuestMegot
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Déclaration des variables
            int terrasseI, secteur, categorie, materiel, reponse, reponseHS, reponseMateriel, idHPAModif, idHPASup, idHPARecherche;
            string coordo, nom, adresse, terrasse, infosConnex;
            bool conversionReussie = true;
            MySqlConnection laConnex;
            MySqlCommand requeteSelect;
            MySqlDataReader lecteur;

            #endregion
            //Déclaration des données de connexion 
            infosConnex = "server=localhost;port=3306;database=gestmegot;uid=root; convert zero datetime = True";
            #region Menu
            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1 : Gérer les hotspots ");
                Console.WriteLine("2 : Gérer le matériel");
                conversionReussie = int.TryParse(Console.ReadLine(), out reponse);
            } while (!conversionReussie);
            #endregion

            switch (reponse)
            {
                case 1:
                    #region HotSpot
                    #region MenuHotspot
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Menu HotSpot");
                        Console.WriteLine("1 : Saisir un nouvel HotSpot");
                        Console.WriteLine("2 : Afficher les HotSpots");
                        Console.WriteLine("3 : Mettre à jour un Hotspot");
                        Console.WriteLine("4 : Supprimer un Hotspot");
                        conversionReussie = int.TryParse(Console.ReadLine(), out reponseHS);
                    } while (!conversionReussie);
                    Console.Clear();
                    #endregion
                    switch (reponseHS)
                    {
                        case 1:
                            #region Saisir
                            //Saisie des données
                            Console.WriteLine("Saisir les coordonnées GPS");
                            coordo = Console.ReadLine();
                            Console.WriteLine("Saisir nom");
                            nom = Console.ReadLine();
                            Console.WriteLine("Saisir adresse");
                            adresse = Console.ReadLine();
                            Console.WriteLine("Le Hotspot a t il une terrasse? ");
                            terrasse = Console.ReadLine().ToLower();
                            if (terrasse == "oui")
                            {
                                terrasseI = 1;
                            }
                            else
                            {
                                terrasseI = 2;
                            }
                            do
                            {
                                Console.WriteLine("Saisir le secteur");
                                conversionReussie = int.TryParse(Console.ReadLine(), out secteur);
                            } while (!conversionReussie);
                            do
                            {
                                Console.WriteLine("Saisir la catégorie");
                                conversionReussie = int.TryParse(Console.ReadLine(), out categorie);
                            } while (!conversionReussie);
                            do
                            {
                                Console.WriteLine("Saisir le matériel attaché");
                                conversionReussie = int.TryParse(Console.ReadLine(), out materiel);
                            } while (!conversionReussie);
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Insert
                            MySqlCommand requeteInsert = laConnex.CreateCommand();
                            requeteInsert.CommandText = "INSERT INTO `hotspot`(`coordoGPS`, `nom`, `adresse`, `terrasse`, `fkSecteur`, `fkCategorie`, `fkMateriel`) VALUES (@coordo,@nom,@adresse,@terrasse,@secteur,@categorie,@materiel) ";
                            //Les données saisies par l'utilisateur sont ajoutées
                            requeteInsert.Parameters.Add("@coordo", MySqlDbType.VarChar).Value = coordo;
                            requeteInsert.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                            requeteInsert.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = adresse;
                            requeteInsert.Parameters.Add("@terrasse", MySqlDbType.Int16).Value = terrasseI;
                            requeteInsert.Parameters.Add("@secteur", MySqlDbType.Int32).Value = secteur;
                            requeteInsert.Parameters.Add("@categorie", MySqlDbType.Int32).Value = categorie;
                            requeteInsert.Parameters.Add("@materiel", MySqlDbType.Int32).Value = materiel;
                            //Envoie de la requete à la base de données 
                            //récupération du nombre de ligne affectés dans la variable nbLigneAjoute
                            int nbLigneAjoute = requeteInsert.ExecuteNonQuery();
                            if (nbLigneAjoute == 1)
                            {
                                Console.WriteLine("Insertion effectuée");
                            }
                            else
                            {
                                Console.WriteLine("Erreur ! renouveller l'insertion");
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();
                            #endregion
                            break;
                        case 2:
                            #region Afficher
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From HotSpot";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table Hotspot
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString() + "-" + lecteur[6].ToString() + "-" + lecteur[7].ToString());
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                        case 3:
                            #region Mettre à jour
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From HotSpot";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table Hotspot
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString() + "-" + lecteur[6].ToString() + "-" + lecteur[7].ToString());
                            }
                            lecteur.Close();
                            requeteSelect.Dispose();
                            Console.WriteLine("Saisir l'identifiant du Hotspot à modifier");
                            int.TryParse(Console.ReadLine(), out idHPAModif);
                            //Saisie des nouvelles valeurs 
                            Console.WriteLine("Saisir les coordonnées GPS");
                            coordo = Console.ReadLine();
                            Console.WriteLine("Saisir nom");
                            nom = Console.ReadLine();
                            Console.WriteLine("Saisir adresse");
                            adresse = Console.ReadLine();
                            Console.WriteLine("Le Hotspot a t il une terrasse? ");
                            terrasse = Console.ReadLine().ToLower();
                            if (terrasse == "oui")
                            {
                                terrasseI = 1;
                            }
                            else
                            {
                                terrasseI = 2;
                            }
                            do
                            {
                                Console.WriteLine("Saisir le secteur");
                                conversionReussie = int.TryParse(Console.ReadLine(), out secteur);
                            } while (!conversionReussie);
                            do
                            {
                                Console.WriteLine("Saisir la catégorie");
                                conversionReussie = int.TryParse(Console.ReadLine(), out categorie);
                            } while (!conversionReussie);
                            do
                            {
                                Console.WriteLine("Saisir le matériel attaché");
                                conversionReussie = int.TryParse(Console.ReadLine(), out materiel);
                            } while (!conversionReussie);
                            MySqlCommand requeteUpdate = laConnex.CreateCommand();
                            requeteUpdate.CommandText = "UPDATE `hotspot` SET `coordoGPS`=@coordo,`nom`=@nom,`adresse`=@adresse,`terrasse`=@terrasse,`fkSecteur`=@secteur,`fkCategorie`=@categorie,`fkMateriel`=@materiel WHERE idHS=@idHS";
                            //Les données saisies par l'utilisateur sont ajoutées
                            requeteUpdate.Parameters.Add("@coordo", MySqlDbType.VarChar).Value = coordo;
                            requeteUpdate.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                            requeteUpdate.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = adresse;
                            requeteUpdate.Parameters.Add("@terrasse", MySqlDbType.Int16).Value = terrasseI;
                            requeteUpdate.Parameters.Add("@secteur", MySqlDbType.Int32).Value = secteur;
                            requeteUpdate.Parameters.Add("@categorie", MySqlDbType.Int32).Value = categorie;
                            requeteUpdate.Parameters.Add("@materiel", MySqlDbType.Int32).Value = materiel;
                            requeteUpdate.Parameters.Add("@idHS", MySqlDbType.Int32).Value = idHPAModif;
                            int nbLigneUpdate = requeteUpdate.ExecuteNonQuery();

                            if (nbLigneUpdate == 1)
                            {
                                Console.WriteLine("Modification reussie");
                            }
                            else
                            {
                                Console.WriteLine("Erreur ! renouveller l'insertion");
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                        case 4:
                            #region Supprimer

                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From HotSpot";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table Hotspot
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString() + "-" + lecteur[6].ToString() + "-" + lecteur[7].ToString());
                            }
                            lecteur.Close();
                            requeteSelect.Dispose();
                            Console.WriteLine("Saisir l'identifiant du Hotspot à supprimer");
                            int.TryParse(Console.ReadLine(), out idHPASup);
                            //Saisie des nouvelles valeurs 

                            MySqlCommand requeteDelete = laConnex.CreateCommand();
                            requeteDelete.CommandText = "DELETE FROM `hotspot` WHERE idHS=@idHS";

                            requeteDelete.Parameters.Add("@idHS", MySqlDbType.Int32).Value = idHPASup;
                            Console.WriteLine("Etes vous sûr de vouloir supprimer le hotspot" + idHPASup + "Oui/Non");
                            string reponseDelete = Console.ReadLine().ToLower();
                            if (reponseDelete == "oui")
                            {
                                int nbLigneDelete = requeteDelete.ExecuteNonQuery();
                                if (nbLigneDelete == 1)
                                {
                                    Console.WriteLine("Suppression reussie");
                                }
                                else
                                {
                                    Console.WriteLine("Erreur ! renouveller l'insertion");
                                }
                            }


                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                    }
                    #endregion
                    break;
                case 2:
                    #region Matériel
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Menu Matériel de collecte");
                        Console.WriteLine("1 : Saisir un nouveau matériel");
                        Console.WriteLine("2 : Afficher les matériaux de collecte");
                        Console.WriteLine("3 : Rechercher un matériel");
                        Console.WriteLine("4 : Mettre à jour ");
                        Console.WriteLine("5 : Supprimer ");
                        conversionReussie = int.TryParse(Console.ReadLine(), out reponseMateriel);
                    } while (!conversionReussie);
                    Console.Clear();

                    switch (reponseMateriel)
                    {
                        case 1:
                            #region ajoutMat
                            Console.WriteLine("Saisir la couleur");
                            string couleur = Console.ReadLine();
                            Console.WriteLine("Saisir la date d'installation (xxxx-xx-xx)");
                            string dateInstallation = Console.ReadLine();
                            Console.WriteLine("Saisir l'adresse");
                            string adresseMat = Console.ReadLine();
                            Console.WriteLine("Saisir les coordoGPS ");
                            string coordoGPSMat = Console.ReadLine();
                            Console.WriteLine("Saisir le fkType");
                            int.TryParse(Console.ReadLine(), out int fkType);
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            MySqlCommand requeteInsert = laConnex.CreateCommand();
                            requeteInsert.CommandText = "INSERT INTO `materiel`(`couleurs`, `dateInstallation`, `adresse`, `coordoGPS`, `fkType`) VALUES (@couleurs,@dateInstallation,@adresse,@coordo, @fkType) ";
                            requeteInsert.Parameters.Add("@couleurs", MySqlDbType.VarChar).Value = couleur;
                            requeteInsert.Parameters.Add("@dateInstallation", MySqlDbType.VarChar).Value = dateInstallation;
                            requeteInsert.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = adresseMat;
                            requeteInsert.Parameters.Add("@coordo", MySqlDbType.VarChar).Value = coordoGPSMat;
                            requeteInsert.Parameters.Add("@fkType", MySqlDbType.Int32).Value = fkType;
                            int nbLigneAjoute = requeteInsert.ExecuteNonQuery();
                            if (nbLigneAjoute == 1)
                            {
                                Console.WriteLine("Insertion effectuée");
                            }
                            else
                            {
                                Console.WriteLine("Erreur ! renouveller l'insertion");
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();
                            #endregion
                            break;
                        case 2:
                            #region Afficher
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From materiel";
                            lecteur = requeteSelect.ExecuteReader();
                           
                            //Affichage des lignes contenues dans la table Hotspot
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString());
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                        case 3:
                            #region Rechercher
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From materiel";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table materiel
                            lecteur.Close();
                            requeteSelect.Dispose();
                            Console.WriteLine("Saisir l'identifiant du materiel à rechercher");
                            int.TryParse(Console.ReadLine(), out idHPARecherche);
                            MySqlCommand requeteSearch = laConnex.CreateCommand();
                            requeteSearch.CommandText = "SELECT * FROM materiel WHERE reference=@reference";
                            requeteSearch.Parameters.Add("@reference", MySqlDbType.Int32).Value = idHPARecherche;
                            lecteur = requeteSearch.ExecuteReader();
                            if (lecteur.Read())
                            {
                                Console.WriteLine("Materiel trouvé :");
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString());
                            }
                            else
                            {
                                Console.WriteLine("Materiel non trouvé");
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();
                            #endregion
                            break;
                        case 4:
                            #region Mettre à jour
                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From materiel";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table materiel
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString());
                            }
                            lecteur.Close();
                            requeteSelect.Dispose();
                            Console.WriteLine("Saisir l'identifiant du materiel à modifier");
                            int.TryParse(Console.ReadLine(), out idHPAModif);
                            Console.WriteLine("Saisir la couleur");
                            couleur = Console.ReadLine();
                            Console.WriteLine("Saisir la date d'installation (xxxx-xx-xx)");
                            dateInstallation = Console.ReadLine();
                            Console.WriteLine("Saisir l'adresse");
                            adresseMat = Console.ReadLine();
                            Console.WriteLine("Saisir les coordoGPS");
                            coordoGPSMat = Console.ReadLine();
                            Console.WriteLine("Saisir le fkType");
                            int.TryParse(Console.ReadLine(), out fkType);

                            MySqlCommand requeteUpdate = laConnex.CreateCommand();
                            requeteUpdate.CommandText = "UPDATE `materiel` SET `coordoGPS`=@coordo,`couleurs`=@couleurs,`adresse`=@adresse,`fkType`=@fkType,`dateInstallation`=@dateInstallation WHERE reference=@reference";
                            //Les données saisies par l'utilisateur sont ajoutées
                            requeteUpdate.Parameters.Add("@reference", MySqlDbType.Int32).Value = idHPAModif;
                            requeteUpdate.Parameters.Add("@couleurs", MySqlDbType.VarChar).Value = couleur;
                            requeteUpdate.Parameters.Add("@dateInstallation", MySqlDbType.VarChar).Value = dateInstallation;
                            requeteUpdate.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = adresseMat;
                            requeteUpdate.Parameters.Add("@coordo", MySqlDbType.VarChar).Value = coordoGPSMat;
                            requeteUpdate.Parameters.Add("@fkType", MySqlDbType.Int32).Value = fkType;
                            int nbLigneUpdate = requeteUpdate.ExecuteNonQuery();

                            if (nbLigneUpdate == 1)
                            {
                                Console.WriteLine("Modification reussie");
                            }
                            else
                            {
                                Console.WriteLine("Erreur ! renouveller l'insertion");
                            }
                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                        case 5:
                            #region Supprimer

                            //Connexion à la base de données
                            laConnex = new MySqlConnection(infosConnex);
                            laConnex.Open();
                            //Définition de la requête Select
                            requeteSelect = laConnex.CreateCommand();
                            requeteSelect.CommandText = "Select * From materiel";
                            lecteur = requeteSelect.ExecuteReader();

                            //Affichage des lignes contenues dans la table Hotspot
                            while (lecteur.Read())
                            {
                                Console.WriteLine(lecteur[0].ToString() + "-" + lecteur[1].ToString() + "-" + lecteur[2].ToString() + "-" + lecteur[3].ToString() + "-" + lecteur[4].ToString() + "-" + lecteur[5].ToString());
                            }
                            lecteur.Close();
                            requeteSelect.Dispose();
                            Console.WriteLine("Saisir l'identifiant du materiel à supprimer");
                            int.TryParse(Console.ReadLine(), out idHPASup);
                            //Saisie des nouvelles valeurs 

                            MySqlCommand requeteDelete = laConnex.CreateCommand();
                            requeteDelete.CommandText = "DELETE FROM `materiel` WHERE reference=@reference";

                            requeteDelete.Parameters.Add("@reference", MySqlDbType.Int32).Value = idHPASup;
                            Console.WriteLine("Etes vous sûr de vouloir supprimer le materiel" + idHPASup + "Oui/Non");
                            string reponseDelete = Console.ReadLine().ToLower();
                            if (reponseDelete == "oui")
                            {
                                int nbLigneDelete = requeteDelete.ExecuteNonQuery();
                                if (nbLigneDelete == 1)
                                {
                                    Console.WriteLine("Suppression reussie");
                                }
                                else
                                {
                                    Console.WriteLine("Erreur ! renouveller l'insertion");
                                }
                            }


                            //Déconnexion de la base de données
                            laConnex.Close();

                            #endregion
                            break;
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
}