/****** Object:  Database [SisRH]    Script Date: 07/12/2023 13:57:05 ******/
CREATE DATABASE [SisRH]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [SisRH] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [SisRH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SisRH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SisRH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SisRH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SisRH] SET ARITHABORT OFF 
GO
ALTER DATABASE [SisRH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SisRH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SisRH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SisRH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SisRH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SisRH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SisRH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SisRH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SisRH] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [SisRH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SisRH] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SisRH] SET  MULTI_USER 
GO
ALTER DATABASE [SisRH] SET ENCRYPTION ON
GO
ALTER DATABASE [SisRH] SET QUERY_STORE = ON
GO
ALTER DATABASE [SisRH] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  User [leticia.santos429@aluno.unip.br]    Script Date: 07/12/2023 13:57:05 ******/
CREATE USER [leticia.santos429@aluno.unip.br] FROM  EXTERNAL PROVIDER  WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [gabriel.arruda10@aluno.unip.br]    Script Date: 07/12/2023 13:57:05 ******/
CREATE USER [gabriel.arruda10@aluno.unip.br] FROM  EXTERNAL PROVIDER  WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculaINSS]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculaINSS](@salario DECIMAL(10, 2), @horas_trabalhadas DECIMAL(10, 2), @horas_extras DECIMAL(10, 2))
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @total_salario DECIMAL(10, 2);
    SET @total_salario = @salario + @horas_extras;

    -- Lógica simplificada do cálculo de INSS
    RETURN CASE
        WHEN @total_salario <= 3000 THEN 0.1 * @total_salario
        ELSE 0.2 * @total_salario
    END;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[CalculaIRPF]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculaIRPF](@salario DECIMAL(10, 2), @horas_trabalhadas DECIMAL(10, 2), @horas_extras DECIMAL(10, 2))
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @total_salario DECIMAL(10, 2);
    SET @total_salario = @salario + @horas_extras;

    -- Lógica simplificada do cálculo de IRPF
    RETURN CASE
        WHEN @total_salario <= 2000 THEN 0.05 * @total_salario
        ELSE 0.1 * @total_salario
    END;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[CalculaSalarioLiquido]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CalculaSalarioLiquido](@salario DECIMAL(10, 2), @horas_trabalhadas DECIMAL(10, 2), @horas_extras DECIMAL(10, 2))
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @total_salario DECIMAL(10, 2),
	@horas_minimo int = 220,
	@hora_trabalho int = @salario / 220;
    SET @total_salario = @salario + ((@horas_extras*1.50)*@hora_trabalho);

    -- Lógica simplificada do cálculo de IRPF
    RETURN CASE
		
		WHEN @horas_trabalhadas < 220 THEN @total_salario - (select 220-@horas_trabalhadas)*@hora_trabalho
        ELSE @total_salario + (@horas_trabalhadas-220) * @hora_trabalho
    END;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_diagramobjects]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE FUNCTION [dbo].[fn_diagramobjects]() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	
GO
/****** Object:  Table [dbo].[tbFuncionario]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbFuncionario](
	[id_func] [int] IDENTITY(1,1) NOT NULL,
	[primeiro_nm_func] [varchar](20) NOT NULL,
	[sobre_nm_func] [varchar](20) NULL,
	[ultimo_nm_func] [varchar](20) NULL,
	[matricula_func] [int] NOT NULL,
	[dt_nasc_func] [date] NOT NULL,
	[sexo_func] [bit] NOT NULL,
	[raca_func] [varchar](20) NULL,
	[tipo_sanguineo_func] [varchar](15) NULL,
	[nm_mae_func] [varchar](max) NULL,
	[nm_pai_func] [varchar](max) NULL,
	[estado_civil_func] [varchar](20) NOT NULL,
	[nome_conjunge_func] [varchar](max) NULL,
	[cidade_nasc] [varchar](50) NULL,
	[numero_res_func] [int] NOT NULL,
	[compl_func] [varchar](20) NOT NULL,
	[tipo_moradia_func] [varchar](20) NOT NULL,
	[celular_func] [varchar](11) NULL,
	[whatsapp_func] [bit] NULL,
	[telefone_func] [varchar](10) NULL,
	[email_func] [varchar](max) NULL,
	[email_corp_func] [varchar](max) NULL,
	[num_agen_func] [varchar](10) NOT NULL,
	[num_conta_func] [varchar](10) NOT NULL,
	[fk_cargo] [int] NOT NULL,
	[fk_banco] [int] NOT NULL,
	[cpf_func] [varchar](11) NOT NULL,
	[rg_func] [varchar](10) NULL,
	[dt_emissao_func] [date] NULL,
	[orgao_emissor_func] [varchar](10) NULL,
	[reservista_func] [varchar](11) NULL,
	[titulo_eleitor_func] [varchar](12) NULL,
	[zona_eleitor_func] [varchar](3) NULL,
	[sessao_eleitor_func] [varchar](4) NULL,
	[cidade_eleitor_func] [varchar](50) NULL,
	[escolaridade_func] [varchar](20) NOT NULL,
	[cns_func] [varchar](15) NULL,
	[cert_nasc_func] [bit] NULL,
	[cert_casamento_func] [bit] NULL,
	[comprovante_res_func] [varchar](50) NULL,
	[nacionalidade_func] [varchar](50) NULL,
	[st_status_func] [int] NULL,
	[Fk_dep] [int] NULL,
	[fk_cep] [int] NULL,
	[Jornada_func] [varchar](20) NULL,
	[ST_Ativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_func] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cpf_func] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[VwConsultaFuncionario]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VwConsultaFuncionario]
AS 
SELECT 
    primeiro_nm_func,
	sobre_nm_func,
    matricula_func,
    id_func, 
	email_corp_func,
	fk_cargo,
	celular_func
FROM 
    tbFuncionario
WHERE 
    matricula_func > 0;
GO
/****** Object:  Table [dbo].[tbFuncEstrang]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbFuncEstrang](
	[id_funcEstrang] [int] IDENTITY(1,1) NOT NULL,
	[visto_func] [varchar](1) NULL,
	[passaporte_func] [varchar](1) NULL,
	[auttrab_func] [varchar](1) NULL,
	[fk_func] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_funcEstrang] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VwFuncionario]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[VwFuncionario]
AS
SELECT dbo.tbFuncionario.id_func AS Funcionario,
				 
                  dbo.tbFuncionario.primeiro_nm_func		AS primeiro_nm_func		,
				  dbo.tbFuncionario.sobre_nm_func			AS sobre_nm_func		,
				  dbo.tbFuncionario.ultimo_nm_func          AS ultimo_nm_func		, 
                  dbo.tbFuncionario.matricula_func			AS Matricula			, 
				  dbo.tbFuncionario.dt_nasc_func			AS DT_Nascimento		,
				  dbo.tbFuncionario.raca_func				AS Raca					, 
				  dbo.tbFuncionario.sexo_func				AS Sexo					, 
				  dbo.tbFuncionario.tipo_sanguineo_func		AS Tipo_Saguineo		, 
                  dbo.tbFuncionario.nm_mae_func				AS Nm_Mae				, 
				  dbo.tbFuncionario.nm_pai_func				AS Nm_Pai				, 
				  dbo.tbFuncionario.estado_civil_func		AS Estado_Civil			, 
				  dbo.tbFuncionario.nome_conjunge_func		AS Nm_Conjugue			, 
				  dbo.tbFuncionario.cidade_nasc				AS Cidade_Nasci			, 
                  dbo.tbFuncionario.numero_res_func 		AS Num_Residencia		, 
				  dbo.tbFuncionario.compl_func				AS Complemento			,
				  dbo.tbFuncionario.celular_func			AS Celular				, 
				  dbo.tbFuncionario.whatsapp_func			AS whatsapp_func		, 
				  dbo.tbFuncionario.tipo_moradia_func		AS moradia_func			,  
                  dbo.tbFuncionario.telefone_func			AS telefone_func		,
				  dbo.tbFuncionario.email_func				AS email_func			,
				  dbo.tbFuncionario.email_corp_func			AS email_crp_func		, 
				  dbo.tbFuncionario.num_agen_func			AS num_agen_func		, 
				  dbo.tbFuncionario.num_conta_func			AS num_conta_func		, 
				  dbo.tbFuncionario.cpf_func				AS cpf_func				, 
				  dbo.tbFuncionario.rg_func					AS rg_func				, 
				  dbo.tbFuncionario.dt_emissao_func			AS dt_emiss_fuc			, 
                  dbo.tbFuncionario.orgao_emissor_func		AS org_emiss_fuc		, 
				  dbo.tbFuncionario.reservista_func			AS reservista_func		, 
				  dbo.tbFuncionario.titulo_eleitor_func		AS titulo_eleitor_func	,
				  dbo.tbFuncionario.sessao_eleitor_func		AS sessao_eleitor_func  , 
                  dbo.tbFuncionario.zona_eleitor_func		AS zona_eleitor_func    ,
				  dbo.tbFuncionario.cidade_eleitor_func		AS cidade_eleitor_func	, 
				  dbo.tbFuncionario.cns_func				AS cns_func				,
				  dbo.tbFuncionario.cert_nasc_func			AS cert_nasc_func		, 
				  dbo.tbFuncionario.escolaridade_func		AS escolaridade_func	, 
                  dbo.tbFuncionario.cert_casamento_func		AS cert_casamento_func	, 
				  dbo.tbFuncionario.comprovante_res_func	AS comprovante_res_func	,
				  dbo.tbFuncionario.nacionalidade_func		AS nacionalidade_func	,	
				  dbo.tbFuncionario.st_status_func			AS st_status_func		, 
				  dbo.tbFuncionario.Jornada_func			AS Jornada_func			, 
				  dbo.tbFuncEstrang.id_funcEstrang									, 
				  dbo.tbFuncEstrang.visto_func										, 
				  dbo.tbFuncEstrang.passaporte_func									, 
                  dbo.tbFuncEstrang.auttrab_func
FROM			  dbo.tbFuncionario 

INNER JOIN  dbo.tbFuncEstrang ON dbo.tbFuncionario.id_func = dbo.tbFuncEstrang.fk_func
GO
/****** Object:  Table [dbo].[Calendario_Feriados]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendario_Feriados](
	[ID_Calendario] [int] IDENTITY(1,1) NOT NULL,
	[Data_Cal] [date] NULL,
	[Descricao] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Calendario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CEP]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CEP](
	[ID_CEP] [int] IDENTITY(1,1) NOT NULL,
	[CEP] [char](8) NOT NULL,
	[Logradouro] [nvarchar](255) NULL,
	[Cidade] [nvarchar](255) NULL,
	[Bairro] [nvarchar](255) NULL,
	[UF] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_CEP] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FolhaPagamentoCompleta]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolhaPagamentoCompleta](
	[id_fpgc] [int] IDENTITY(1,1) NOT NULL,
	[fk_func] [int] NULL,
	[ano_fpgc] [int] NULL,
	[mes_fpgc] [int] NULL,
	[salario_base] [decimal](10, 2) NULL,
	[horas_trabalhadas] [decimal](10, 2) NULL,
	[horas_extras] [decimal](10, 2) NULL,
	[inss] [decimal](10, 2) NULL,
	[irpf] [decimal](10, 2) NULL,
	[salario_liquido] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_fpgc] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FolhaPagamentoGerada]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolhaPagamentoGerada](
	[id_fpg] [int] IDENTITY(1,1) NOT NULL,
	[fk_func] [int] NULL,
	[ano_fpg] [int] NULL,
	[mes_fpg] [int] NULL,
	[horas_fpg] [int] NULL,
	[extras_fpg] [int] NULL,
	[total_horas] [varchar](20) NULL,
	[total_horas_extras] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_fpg] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbArquivos]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbArquivos](
	[id_arq] [int] IDENTITY(1,1) NOT NULL,
	[caminho_arq] [varchar](max) NULL,
	[tipo_arq] [varchar](5) NULL,
	[tipo_doc_arq] [varchar](20) NULL,
	[st_ativo_arq] [int] NULL,
	[fk_func] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_arq] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbBancos]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbBancos](
	[id_banco] [int] IDENTITY(1,1) NOT NULL,
	[banco] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_banco] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbCargo]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCargo](
	[id_cargo] [int] IDENTITY(1,1) NOT NULL,
	[desc_cargo] [varchar](50) NULL,
	[salario_cargo] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cargo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbDeclaracao]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDeclaracao](
	[id_dec] [int] IDENTITY(1,1) NOT NULL,
	[tipo_dec] [varchar](20) NULL,
	[fk_arq] [int] NULL,
	[dt_in_dec] [date] NULL,
	[dt_fm_dec] [date] NULL,
	[hr_in_dec] [time](7) NULL,
	[hr_fm_dec] [time](7) NULL,
	[desc_dec] [varchar](max) NULL,
	[aut_desc] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_dec] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbDepartamento]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDepartamento](
	[id_dep] [int] IDENTITY(1,1) NOT NULL,
	[nome_dep] [varchar](50) NULL,
	[fk_chefe_dep] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_dep] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbDependente]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDependente](
	[id_dep] [int] IDENTITY(1,1) NOT NULL,
	[nome_dep] [varchar](50) NULL,
	[Cpf_dep] [char](11) NULL,
	[Dt_nasc_dep] [date] NULL,
	[Grau_parent_dep] [varchar](25) NULL,
	[Desconto_salario_dep] [bit] NULL,
	[Gera_Salario_Dep] [bit] NULL,
	[Fk_func] [int] NULL,
	[Pensao_dep] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_dep] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[id_dep] ASC,
	[Cpf_dep] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbFerias]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbFerias](
	[id_ferias] [int] IDENTITY(1,1) NOT NULL,
	[dt_inicio_ferias] [date] NULL,
	[dt_fim_ferias] [date] NULL,
	[dt_agendamento_ferias] [date] NULL,
	[valor_receber_ferias] [decimal](10, 2) NULL,
	[fk_func] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ferias] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbFolhaPonto]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbFolhaPonto](
	[id_fp] [int] IDENTITY(1,1) NOT NULL,
	[fk_func] [int] NULL,
	[dt_apont_fp] [date] NULL,
	[hr_apont_fp_E1] [time](0) NULL,
	[atraso_fp] [bit] NULL,
	[adv_fp] [bit] NULL,
	[desc_adv_fp] [varchar](max) NULL,
	[lat_fp] [varchar](max) NULL,
	[long_fp] [varchar](max) NULL,
	[fk_arq] [int] NULL,
	[AlterarFolha_fp] [int] NOT NULL,
	[hr_apont_fp_S1] [time](0) NULL,
	[hr_apont_fp_E2] [time](0) NULL,
	[hr_apont_fp_S2] [time](0) NULL,
	[hr_apont_fp_E3] [time](0) NULL,
	[hr_apont_fp_S3] [time](0) NULL,
	[Mes_fp] [int] NULL,
	[Ano_fp] [int] NULL,
	[Dia_fp] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_fp] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbHolerite]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbHolerite](
	[id_hol] [int] IDENTITY(1,1) NOT NULL,
	[fk_fp] [int] NULL,
	[beneficios_hol] [decimal](10, 2) NULL,
	[fk_func] [int] NULL,
	[fk_ferias] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_hol] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbLogin]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLogin](
	[id_login] [int] IDENTITY(1,1) NOT NULL,
	[senha_login] [varchar](25) NULL,
	[status_login] [bit] NULL,
	[fk_func] [int] NULL,
	[nivel_acesso_Login] [int] NULL,
	[TrocaSenha] [bit] NULL,
	[nm_funcionario] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_login] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbRescisao]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbRescisao](
	[id_rescisao] [int] IDENTITY(1,1) NOT NULL,
	[dt_rescisao] [date] NULL,
	[valor_rescisao] [decimal](10, 2) NULL,
	[obs_rescisao] [varchar](200) NULL,
	[fk_func] [int] NULL,
	[fk_resp_func] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_rescisao] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbFolhaPonto] ADD  CONSTRAINT [DF_tbFolhaPonto_Mes_fp]  DEFAULT (datepart(month,getdate())) FOR [Mes_fp]
GO
ALTER TABLE [dbo].[tbFolhaPonto] ADD  CONSTRAINT [DF_tbFolhaPonto_Ano_fp]  DEFAULT (datepart(year,getdate())) FOR [Ano_fp]
GO
ALTER TABLE [dbo].[tbFolhaPonto] ADD  CONSTRAINT [DF_tbFolhaPonto_Dia_fp]  DEFAULT (datepart(day,getdate())) FOR [Dia_fp]
GO
ALTER TABLE [dbo].[tbLogin] ADD  DEFAULT ('0000') FOR [senha_login]
GO
ALTER TABLE [dbo].[tbLogin] ADD  DEFAULT ((1)) FOR [status_login]
GO
ALTER TABLE [dbo].[FolhaPagamentoGerada]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbArquivos]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbDeclaracao]  WITH CHECK ADD FOREIGN KEY([fk_arq])
REFERENCES [dbo].[tbArquivos] ([id_arq])
GO
ALTER TABLE [dbo].[tbDependente]  WITH CHECK ADD FOREIGN KEY([Fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbFerias]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbFolhaPonto]  WITH CHECK ADD FOREIGN KEY([fk_arq])
REFERENCES [dbo].[tbArquivos] ([id_arq])
GO
ALTER TABLE [dbo].[tbFolhaPonto]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbFuncEstrang]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbFuncionario]  WITH CHECK ADD FOREIGN KEY([fk_banco])
REFERENCES [dbo].[tbBancos] ([id_banco])
GO
ALTER TABLE [dbo].[tbFuncionario]  WITH CHECK ADD FOREIGN KEY([fk_cargo])
REFERENCES [dbo].[tbCargo] ([id_cargo])
GO
ALTER TABLE [dbo].[tbFuncionario]  WITH CHECK ADD FOREIGN KEY([fk_cep])
REFERENCES [dbo].[CEP] ([ID_CEP])
GO
ALTER TABLE [dbo].[tbFuncionario]  WITH CHECK ADD FOREIGN KEY([Fk_dep])
REFERENCES [dbo].[tbDepartamento] ([id_dep])
GO
ALTER TABLE [dbo].[tbHolerite]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbHolerite]  WITH CHECK ADD FOREIGN KEY([fk_ferias])
REFERENCES [dbo].[tbFerias] ([id_ferias])
GO
ALTER TABLE [dbo].[tbHolerite]  WITH CHECK ADD FOREIGN KEY([fk_fp])
REFERENCES [dbo].[tbFolhaPonto] ([id_fp])
GO
ALTER TABLE [dbo].[tbLogin]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbRescisao]  WITH CHECK ADD FOREIGN KEY([fk_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
ALTER TABLE [dbo].[tbRescisao]  WITH CHECK ADD FOREIGN KEY([fk_resp_func])
REFERENCES [dbo].[tbFuncionario] ([id_func])
GO
/****** Object:  StoredProcedure [dbo].[AbrirFolhaPonto]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[AbrirFolhaPonto]

@mes int,
@ano int
as
begin
Update tbFolhaPonto set AlterarFolha_fp = 1 where mes_fp = @mes and ano_fp = @ano
end
GO
/****** Object:  StoredProcedure [dbo].[AlterarSenha]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AlterarSenha]
@matricula int,
@senha varchar(50)

as
begin

Update tbLogin set senha_login = @senha, TrocaSenha = 0
where fk_func = (select id_func from tbFuncionario where matricula_func = @matricula and st_ativo = 1)
and status_login = 1
end
GO
/****** Object:  StoredProcedure [dbo].[ApontarFP]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApontarFP]
    @DIAFP INT,
    @MESFP INT,
    @ANOFP INT,
    @APONT TIME,
    @ID_APONT INT
AS 
BEGIN
    DECLARE
        @DIA INT = @DIAFP,
        @MES INT = @MESFP,
        @ANO INT = @ANOFP

    DECLARE
        @E1 TIME = NULL,
        @S1 TIME = NULL,
        @E2 TIME = NULL,
        @S2 TIME = NULL,
        @E3 TIME = NULL,
        @S3 TIME = NULL

    SELECT 
        @E1 = HR_APONT_FP_E1,
        @S1 = HR_APONT_FP_S1,
        @E2 = HR_APONT_FP_E2,
        @S2 = HR_APONT_FP_S2,
        @E3 = HR_APONT_FP_E3,
        @S3 = HR_APONT_FP_S3
    FROM TBFOLHAPONTO
    WHERE ID_FP = @ID_APONT
    AND MES_FP = @MES
    AND ANO_FP = @ANO
    AND DIA_FP = @DIA

    -- Atualizar apenas o primeiro campo vazio
    UPDATE TBFOLHAPONTO
    SET
        HR_APONT_FP_E1 = CASE WHEN HR_APONT_FP_E1 IS NULL THEN @APONT ELSE HR_APONT_FP_E1 END,
        HR_APONT_FP_S1 = CASE WHEN HR_APONT_FP_S1 IS NULL AND HR_APONT_FP_E1 IS NOT NULL THEN @APONT ELSE HR_APONT_FP_S1 END,
        HR_APONT_FP_E2 = CASE WHEN HR_APONT_FP_E2 IS NULL AND HR_APONT_FP_S1 IS NOT NULL THEN @APONT ELSE HR_APONT_FP_E2 END,
        HR_APONT_FP_S2 = CASE WHEN HR_APONT_FP_S2 IS NULL AND HR_APONT_FP_E2 IS NOT NULL THEN @APONT ELSE HR_APONT_FP_S2 END,
        HR_APONT_FP_E3 = CASE WHEN HR_APONT_FP_E3 IS NULL AND HR_APONT_FP_S2 IS NOT NULL THEN @APONT ELSE HR_APONT_FP_E3 END,
        HR_APONT_FP_S3 = CASE WHEN HR_APONT_FP_S3 IS NULL AND HR_APONT_FP_E3 IS NOT NULL THEN @APONT ELSE HR_APONT_FP_S3 END
    WHERE ID_FP = @ID_APONT
    AND MES_FP = @MES
    AND ANO_FP = @ANO
    AND DIA_FP = @DIA
END
GO
/****** Object:  StoredProcedure [dbo].[AtivarUsuario]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AtivarUsuario]
@matricula int
as
begin
Update tbLogin set status_login = 1 where fk_func = (select 
id_func from tbFuncionario where matricula_func = @matricula and st_ativo = 1)
and status_login = 0
end
GO
/****** Object:  StoredProcedure [dbo].[AtualizarFolhaPonto]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[AtualizarFolhaPonto]
@id int,
@e1 time = null,
@e2 time = null,
@e3 time = null,
@s1 time = null,
@s2 time = null,
@s3 time = null,
@obs nvarchar(max) = null
as
begin
update tbFolhaPonto set
hr_apont_fp_e1 = @e1,
hr_apont_fp_e2 = @e2,
hr_apont_fp_e3 = @e3,
hr_apont_fp_s1 = @s1,
hr_apont_fp_s2 = @s2,
hr_apont_fp_s3 = @s3,
desc_adv_fp = @obs
where id_fp = @id
end








GO
/****** Object:  StoredProcedure [dbo].[CadastrarFunc]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[CadastrarFunc]
@primeiro_nm_func varchar(20)
,@sobre_nm_func varchar(20)
,@ultimo_nm_func varchar(20)
,@matricula_func int
,@dt_nasc_func date
,@sexo_func bit
,@raca_func varchar(20)
,@tipo_sanguineo_func varchar(15)
,@nm_mae_func varchar(20)
,@nm_pai_func varchar(max)
,@estado_civil_func varchar(20)
,@nome_conjunge_func varchar(max)
,@cidade_nasc varchar(50)
,@numero_res_func int
,@compl_func varchar(20)
,@tipo_moradia_func varchar(20)
,@celular_func varchar(11)
,@whatsapp_func bit
,@telefone_func varchar(10)
,@email_func varchar(max)
,@email_corp_func varchar(max)
,@num_agen_func varchar(10)
,@num_conta_func varchar(10)
,@fk_cargo int
,@fk_banco int
,@cpf_func varchar(11)
,@rg_func varchar(10)
,@dt_emissao_func date
,@orgao_emissor_func varchar(10)
,@reservista_func varchar(11)
,@titulo_eleitor_func varchar(12)
,@zona_eleitor_func varchar(3)
,@sessao_eleitor_func varchar(4)
,@cidade_eleitor_func varchar(50)
,@escolaridade_func varchar(20)
,@cns_func varchar(15)
,@cert_nasc_func bit
,@cert_casamento_func bit
,@comprovante_res_func varchar(50)
,@nacionalidade_func varchar(50)
,@st_status_func int
,@Fk_dep int
,@fk_cep int
,@Jornada_func varchar(20)
,@ST_Ativo bit
as
begin
INSERT INTO SisRH.dbo.tbFuncionario
           ([primeiro_nm_func]
           ,[sobre_nm_func]
           ,[ultimo_nm_func]
           ,[matricula_func]
           ,[dt_nasc_func]
           ,[sexo_func]
           ,[raca_func]
           ,[tipo_sanguineo_func]
           ,[nm_mae_func]
           ,[nm_pai_func]
           ,[estado_civil_func]
           ,[nome_conjunge_func]
           ,[cidade_nasc]
           ,[numero_res_func]
           ,[compl_func]
           ,[tipo_moradia_func]
           ,[celular_func]
           ,[whatsapp_func]
           ,[telefone_func]
           ,[email_func]
           ,[email_corp_func]
           ,[num_agen_func]
           ,[num_conta_func]
           ,[fk_cargo]
           ,[fk_banco]
           ,[cpf_func]
           ,[rg_func]
           ,[dt_emissao_func]
           ,[orgao_emissor_func]
           ,[reservista_func]
           ,[titulo_eleitor_func]
           ,[zona_eleitor_func]
           ,[sessao_eleitor_func]
           ,[cidade_eleitor_func]
           ,[escolaridade_func]
           ,[cns_func]
           ,[cert_nasc_func]
           ,[cert_casamento_func]
           ,[comprovante_res_func]
           ,[nacionalidade_func]
           ,[st_status_func]
           ,[Fk_dep]
           ,[fk_cep]
           ,[Jornada_func]
           ,[ST_Ativo])
     VALUES
           (@primeiro_nm_func
           ,@sobre_nm_func
           ,@ultimo_nm_func
           ,@matricula_func
           ,@dt_nasc_func
           ,@sexo_func
           ,@raca_func
           ,@tipo_sanguineo_func
           ,@nm_mae_func
           ,@nm_pai_func
           ,@estado_civil_func
           ,@nome_conjunge_func
           ,@cidade_nasc
           ,@numero_res_func
           ,@compl_func
           ,@tipo_moradia_func
           ,@celular_func
           ,@whatsapp_func
           ,@telefone_func
           ,@email_func
           ,@email_corp_func
           ,@num_agen_func
           ,@num_conta_func
           ,@fk_cargo
           ,@fk_banco
           ,@cpf_func
           ,@rg_func
           ,@dt_emissao_func
           ,@orgao_emissor_func
           ,@reservista_func
           ,@titulo_eleitor_func
           ,@zona_eleitor_func
           ,@sessao_eleitor_func
           ,@cidade_eleitor_func
           ,@escolaridade_func
           ,@cns_func
           ,@cert_nasc_func
           ,@cert_casamento_func
           ,@comprovante_res_func
           ,@nacionalidade_func
           ,@st_status_func
           ,@Fk_dep
           ,@fk_cep
           ,@Jornada_func
           ,@ST_Ativo)
end
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFolhaPonto]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ConsultarFolhaPonto]
@prof int = null,
@mes int = null,
@dia int = null,
@ano int = null
as 
begin
SELECT 
	f.id_func as ID,
	f.matricula_func as Matricula,
    CONCAT(f.primeiro_nm_func, ' ', f.sobre_nm_func, ' ', f.ultimo_nm_func) AS Nome,
    fp.Dt_Apont_fp AS Data1,
    fp.hr_apont_fp_E1 AS E1,
    fp.hr_apont_fp_s1 AS S1,
    fp.hr_apont_fp_E2 AS E2,
    fp.hr_apont_fp_s2 AS S2,
    fp.hr_apont_fp_E3 AS E3,
    fp.hr_apont_fp_s3 AS S3,
    fp.adv_fp AS Observacao,
    CONCAT(
        CAST(horas_minutos_temp.total_minutos_trabalhados / 60 AS VARCHAR(2)),
        'h ',
        CAST(horas_minutos_temp.total_minutos_trabalhados % 60 AS VARCHAR(2)),
        'min'
    ) AS horas_trabalhadas,
    CONCAT(
        CAST(horas_minutos_temp.minutos_extras / 60 AS VARCHAR(2)),
        'h ',
        CAST(horas_minutos_temp.minutos_extras % 60 AS VARCHAR(2)),
        'min'
    ) AS horas_extras
FROM (
    SELECT
        fk_func,
        dt_apont_fp,
        SUM(
            CASE
                WHEN hr_apont_fp_E1 IS NOT NULL AND hr_apont_fp_s1 IS NOT NULL THEN
                    CASE
                        WHEN DATEDIFF(MINUTE, hr_apont_fp_E1, hr_apont_fp_s1) > 480 THEN 480
                        ELSE DATEDIFF(MINUTE, hr_apont_fp_E1, hr_apont_fp_s1)
                    END
                ELSE 0
            END
            +
            CASE
                WHEN hr_apont_fp_E2 IS NOT NULL AND hr_apont_fp_s2 IS NOT NULL THEN
                    CASE
                        WHEN DATEDIFF(MINUTE, hr_apont_fp_E2, hr_apont_fp_s2) > 480 THEN 480
                        ELSE DATEDIFF(MINUTE, hr_apont_fp_E2, hr_apont_fp_s2)
                    END
                ELSE 0
            END
        ) AS total_minutos_trabalhados,
         SUM(
                CASE
                    WHEN hr_apont_fp_E1 IS NOT NULL AND hr_apont_fp_S1 IS NOT NULL THEN
                        CASE
                            WHEN DATEDIFF(MINUTE, hr_apont_fp_E1, hr_apont_fp_S1) > 480 THEN DATEDIFF(MINUTE, hr_apont_fp_E1, hr_apont_fp_S1) - 480
                            ELSE 0
                        END
                    ELSE 0
                END
                +
                CASE
                    WHEN hr_apont_fp_E2 IS NOT NULL AND hr_apont_fp_S2 IS NOT NULL THEN
                        CASE
                            WHEN DATEDIFF(MINUTE, hr_apont_fp_E2, hr_apont_fp_S2) > 480 THEN DATEDIFF(MINUTE, hr_apont_fp_E2, hr_apont_fp_S2) - 480
                            ELSE 0
                        END
                    ELSE 0
                END
                +
                CASE
                    WHEN hr_apont_fp_E3 IS NOT NULL AND hr_apont_fp_S3 IS NOT NULL THEN
                        CASE
                            WHEN DATEDIFF(MINUTE, hr_apont_fp_E3, hr_apont_fp_S3) < 480 THEN DATEDIFF(MINUTE, hr_apont_fp_E3, hr_apont_fp_S3)
                            ELSE 0
                        END
                    ELSE 0
                END
            ) AS minutos_extras
    FROM tbFolhaPonto WITH (NOLOCK)
    GROUP BY fk_func, dt_apont_fp
) AS horas_minutos_temp
INNER JOIN tbfuncionario f WITH (NOLOCK) ON f.id_func = horas_minutos_temp.fk_func
INNER JOIN tbFolhaPonto fp WITH (NOLOCK) ON fp.fk_func = horas_minutos_temp.fk_func AND fp.dt_apont_fp = horas_minutos_temp.dt_apont_fp
WHERE 
(f.matricula_func = @prof or @prof = -1)
and(fp.Mes_fp = @mes or @mes = -1)
and(fp.Ano_fp = @ano or @ano = -1)
and(fp.Dia_fp = @dia or @dia = -1)
end



GO
/****** Object:  StoredProcedure [dbo].[CriarFolhaPontoDiaria]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CriarFolhaPontoDiaria]
@matricula int,
@mes int,
@ano int,
@dia int
as begin

Insert into tbFolhaPonto values((select id_func from tbFuncionario where matricula_func = @matricula),GetDate(),null,0,null,null,null,null,null,0,null,null,null,null,null,@mes,@ano,@dia)

end
GO
/****** Object:  StoredProcedure [dbo].[ExcluirUsuario]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ExcluirUsuario]
@matricula int
as
begin
Update tbLogin set status_login = 0 where fk_func = (select 
id_func from tbFuncionario where matricula_func = @matricula)
end
GO
/****** Object:  StoredProcedure [dbo].[FecharFolhaPonto]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[FecharFolhaPonto]

@mes int,
@ano int
as
begin
Update tbFolhaPonto set AlterarFolha_fp = 0 where mes_fp = @mes and ano_fp = @ano
end
GO
/****** Object:  StoredProcedure [dbo].[GerarFolhaPagamento]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GerarFolhaPagamento](@ano INT, @mes INT)
AS
BEGIN
    INSERT INTO FolhaPagamentoCompleta(fk_func, ano_fpg, mes_fpg, salario_base, horas_trabalhadas, horas_extras, inss, irpf, salario_liquido)
    SELECT
        f.id_func,
        @ano,
        @mes,
        c.salario_cargo,
        ISNULL(SUM(fpg.horas_fpg), 0) AS horas_trabalhadas,
        ISNULL(SUM(fpg.extras_fpg), 0) AS horas_extras,
        -- Cálculo simplificado de INSS e IRPF
        CASE 
            WHEN ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0) <= 3000 THEN 0.1 * (ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0))
            ELSE 0.2 * (ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0))
        END AS inss,
        CASE 
            WHEN ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0) <= 2000 THEN 0.05 * (ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0))
            ELSE 0.1 * (ISNULL(SUM(fpg.horas_fpg), 0) + ISNULL(SUM(fpg.extras_fpg), 0))
        END AS irpf,
        0 -- Adicione aqui outros descontos ou benefícios
    FROM
        tbFuncionario f
        INNER JOIN tbCargo c ON f.fk_cargo = c.id_cargo
        LEFT JOIN FolhaPontoGerada fpg ON fpg.fk_func = f.id_func AND YEAR(fpg.dt_apont_fp) = @ano AND MONTH(fpg.dt_apont_fp) = @mes
    GROUP BY
        f.id_func, c.salario_cargo;
END;
GO
/****** Object:  StoredProcedure [dbo].[GerarFolhaPagamentov2]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GerarFolhaPagamentov2]
    @ano INT,
    @mes INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN InserirNaTabela;

        INSERT INTO FolhaPagamentoCompleta (fk_func, ano_fpgc, mes_fpgc, salario_base ,horas_trabalhadas, horas_extras, inss, irpf, salario_liquido)
        SELECT
            fk_func,
            @ano AS ano,
            @mes AS mes,
			c.salario_cargo as Salario,
            cast(total_minutos_trabalhados / 60  AS INT)AS Horas,
            cast(total_minutos_extras / 60 as INT) AS Extras,
			dbo.CalculaINSS(c.salario_cargo,total_minutos_trabalhados,total_minutos_extras) as INSS,
			dbo.CalculaIRPF(c.salario_cargo,total_minutos_trabalhados,total_minutos_extras) as IRPF,
			CAST(
    dbo.CalculaSalarioLiquido(c.salario_cargo, total_minutos_trabalhados, total_minutos_extras) -
    dbo.CalculaINSS(c.salario_cargo, total_minutos_trabalhados, total_minutos_extras) -
    dbo.CalculaIRPF(c.salario_cargo, total_minutos_trabalhados, total_minutos_extras)
AS DECIMAL(10, 2))
			as Salario_Bruto
        FROM (
            SELECT
                fk_func,
                DATEPART(YEAR, dt_apont_fp) AS ano,
                DATEPART(MONTH, dt_apont_fp) AS mes,
                SUM(total_minutos_trabalhados) AS total_minutos_trabalhados,
                SUM(minutos_extras) AS total_minutos_extras
            FROM (
                SELECT
                    fk_func,
                    dt_apont_fp,
                    SUM(
                        CASE
                            WHEN hr_apont_fp_E1 IS NOT NULL AND hr_apont_fp_S1 IS NOT NULL THEN DATEDIFF(MINUTE, hr_apont_fp_E1, hr_apont_fp_S1)
                            ELSE 0
                        END
                        +
                        CASE
                            WHEN hr_apont_fp_E2 IS NOT NULL AND hr_apont_fp_S2 IS NOT NULL THEN DATEDIFF(MINUTE, hr_apont_fp_E2, hr_apont_fp_S2)
                            ELSE 0
                        END
                    ) AS total_minutos_trabalhados,
                    SUM(
                        CASE
                            WHEN hr_apont_fp_E3 IS NOT NULL AND hr_apont_fp_S3 IS NOT NULL THEN
                                CASE
                                    WHEN DATEDIFF(MINUTE, hr_apont_fp_E3, hr_apont_fp_S3) > 480 THEN 480
                                    ELSE DATEDIFF(MINUTE, hr_apont_fp_E3, hr_apont_fp_S3)
                                END
                            ELSE 0
                        END
                    ) AS minutos_extras
                FROM
                    tbFolhaPonto fp
                WHERE
                    fp.Ano_fp = @ano
                    and fp.Mes_fp = @mes
					AND fp.AlterarFolha_fp = 0
                GROUP BY
                    fk_func, dt_apont_fp
            ) AS horas_minutos_temp
            GROUP BY
                fk_func, DATEPART(YEAR, dt_apont_fp), DATEPART(MONTH, dt_apont_fp)
        ) AS horas_minutos
        INNER JOIN tbFuncionario f ON f.id_func = fk_func
		INNER JOIN tbCargo c on c.id_cargo = f.fk_cargo

        COMMIT TRAN InserirNaTabela;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN InserirNaTabela;
        -- Lidar com erros, registrar ou retornar uma mensagem de erro, se necessário
        PRINT ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[LiberarTrocaSenha]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LiberarTrocaSenha]
@matricula int
as
begin
Update tbLogin set TrocaSenha = 1 where fk_func = (select 
id_func from tbFuncionario where matricula_func = @matricula and st_ativo = 1)
and status_login = 1
end
GO
/****** Object:  StoredProcedure [dbo].[ListarFolhaPagamento]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ListarFolhaPagamento]
@mes int = null,
@ano int = null,
@matricula int = null

as 
begin

SELECT 
CONCAT(F.primeiro_nm_func,' ',F.sobre_nm_func,' ',F.ultimo_nm_func) as Nome,
f.matricula_func as Matricula,
f.email_func as Email,
ano_fpgc as Ano,
mes_fpgc as Mes,
salario_base as Salario_Base,
horas_trabalhadas as Horas_Trabalhadas,
horas_extras as Horas_Extras,
inss as INSS,
irpf as IRPF,
salario_liquido as Salario_Liquido

FROM FolhaPagamentoCompleta fpg
inner join tbFuncionario f on f.id_func = fpg.fk_func
WHERE (fpg.ano_fpgc = @ano or @ano = -1) and
(fpg.mes_fpgc = @mes or @mes = -1) and
(f.matricula_func = @matricula or @matricula = -1)

end
GO
/****** Object:  StoredProcedure [dbo].[ListarFunc_Select]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[ListarFunc_Select]

@matricula int, 
@nome varchar(50) ,
@cpf varchar(11), 
@nascimento date = null,
@nascimentoInt int, 
@cargo int, 
@dep int,
@jornada varchar(20) 

as
begin
select 
Matricula_func,
concat(primeiro_nm_func, ' ' ,sobre_nm_func, ' ' ,ultimo_nm_func) as Nome,
dt_nasc_func as Data_Nascimento,
case when sexo_func = 1 then 'Masculino'
else 'Feminino' end Sexo,
raca_func as Raca,
estado_civil_func as Estado_Civil,
Tipo_sanguineo_func as Tipo_Sanguineo,
nm_mae_func as Nome_Mae,
nm_pai_func as Nome_Pai,
nome_conjunge_func as Conjunge,
cep.cep as CEP,
cep.Logradouro as Logradouro,
cep.Cidade as Cidade,
cep.Bairro as Bairro,
cep.UF AS UF,
func.Compl_func as Complemento,
tipo_moradia_func as Tipo_Moradia,
celular_func as Celular,
case when whatsapp_func = 1 then 'Sim'
else 'Não' end Whatsapp,
telefone_func as Telefone,
email_func as Email,
email_corp_func as Email_Corp,
num_Agen_func as Agencia,
num_conta_func as Conta,
ban.banco as Banco,
car.desc_cargo as Cargo,
dep.nome_dep as Departamento,
Jornada_func as Jornada,
cpf_func as CPF,
rg_func as RG,
dt_emissao_func as Data_Emissao,
orgao_emissor_func as Orgao_Emissor,
reservista_func as Reservista,
titulo_eleitor_func as Titulo_Eleitor,
zona_eleitor_func as Zona_Eleitoral,
sessao_eleitor_func as Cidade_Eleitoral,
escolaridade_func as Escolaridade,
cns_func as CNS,
case when cert_casamento_func = 1 then 'Sim'
else 'Não' end Certidao_Casamento,
case when cert_nasc_func = 1 then 'Sim'
else 'Não' end Certidao_Nascimento,
comprovante_res_func as Comprov_Residencia,
nacionalidade_func as Nacionalidade,
case when st_status_func = 1 then 'Ativo' 
when st_status_func = 2 then 'Ferias'
when st_status_func = 3 then 'Afastado'
else 'Inativo' end Situacao

from tbFuncionario func
inner join CEP cep with(nolock)
on cep.id_Cep = func.fk_cep
inner join tbBancos ban with(nolock)
on ban.id_banco = func.fk_banco
inner join tbCargo car with(nolock)
on car.id_cargo = func.fk_cargo
inner join tbDepartamento dep with(nolock)
on dep.id_dep = func.fk_dep

where (func.matricula_func = @matricula or @matricula = -1)
and (func.primeiro_nm_func like '%' + @nome + '%' or @nome = '-1')
and (func.cpf_func = @cpf or @cpf = '-1')
and (func.dt_nasc_func = @nascimento or @nascimentoInt = -1)
and (func.fk_cargo = @cargo or @cargo = -1)
and (func.fk_dep = @dep or @dep = -1)
and (func.Jornada_func = @jornada or @jornada = '-1')

end
GO
/****** Object:  StoredProcedure [dbo].[ListarUltimaFP]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ListarUltimaFP]
@MATRICULA INT
as begin
SELECT MAX(ID_FP) FROM tbfolhaponto
where fk_func = (select id_func from tbfuncionario
where matricula_func = @matricula)
end
GO
/****** Object:  StoredProcedure [dbo].[ListaUusarios]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ListaUusarios]
@matricula int
as
begin
select f.matricula_func AS Matricula,
Concat(f.primeiro_nm_func,' ',f.sobre_nm_func,' ',ultimo_nm_func) as Nome,
l.nivel_acesso_login as Nivel_Acesso,
TrocaSenha,
status_login as Status
from tbLogin l
inner join tbFuncionario f with(nolock)
on f.id_func = l.fk_func
where (f.matricula_func = @matricula or @matricula = -1)
end
GO
/****** Object:  StoredProcedure [dbo].[Logar_select]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Logar_select]

@matricula int,
@senha varchar(50)
as
begin
select f.matricula_func,l.senha_login,l.nivel_acesso_login,TrocaSenha from tbLogin l
inner join tbFuncionario f with(nolock)
on f.id_func = l.fk_func
where f.matricula_func = @matricula and
l.senha_login = @senha and
status_login = 1
end




GO
/****** Object:  StoredProcedure [dbo].[sp_alterdiagram]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_alterdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_creatediagram]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_creatediagram]
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_dropdiagram]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_dropdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagramdefinition]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagramdefinition]
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagrams]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagrams]
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_renamediagram]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_renamediagram]
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_upgraddiagrams]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_upgraddiagrams]
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[VerficarFPDiaria]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VerficarFPDiaria]
    @matricula INT,
	@dia int,
	@mes int,
	@ano int
AS
BEGIN
    SELECT *
    FROM tbFolhaPonto
    WHERE fk_func = (SELECT id_func FROM tbFuncionario WHERE matricula_func = @matricula)and
	ano_fp = @ano and
	dia_fp = @dia and
	mes_fp = @mes
        
END;

GO
/****** Object:  StoredProcedure [dbo].[VerificaTrocaSenha]    Script Date: 07/12/2023 13:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[VerificaTrocaSenha]
@matricula int,
@senha varchar(50)
as
begin
select f.matricula_func,l.senha_login,l.nivel_acesso_login,TrocaSenha from tbLogin l
inner join tbFuncionario f with(nolock)
on f.id_func = l.fk_func
where f.matricula_func = @matricula and
l.senha_login = @senha and
l.TrocaSenha = 1
end
GO
EXEC sys.sp_addextendedproperty @name=N'microsoft_database_tools_support', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sysdiagrams'
GO
ALTER DATABASE [SisRH] SET  READ_WRITE 
GO
