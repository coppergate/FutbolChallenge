

CREATE LOGIN [futbolchallenge] WITH PASSWORD=N'futbolchallenge', DEFAULT_DATABASE=[FutbolChallenge], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE USER [futbolchallenge] FOR LOGIN [futbolchallenge];

