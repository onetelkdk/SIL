--SQL SCRIPT: Created by Gregorio Ramírez [Development - MaxApp]
--Tables: mAdmLeg, dComMbr, mIniIni
--Dependencies: mAdmCat, dAdmEst, dAdmDst, mAdmPar, mAdmFun, rFunUsr

/********************************** TABLES **********************************/
 
/********************************** TABLES **********************************/


/********************************** SEQUENCES **********************************/

/********************************** SEQUENCES **********************************/


/********************************** QUERIES **********************************/

--Períodos electos
SELECT AdmpcoCodigo, AdmpcoDescripcion, AdmPcoUserdata, AdmpcoDesde, AdmpcoHasta, AdmstsCodigo, AdmpcoUsuario, AdmpcoFechaReg
FROM mAdmPco

--Municipios
SELECT Municipios.AdmMunCodigo, Municipios.AdmMunDescripcion, Municipios.AdmMunFechaReg, Municipios.AdmMunUsuario, Provincias.AdmPrvCodigo, Provincias.AdmPrvDescripcion, Provincias.AdmPrvUserdata, Provincias.AdmPrvUsuario
FROM mAdmMun AS Municipios INNER JOIN mAdmPrv AS Provincias ON Municipios.AdmMunCodigo = Provincias.AdmPrvCodigo

/********************************** QUERIES **********************************/


/********************************** PROCEDURES **********************************/
ALTER PROCEDURE Spc_mAdmLeg_Put(
	@IsDelete BIT = 0, 
	@AdmLegCodigo INT,
	@AdmLegTipoId INT, 
	@AdmLegCedula VARCHAR(11), 
	@AdmlegNombres VARCHAR(50), 
	@AdmlegApellido1 VARCHAR(50), 
	@AdmlegApellido2 VARCHAR(50), 
	@AdmlegSexo VARCHAR(15), 
	@AdmFunCodigo INT, 
	@AdmProProvincia SMALLINT, 
	@AdmlegFoto VARCHAR(200), 
	@AdmLegHuella VARCHAR(200), 
	@AdmPdoCodigo INT, 
	@AdmPcoCodigo SMALLINT, 
	@AdmLegProfesion VARCHAR(MAX), 
	@AdmLegFechaNac DATE, 
	@AdmLegDireccion VARCHAR(255), 
	@AdmPrvCodigo INT, 
	@AdmMunCodigo INT, 
	@AdmSecCodigo INT, 
	@AdmLegCelular VARCHAR(12), 
	@AdmLegTelefonoSenado VARCHAR(12), 
	@AdmLegTelefonoProvincial VARCHAR(12), 
	@AdmLegCorreo VARCHAR(75), 
	@AdmLegApartadoPostal VARCHAR(10), 
	@AdmLegFax VARCHAR(12), 
	@AdmLegTwitter VARCHAR(75), 
	@AdmLegSitioWeb VARCHAR(75), 
	@AdmLegLinkedlin VARCHAR(75), 
	@AdmLegAreasInteres VARCHAR(MAX), 
	@AdmLegPrioridad SMALLINT, 
	@ExpExpNumero INT, 
	@AdmLegUserdata INT, 
	@AdmEstCodigo INT, 
	@AdmLegUsuario VARCHAR(25)
)
AS SET NOCOUNT ON;

--Remover máscara y espacios
SET @AdmLegCedula = ISNULL(REPLACE(@AdmLegCedula,'-',''),'');

	BEGIN
		BEGIN TRAN
			IF @IsDelete = 1
			BEGIN
				DELETE
				FROM mAdmLeg
				WHERE AdmLegCodigo = @AdmLegCodigo
			END

			IF @AdmLegCodigo = 0
			BEGIN
				INSERT INTO mAdmLeg(AdmLegTipoId,AdmLegCedula,AdmlegNombres,AdmlegApellido1,AdmlegApellido2,AdmlegSexo,AdmFunCodigo,AdmProProvincia,AdmlegFoto,AdmLegHuella,AdmPdoCodigo,AdmPcoCodigo,AdmLegProfesion,AdmLegFechaNac,AdmLegDireccion,AdmPrvCodigo,AdmMunCodigo,AdmSecCodigo,AdmLegCelular,AdmLegTelefonoSenado,AdmLegTelefonoProvincial,AdmLegCorreo,AdmLegApartadoPostal,AdmLegFax,AdmLegTwitter,AdmLegSitioWeb,AdmLegLinkedlin,AdmLegAreasInteres,AdmLegPrioridad,ExpExpNumero,AdmLegUserdata,AdmEstCodigo,AdmLegUsuario,AdmLegFechaReg)
				VALUES(ISNULL(@AdmLegTipoId,''),ISNULL(@AdmLegCedula,''),@AdmlegNombres,@AdmlegApellido1,@AdmlegApellido2,@AdmlegSexo,@AdmFunCodigo,@AdmProProvincia,@AdmlegFoto,@AdmLegHuella,@AdmPdoCodigo,@AdmPcoCodigo,@AdmLegProfesion,@AdmLegFechaNac,@AdmLegDireccion,@AdmPrvCodigo,@AdmMunCodigo,@AdmSecCodigo,@AdmLegCelular,@AdmLegTelefonoSenado,@AdmLegTelefonoProvincial,@AdmLegCorreo,@AdmLegApartadoPostal,@AdmLegFax,@AdmLegTwitter,@AdmLegSitioWeb,@AdmLegLinkedlin,@AdmLegAreasInteres,@AdmLegPrioridad,@ExpExpNumero,@AdmLegUserdata,@AdmEstCodigo,@AdmLegUsuario,DEFAULT)
			END
			ELSE
			BEGIN
				UPDATE mAdmLeg
				SET AdmLegTipoId = ISNULL(@AdmLegTipoId,''),
				AdmLegCedula = ISNULL(@AdmLegCedula,''),
				AdmlegNombres = @AdmlegNombres,
				AdmlegApellido1 = @AdmlegApellido1,
				AdmlegApellido2 = @AdmlegApellido2,
				AdmlegSexo = @AdmlegSexo,
				AdmFunCodigo = @AdmFunCodigo,
				AdmProProvincia = @AdmProProvincia,
				AdmlegFoto = @AdmlegFoto,
				AdmLegHuella = @AdmLegHuella,
				AdmPdoCodigo = @AdmPdoCodigo,
				AdmPcoCodigo = @AdmPcoCodigo ,
				AdmLegProfesion = @AdmLegProfesion,
				AdmLegFechaNac = @AdmLegFechaNac,
				AdmLegDireccion = @AdmLegDireccion,
				AdmPrvCodigo = @AdmPrvCodigo,
				AdmMunCodigo = @AdmMunCodigo,
				AdmSecCodigo = @AdmSecCodigo,
				AdmLegCelular = @AdmLegCelular,
				AdmLegTelefonoSenado = @AdmLegTelefonoSenado,
				AdmLegTelefonoProvincial = @AdmLegTelefonoProvincial,
				AdmLegCorreo = @AdmLegCorreo,
				AdmLegApartadoPostal = @AdmLegApartadoPostal,
				AdmLegFax = @AdmLegFax,
				AdmLegTwitter = @AdmLegTwitter,
				AdmLegSitioWeb = @AdmLegSitioWeb,
				AdmLegLinkedlin = @AdmLegLinkedlin,
				AdmLegAreasInteres = @AdmLegAreasInteres,
				AdmLegPrioridad = @AdmLegPrioridad,
				ExpExpNumero = @ExpExpNumero,
				AdmLegUserdata = @AdmLegUserdata,
				AdmEstCodigo = @AdmEstCodigo,
				AdmLegUsuario = @AdmLegUsuario
				WHERE AdmLegCodigo = @AdmLegCodigo
			END
		COMMIT TRAN
	END
GO
--

ALTER PROCEDURE Spc_mAdmPar_Get(@AdmParCodigo VARCHAR(50))
AS
SET NOCOUNT ON
	BEGIN
		SELECT AdmParClase, AdmParCodigo, AdmParSeq, AdmParDescripcion, AdmCiaCodigo, AdmSucCodigo, AdmParNumerico, AdmParDouble, AdmParString, AdmParBoolean, AdmParFecha, AdmStsCodigo, AdmParUserdata, AdmParUsuario, AdmParFechaReg
		FROM mAdmPar
		WHERE AdmParCodigo = @AdmParCodigo
		ORDER BY AdmParString ASC
	END
GO
--

CREATE PROCEDURE Spc_mAdmPdo_Get(@AdmPdoCodigo SMALLINT = -1)
AS
SET NOCOUNT ON
	BEGIN
	IF @AdmPdoCodigo = -1
	BEGIN
		SELECT AdmPdoCodigo, AdmPdoDescripcion, AdmPdoUserdata, AdmStsCodigo, AdmPdoUsuario, AdmPdoFechaReg
		FROM mAdmPdo
	END

	ELSE
	BEGIN
		SELECT AdmPdoCodigo, AdmPdoDescripcion, AdmPdoUserdata, AdmStsCodigo, AdmPdoUsuario, AdmPdoFechaReg
		FROM mAdmPdo
		WHERE AdmPdoCodigo = @AdmPdoCodigo
	END

	END
GO

CREATE PROCEDURE Spc_mAdmPrv_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'SELECT AdmPrvCodigo, AdmPrvDescripcion, AdmPrvUserdata, AdmStsCodigo, AdmPrvUsuario, AdmPrvFechaReg FROM mAdmPrv';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

ALTER PROCEDURE Spc_mAdmMun_Put(@AdmMunDescripcion VARCHAR(20), @AdmCiuCodigo INT, @AdmMunUsuario VARCHAR(25) = 'MIGRACION', @AdmStsCodigo BIT = 1)
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'INSERT INTO mAdmMun(AdmMunDescripcion, AdmCiuCodigo, AdmMunUsuario, AdmMunFechaReg, AdmStsCodigo)'
	SET @SQLString += ' VALUES(@Local_AdmMunDescripcion, @Local_AdmCiuCodigo, @Local_AdmMunUsuario, DEFAULT, @Local_AdmStsCodigo)';
	SET @ParmDefinition = N'@Local_AdmMunDescripcion VARCHAR(20), @Local_AdmCiuCodigo INT, @Local_AdmMunUsuario VARCHAR(25), @Local_AdmStsCodigo BIT';

	EXECUTE sp_executesql @SQLString, @ParmDefinition,
						  @Local_AdmMunDescripcion = @AdmMunDescripcion,
						  @Local_AdmCiuCodigo = @AdmCiuCodigo,
						  @Local_AdmMunUsuario = @AdmMunUsuario,
						  @Local_AdmStsCodigo = @AdmStsCodigo
END
GO
--

CREATE PROCEDURE Spc_mAdmPco_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'SELECT AdmpcoCodigo, AdmpcoDescripcion, AdmPcoUserdata, AdmpcoDesde, AdmpcoHasta, AdmstsCodigo, AdmpcoUsuario, AdmpcoFechaReg FROM mAdmPco';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

CREATE PROCEDURE Spc_mAdmSec_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'SELECT AdmSecCodigo, AdmSecDescripcion, AdmMunCodigo, AdmStscodigo, AdmSecUsuario, AdmSecFechaReg FROM mAdmSec';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

CREATE PROCEDURE Spc_mAdmMun_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'SELECT AdmMunCodigo, AdmMunDescripcion, AdmCiuCodigo, AdmMunUsuario, AdmMunFechaReg, AdmStsCodigo FROM mAdmMun';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

ALTER PROCEDURE Spc_dComMbr_Get(@Proponente VARCHAR(150) = NULL)
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(2500);
	DECLARE @ParmDefinition NVARCHAR(500);

	--COMISIONES DEL LEGISLADOR
	SET @Proponente = RTRIM(LTRIM(@Proponente))
	SET @SQLString = N'SELECT A.ComCfmId,A.ComComCodigo,ExpExpNumero,ComTipCodigo,AdmPcoCodigo,ComCfmFecha,ComCfmCoordinador,ComCfmSecretaria,ComCfmDisuelta,ComCfmAtribucion,ComCfmUserdata,AdmEstCodigo,ComCfmUsuario,ComCfmFechaReg,ComComNombre,ComComUserdata,ComComUsuario,ComComFechaReg,ComMbrSecuencia,ComMbrNombre,D.AdmFunCodigo,ComMbrInterno,ComMbrUsuario,ComMbrFechaReg,AdmFunDescripcion,AdmCatCodigo,AdmFunUserdata,D.AdmStsCodigo,AdmFunUsuario,AdmFunFechaReg ';
	SET @SQLString += N'FROM mComCfm AS A ';
	SET @SQLString += N'INNER JOIN mComCom AS B ON A.ComComCodigo = B.ComComCodigo ';
	SET @SQLString += N'INNER JOIN dComMbr AS C ON A.ComCfmId = C.ComCfmId ';
	SET @SQLString += N'INNER JOIN mAdmFun AS D ON C.AdmFunCodigo = D.AdmFunCodigo '
	SET @SQLString += N'WHERE C.ComMbrNombre LIKE CONCAT('''',@Local_Proponente,''%'') --(AdmLegNombres+AdmLegApellido1)';
    SET @ParmDefinition = N'@Local_Proponente VARCHAR(150)';

	EXECUTE sp_executesql @SQLString, @ParmDefinition,
						  @Local_Proponente = @Proponente
END
GO
--

ALTER PROCEDURE Spc_mAdmLeg_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(2500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @SQLString = N'SELECT AdmLegCodigo, AdmLegTipoId, mAdmPar.AdmParString, AdmLegCedula, CONCAT(ISNULL(AdmlegNombres,''''),'' '',ISNULL(AdmlegApellido1,''''),'' '',ISNULL(AdmlegApellido2,'''')) AS FullName, AdmlegNombres, AdmlegApellido1, AdmlegApellido2, AdmlegSexo, mAdmFun.AdmFunCodigo, mAdmFun.AdmFunDescripcion, AdmProProvincia, AdmlegFoto, AdmLegHuella, AdmPdoCodigo, mAdmPco.AdmPcoCodigo, mAdmPco.AdmpcoDescripcion, AdmLegProfesion, AdmLegFechaNac, AdmLegDireccion, AdmPrvCodigo, AdmMunCodigo, AdmSecCodigo, AdmLegCelular, AdmLegTelefonoSenado, AdmLegTelefonoProvincial, AdmLegCorreo, AdmLegApartadoPostal, AdmLegFax, AdmLegTwitter, AdmLegSitioWeb, AdmLegLinkedlin, AdmLegAreasInteres, AdmLegPrioridad, ExpExpNumero, dAdmEst.AdmCatCodigo, dAdmEst.AdmEstDescripcion, dAdmEst.AdmEstCodigo, AdmLegUsuario, AdmLegFechaReg ';
	SET @SQLString += N'FROM mAdmLeg LEFT OUTER JOIN ';
	SET @SQLString += N'mAdmPar ON mAdmLeg.AdmLegTipoId = mAdmPar.AdmParNumerico LEFT OUTER JOIN ';
	SET @SQLString += N'mAdmPco ON mAdmLeg.AdmPcoCodigo = mAdmPco.AdmpcoCodigo LEFT OUTER JOIN ';
	SET @SQLString += N'mAdmFun ON mAdmLeg.AdmFunCodigo = mAdmFun.AdmFunCodigo LEFT OUTER JOIN ';
	SET @SQLString += N'dAdmEst ON mAdmLeg.AdmEstCodigo = dAdmEst.AdmEstCodigo ';
	SET @SQLString += N'WHERE mAdmPar.AdmParCodigo = ''TipoId'' '
	SET @SQLString += N'ORDER BY AdmlegNombres ASC, AdmlegApellido1 ASC, AdmlegApellido2 ASC';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

ALTER PROCEDURE Spc_dComAle_Get(@AdmLegCedula VARCHAR(11))
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(2500);
	DECLARE @ParmDefinition NVARCHAR(500);

	SET @AdmLegCedula = LTRIM(RTRIM(@AdmLegCedula));
	SET @SQLString = N'SELECT ComAsiNumReporte,AdmLegCedula,ComAleHoraLLegada,ComAleHoraSalida,ComAlePresente,ComAleExcusa,mAdmExc.AdmExcCodigo,ComAleDefinicionExc,ComAleNotificar,ComAleUsuario,CONVERT(VARCHAR(10), ComAleFechaReg, 103) AS ComAleFechaReg,AdmExcDescripcion,AdmExcUsuario,AdmExcFechaReg ';
	SET @SQLString += N'FROM dComAle INNER JOIN mAdmExc ON dComAle.AdmExcCodigo = mAdmExc.AdmExcCodigo ';
	SET @SQLString += N'WHERE AdmLegCedula = @Local_AdmLegCedula'
	SET @ParmDefinition = N'@Local_AdmLegCedula VARCHAR(11)';

	EXECUTE sp_executesql @SQLString, @ParmDefinition,
						  @Local_AdmLegCedula = @AdmLegCedula
END
GO
--

--CREATE PROCEDURE Spc_mIniIni_Get
--AS SET NOCOUNT ON
--BEGIN
--	DECLARE @SQLString NVARCHAR(2500);
--	DECLARE @ParmDefinition NVARCHAR(500);

--	/* AdmEstCodigo: FALTANTE */
--	SET @SQLString = N'SELECT ComAsiNumReporte,AdmLegCedula,ComAleHoraLLegada,ComAleHoraSalida,ComAlePresente,ComAleExcusa,mAdmExc.AdmExcCodigo,ComAleDefinicionExc,ComAleUsuario,ComAleFechaReg,AdmExcDescripcion,AdmExcUsuario,AdmExcFechaReg ';
--	SET @SQLString += N'FROM dComAle INNER JOIN mAdmExc ON dComAle.AdmExcCodigo = mAdmExc.AdmExcCodigo';
--	SET @ParmDefinition = N'';

--	EXECUTE sp_executesql @SQLString, @ParmDefinition
--END
--GO
--


ALTER PROCEDURE [dbo].[Spc_mIniIni_Get](@FechaDesde DATETIME, @FechaHasta DATETIME, @PorLegislador BIT = 0, @EsCierre BIT = 0, @AdmEstCodigo INT = NULL, @IniIniProponente VARCHAR(MAX) = NULL)
AS
BEGIN

IF (@PorLegislador = 0 AND @EsCierre = 0)
BEGIN
	SELECT
		IniIniCodigoSis
		,IniIniSecuencia
		,IniIniNumero
		,IniIniPriorizada
		,IniTipCodigo
		,IniStpCodigo
		,IniIniMateria
		,AdmpcoCodigo
		,AdmLetCodigoInicio
		,IniIniFecha
		,IniIniDescripcion
		,IniIniProponentes
		,IniIniObservaciones
		,ComCfmId
		,AdmCamCodigo
		,IniIniPoderOrigen
		,IniIniNumOficioOrig
		,IniIniVecesDev
		,IniIniCreadoPor
		,IniIniDigitadoPor
		,AdmAnoCodigo
		,IniIniConteoLeg
		,IniIniMbrComisionesEsp
		,IniIniUsuario
		,IniIniAnalisisLeg
		,IniIniCorreccionEst
		,IniIniAnalizadoPor
		,IniIniCorreccionTec
		,IniIniInformeElaborado
		,IniIniCorregidoTrans
		,IniIniInformeDtrl
		,IniIniInformeOpa
		,IniIniInformeComisiones
		,IniIniInformeOtros
		,IniIniInformeAses
		,IniIniNumExpDiputados
		,IniIniAprobPresidida
		,IniIniSecretarios
		,IniIniNumProm
		,IniIniPromulgada
		,IniIniNumArchivo
		,IniIniEnviadoComPor
		,IniIniDespachada
		,IniIniDespachadopor
		,IniIniDespachadaHacia
		,IniIniRecibidoTrans
		,IniIniRevisadoTrans
		,IniIniTranscritoPor
		,IniIniDespachadoTrans
		,IniIniOficioEnvComis
		,IniIniNumOficioDesp
		,IniIniNumLegislaturaVigente
		,IniIniNotasDespacho
		,IniCacCodigo
		,IniIniFechaReg
	FROM mIniIni
	WHERE IniIniFecha>=@FechaDesde AND IniIniFecha<=@FechaHasta
	ORDER BY IniIniCodigoSis DESC
END

ELSE IF @PorLegislador = 0 AND @EsCierre = 1
BEGIN
	SELECT TOP(100) A.AdmEstCodigo, A.IniIniCodigoSis, A.IniIniSecuencia, A.IniIniNumero, A.IniIniDescripcion, A.IniIniFechaConteoLeg, A.IniIniNumLegislaturaVigente, A.IniCacCodigo, B.IniCacDescripcion
	FROM mIniIni AS A 
	INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo
	INNER JOIN dAdmEst AS C ON A.AdmEstCodigo = C.AdmEstCodigo
	WHERE A.IniIniFechaConteoLeg BETWEEN @FechaDesde AND @FechaHasta AND A.IniIniNumLegislaturaVigente = 2 --AND A.IniCacCodigo = 3 AND C.AdmEstCodigo = 72
	ORDER BY IniIniNumero ASC
END

ELSE
BEGIN
	--INICIATIVAS DEL LEGISLADOR
	SET @IniIniProponente = RTRIM(LTRIM(@IniIniProponente))
	SELECT 
	IniIniCodigoSis
	,IniIniSecuencia
	,IniIniNumero
	,AdmpcoCodigo
	,IniTipCodigo
	,IniStpCodigo
	,IniIniDescripcion
	,CONVERT(VARCHAR(10), IniIniFecha, 103) AS IniIniFecha
	,AdmLetCodigoInicio
	,AdmAnoCodigo
	,ComCfmId
	,ExpExpNumero
	,IniIniObservaciones
	,IniIniMateria
	,AdmCamCodigo
	,IniIniVecesDev
	,IniIniConteoLeg
	,IniIniPoderOrigen
	,IniIniNumOficioOrig
	,IniIniProponentes
	,IniIniMbrComisionesEsp
	,IniIniPriorizada
	,IniIniInformeAses
	,IniIniInformeDtrl
	,IniIniInformeOpa
	,IniIniInformeOtros
	,IniIniNumExpDiputados
	,IniIniInformeElaborado
	,IniIniInformeComisiones
	,IniIniCorreccionEst
	,IniIniAprobPresidida
	,IniIniSecretarios
	,IniIniDigitadoPor
	,IniIniEnviadoComPor
	,IniIniRevisadoPor
	,IniIniOficioEnvComis
	,IniIniRecibidoTrans
	,IniIniDespachada
	,IniIniDespachadopor
	,IniIniDespachadaHacia
	,IniIniNumOficioDesp
	,IniIniPromulgada
	,IniIniNumProm
	,IniIniNumArchivo
	,IniIniNumLegislaturaVigente
	,A.IniCacCodigo
	,C.IniCacDescripcion
	,IniIniNotasDespacho
	,IniIniDespachado
	,IniIniTranscritoPor
	,IniIniRevisadoTrans
	,IniIniDespachadoTrans
	,IniIniCreadoPor
	,IniIniCorreccionTec
	,IniIniCorregidoTrans
	,IniIniAnalisisLeg
	,IniIniAnalizadoPor
	,AdmDepCodigo
	,AdmEstCodigo
	,IniIniUsuario
	,IniIniFechaReg
	,AdmParClase
	,AdmParCodigo
	,AdmParSeq
	,AdmParDescripcion
	,AdmCiaCodigo
	,AdmSucCodigo
	,AdmParNumerico
	,AdmParDouble
	,AdmParString
	,AdmParBoolean
	,AdmParFecha
	,AdmStsCodigo
	,AdmParUserdata
	,AdmParUsuario
	,AdmParFechaReg
	FROM mIniIni AS A
	INNER JOIN mAdmPar AS b ON A.IniIniPoderOrigen=b.AdmParNumerico AND AdmParCodigo='Poderes' AND AdmParString='Senado de la República'
	INNER JOIN mIniCac AS C ON A.IniCacCodigo = C.IniCacCodigo
	WHERE IniIniProponentes LIKE CONCAT('',@IniIniProponente,'%') --(AdmLegNombres+AdmLegApellido1)
END
	
END

GO

/********************************** PROCEDURES **********************************/