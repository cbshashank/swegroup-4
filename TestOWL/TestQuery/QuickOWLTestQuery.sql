DROP TABLE plant
DROP TABLE location
DROP TABLE planttype

USE [OWL]
GO

/****** Object:  Table [OWL].[dbo].[plant]    Script Date: 10/18/2015 12:54:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [OWL].[dbo].[plant](
	[plant_id] [varchar](30) NOT NULL,
	[name] [varchar](200) NULL,
	[color_flower] [varchar](100) NULL,
	[color_foliage] [varchar](200) NULL,
	[color_fruit_seed] [varchar](100) NULL,
	[texture_foliage] [varchar](100) NULL,
	[shape] [varchar](150) NULL,
	[pattern] [varchar](150) NULL,
	[image] [varchar](5000) NULL,
PRIMARY KEY CLUSTERED 
(
	[plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [model]
GO

/****** Object:  Table [OWL].[dbo].[plantType]    Script Date: 10/18/2015 12:55:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [OWL].[dbo].[plantType](
	[plant_id] [varchar](20) NULL,
	[type] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [model]
GO

/****** Object:  Table [dbo].[location]    Script Date: 10/18/2015 12:55:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [OWL].[dbo].[location](
	[plant_id] [varchar](20) NULL,
	[us_state] [varchar](20) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Insert data into plant, location, and planttype tables ******/

insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into location values ('ABGR4','FL')
insert into planttype values ('ABGR4','shrub')

GO

/****** Add Test Cases for invalid data in plant, location, and planttype tables ******/

-- Place NULLs in different columns in plant, location, and planttype tables
insert into plant values (NULL,'Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4',NULL,'Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora',NULL,'Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple',NULL, 'Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', NULL, 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', NULL,'Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium',NULL, 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', NULL, 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'Dicot', NULL)
insert into location values (NULL,'FL')
insert into location values ('ABGR4',NULL)
insert into planttype values (NULL,'shrub')
insert into planttype values ('ABGR4',NULL)

-- Place numbers into different columns in plant, location, and planttype tables
insert into plant values (100,'Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4',100,'Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora',100,'Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple',100, 'Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 100, 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 100,'Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium',100, 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 100, 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'Dicot', 100)
insert into location values (100,'FL')
insert into location values ('ABGR4',100)
insert into planttype values (100,'shrub')
insert into planttype values ('ABGR4',100)

-- Test character lengths of different columns in plant, location, and planttype tables
---- at length
insert into plant values ('7Z16P4zU9vPJKcQALJUz2V3oFu0Zj5','Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','a4qTjH7lwBHzvh9Y5IhM1IoQEDqxoQYeLAxo3hStdd4t2xqg5ErGO9qd1OmqacC41LCQU4SVGMm4tjN04Vo0OXIeAfQMCJtTkm5VVPmmJqABfK2F5t5L6qlf28S5ZVsZCll4sYATr0on7rk7bLtTwC7krpgLwlNBeqNcM93iZOu56x1D8R8GE6p79kIs9T21rIq9lTJI','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','czy4q8ASP4NTnR1YvnNORuOOQZSvVZVw21SeRAFWO5iJrxjeYxUA8isKDrFx3nkiDh7y9fC122zno9242VDuT5tBVttdXPnGkMAB','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','uKf0CL52rDQ3thjVak17ggttCkJ8FkxYipnaeVKxM8jssI9je74yqqDrQ7LGkX40IOeP03mxo2uUXqM58FF81xydIfvsgJ4iDNqg0gOufNvZCXI9svaFrBvBFn3I7OS67XDSVacLgLvD769GAC3gpZTRsvOWHoeOlPSzwwO3ghfLpp5FO5HoTqQGa6j6IseOOY1zhI8r', 'Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'TImdavb2ldfDtcXNqYrOchmVdRatleYawRScOv4CadI1sRE5F9QqtAKAUC3QXtiCuKRtOnWp8FWVfhN8ck4fGRoAiZ6Cjvo4I4CB', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'TImdavb2ldfDtcXNqYrOchmVdRatleYawRScOv4CadI1sRE5F9QqtAKAUC3QXtiCuKRtOnWp8FWVfhN8ck4fGRoAiZ6Cjvo4I4CB', 'Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium', '8Ut69Ip16Qq4ztcruTZttsnrs1iu90lsrfrIP1uwwW60pFCRI7uIxou7OtDDusXNFwIXvvOtpjJlvAdFC58ZRcYde9aizAyKJEFPQVxKehhZMn0P3wIMY8G6HzkN47oLOvVnCv7UmEpFBfV90nskMj','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', '8Ut69Ip16Qq4ztcruTZttsnrs1iu90lsrfrIP1uwwW60pFCRI7uIxou7OtDDusXNFwIXvvOtpjJlvAdFC58ZRcYde9aizAyKJEFPQVxKehhZMn0P3wIMY8G6HzkN47oLOvVnCv7UmEpFBfV90nskMj', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'Dicot', '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')
insert into location values ('PgLrBtIDBgd37Z4TdtyU','FL')
insert into location values ('ABGR4','PgLrBtIDBgd37Z4TdtyU')
insert into planttype values ('OG5Zy0V1vpbusjvbiCo1','shrub')
insert into planttype values ('ABGR4','nsaPq0wsPgKAp9lNQxm1yhMbi3SVKzjbgXrqmcRqSXqeCJtIwd')
---- at length + 1
insert into plant values ('a7Z16P4zU9vPJKcQALJUz2V3oFu0Zj5','Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','aa4qTjH7lwBHzvh9Y5IhM1IoQEDqxoQYeLAxo3hStdd4t2xqg5ErGO9qd1OmqacC41LCQU4SVGMm4tjN04Vo0OXIeAfQMCJtTkm5VVPmmJqABfK2F5t5L6qlf28S5ZVsZCll4sYATr0on7rk7bLtTwC7krpgLwlNBeqNcM93iZOu56x1D8R8GE6p79kIs9T21rIq9lTJI','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','aczy4q8ASP4NTnR1YvnNORuOOQZSvVZVw21SeRAFWO5iJrxjeYxUA8isKDrFx3nkiDh7y9fC122zno9242VDuT5tBVttdXPnGkMAB','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','auKf0CL52rDQ3thjVak17ggttCkJ8FkxYipnaeVKxM8jssI9je74yqqDrQ7LGkX40IOeP03mxo2uUXqM58FF81xydIfvsgJ4iDNqg0gOufNvZCXI9svaFrBvBFn3I7OS67XDSVacLgLvD769GAC3gpZTRsvOWHoeOlPSzwwO3ghfLpp5FO5HoTqQGa6j6IseOOY1zhI8r', 'Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'aTImdavb2ldfDtcXNqYrOchmVdRatleYawRScOv4CadI1sRE5F9QqtAKAUC3QXtiCuKRtOnWp8FWVfhN8ck4fGRoAiZ6Cjvo4I4CB', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'TaImdavb2ldfDtcXNqYrOchmVdRatleYawRScOv4CadI1sRE5F9QqtAKAUC3QXtiCuKRtOnWp8FWVfhN8ck4fGRoAiZ6Cjvo4I4CB', 'Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium', 'a8Ut69Ip16Qq4ztcruTZttsnrs1iu90lsrfrIP1uwwW60pFCRI7uIxou7OtDDusXNFwIXvvOtpjJlvAdFC58ZRcYde9aizAyKJEFPQVxKehhZMn0P3wIMY8G6HzkN47oLOvVnCv7UmEpFBfV90nskMj','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'a8Ut69Ip16Qq4ztcruTZttsnrs1iu90lsrfrIP1uwwW60pFCRI7uIxou7OtDDusXNFwIXvvOtpjJlvAdFC58ZRcYde9aizAyKJEFPQVxKehhZMn0P3wIMY8G6HzkN47oLOvVnCv7UmEpFBfV90nskMj', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'Dicot', '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')
insert into location values ('aPgLrBtIDBgd37Z4TdtyU','FL')
insert into location values ('ABGR4','aPgLrBtIDBgd37Z4TdtyU')
insert into planttype values ('aOG5Zy0V1vpbusjvbiCo1','shrub')
insert into planttype values ('ABGR4','ansaPq0wsPgKAp9lNQxm1yhMbi3SVKzjbgXrqmcRqSXqeCJtIwd')

-- Test special characters inside names (accented letters and commas)
insert into plant values ('à, è, ì, ò, ù, À, È, Ì, Ò, Ù','Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','à, è, ì, ò, ù, À, È, Ì, Ò, Ù','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','à, è, ì, ò, ù, À, È, Ì, Ò, Ù','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','à, è, ì, ò, ù, À, È, Ì, Ò, Ù', 'Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'à, è, ì, ò, ù, À, È, Ì, Ò, Ù', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'à, è, ì, ò, ù, À, È, Ì, Ò, Ù','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','à, è, ì, ò, ù, À, È, Ì, Ò, Ù', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'à, è, ì, ò, ù, À, È, Ì, Ò, Ù', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green', 'Brown', 'Medium','Semi - Erect', 'Dicot', 'à, è, ì, ò, ù, À, È, Ì, Ò, Ù')
insert into location values ('à, è, ì, ò, ù, À, È','FL')
insert into location values ('ABGR4','à, è, ì, ò, ù, À, È')
insert into planttype values ('à, è, ì, ò, ù, À, È','shrub')
insert into planttype values ('ABGR4','à, è, ì, ò, ù, À, È')

GO
/****** Group data in plant, location, and planttype tables ******/
select color_flower
from plant
group by color_flower
order by asc;

select color_foliage
from plant
group by color_foliage
order by asc;

select color_fruit_seed
from plant
group by color_fruit_seed
order by asc;

select texture_foliage
from plant
group by texture_foliage
order by asc;

select shape
from plant
group by shape
order by asc;

select pattern
from plant
group by pattern
order by asc;

-------------------
select us_state
from location
group by us_state
order by asc;

-------------------
select type
from plant_type
group by type
order by asc;