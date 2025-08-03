-- Skrypt do utworzenia bazy danych "Książka Kucharska"
-- Uruchom ten skrypt w MySQL Workbench lub phpMyAdmin

-- Tworzenie bazy danych
CREATE DATABASE IF NOT EXISTS cookbook CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE cookbook;

-- Tabela przepisów
CREATE TABLE Przepisy (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nazwa VARCHAR(200) NOT NULL,
    Instrukcje TEXT NOT NULL,
    CzasPrzygotowania INT NOT NULL,
    LiczbaOsob INT NOT NULL,
    DataDodania DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Tabela składników
CREATE TABLE Skladniki (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nazwa VARCHAR(100) NOT NULL,
    Ilosc VARCHAR(50) NOT NULL,
    PrzepisId INT NOT NULL,
    FOREIGN KEY (PrzepisId) REFERENCES Przepisy(Id) ON DELETE CASCADE
);

-- Indeksy dla lepszej wydajności
CREATE INDEX IX_Skladniki_PrzepisId ON Skladniki(PrzepisId);
CREATE INDEX IX_Przepisy_Nazwa ON Przepisy(Nazwa);
