SELECT TOP (1000) [Id]
      ,[Name]
      ,[Description]
      ,[Type]
  FROM [dbo].[Tags]
  WHERE [Type] = 'Location'
  ORDER BY [Name];