USE [productosDB]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 17/10/2022 11:43:35 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[RestriccionEdad] [int] NULL,
	[Compania] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [RestriccionEdad], [Compania]) VALUES (1, N'name1', N'Desc1', 15, N'Company1')
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [RestriccionEdad], [Compania]) VALUES (2, N'n2', N'D2', 11, N'C2')
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [RestriccionEdad], [Compania]) VALUES (7, N'hnnggg', N'', 0, N'fgffgfff')
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
ALTER TABLE [dbo].[Productos] ADD  DEFAULT ('') FOR [Descripcion]
GO
ALTER TABLE [dbo].[Productos] ADD  DEFAULT ((0)) FOR [RestriccionEdad]
GO
