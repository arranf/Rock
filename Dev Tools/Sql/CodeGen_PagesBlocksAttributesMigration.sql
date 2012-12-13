-- Do these to make the output a little nicer
-- 1) Right Click | Results To > Results To Text
-- 2) Right Click | Query Options | Results | Text | 
---        uncheck 'Include Column Headers...'      
---        change 'maximum number of characters...' to 999

set nocount on

begin

    select '// Migration Up()'
    
    -- pages
    SELECT 
        CONCAT('            AddPage("',
        [parentPage].[Guid], '","', 
        [p].[Name],  '","',  
        [p].[Description],  '","',
        [p].[Guid], '");') [//Pages]
    FROM 
      [Page] p
    join [Page] [parentPage] on [p].[ParentPageId] = [parentPage].[Id]
    where [p].[IsSystem] = 0
    order by [p].[Id]

    -- block types
    select 
        CONCAT('            AddBlockType("',
        [Name], '","',  
        [Description], '","',  
        [Path], '","',  
        [Guid], '");') AddBlockType
    from [BlockType]
    where [IsSystem] = 0
    order by [Id]

    -- blocks
    select 
        CONCAT('            AddBlock("',
        [p].[Guid], '","', 
        [bt].[Guid], '","',
        [b].[Name], '","',
        [b].[Zone], '","',
        [b].[Guid], '",',
        [b].[Order], ');') AddBlock
    from [Block] [b]
    join [Page] [p] on [p].[Id] = [b].[PageId]
    join [BlockType] [bt] on [bt].[Id] = [b].[BlockTypeId]
    where 
      [b].[IsSystem] = 0
    order by [b].[Id]

    -- attributes
    if object_id('tempdb..#attributeIds') is not null
    begin
      drop table #attributeIds
    end

    select * into #attributeIds from (select [Id] from [dbo].[Attribute] where [IsSystem] = 0) [newattribs]

    select
        CONCAT('            AddBlockTypeAttribute("', 
        bt.Guid, '","',   
        ft.Guid, '","',   
        a.name, '","',  
        a.[Key], '","', 
        a.Category, '","', 
        a.Description, '",', 
        a.[Order], ',"', 
        a.DefaultValue, '","', 
        a.Guid, '");') [AddBlockTypeAttribute]
    from [Attribute] [a]
    left outer join [FieldType] [ft] on [ft].[Id] = [a].[FieldTypeId]
    left outer join [BlockType] [bt] on [bt].[Id] = cast([a].[EntityTypeQualifierValue] as int)
    where EntityTypeQualifierColumn = 'BlockTypeId'
    and [a].[id] in (select [Id] from #attributeIds)

    -- attributes values    
    select 
        CONCAT('            // Attrib Value for ', b.Name, ':', a.Name,
        CHAR(13),
        '            AddBlockAttributeValue("',     
        b.Guid, '","', 
        a.Guid, '","', 
        av.Value, '");',
        CHAR(13)  ) [AddBlockAttributeValue]
    from [AttributeValue] [av]
    join Block b on b.Id = av.EntityId
    join Attribute a on a.id = av.AttributeId
    where [av].[AttributeId] in (select [Id] from #attributeIds)

    drop table #attributeIds

    select '// Migration Down()'

    select CONCAT('            DeleteAttribute("', [Guid], '");') from [Attribute] where [IsSystem] = 0 order by [Id]    
    select CONCAT('            DeleteBlock("', [Guid], '");') from [Block] where [IsSystem] = 0 order by [Id]
    select CONCAT('            DeleteBlockType("', [Guid], '");') from [BlockType] where [IsSystem] = 0 order by [Id]
    select CONCAT('            DeletePage("', [Guid], '");') from [Page] where [IsSystem] = 0 order by [Id] asc 

end