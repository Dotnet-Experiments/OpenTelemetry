CREATE DATABASE TestDb;
GO

USE TestDb;
GO

CREATE TABLE WeatherForecasts
(
    Id INT identity primary key,
    Date DATETIME,
    TemperatureC INT,
    --    Summary NVARCHAR(20)
)
GO

