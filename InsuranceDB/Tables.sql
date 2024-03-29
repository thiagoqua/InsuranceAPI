CREATE DATABASE [db_insurance]
GO

USE [db_insurance]
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
/****** Object:  Table [dbo].[admin]    Script Date: 2/8/2023 20:54:02 ******/
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
/****** Object:  Table [dbo].[company]    Script Date: 2/8/2023 20:54:02 ******/
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
/****** Object:  Table [dbo].[insured]    Script Date: 2/8/2023 20:54:02 ******/
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
/****** Object:  Table [dbo].[phone]    Script Date: 2/8/2023 20:54:02 ******/
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
/****** Object:  Table [dbo].[producer]    Script Date: 2/8/2023 20:54:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producer](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](20) NOT NULL,
	[lastname] [varchar](30) NOT NULL,
	[joined] [date] NOT NULL,
	[code] [int] NOT NULL,
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
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1364, N'tokyo', N'938293', NULL, NULL, N'shangai', N'china', N'Paraguay')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1539, N'Calle Real', N'101', 1, N'1A', N'Barcelona', N'Catalonia', N'Spain')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1540, N'Avenida Libertad', N'202', 2, N'2B', N'Madrid', N'Madrid', N'Spain')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1541, N'Paseo de la Castellana', N'303', 3, N'3C', N'Paris', N'Île-de-France', N'France')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1542, N'Via della Conciliazione', N'404', 4, N'4D', N'Rome', N'Lazio', N'Italy')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1543, N'Strasse des 17. Juni', N'505', 5, N'5E', N'Berlin', N'Berlin', N'Germany')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1544, N'Schönbrunner Schloßstraße', N'606', 6, N'6F', N'Vienna', N'Vienna', N'Austria')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1545, N'Baker Street', N'707', 7, N'7G', N'London', N'England', N'UK')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1546, N'Pennsylvania Avenue', N'808', 8, N'8H', N'Washington D.C.', N'DC', N'USA')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1547, N'Chang an Avenue', N'909', 9, N'9I', N'Beijing', N'Beijing', N'China')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1548, N'Ginza Street', N'1010', 10, N'10J', N'Tokyo', N'Tokyo', N'Japan')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1549, N'Rua Augusta', N'111', 11, N'11A', N'Lisbon', N'Lisbon', N'Portugal')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1550, N'Wall Street', N'222', 12, N'12B', N'New York', N'NY', N'USA')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1551, N'Main Street', N'333', 13, N'13C', N'Los Angeles', N'CA', N'USA')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1552, N'Rue du Faubourg Saint-Antoine', N'444', 14, N'14D', N'Paris', N'Île-de-France', N'France')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1553, N'Via dei Fori Imperiali', N'555', 15, N'15E', N'Rome', N'Lazio', N'Italy')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1554, N'Boulevard de Clichy', N'666', 16, N'16F', N'Paris', N'Île-de-France', N'France')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1555, N'Bahnhofstraße', N'777', 17, N'17G', N'Zurich', N'Zurich', N'Switzerland')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1556, N'Kurfürstendamm', N'888', 18, N'18H', N'Berlin', N'Berlin', N'Germany')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1557, N'Strøget', N'999', 19, N'19I', N'Copenhagen', N'Copenhagen', N'Denmark')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1558, N'Fifth Avenue', N'1011', 20, N'20J', N'New York', N'NY', N'USA')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1559, N'   MAIN ST  ', N' 478', NULL, NULL, N'NEW YORK', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1560, N'   SECOND AVE  ', N' 259', NULL, NULL, N'LOS ANGELES', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1561, N'   THIRD ST  ', N' 789', NULL, NULL, N'CHICAGO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1562, N'   FOURTH RD  ', N' 789', NULL, NULL, N'HOUSTON', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1563, N'   FIFTH LN  ', N' 924', NULL, NULL, N'PHOENIX', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1564, N'   SIXTH TER  ', N' 48 BIS', NULL, NULL, N'PHILADELPHIA', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1565, N'   SEVENTH PL  ', N' 369', NULL, NULL, N'SAN ANTONIO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1566, N'   EIGHTH DR  ', N' 1595', NULL, NULL, N'SAN DIEGO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1567, N'   NINTH AVE  ', N' 584', NULL, NULL, N'DALLAS', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1568, N'   TENTH ST  ', N' 626 ', NULL, NULL, N'SAN JOSE', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1569, N'   ELEVENTH AVE  ', N' 963', NULL, NULL, N'AUSTIN', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1570, N'   TWELFTH RD  ', N' 852', NULL, NULL, N'JACKSONVILLE', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1571, N'   THIRTEENTH ST  ', N' 741', NULL, NULL, N'FORT WORTH', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1572, N'   FOURTEENTH LN  ', N' 630', NULL, NULL, N'COLUMBUS', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1573, N'   FIFTEENTH PL  ', N' 519', NULL, NULL, N'CHARLOTTE', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1574, N'   SIXTEENTH DR  ', N' 408', NULL, NULL, N'INDIANAPOLIS', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1575, N'   SEVENTEENTH TER  ', N' 297', NULL, NULL, N'SAN FRANCISCO', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1576, N'   EIGHTEENTH AVE  ', N' 186', NULL, NULL, N'SEATTLE', N'Santa Fe', N'Argentina')
INSERT [dbo].[address] ([id], [street], [number], [_floor], [departament], [city], [province], [country]) VALUES (1577, N'   NINETEENTH ST  ', N' 75', NULL, NULL, N'DENVER', N'Santa Fe', N'Argentina')
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

INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1167, N'JOHN', N'SMITH', N'M-1234567', 1192, N'12/06-12/06', CAST(N'1980-03-17' AS Date), 1559, N'12345678', N'', 1, N'John''s Case', 1, NULL, N'ACTIVA', 12)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1168, N'JANE', N'DOE', N'M-2345678', 1192, N'13/07-13/01', CAST(N'1988-02-29' AS Date), 1560, N'23456789', N'', 3, N'', 1, NULL, N'ACTIVA', 13)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1169, N'JACK', N'JOHNSON', N'M-3456789', 1192, N'15/08-15/08', CAST(N'1965-08-04' AS Date), 1561, N'34567890', N'', 2, N'Jack''s Issue', 1, NULL, N'ACTIVA', 15)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1170, N'BOB', N'BROWN', N'M-4567890', 1192, N'05/11-05/03', CAST(N'1965-08-04' AS Date), 1562, N'45678901', N'', 1, N'', 1, NULL, N'ACTIVA', 5)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1171, N'DAVE', N'DAVIS', N'M-5678901', 1192, N'26/04-26/04', CAST(N'1986-11-18' AS Date), 1563, N'56789012', N'', 3, N'', 1, NULL, N'ACTIVA', 26)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1172, N'MIKE', N'MILLER', N'M-6789012', 1192, N'01/12-01/04', CAST(N'1974-07-10' AS Date), 1564, N'67890123', N'', 2, N'', 1, NULL, N'ACTIVA', 1)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1173, N'WILLIAM', N'WILSON', N'M-7890123', 1192, N'20/12-20/12', CAST(N'1989-11-05' AS Date), 1565, N'78901234', N'', 1, N'William''s Matter', 1, NULL, N'ACTIVA', 20)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1174, N'MARY', N'MOORE', N'M-8901234', 1192, N'03/01-03/07', CAST(N'1951-05-28' AS Date), 1566, N'89012345', N'23000000000', 3, N'', 1, NULL, N'ACTIVA', 3)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1175, N'THOMAS', N'TAYLOR', N'M-9012345', 1192, N'19/07-19/01', CAST(N'1981-02-25' AS Date), 1567, N'90123456', N'', 2, N'', 1, NULL, N'ACTIVA', 19)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1176, N'ALICE', N'ANDERSON', N'M-0123456', 1192, N'06/01-06/05', CAST(N'1967-05-14' AS Date), 1568, N'01234567', N'', 1, N'', 1, NULL, N'ACTIVA', 6)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1177, N'PETER', N'PATTERSON', N'M-7896543', 1192, N'11/03-11/09', CAST(N'1995-04-01' AS Date), 1569, N'78965432', N'', 3, N'Peter''s Case', 1, NULL, N'ACTIVA', 11)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1178, N'JIM', N'JONES', N'M-8765432', 1192, N'17/10-17/04', CAST(N'1996-06-03' AS Date), 1570, N'87654321', N'', 2, N'', 1, NULL, N'ACTIVA', 17)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1179, N'PAUL', N'PARKER', N'M-7654321', 1192, N'09/11-09/05', CAST(N'1997-08-08' AS Date), 1571, N'76543210', N'', 1, N'Paul''s Issue', 1, NULL, N'ACTIVA', 9)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1180, N'PHIL', N'PHILLIPS', N'M-6543210', 1192, N'23/05-23/11', CAST(N'1998-09-14' AS Date), 1572, N'65432109', N'', 3, N'', 1, NULL, N'ACTIVA', 23)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1181, N'ROB', N'ROBERTS', N'M-5432109', 1192, N'02/01-02/07', CAST(N'1999-10-19' AS Date), 1573, N'54321098', N'', 2, N'', 1, NULL, N'ACTIVA', 2)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1182, N'TOM', N'TURNER', N'M-4321098', 1192, N'14/02-14/08', CAST(N'2000-11-24' AS Date), 1574, N'43210987', N'', 1, N'Tom''s Matter', 1, NULL, N'ACTIVA', 14)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1183, N'TINA', N'THOMPSON', N'M-3210987', 1192, N'07/03-07/09', CAST(N'2001-12-29' AS Date), 1575, N'32109876', N'', 3, N'', 1, NULL, N'ACTIVA', 7)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1184, N'WENDY', N'WHITE', N'M-2109876', 1192, N'16/04-16/10', CAST(N'2002-01-03' AS Date), 1576, N'21098765', N'', 2, N'', 1, NULL, N'ACTIVA', 16)
INSERT [dbo].[insured] ([id], [firstname], [lastname], [license], [folder], [life], [born], [address], [dni], [cuit], [producer], [description], [company], [insurancePolicy], [status], [paymentExpiration]) VALUES (1185, N'YVONNE', N'YOUNG', N'M-1098765', 1192, N'03/05-03/11', CAST(N'2003-02-08' AS Date), 1577, N'10987654', N'', 1, N'Yvonne''s Case', 1, NULL, N'ACTIVA', 3)
SET IDENTITY_INSERT [dbo].[insured] OFF
GO
SET IDENTITY_INSERT [dbo].[phone] ON 

INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1233, 1167, N'12345678', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1234, 1168, N'23456789', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1235, 1169, N'34567890', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1236, 1170, N'45678901', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1237, 1171, N'56789012', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1238, 1172, N'67890123', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1239, 1173, N'78901234', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1240, 1174, N'89012345', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1241, 1175, N'90123456', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1242, 1176, N'1234567', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1243, 1177, N'78965432', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1244, 1178, N'87654321', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1245, 1179, N'76543210', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1246, 1180, N'65432109', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1247, 1181, N'54321098', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1248, 1182, N'43210987', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1249, 1183, N'32109876', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1250, 1184, N'21098765', NULL)
INSERT [dbo].[phone] ([id], [insured], [number], [description]) VALUES (1251, 1185, N'10987654', NULL)
SET IDENTITY_INSERT [dbo].[phone] OFF
GO
SET IDENTITY_INSERT [dbo].[producer] ON 

INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined], [code]) VALUES (1, N'tiki', N'quaglia', CAST(N'2023-05-01' AS Date), 2221)
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined], [code]) VALUES (2, N'checo', N'perez', CAST(N'2000-01-01' AS Date), 2221)
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined], [code]) VALUES (3, N'pepe', N'argento', CAST(N'1980-01-01' AS Date), 2221)
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined], [code]) VALUES (4, N'zurdo', N'nieri', CAST(N'2007-01-01' AS Date), 2221)
INSERT [dbo].[producer] ([id], [firstname], [lastname], [joined], [code]) VALUES (5, N'rogelio', N'verderone', CAST(N'2007-01-01' AS Date), 2221)
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
ALTER TABLE [dbo].[producer] ADD  DEFAULT ((2221)) FOR [code]
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
