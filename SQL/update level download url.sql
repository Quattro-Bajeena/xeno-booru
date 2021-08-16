UPDATE [XenoBooru].[dbo].[Posts]
SET [SourceDownload] = CONCAT('LevelDownload_', SUBSTRING([Name],6,3),'.zip')
WHERE Type = 'Model';