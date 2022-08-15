CREATE DATABASE TestDb;
GO

USE TestDb;
GO

CREATE TABLE WeatherForecasts
(
    Id INT identity primary key,
    Date DATE,
    TemperatureC INT,
    --    Summary NVARCHAR(20)
)
GO

BULK INSERT dbo.WeatherForecasts
FROM '/opt/mssql-data/data.csv'
WITH 
(
    FORMAT = 'CSV',
    FORMATFILE = '/opt/mssql-data/data.xml',
    fieldterminator = ',',
    rowterminator = '\n',
    errorfile = '/opt/mssql-data/error.csv',
    maxerrors = 100000
);
GO



-- DECLARE @from DATE = '2022-07-01'
-- DECLARE @to DATE = '2022-07-30'
-- DECLARE @min INT = 1
-- DECLARE @max INT = 1

-- WITH
--     CTE
--     AS
--     (
--                     SELECT @from AS [Date]

--         UNION ALL

--             SELECT DATEADD(d, 1, [Date]) AS [Date]
--             FROM CTE
--             WHERE DATEADD(d, 1, [Date]) <= @to
--     )

-- INSERT INTO WeatherForecasts
--     ([Date], TemperatureC)
-- SELECT [Date], FLOOR (RAND() * (max-min+1))+min
-- FROM CTE



-- Declare @Id int
-- Set @Id = 1

-- While @Id <= 10
-- Begin
--     Insert INTO WeatherForecasts
--         ([Date], TemperatureC)
--     values
--         (
--             "2022-07-01",
--             12
--     )
--     Print @Id
--     Set @Id = @Id + 1
-- End