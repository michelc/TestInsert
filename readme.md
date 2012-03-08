# TestInsert : Optimiser les INSERT multiples (dans Oracle)


## Présentation

Un projet qui regroupe mes différents essais pour optimiser l'insertion d'un
très grand nombre de données dans une table Oracle et qui a servi de base pour
le billet [Optimiser les INSERT multiples (dans Oracle)](http://blog.pagesd.info/post/2012/03/08/optimiser-insert-multiples-oracle)
publié sur mon blog.

* CopyDb2_Version1 : lance des commandes INSERT ... VALUES successives
* CopyDb2_Version2 : lance une commande BEGIN ... END contenant plusieurs INSERT
* CopyDb2_Version3 : lance une commande INSERT ... FROM SELECT de plusieurs
lignes
* CopyDb2_Version4 : lance une commande INSERT /*+ append */ ... FROM SELECT de
plusieurs lignes

Chaque version est déclinée deux fois, une en mode logging (CopyDb2\_Version#a)
et l'autre en mode nologging (CopyDb2_Version#b).


## Inspiration

L'article
[Optimize Oracle SQL INSERT performance](http://dba-oracle.com/t_optimize_insert_sql_performance.htm)
de Don Burleson a servi de point de départ pour les essais sur les modes logging
/ nologging et le hint /*+ append */.

L'idée de regrouper les commandes INSERT provient de la question
[How can I run several different insert statements in a single call with ADO.NET and Oracle?](http://stackoverflow.com/questions/6225536/how-can-i-run-several-different-insert-statements-in-a-single-call-with-ado-ne) sur StackOverflow.


## Résultats

Dans le cas des premiers tests lancés depuis mon portable vers le serveur
Oracle distant (donc avec une forte incidence de la qualité de la connexion
internet), j'ai obtenu les résultats suivants :

### Version1a : INSERT avec LOGGING
* 1° Sauvegarde => 1023 lignes en 118,88 secondes
* 2° Sauvegarde => 1023 lignes en 86,97 secondes

### Version1b : INSERT avec NOLOGGING
* 1° Sauvegarde => 1023 lignes en 78,89 secondes
* 2° Sauvegarde => 1023 lignes en 70,51 secondes

### Version2a : INSERT avec LOGGING + BEGIN / END
* 1° Sauvegarde => 1023 lignes en 6,09 secondes
* 2° Sauvegarde => 1023 lignes en 4,91 secondes

### Version2a : INSERT avec NOLOGGING + BEGIN / END
* 1° Sauvegarde => 1023 lignes en 5,40 secondes
* 2° Sauvegarde => 1023 lignes en 4,71 secondes

### Version3a : INSERT FROM SELECT avec LOGGING
* 1° Sauvegarde => 1023 lignes en 4,15 secondes
* 2° Sauvegarde => 1023 lignes en 3,13 secondes

### Version3b : INSERT FROM SELECT avec NOLOGGING
* 1° Sauvegarde => 1023 lignes en 3,30 secondes
* 2° Sauvegarde => 1023 lignes en 2,85 secondes

### Version4a : APPEND FROM SELECT avec LOGGING
* 1° Sauvegarde => 1023 lignes en 3,02 secondes
* 2° Sauvegarde => 1023 lignes en 3,19 secondes

### Version4b : APPEND FROM SELECT avec NOLOGGING
* 1° Sauvegarde => 1023 lignes en 2,89 secondes
* 2° Sauvegarde => 1023 lignes en 2,89 secondes

Le billet [Optimiser les INSERT multiples (dans Oracle)](http://blog.pagesd.info/post/2012/03/08/optimiser-insert-multiples-oracle)
présente des résultats pour 4500 insertions depuis un PC dans le même réseau
local que le serveur Oracle.