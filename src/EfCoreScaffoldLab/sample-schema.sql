CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL,
    DisplayName TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    PhoneNumber TEXT
);

CREATE TABLE Posts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Content TEXT,
    CreatedAt TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

INSERT INTO Users (Email, DisplayName, CreatedAt)
VALUES ('alice@example.com', 'Alice', '2026-06-03');

INSERT INTO Users (Email, DisplayName, CreatedAt)
VALUES ('bob@example.com', 'Bob', '2026-06-03');

INSERT INTO Posts (UserId, Title, Content, CreatedAt)
VALUES (1, 'First Post', 'Hello EF Core Scaffold', '2026-06-03');

INSERT INTO Posts (UserId, Title, Content, CreatedAt)
VALUES (2, 'Second Post', 'Studying reverse engineering', '2026-06-03');
