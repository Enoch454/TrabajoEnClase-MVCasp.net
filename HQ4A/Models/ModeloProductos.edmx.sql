
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2020 00:17:44
-- Generated from EDMX file: D:\Downloads\UnidadIVPW2020 (1)\UnidadIVPW2020\HQ4A\Models\ModeloProductos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HQ4A];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_prod_prov]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Productos] DROP CONSTRAINT [fk_prod_prov];
GO
IF OBJECT_ID(N'[dbo].[fk_ro_usu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT [fk_ro_usu];
GO
IF OBJECT_ID(N'[dbo].[fk_ven_prod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ventas] DROP CONSTRAINT [fk_ven_prod];
GO
IF OBJECT_ID(N'[dbo].[fk_ven_usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ventas] DROP CONSTRAINT [fk_ven_usuario];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Productos];
GO
IF OBJECT_ID(N'[dbo].[Proveedores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Proveedores];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[Ventas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ventas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Productos'
CREATE TABLE [dbo].[Productos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NombreProducto] varchar(100)  NULL,
    [Descripcion] varchar(500)  NULL,
    [Precio] decimal(18,2)  NULL,
    [Stock] int  NULL,
    [IdProveedor] int  NULL
);
GO

-- Creating table 'Proveedores'
CREATE TABLE [dbo].[Proveedores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NombreProveedor] varchar(100)  NULL,
    [Correo] varchar(200)  NULL,
    [RFC] varchar(20)  NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NULL,
    [Pwd] varchar(100)  NULL,
    [IdRol] int  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rol] varchar(50)  NULL
);
GO

-- Creating table 'Ventas'
CREATE TABLE [dbo].[Ventas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdProducto] int  NULL,
    [IdCliente] int  NULL,
    [Cantidad] int  NULL,
    [Total] decimal(18,2)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [PK_Productos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Proveedores'
ALTER TABLE [dbo].[Proveedores]
ADD CONSTRAINT [PK_Proveedores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ventas'
ALTER TABLE [dbo].[Ventas]
ADD CONSTRAINT [PK_Ventas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdProveedor] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [fk_prod_prov]
    FOREIGN KEY ([IdProveedor])
    REFERENCES [dbo].[Proveedores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_prod_prov'
CREATE INDEX [IX_fk_prod_prov]
ON [dbo].[Productos]
    ([IdProveedor]);
GO

-- Creating foreign key on [IdRol] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [fk_ro_usu]
    FOREIGN KEY ([IdRol])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ro_usu'
CREATE INDEX [IX_fk_ro_usu]
ON [dbo].[Usuarios]
    ([IdRol]);
GO

-- Creating foreign key on [IdProducto] in table 'Ventas'
ALTER TABLE [dbo].[Ventas]
ADD CONSTRAINT [fk_ven_prod]
    FOREIGN KEY ([IdProducto])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ven_prod'
CREATE INDEX [IX_fk_ven_prod]
ON [dbo].[Ventas]
    ([IdProducto]);
GO

-- Creating foreign key on [IdCliente] in table 'Ventas'
ALTER TABLE [dbo].[Ventas]
ADD CONSTRAINT [fk_ven_usuario]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ven_usuario'
CREATE INDEX [IX_fk_ven_usuario]
ON [dbo].[Ventas]
    ([IdCliente]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------