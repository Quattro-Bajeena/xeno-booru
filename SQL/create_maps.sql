DECLARE @cnt INT = 1;

WHILE @cnt <= 729
BEGIN
	INSERT INTO dbo.Posts
	(Type, FileName, Name, Description, 
	Likes, Source, Pending, ThumbnailFileName)
	VALUES
	(
	'Model' ,CONCAT('level', @cnt,'.glb'), CONCAT('Level ', @cnt), CONCAT('Level ', @cnt), 
	0, 'In-game files', 0, CONCAT('LevelThumbnail_', @cnt, '.webp')
	);
	SET @CNT = @cnt + 1;
END;