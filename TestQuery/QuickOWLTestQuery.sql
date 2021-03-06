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


insert into plant values ('ABGR4','Abelia ??grandiflora','Purple','Dark Green','Brown', 'Medium','Semi - Erect', 'Dicot', 'http://plants.usda.gov/gallery/pubs/ABGR4_001_pvp.jpg')
insert into location values ('ABGR4','FL')
insert into planttype values ('ABGR4','shrub')

GO

/****** Check for what category values exist for each question ******/
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

------------------
select us_state
from location
group by us_state
order by asc;

------------------
select type
from plant_type
group by type
order by asc;

GO

