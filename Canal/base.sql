CREATE TABLE REGION(
    IDREGION  INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(500),
    frequence FLOAT
);

INSERT INTO REGION(nom, frequence) VALUES
('Analamanga', 98);
INSERT INTO REGION(nom, frequence) VALUES
('Itasy', 98);
INSERT INTO REGION(nom, frequence) VALUES
('Analanjirofo', 98);
INSERT INTO REGION(nom, frequence) VALUES
('Vakinakaratra', 98);

CREATE TABLE UTILISATEUR(
    idUtilisateur  INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(300),
    prenom VARCHAR(300),
    email VARCHAR(300),
    password VARCHAR(500),
    idregion INT REFERENCES REGION(IDREGION)
);

CREATE TABLE CHAINE(
    idChaine  INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(500),
    prix FLOAT,
    frequence FLOAT
);

CREATE TABLE BOUQUET(
    idBouquet  INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(500),
    remise float
);


CREATE TABLE CHAINE_BOUQUET(
    idChaine INT REFERENCES CHAINE(idChaine),
    idBouquet INT REFERENCES BOUQUET(idBouquet),
    remise INT
);

DROP VIEW BOUQUETDETAILS
CREATE VIEW BOUQUETDETAILS
    AS
        SELECT c.idChaine, c.nom, c.prix, c.frequence, b.idBouquet, b.nom nombouquet, b.remise FROM CHAINE c
            JOIN CHAINE_BOUQUET cb 
                ON c.idChaine = cb.idChaine
            JOIN BOUQUET b
                ON b.idBouquet = cb.idBouquet

drop view FETCHBOUQUET;
CREATE VIEW FETCHBOUQUET AS
    SELECT SUM(prix*((100-remise)/100)) AS TotalPrice, nombouquet nom, idBouquet
    FROM BOUQUETDETAILS
    GROUP BY nombouquet, idBouquet;

drop view FETCHBOUQUET2;
CREATE VIEW FETCHBOUQUET2 AS
    SELECT SUM(prix) AS TotalPrice, nombouquet nom, idBouquet
    FROM BOUQUETDETAILS
    GROUP BY nombouquet, idBouquet;

DROP TABLE USERBOUQUETS;
CREATE TABLE USERBOUQUETS(
    idUtilisateur INT,
    idBouquet INT,
    dateDebut DATETIME,
    dateFin DATETIME,
    etat INT DEFAULT(1),
    prix float,
    idRechargement INT IDENTITY(1,1) PRIMARY KEY
);

CREATE  VIEW USERVALIDEBOUQUET
	AS SELECT ub.idRechargement ,ub.prix, ub.etat, b.idBouquet, b.nom, b.remise, ub.dateDebut, ub.dateFin, ub.idUtilisateur FROM BOUQUET b
		JOIN USERBOUQUETS ub
			ON b.idBouquet = ub.idBouquet
		;

INSERT INTO CHAINE(nom, prix, frequence)
VALUES ('TF1', 9.99, 700),
('France 2', 0.00, 600),
('M6', 7.50, 650),
('Canal+', 29.99, 800),
('NRJ12', 2.99, 550),
('Arte', 5.99, 620),
('RMC Découverte', 4.99, 570);

INSERT INTO BOUQUET(nom)
VALUES ('Bouquet Basique'),
('Bouquet Sport'),
('Bouquet Cinéma'),
('Bouquet Premium'),
('Bouquet Famille');

INSERT INTO CHAINE_BOUQUET(idChaine, idBouquet, remise)
VALUES (1, 1, 0),
(2, 1, 0),
(3, 1, 0),
(4, 1, 0),
(1, 2, 10),
(4, 2, 5),
(1, 3, 5),
(2, 3, 5),
(3, 3, 5),
(4, 3, 5),
(1, 4, 15),
(2, 4, 10),
(3, 4, 10),
(4, 4, 10),
(1, 5, 20),
(2, 5, 20),
(3, 5, 20),
(4, 5, 20),
(5, 1, 0),
(5, 2, 10),
(5, 3, 20),
(5, 4, 30),
(6, 1, 0),
(6, 3, 10),
(6, 4, 15),
(7, 1, 0),
(7, 2, 5),
(7, 3, 5),
(7, 4, 5);