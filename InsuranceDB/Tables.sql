CREATE DATABASE [db_insurance]
GO

USE db_insurance
GO

CREATE TABLE [dbo].[address](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[street] [varchar](50) NOT NULL,
	[number] [varchar](8) NOT NULL,
	[_floor] [int] NULL,
	[departament] [varchar](5) NULL,
	[city] [varchar](30) NOT NULL,
	[province] [varchar](30) NOT NULL,
	[country] [varchar](20) NOT NULL,
 CONSTRAINT [PK_address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 15/6/2023 19:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](15) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[token] [varchar](200) NULL,
	[producer] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[company]    Script Date: 15/6/2023 19:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
	[logo] [varchar](200) NOT NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[insured]    Script Date: 15/6/2023 19:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[insured](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](25) NOT NULL,
	[lastname] [varchar](30) NOT NULL,
	[license] [varchar](15) NOT NULL,
	[folder] [int] NOT NULL,
	[life] [varchar](11) NOT NULL,
	[born] [date] NOT NULL,
	[address] [bigint] NOT NULL,
	[dni] [nchar](8) NOT NULL,
	[cuit] [varchar](15) NULL,
	[producer] [bigint] NOT NULL,
	[description] [varchar](100) NULL,
	[company] [bigint] NOT NULL,
	[insurancePolicy] [varchar](50) NULL,
	[status] [varchar](10) NOT NULL,
	[paymentExpiration] [smallint] NOT NULL,
 CONSTRAINT [PK_insured] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phone]    Script Date: 15/6/2023 19:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phone](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[insured] [bigint] NOT NULL,
	[number] [varchar](20) NOT NULL,
	[description] [varchar](30) NULL,
 CONSTRAINT [PK_phone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producer]    Script Date: 15/6/2023 19:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producer](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](20) NOT NULL,
	[lastname] [varchar](30) NOT NULL,
	[joined] [date] NOT NULL,
	[code] [int] DEFAULT ((2221)) NOT NULL,
 CONSTRAINT [PK_producer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[address] ON 

INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1, N'San Nicolás', N'727', NULL, NULL, N'Arroyo Seco', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (3, N'Lionel Andrés Messi', N'2022', 4, N'86', N'Rosario', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (5, N'Enzo Ferrari', N'016', 7, N'55', N'Maranello', N'Modena', N'Italia')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (8, N'red bull', N'1', NULL, NULL, N'milton keys', N'inglaterra', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (9, N'red bull', N'1', NULL, NULL, N'milton keys', N'inglaterra', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (10, N'red bull', N'1', NULL, NULL, N'milton keys', N'inglaterra', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (11, N'red bull', N'1', NULL, NULL, N'milton keys', N'inglaterra', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (17, N'bs as', N'2005', NULL, NULL, N'buenos aires', N'buenos aires', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (19, N'bs as', N'2005', NULL, NULL, N'buenos aires', N'buenos aires', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (22, N'red bull', N'11', NULL, NULL, N'cdmx', N'cdmx', N'Chile')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (26, N'bs as', N'2005', NULL, NULL, N'buenos aires', N'buenos aires', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (27, N'bs as', N'2005', NULL, NULL, N'buenos aires', N'buenos aires', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (28, N'Lionel Andrés Messi', N'2022', 4, N'86', N'Rosario', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (29, N'San Nicolás', N'727', NULL, NULL, N'Arroyo Seco', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (30, N'San Nicolás', N'727', NULL, NULL, N'Arroyo Seco', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (31, N'San Nicolás', N'727', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (32, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (33, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (34, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (35, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (36, N'Lionel Andrés Messi', N'2022', 4, N'86', N'Rosario', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (37, N'Lionel Andrés Messi', N'2022', 4, N'86', N'Rosario', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (38, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (39, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (40, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (41, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (42, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (43, N'red bull', N'1', NULL, NULL, N'milton keys', N'inglaterra', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (44, N'enzo', N'16', 2, N'28', N'maranello', N'italia', N'Brasil')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (45, N'San Nicolás', N'200', NULL, NULL, N'Arroyo Seco', N'cordoba', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (51, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (81, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (83, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (375, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (376, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (404, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (406, N' CTDA. MEDICINA', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (694, N'  M. MAIORANO ', N' 478', NULL, N'B', N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1330, N'  CTDA. MEDICINA ', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1331, N'  CTDA. MEDICINA ', N' 48 BIS', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1332, N'  M. MAIORANO ', N' 478', NULL, N'B', N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1333, N'  URQUIZA ', N' 259', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1334, N'  JUAREZ CELMAN ', N' 789', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1335, N'  JUAREZ CELMAN ', N' 789', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1336, N'  BELGRANO ', N' 369', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1337, N'  COLON ', N' 332', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1338, N'  COLON ', N' 332', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1339, N'  ZONA RURAL ', N' S/N', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1340, N'  J.SALK ', N' S/N', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1341, N'  H.IRIGOYEN ', N' 573', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1342, N'  H.IRIGOYEN ', N' 573', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1343, N'  BELGRANO ', N' 741 ', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1344, N'  JUAN PAGNAN ', N' 584', NULL, NULL, N'FIGHIERA', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1345, N'  JUAN PAGNAN ', N' 585', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1346, N'  ALEJ. GOMEZ ', N' 960', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1347, N'  MITRE ', N' 626 ', NULL, NULL, N'GRAL.LAGOS', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1348, N'  RIVADAVIA ', N' 924', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1349, N'  MORENO ', N' 1175', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1350, N'  SAN NICOLAS ', N' 78', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1351, N'  SARMIENTO ', N' 547', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1352, N'  MORENO ', N' 645', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1353, N'  INDEPENDENCIA ', N' 72', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1354, N'  MORENO ', N' 5883', NULL, NULL, N'ROSARIO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1355, N'  INDEPENDENCIA ', N' 1595', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1356, N'  FILIBERTI ', N' 860', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1357, N'  SAN MARTIN ', N' 400', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1358, N'  INDEPENDENCIA ', N' 1584', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1359, N'  I. COSTANTINI ', N' 860', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1360, N'  INDEPENDECIA ', N' 1330', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1361, N'  COSTANTINI ', N' 669', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1362, N'  AMEGHINO ', N' 49', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1363, N'  ZONA PUERTO ', N' S/N', NULL, NULL, N'ARROYO SECO', N'Santa Fe', N'Argentina')
SET IDENTITY_INSERT [dbo].[address] OFF
GO
SET IDENTITY_INSERT [dbo].[admin] ON 

INSERT [dbo].[admin] ([Id], [username], [password], [token], [producer]) VALUES (1, N'tiki', N'$2a$13$bDdBQmqR2O.rByA8r3nMHOzZbUycs1RVqwCkJ7QTDILccR8E0m6iC', NULL, 1)
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[company] ON 

INSERT [dbo].[company] ([id], [name], [logo]) VALUES (1, N'Cooperación Seguros', N'https://selfie.100seguro.com.ar/wp-content/uploads/2022/10/logo-cia.png')
INSERT [dbo].[company] ([id], [name], [logo]) VALUES (2, N'Federación Patronal', N'https://www.eliasseguros.com.ar/images/logo-federacion.png')
SET IDENTITY_INSERT [dbo].[company] OFF
GO
SET IDENTITY_INSERT [dbo].[insured] ON 

INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (959, N'EL', N'PEPE', N'464778/05', 1509, N'04/08-04/02', CAST(N'1974-07-09' AS Date), 1330, N'24007576', N'', 1, N'', 1, N'F512 T ', N'EN JUICIO', 6)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (960, N'CASTELUCHE', N'ESTELA', N'464778/05', 0, N'01/12-01/04', CAST(N'1974-07-10' AS Date), 1331, N'24007576', N'', 1, N'', 1, N'Focus ', N'ACTIVA', 18)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (961, N'CASTILLO', N'CARLOS', N'M-4303104', 0, N'21/04-21/04', CAST(N'1990-03-17' AS Date), 1332, N'34597754', N'', 1, N'Lopez Carlos', 2, N'incendio Lopez C. ', N'ACTIVA', 21)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (962, N'CASTILLO', N'DIEGO', N'827501', 0, N'17/10-17/02', CAST(N'1988-02-29' AS Date), 1333, N'33449079', N'', 1, N'', 1, NULL, N'ACTIVA', 17)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (963, N'CASTILLO', N'DOMINGO', N'M-5000657', 0, N'15/08-15/08', CAST(N'1965-08-04' AS Date), 1334, N'17417465', N'', 1, N'Lopez Carlos', 2, N'incendio ', N'ACTIVA', 15)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (964, N'CASTILLO', N'DOMINGO', N'345464/09', 0, N'05/11-05/03', CAST(N'1965-08-04' AS Date), 1335, N'17417465', N'', 1, N'', 1, N'Peug. 206 ', N'ACTIVA', 12)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (965, N'CASTILLO', N'EZEQUIEL', N'M-5730034', 0, N'20/12-20/12', CAST(N'1989-11-05' AS Date), 1336, N'34651441', N'', 1, N'Lopez Carlos', 2, NULL, N'ACTIVA', 20)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (966, N'CASTRELO', N'JORGE', N'M-4588931', 1109, N'13/03-13/03', CAST(N'1969-04-23' AS Date), 1337, N'20590230', N'', 1, N'', 1, N'Cronos ', N'ACTIVA', 16)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (967, N'CASTRELO', N'JORGE', N'M-4588931', 0, N'26/05-26/05', CAST(N'1969-04-23' AS Date), 1338, N'20590230', N'', 1, N'', 1, N'Siena ', N'ACTIVA', 29)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (968, N'CASTRICINI', N'JUAN', N'745206/03', 1269, N'11/12-11/04', CAST(N'1954-04-08' AS Date), 1339, N'11108014', N'', 2, N'', 1, N'Megane ', N'ACTIVA', 15)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (969, N'CASTRICINI', N'JUAN', N'M-888866', 142, N'06/04-06/10', CAST(N'1954-04-08' AS Date), 1340, N'11108014', N'23000000000', 2, N'', 1, N'camión ', N'ACTIVA', 9)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (970, N'CASTRICINI', N'MARIELA', N'M-1739310', 606, N'30/11-31/05', CAST(N'1972-08-10' AS Date), 1341, N'22833279', N'', 1, N'', 2, N'auto ', N'ACTIVA', 3)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (971, N'CASTRICINI', N'MARIELA', N'M-1739310', 0, N'31/08-30/09', CAST(N'1972-08-10' AS Date), 1342, N'22833279', N'', 1, N'', 2, N'a.p. albañil ', N'ACTIVA', 31)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (972, N'CASTRO', N'ROMUALDO', N'460644/02', 583, N'27/05-27/11', CAST(N'1947-05-22' AS Date), 1343, N'6142396 ', N'', 2, N'', 1, NULL, N'ACTIVA', 10)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (973, N'CATALANI', N'JAQUELINA', N'513487/00', 1062, N'19/07-19/01', CAST(N'1981-02-25' AS Date), 1344, N'28486829', N'341-5068339', 2, N'', 1, N'Twingo ', N'ACTIVA', 4)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (974, N'CATALANI', N'JUAN', N'M- 3752425', 659, N'19/07-19/01', CAST(N'1951-01-15' AS Date), 1345, N'6058772 ', N'', 1, N'', 2, NULL, N'EN JUICIO', 4)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (975, N'CATALANI', N'NORBERTO', N'M-1547838', 1460, N'08/09-08/09', CAST(N'1951-01-16' AS Date), 1346, N'8374336 ', N'', 1, N'', 2, NULL, N'ACTIVA', 11)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (976, N'CATILLI', N'ROBERTO', N'835776', 0, N'06/01-06/05', CAST(N'1967-05-14' AS Date), 1347, N'17982733', N'', 2, N'', 1, NULL, N'ACTIVA', 6)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (977, N'CATTANI', N'ESTEBAN', N'M-3177480', 1192, N'26/04-26/04', CAST(N'1986-11-18' AS Date), 1348, N'32648315', N'', 1, N'', 2, N'Oroch ', N'ACTIVA', 29)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (978, N'CATTANI', N'MARIANA', N'554793/08', 1602, N'13/08-13/12', CAST(N'1973-06-12' AS Date), 1349, N'23356047', N'', 1, N'', 1, N'Eco Sport 2015 ', N'ACTIVA', 20)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (979, N'CAUCOTE', N'ROSA', N'M-3155570', 1223, N'13/04-13/08', CAST(N'1966-02-08' AS Date), 1350, N'17507317', N'', 1, N'', 2, N'UP ', N'ACTIVA', 16)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (980, N'CEBALLEZ', N'MARIA', N'M-4009298', 0, N'15/09-15/09', CAST(N'1987-08-26' AS Date), 1351, N'32897866', N'', 4, N'', 1, N'aparte ', N'ACTIVA', 15)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (981, N'CECCARELLI', N'MIGUEL', N'M-5192198', 0, N'01/12-01/12', CAST(N'1952-03-06' AS Date), 1352, N'10060488', N'', 4, N'Moresi inmobiliaria', 1, NULL, N'ACTIVA', 1)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (982, N'CECIA', N'JENIMA', N'M-5056060', 0, N'07/05-07/05', CAST(N'1991-09-04' AS Date), 1353, N'36681372', N'', 4, N'Nozzi Inmobiliaria', 1, NULL, N'ACTIVA', 7)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (983, N'CEFARELLI', N'RUBEN', N'M-4160777', 1523, N'20/09-20/09', CAST(N'1953-09-13' AS Date), 1354, N'10866004', N'', 5, N'', 1, NULL, N'ACTIVA', 23)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (984, N'CEJAS', N'GERMAN', N'M-703208', 94, N'03/01-03/07', CAST(N'1951-05-28' AS Date), 1355, N'8504130 ', N'', 2, N'', 1, NULL, N'ACTIVA', 6)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (985, N'CEJAS', N'JOANA', N'758685/10', 802, N'18/06-18/06', CAST(N'1986-09-28' AS Date), 1356, N'32430634', N'', 1, N'', 1, NULL, N'ACTIVA', 3)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (986, N'CEJAS', N'JUAN', N'M-1830448', 2726, N'03/11-03/11', CAST(N'1971-10-19' AS Date), 1357, N'22479072', N'', 1, N'', 2, NULL, N'ACTIVA', 6)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (987, N'CEJAS', N'LAURA', N'782244', 0, N'18/03-18/07', CAST(N'1971-10-07' AS Date), 1358, N'22308443', N'', 1, N'', 1, N'Magnate Maximo ', N'ACTIVA', 18)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (988, N'CEJAS', N'SEBASTIAN', N'442716/05', 1456, N'26/11-26/05', CAST(N'1991-01-22' AS Date), 1359, N'35644847', N'', 1, N'', 1, NULL, N'ANULADA', 14)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (989, N'CEJAS', N'SEBASTIAN', N'M-5309593', 0, N'15/05-15/05', CAST(N'1991-01-22' AS Date), 1360, N'35644847', N'', 1, N'', 2, N'moto ', N'ACTIVA', 18)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (990, N'CEJAS', N'WALTER', N'817453', 0, N'01/07-01/11', CAST(N'1985-10-29' AS Date), 1361, N'31613287', N'', 1, N'', 1, NULL, N'ACTIVA', 8)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (991, N'CELAYA', N'SERGIO', N'794941', 0, N'15/09-15/01', CAST(N'1979-06-22' AS Date), 1362, N'27115036', N'', 1, N'', 1, NULL, N'ANULADA', 22)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (992, N'CELESTE', N'RICARDO', N'M-1690136', 6, N'08/02-08/08', CAST(N'1959-06-09' AS Date), 1363, N'13413416', N'', 2, N'', 1, N'auto ', N'ACTIVA', 11)
SET IDENTITY_INSERT [dbo].[insured] OFF
GO
SET IDENTITY_INSERT [dbo].[phone] ON 

INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1001, 959, N'15444305', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1002, 960, N'15444305', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1003, 961, N'15482523', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1004, 962, N'15548546', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1005, 963, N'15530926', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1006, 964, N'15530926', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1007, 965, N'0341-15-588-5701', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1008, 966, N'15543934', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1009, 967, N'15543934', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1010, 968, N'15448436', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1011, 969, N'3402448436', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1012, 970, N'426489 ', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1013, 970, N' 15447816', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1014, 971, N'426489', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1015, 972, N'427002', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1016, 972, N'15658988', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1017, 973, N'427944  ', N'padre')
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1018, 974, N'427944  ', N'padre')
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1019, 975, N'15659512', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1020, 976, N'15541035', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1021, 977, N'0341-153554270', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1022, 978, N'0341 153153201 ', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1023, 978, N' 429046', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1024, 979, N'427738 ', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1025, 979, N' 15484174', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1026, 980, N'02473-15-44-9828', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1027, 981, N'15545171', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1028, 982, N'15-50-8823', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1029, 983, N'15-50-8823', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1030, 984, N'15503410', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1031, 985, N'15546842', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1032, 986, N'011-15-5000-8777', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1033, 987, N'15507981', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1034, 988, N'15527972', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1035, 989, N'15527972', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1036, 990, N'15501985', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1037, 991, N'15559280', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1038, 992, N'15521227', NULL)
SET IDENTITY_INSERT [dbo].[phone] OFF
GO
SET IDENTITY_INSERT [dbo].[producer] ON 

INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined]) VALUES (1, N'tiki', N'quaglia', CAST(N'2023-05-01' AS Date))
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined]) VALUES (2, N'ricardo', N'quaglia', CAST(N'2000-01-01' AS Date))
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined]) VALUES (3, N'leo', N'lionetti', CAST(N'1980-01-01' AS Date))
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined]) VALUES (4, N'zurdo', N'nieri', CAST(N'2007-01-01' AS Date))
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined]) VALUES (5, N'rogelio', N'verderone', CAST(N'2007-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[producer] OFF
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT ((1)) FOR [producer]
GO
ALTER TABLE [dbo].[insured] ADD  DEFAULT ((0)) FOR [company]
GO
ALTER TABLE [dbo].[insured] ADD  DEFAULT ('active') FOR [status]
GO
ALTER TABLE [dbo].[insured] ADD  DEFAULT ((0)) FOR [paymentExpiration]
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD  CONSTRAINT [FK_admin_producer] FOREIGN KEY([producer])
REFERENCES [dbo].[producer] ([id])
GO
ALTER TABLE [dbo].[admin] CHECK CONSTRAINT [FK_admin_producer]
GO
ALTER TABLE [dbo].[insured]  WITH CHECK ADD  CONSTRAINT [FK_address_insured] FOREIGN KEY([address])
REFERENCES [dbo].[address] ([id])
GO
ALTER TABLE [dbo].[insured] CHECK CONSTRAINT [FK_address_insured]
GO
ALTER TABLE [dbo].[insured]  WITH CHECK ADD  CONSTRAINT [FK_company_insured] FOREIGN KEY([company])
REFERENCES [dbo].[company] ([id])
GO
ALTER TABLE [dbo].[insured] CHECK CONSTRAINT [FK_company_insured]
GO
ALTER TABLE [dbo].[insured]  WITH CHECK ADD  CONSTRAINT [FK_producer_insured] FOREIGN KEY([producer])
REFERENCES [dbo].[producer] ([id])
GO
ALTER TABLE [dbo].[insured] CHECK CONSTRAINT [FK_producer_insured]
GO
ALTER TABLE [dbo].[phone]  WITH CHECK ADD  CONSTRAINT [FK_insured_phone] FOREIGN KEY([insured])
REFERENCES [dbo].[insured] ([id])
GO
ALTER TABLE [dbo].[phone] CHECK CONSTRAINT [FK_insured_phone]
GO
USE [master]
GO
ALTER DATABASE [db_insurance] SET  READ_WRITE 
GO
