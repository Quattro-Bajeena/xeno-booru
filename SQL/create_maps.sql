DECLARE @cnt INT = 1;

WHILE @cnt <= 729
BEGIN
	INSERT INTO dbo.Maps
	(Name)
	VALUES
	(CONCAT('map-', @cnt))
	SET @CNT = @cnt + 1;
END;