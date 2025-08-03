-- Skrypt do wypełnienia bazy danych przykładowymi danymi
-- Uruchom po utworzeniu struktury bazy danych

USE cookbook;

-- Wstawianie przykładowych przepisów
INSERT INTO Przepisy (Nazwa, Instrukcje, CzasPrzygotowania, LiczbaOsob, DataDodania) VALUES
('Spaghetti Carbonara', '1. Ugotuj makaron al dente\n2. Podsmaż boczek\n3. Wymieszaj jajka z parmezanem\n4. Połącz wszystko razem', 20, 4, NOW()),
('Rosół z kurczaka', '1. Włóż kurczaka do garnka z wodą\n2. Dodaj warzywa\n3. Gotuj 2 godziny na wolnym ogniu\n4. Przecedź bulion', 150, 6, NOW()),
('Naleśniki', '1. Wymieszaj mąkę z mlekiem\n2. Dodaj jajka i szczyptę soli\n3. Smaż na patelni z obu stron', 30, 4, NOW()),
('Kotlet schabowy', '1. Rozbij mięso\n2. Obtocz w mące, jajku i bułce tartej\n3. Smaż na złoty kolor', 25, 4, NOW()),
('Sernik na zimno', '1. Rozkrusz herbatniki\n2. Wymieszaj ser z cukrem i śmietaną\n3. Ułóż warstwami\n4. Schłodź w lodówce', 30, 8, NOW());

-- Wstawianie składników dla każdego przepisu

-- Spaghetti Carbonara (Id = 1)
INSERT INTO Skladniki (Nazwa, Ilosc, PrzepisId) VALUES
('Spaghetti', '500g', 1),
('Boczek', '200g', 1),
('Jajka', '4 sztuki', 1),
('Parmezan', '100g', 1);

-- Rosół z kurczaka (Id = 2)
INSERT INTO Skladniki (Nazwa, Ilosc, PrzepisId) VALUES
('Kurczak', '1 cały', 2),
('Marchewka', '3 sztuki', 2),
('Pietruszka', '2 sztuki', 2),
('Cebula', '1 sztuka', 2);

-- Naleśniki (Id = 3)
INSERT INTO Skladniki (Nazwa, Ilosc, PrzepisId) VALUES
('Mąka', '2 szklanki', 3),
('Mleko', '500ml', 3),
('Jajka', '3 sztuki', 3),
('Sól', 'szczypta', 3);

-- Kotlet schabowy (Id = 4)
INSERT INTO Skladniki (Nazwa, Ilosc, PrzepisId) VALUES
('Schab', '800g', 4),
('Mąka', '100g', 4),
('Jajka', '2 sztuki', 4),
('Bułka tarta', '200g', 4);

-- Sernik na zimno (Id = 5)
INSERT INTO Skladniki (Nazwa, Ilosc, PrzepisId) VALUES
('Herbatniki', '200g', 5),
('Ser twarogowy', '1kg', 5),
('Cukier puder', '200g', 5),
('Śmietana 30%', '400ml', 5);

-- Sprawdzenie wstawionych danych
SELECT 'Przepisy:' as Info;
SELECT Id, Nazwa, CzasPrzygotowania, LiczbaOsob FROM Przepisy;

SELECT 'Składniki:' as Info;
SELECT s.Id, p.Nazwa as Przepis, s.Nazwa as Skladnik, s.Ilosc 
FROM Skladniki s 
JOIN Przepisy p ON s.PrzepisId = p.Id 
ORDER BY p.Id, s.Id;
