	INSERT Person
	VALUES('Niklas', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(1, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn', '27', '8000');
	INSERT PersonAdresse
	VALUES(1, 1);

	INSERT Person
	VALUES('Niklas2', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(2, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn2', '27', '8000');
	INSERT PersonAdresse
	VALUES(2, 2);

	INSERT Person
	VALUES('Niklas3', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(3, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn3', '27', '8000');
	INSERT PersonAdresse
	VALUES(3, 3);

	INSERT Person
	VALUES('Niklas4', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(4, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn4', '27', '8000');
	INSERT PersonAdresse
	VALUES(4, 4);

	INSERT Person
	VALUES('Niklas5', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(5, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn5', '27', '8000');
	INSERT PersonAdresse
	VALUES(5, 5);

	INSERT Person
	VALUES('Niklas6', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(6, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn6', '27', '8000');
	INSERT PersonAdresse
	VALUES(6, 6);

	INSERT Person
	VALUES('Niklas7', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(7, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn7', '27', '8000');
	INSERT PersonAdresse
	VALUES(7, 7);

	INSERT Person
	VALUES('Niklas8', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(8, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn8', '27', '8000');
	INSERT PersonAdresse
	VALUES(8, 8);

	INSERT Person
	VALUES('Niklas9', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(9, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn9', '27', '8000');
	INSERT PersonAdresse
	VALUES(9, 9);

	INSERT Person
	VALUES('Niklas10', 'Søby', 'Jørgensen', 'nub');
	INSERT Telefon(PersonID, TelefonType, Telefonnummer)
	VALUES(10, 'Privat', '11223344');
	INSERT Adresse(Vejnavn, Husnummer, Postnummer)
	VALUES('Vejnavn10', '27', '8000');
	INSERT PersonAdresse
	VALUES(10, 10);

	UPDATE Person
	SET Fornavn='Bent'
	WHERE Fornavn='Niklas2'

	SELECT * FROM Person
	WHERE PersonID='7'

	/* Her har vi ON DELETE CASCADE på Telefon, så når vi sletter en Person, så sletter vi også den tilhørende telefon, da dens foreign key bliver slettet. */
	/* Vi har også ON DELETE CASCADE på PersonAdresse, så når vi sletter en person, så sletter den relationen mellem Person og Adresse i PersonAdresse */
	DELETE FROM Person
	WHERE PersonID='5'

