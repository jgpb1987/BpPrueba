USE [bpPrueba]
GO
/****** Object:  Schema [ahorro]    Script Date: 03/07/2022 4:31:09 PM ******/
CREATE SCHEMA [ahorro]
GO
/****** Object:  Schema [cliente]    Script Date: 03/07/2022 4:31:09 PM ******/
CREATE SCHEMA [cliente]
GO
/****** Object:  Schema [financiero]    Script Date: 03/07/2022 4:31:09 PM ******/
CREATE SCHEMA [financiero]
GO
/****** Object:  Schema [sujeto]    Script Date: 03/07/2022 4:31:09 PM ******/
CREATE SCHEMA [sujeto]
GO
/****** Object:  Table [ahorro].[cuenta]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ahorro].[cuenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[clienteId] [int] NOT NULL,
	[codigoTipoCuenta] [nvarchar](5) NOT NULL,
	[numero] [nvarchar](15) NOT NULL,
	[saldoInicial] [decimal](18, 6) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_cuenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ahorro].[montoMaximoRetiro_tipoCuenta]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ahorro].[montoMaximoRetiro_tipoCuenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoTipoCuenta] [nvarchar](5) NOT NULL,
	[monto] [decimal](18, 2) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_montoMaximoRetiro_tipoCuenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ahorro].[tipoCuenta]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ahorro].[tipoCuenta](
	[codigo] [nvarchar](5) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_tipoCuenta] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [cliente].[cliente]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cliente].[cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[personaId] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [cliente].[cliente_contrasenia]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cliente].[cliente_contrasenia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[clienteId] [int] NOT NULL,
	[contrasenia] [nvarchar](200) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_cliente_contrasenia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [financiero].[movimiento]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [financiero].[movimiento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cuentaId] [int] NOT NULL,
	[fechaProceso] [datetime] NOT NULL,
	[fecha] [date] NOT NULL,
	[valor] [decimal](18, 6) NOT NULL,
	[saldo] [decimal](18, 6) NOT NULL,
	[esDebito] [bit] NOT NULL,
 CONSTRAINT [pk_movimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sujeto].[genero]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sujeto].[genero](
	[codigo] [nvarchar](1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [pk_genero] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sujeto].[persona]    Script Date: 03/07/2022 4:31:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sujeto].[persona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoGenero] [nvarchar](1) NOT NULL,
	[identificacion] [nvarchar](20) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[fechaNacimiento] [date] NOT NULL,
	[direccion] [nvarchar](200) NOT NULL,
	[telefono] [nvarchar](13) NOT NULL,
 CONSTRAINT [pk_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [ahorro].[cuenta] ON 

INSERT [ahorro].[cuenta] ([id], [clienteId], [codigoTipoCuenta], [numero], [saldoInicial], [estado]) VALUES (1, 1, N'COR', N'123456', CAST(500.000000 AS Decimal(18, 6)), 1)
INSERT [ahorro].[cuenta] ([id], [clienteId], [codigoTipoCuenta], [numero], [saldoInicial], [estado]) VALUES (2, 1, N'AHO', N'123457', CAST(500.000000 AS Decimal(18, 6)), 1)
SET IDENTITY_INSERT [ahorro].[cuenta] OFF
GO
SET IDENTITY_INSERT [ahorro].[montoMaximoRetiro_tipoCuenta] ON 

INSERT [ahorro].[montoMaximoRetiro_tipoCuenta] ([id], [codigoTipoCuenta], [monto], [estado]) VALUES (1, N'AHO', CAST(1000.00 AS Decimal(18, 2)), 1)
INSERT [ahorro].[montoMaximoRetiro_tipoCuenta] ([id], [codigoTipoCuenta], [monto], [estado]) VALUES (2, N'COR', CAST(1000.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [ahorro].[montoMaximoRetiro_tipoCuenta] OFF
GO
INSERT [ahorro].[tipoCuenta] ([codigo], [descripcion], [estado]) VALUES (N'AHO', N'AHORROS', 1)
INSERT [ahorro].[tipoCuenta] ([codigo], [descripcion], [estado]) VALUES (N'COR', N'CORRIENTE', 1)
GO
SET IDENTITY_INSERT [cliente].[cliente] ON 

INSERT [cliente].[cliente] ([id], [personaId], [fecha], [estado]) VALUES (1, 1, CAST(N'2022-07-03' AS Date), 1)
SET IDENTITY_INSERT [cliente].[cliente] OFF
GO
SET IDENTITY_INSERT [cliente].[cliente_contrasenia] ON 

INSERT [cliente].[cliente_contrasenia] ([id], [clienteId], [contrasenia], [estado]) VALUES (1, 1, N'19879999999', 1)
SET IDENTITY_INSERT [cliente].[cliente_contrasenia] OFF
GO
SET IDENTITY_INSERT [financiero].[movimiento] ON 

INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (1, 1, CAST(N'2022-07-03T15:35:35.313' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(-500.000000 AS Decimal(18, 6)), CAST(0.000000 AS Decimal(18, 6)), 1)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (2, 1, CAST(N'2022-07-03T15:36:09.853' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(400.000000 AS Decimal(18, 6)), CAST(400.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (3, 1, CAST(N'2022-07-03T15:36:18.900' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(250.000000 AS Decimal(18, 6)), CAST(650.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (4, 1, CAST(N'2022-07-03T15:36:41.073' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(600.000000 AS Decimal(18, 6)), CAST(1250.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (5, 1, CAST(N'2022-07-03T15:36:54.697' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(-500.000000 AS Decimal(18, 6)), CAST(750.000000 AS Decimal(18, 6)), 1)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (7, 1, CAST(N'2022-07-03T15:43:01.640' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(350.000000 AS Decimal(18, 6)), CAST(1100.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (8, 1, CAST(N'2022-07-03T16:26:56.727' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(350.000000 AS Decimal(18, 6)), CAST(1450.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (9, 2, CAST(N'2022-07-03T16:27:02.450' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(350.000000 AS Decimal(18, 6)), CAST(850.000000 AS Decimal(18, 6)), 0)
INSERT [financiero].[movimiento] ([id], [cuentaId], [fechaProceso], [fecha], [valor], [saldo], [esDebito]) VALUES (10, 2, CAST(N'2022-07-03T16:27:08.710' AS DateTime), CAST(N'2022-07-03' AS Date), CAST(1000.000000 AS Decimal(18, 6)), CAST(1850.000000 AS Decimal(18, 6)), 0)
SET IDENTITY_INSERT [financiero].[movimiento] OFF
GO
INSERT [sujeto].[genero] ([codigo], [descripcion], [estado]) VALUES (N'F', N'FEMENINO', 1)
INSERT [sujeto].[genero] ([codigo], [descripcion], [estado]) VALUES (N'M', N'MASCULINO', 1)
GO
SET IDENTITY_INSERT [sujeto].[persona] ON 

INSERT [sujeto].[persona] ([id], [codigoGenero], [identificacion], [nombre], [fechaNacimiento], [direccion], [telefono]) VALUES (1, N'M', N'1310079072', N'JOSE GREGORIO RIGOBERTO PINTO BARRETO ', CAST(N'1987-03-01' AS Date), N'REINALDO VALDIVIEZO Y GONZALO VALDIVIEZO', N'0981030387')
SET IDENTITY_INSERT [sujeto].[persona] OFF
GO
ALTER TABLE [ahorro].[cuenta]  WITH CHECK ADD  CONSTRAINT [fk_cuenta_cliente] FOREIGN KEY([clienteId])
REFERENCES [cliente].[cliente] ([id])
GO
ALTER TABLE [ahorro].[cuenta] CHECK CONSTRAINT [fk_cuenta_cliente]
GO
ALTER TABLE [ahorro].[cuenta]  WITH CHECK ADD  CONSTRAINT [fk_cuenta_tipoCuenta] FOREIGN KEY([codigoTipoCuenta])
REFERENCES [ahorro].[tipoCuenta] ([codigo])
GO
ALTER TABLE [ahorro].[cuenta] CHECK CONSTRAINT [fk_cuenta_tipoCuenta]
GO
ALTER TABLE [ahorro].[montoMaximoRetiro_tipoCuenta]  WITH CHECK ADD  CONSTRAINT [fk_montoMaximoRetiro_tipoCuenta_tipoCuenta] FOREIGN KEY([codigoTipoCuenta])
REFERENCES [ahorro].[tipoCuenta] ([codigo])
GO
ALTER TABLE [ahorro].[montoMaximoRetiro_tipoCuenta] CHECK CONSTRAINT [fk_montoMaximoRetiro_tipoCuenta_tipoCuenta]
GO
ALTER TABLE [cliente].[cliente]  WITH CHECK ADD  CONSTRAINT [fk_cliente_persona] FOREIGN KEY([personaId])
REFERENCES [sujeto].[persona] ([id])
GO
ALTER TABLE [cliente].[cliente] CHECK CONSTRAINT [fk_cliente_persona]
GO
ALTER TABLE [cliente].[cliente_contrasenia]  WITH CHECK ADD  CONSTRAINT [fk_cliente_contrasenia_cliente] FOREIGN KEY([clienteId])
REFERENCES [cliente].[cliente] ([id])
GO
ALTER TABLE [cliente].[cliente_contrasenia] CHECK CONSTRAINT [fk_cliente_contrasenia_cliente]
GO
ALTER TABLE [financiero].[movimiento]  WITH CHECK ADD  CONSTRAINT [fk_movimiento_cuenta] FOREIGN KEY([cuentaId])
REFERENCES [ahorro].[cuenta] ([id])
GO
ALTER TABLE [financiero].[movimiento] CHECK CONSTRAINT [fk_movimiento_cuenta]
GO
ALTER TABLE [sujeto].[persona]  WITH CHECK ADD  CONSTRAINT [fk_persona_genero] FOREIGN KEY([codigoGenero])
REFERENCES [sujeto].[genero] ([codigo])
GO
ALTER TABLE [sujeto].[persona] CHECK CONSTRAINT [fk_persona_genero]
GO
