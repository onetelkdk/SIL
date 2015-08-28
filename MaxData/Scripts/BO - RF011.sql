--SQL SCRIPT: Created by Gregorio Ramírez [Development - MaxApp]
--Tables: mAdmFun
--Dependencies: mAdmCat, dAdmEst, dAdmDst, mAdmPar, mAdmFun, rFunUsr

/********************************** TABLES **********************************/
 
/********************************** TABLES **********************************/



/********************************** SEQUENCES **********************************/

/********************************** SEQUENCES **********************************/



/********************************** QUERIES **********************************/

/********************************** QUERIES **********************************/



/********************************** PROCEDURES **********************************/

CREATE PROCEDURE Spc_mAdmFun_Get(@AdmFunCodigo SMALLINT = NULL, @IsActive CHAR(1) = 2)
AS
SET NOCOUNT ON
	BEGIN
IF @IsActive = '2'
BEGIN
		SELECT AdmFunCodigo, AdmFunDescripcion, AdmCatCodigo, AdmFunUserdata, AdmStsCodigo, AdmFunUsuario, AdmFunFechaReg
		FROM mAdmFun
		WHERE (@AdmFunCodigo IS NULL) OR (@AdmFunCodigo = 9 AND AdmFunCodigo = 9) OR (@AdmFunCodigo = 3 AND AdmCatCodigo = 3 AND AdmFunCodigo NOT IN(11,12))
		ORDER BY AdmFunDescripcion ASC
END
ELSE
BEGIN
		SELECT AdmFunCodigo, AdmFunDescripcion, AdmCatCodigo, AdmFunUserdata, AdmStsCodigo, AdmFunUsuario, AdmFunFechaReg
		FROM mAdmFun
		WHERE AdmStsCodigo = (CASE WHEN @IsActive = '0' THEN 0 WHEN @IsActive = '1' THEN 1 END) AND ((@AdmFunCodigo IS NULL) OR (@AdmFunCodigo = 9 AND AdmFunCodigo = 9) OR (@AdmFunCodigo = 3 AND AdmCatCodigo = 3 AND AdmFunCodigo NOT IN(11,12)))
		ORDER BY AdmFunDescripcion ASC
END
END
GO
--

CREATE PROCEDURE Spc_AdmFun_Put
(
	@AdmFunCodigo SMALLINT, 
	@AdmFunDescripcion VARCHAR(100), 
	@AdmCatCodigo SMALLINT, 
	@AdmFunUserdata INT, 
	@AdmStsCodigo BIT, 
	@AdmFunUsuario VARCHAR(25),
	@IsDelete BIT = 0
)
AS SET NOCOUNT ON;
BEGIN

IF @IsDelete = 1
BEGIN
	DELETE
	FROM mAdmFun
	WHERE AdmFunCodigo = @AdmFunCodigo
END

ELSE IF @IsDelete = 0 AND @AdmFunCodigo = 0
BEGIN

	BEGIN TRAN _Insert_mComAct

		INSERT INTO mAdmFun(AdmFunDescripcion, AdmCatCodigo, AdmFunUserdata, AdmStsCodigo, AdmFunUsuario, AdmFunFechaReg)
		VALUES (@AdmFunDescripcion, @AdmCatCodigo, @AdmFunUserData, @AdmStsCodigo, @AdmFunUsuario, DEFAULT)
	
	COMMIT TRAN _Insert_mComAct
END
ELSE IF @IsDelete = 0 AND @AdmFunCodigo != 0
BEGIN
	UPDATE mAdmFun
	SET 
	 AdmFunDescripcion =	@AdmFunDescripcion
	,AdmCatCodigo =			@AdmCatCodigo		
	,AdmFunUserdata =		@AdmFunUserData
	,AdmStsCodigo =			@AdmStsCodigo
	,AdmFunUsuario =		@AdmFunUsuario
	WHERE AdmFunCodigo =	@AdmFunCodigo
END
END
GO
--

CREATE PROCEDURE Spc_mAdmCat_Get(@AdmCatCargaDoc TINYINT = 1)
AS
BEGIN
IF @AdmCatCargaDoc = 1
BEGIN
	SELECT AdmCatCodigo,AdmCatNombre,AdmCatuserdata,AdmCatTabla,AdmCatCargaDoc,AdmCatUsuario,AdmCatFechaReg
	FROM mAdmCat 
	WHERE AdmCatCargaDoc = 1
	ORDER BY AdmCatNombre ASC
END
ELSE IF @AdmCatCargaDoc = 0
BEGIN
	SELECT AdmCatCodigo,AdmCatNombre,AdmCatuserdata,AdmCatTabla,AdmCatCargaDoc,AdmCatUsuario,AdmCatFechaReg
	FROM mAdmCat 
	WHERE AdmCatCargaDoc = 0
	ORDER BY AdmCatNombre ASC
END
ELSE
BEGIN
	SELECT AdmCatCodigo,AdmCatNombre,AdmCatuserdata,AdmCatTabla,AdmCatCargaDoc,AdmCatUsuario,AdmCatFechaReg
	FROM mAdmCat 
	ORDER BY AdmCatNombre ASC
END
END
GO
--

CREATE PROCEDURE Spc_mAdmLet_Get(@Abierto CHAR(1)  = NULL, @IsPreCierre BIT = 0)
AS
BEGIN
--@Abierto = 'Y' (Yes) | 'N' (No)
IF @IsPreCierre = 0
BEGIN
	SELECT 
	AdmLetCodigo
	,AdmLetAno
	,AdmLetDescripcion
	,AdmLetFechaDesde
	,AdmLetFechaHasta
	,AdmLtpCodigo
	,AdmLetUserdata
	,AdmEstCodigo
	,AdmLetUsuario
	,AdmLetFechaReg
  FROM mAdmLet
  WHERE (@Abierto IS NULL AND AdmEstCodigo = AdmEstCodigo) OR (@Abierto = 'Y' AND AdmEstCodigo = 176) OR (@Abierto = 'N' AND AdmEstCodigo = 178)
  ORDER BY AdmLetDescripcion DESC
END
ELSE IF @IsPreCierre = 1 --Referido al módulo: PreCierre
BEGIN
	SELECT 
	TOP(1)
	 A.AdmLetCodigo
	,A.AdmLetAno
	,A.AdmLetDescripcion
	,A.AdmLetFechaDesde
	,A.AdmLetFechaHasta
	,A.AdmLtpCodigo
	,A.AdmLetUserdata
	,A.AdmEstCodigo
	,A.AdmLetUsuario
	,MAX(AdmLetFechaReg) AS AdmLetFechaReg
	FROM mAdmLet AS A
	INNER JOIN mAdmPar B ON A.AdmLtpCodigo = B.AdmParNumerico AND B.AdmParCodigo='SesionReunion' 
	WHERE A.AdmEstCodigo IN (176,177) --PreCierre/Cierre
	GROUP BY A.AdmLetCodigo, A.AdmLetAno, A.AdmLetDescripcion, A.AdmLetFechaDesde, A.AdmLetFechaHasta, A.AdmLtpCodigo, A.AdmLetUserdata, A.AdmEstCodigo, A.AdmLetUsuario
END
END
GO
--

--TEST
--EXEC Spc_mAdmLet_Get 'Y', 1
--

exec Spc_mIniIniCierre_Put @FechaDesde='2015-08-16 00:00:00',@Type=N'U',@FechaHasta='2016-01-12 00:00:00',@IsPreCierre=1,@AdmLetCodigo=99

ALTER PROCEDURE Spc_mIniIniCierre_Put(@Type CHAR(1), @IsPreCierre BIT, @AdmLetCodigo SMALLINT, @FechaDesde DATETIME, @FechaHasta DATETIME)
AS SET NOCOUNT ON;
BEGIN

IF @Type = 'U' /* Update */ AND @IsPreCierre = 1
BEGIN

	--Aumento de conteo en Iniciativas
	UPDATE mIniIni
	SET IniIniNumLegislaturaVigente = ISNULL(IniIniNumLegislaturaVigente,0) + 1
	FROM mIniIni AS A 
	INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo 
	WHERE IniIniFechaConteoLeg BETWEEN @FechaDesde AND @FechaHasta AND A.IniCacCodigo = 2 --Vigente

	--Realizar Pre-Cierre
	UPDATE mAdmLet
	SET AdmEstCodigo = 177
	FROM mAdmLet
	WHERE AdmLetCodigo = @AdmLetCodigo

END
ELSE IF @Type = 'I' /* Insert */ AND @IsPreCierre = 0
BEGIN

--Obtención de valores a actualizar/registrar
	DECLARE 
	@Var_IniIniCodigoSis INT, 
	@Var_IniIniSecuencia INT, 
	@Var_IniIniNumero VARCHAR(25), 
	@Var_AdmEstCodigo INT, 
	@Var_IniTipCodigo INT, 
	@Var_IniStpCodigo INT, 
	@Var_IniIniFecha DATETIME,
	@Var_IniCacCodigo INT,
	@Var_IniIniNumLegislaturaVigente INT,
	@Var_IniIniFechaPerimida DATETIME,
	@Var_CantIniPendiente INT,
	@Var_GetDate DATETIME = GETDATE()

	--Actualización de valores: Cierre de legislatura
	IF EXISTS(SELECT AdmLtpCodigo FROM mAdmLet WHERE AdmLetCodigo = @AdmLetCodigo AND AdmLtpCodigo IN(3,4)) --De ser EXTRAORDINARIA
	BEGIN
		--Actualiza estado de Legislatura: EXTRAORDINARIA
		UPDATE mAdmLet
		SET AdmEstCodigo = 178 --Cerrada
		WHERE AdmLetCodigo = @AdmLetCodigo
	END
	ELSE
	BEGIN

	--Obtención de valores a registrar en histórico
	SET @Var_CantIniPendiente = 
	( 
		SELECT COUNT(A.IniIniCodigoSis)
		FROM mIniIni AS A
		INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo 
		INNER JOIN mIniTip AS C ON A.IniTipCodigo = C.IniTipCodigo 
		INNER JOIN mIniStp AS D ON A.IniStpCodigo = D.IniStpCodigo 
		WHERE A.IniIniNumLegislaturaVigente = 3 AND A.IniCacCodigo = 2 AND  
		RTRIM(c.IniTipDescripcion) = 'Proyecto de Ley' AND RTRIM(d.IniStpDescripcion) = 'Proyecto de Ley'
	)

	DECLARE @IniCountLoop INT = 0
	WHILE(@Var_CantIniPendiente != 0)
	BEGIN
		SELECT 
		@Var_IniIniCodigoSis =	A.IniIniCodigoSis,
		@Var_IniIniSecuencia =	A.IniIniSecuencia,
		@Var_AdmEstCodigo =		A.AdmEstCodigo,
		@Var_IniTipCodigo =		A.IniTipCodigo,
		@Var_IniStpCodigo =		A.IniStpCodigo,
		@Var_IniIniFecha  =		A.IniIniFecha,
		@Var_IniCacCodigo =		A.IniCacCodigo,
		@Var_IniIniNumLegislaturaVigente = IniIniNumLegislaturaVigente,
		@Var_IniIniFechaPerimida = IniIniFechaPerimida
		FROM mIniIni AS A
		INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo 
		INNER JOIN mIniTip AS C ON A.IniTipCodigo = C.IniTipCodigo 
		INNER JOIN mIniStp AS D ON A.IniStpCodigo = D.IniStpCodigo 
		WHERE A.IniIniNumLegislaturaVigente = 3 AND A.IniCacCodigo = 2 AND  
		RTRIM(c.IniTipDescripcion) = 'Proyecto de Ley' AND RTRIM(d.IniStpDescripcion) = 'Proyecto de Ley'
		ORDER BY A.IniIniCodigoSis ASC
		OFFSET @IniCountLoop ROWS FETCH NEXT 1 ROWS ONLY

		--Crear histórico
		EXEC Spc_dIniHis_Put 
		@IniHisCodigo = 0, 
		@IniIniCodigoSis =   @Var_IniIniCodigoSis, 
		@AdmEstCodigo =		 @Var_AdmEstCodigo, 
		@IniHisFechainicio = @Var_GetDate, /* GETDATE() desde Appp */
		@IniHisFechafin =    @Var_GetDate, 
		@IniHisPreaviso =    NULL, 
		@IniHisNotas =       NULL

		SET @IniCountLoop = (@IniCountLoop + 1);
		SET @Var_CantIniPendiente -= 1
	END

		--Actualiza iniciativas asociadas al período de ser ORDINARIA
		UPDATE mIniIni
		SET mIniIni.AdmEstCodigo = 72, --Perimida
		mIniIni.IniCacCodigo = 3, --Perimida,
		IniIniPerimida = 1, --True,
		IniIniFechaPerimida = GETDATE()
		FROM mIniIni AS A
		INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo 
		INNER JOIN mIniTip AS C ON A.IniTipCodigo = C.IniTipCodigo 
		INNER JOIN mIniStp AS D ON A.IniStpCodigo = D.IniStpCodigo 
		WHERE A.IniIniNumLegislaturaVigente = 3 AND A.IniCacCodigo = 2 AND  
		RTRIM(c.IniTipDescripcion) = 'Proyecto de Ley' AND RTRIM(d.IniStpDescripcion) = 'Proyecto de Ley'

		--Actualiza estado de Legislatura: ORDINARIA
		UPDATE mAdmLet
		SET AdmEstCodigo = 178 --Cerrada
		WHERE AdmLetCodigo = @AdmLetCodigo
	END
END 

END
GO
--

ALTER PROCEDURE Spc_mIniIni_Get
(
 @FechaDesde DATETIME
,@FechaHasta DATETIME
,@PorLegislador BIT = 0
,@EsCierre BIT = 0
,@AdmEstCodigo INT = NULL
,@IniIniProponente VARCHAR(MAX) = NULL
)
AS
BEGIN

IF (@PorLegislador = 0 AND @EsCierre = 0)
BEGIN
	SELECT
		A.IniIniCodigoSis
		,A.IniIniSecuencia
		,A.IniIniNumero
		,A.AdmpcoCodigo
		,A.IniTipCodigo
		,A.IniStpCodigo
		,A.IniIniDescripcion
		,A.IniIniFecha
		,A.IniIniReintroducida
		,A.AdmLetCodigoInicio
		,A.AdmAnoCodigo
		,A.ComCfmId
		,A.ExpExpNumero
		,A.IniIniObservaciones
		,A.IniIniMateria
		,A.AdmCamCodigo
		,A.IniIniVecesDev
		,A.IniIniConteoLeg
		,A.IniIniPoderOrigen
		,A.IniIniNumOficioOrig
		,A.IniIniProponentes
		,A.IniIniMbrComisionesEsp
		,A.IniIniPriorizada
		,A.IniIniInformeAses
		,A.IniIniInformeDtrl
		,A.IniIniInformeOpa
		,A.IniIniInformeOtros
		,A.IniIniNumExpDiputados
		,A.IniIniInformeElaborado
		,A.IniIniInformeComisiones
		,A.IniIniCorreccionEst
		,A.IniIniAprobPresidida
		,A.IniIniSecretarios
		,A.IniIniDigitadoPor
		,A.IniIniEnviadoComPor
		,A.IniIniRevisadoPor
		,A.IniIniOficioEnvComis
		,A.IniIniRecibidoTrans
		,A.IniIniDespachada
		,A.IniIniDespachadopor
		,A.IniIniDespachadaHacia
		,A.IniIniNumOficioDesp
		,A.IniIniPromulgada
		,A.IniIniNumProm
		,A.IniIniNumArchivo
		,A.IniIniNumLegislaturaVigente
		,A.IniCacCodigo
		,A.IniIniNotasDespacho
		,A.IniIniDespachado
		,A.IniIniTranscritoPor
		,A.IniIniRevisadoTrans
		,A.IniIniDespachadoTrans
		,A.IniIniCreadoPor
		,A.IniIniCorreccionTec
		,A.IniIniCorregidoTrans
		,A.IniIniAnalisisLeg
		,A.IniIniAnalizadoPor
		,A.AdmEstCodigo
		,A.IniIniUsuario
		,A.IniIniFechaReg
		,A.IniIniFechaReintroducida
		,A.IniIniFechaConteoLeg
		,A.IniIniFechaPriorizada
		,A.IniIniPerimida
		,A.IniIniFechaPerimida
		,A.IniIniFechaInformeAses
		,A.IniIniFechaInformeDtrl
		,A.IniIniFechaInformeOpa
		,A.IniIniFechaInformeOtros
		,A.IniIniFechaInformeComisiones
		,A.IniIniFechaPromulgada
		,A.IniIniFechaDespachada
		,A.IniIniFechAnalisisLeg
		,B.AdmEstDescripcion
	FROM mIniIni A
	INNER JOIN dAdmEst B
	ON A.AdmEstCodigo=B.AdmEstCodigo
	WHERE IniIniFecha>=@FechaDesde AND IniIniFecha<=@FechaHasta AND	A.AdmEstCodigo=ISNULL(@AdmEstCodigo,A.AdmEstCodigo)
	ORDER BY IniIniCodigoSis DESC
END

ELSE IF @PorLegislador = 0 AND @EsCierre = 1
BEGIN

	SELECT A.AdmEstCodigo, A.IniIniCodigoSis, A.IniIniSecuencia, A.IniIniNumero, A.IniIniDescripcion, A.IniIniNumLegislaturaVigente, A.IniIniFechaConteoLeg, B.IniCacCodigo, B.IniCacDescripcion, D.AdmstsCodigo, D.IniStpCodigo, D.IniStpDescripcion
	FROM mIniIni AS A 
	INNER JOIN mIniCac AS B ON a.IniCacCodigo=B.IniCacCodigo 
	INNER JOIN mIniTip AS C ON a.IniTipCodigo=C.IniTipCodigo 
	INNER JOIN mIniStp AS D ON a.IniStpCodigo=D.IniStpCodigo 
	WHERE IniIniFechaConteoLeg BETWEEN @FechaDesde AND @FechaHasta 
	AND IniIniNumLegislaturaVigente = 2  AND A.IniCacCodigo = 2 
	AND  RTRIM(c.IniTipDescripcion)='Proyecto de Ley' AND RTRIM(d.IniStpDescripcion)='Proyecto de Ley' 
	ORDER BY A.IniIniNumero ASC
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
--


EXEC Spc_mIniIni_Get '20150227','20150726', 0, 1, NULL


SELECT A.IniIniNumero, A.IniIniDescripcion, B.IniCacCodigo, B.IniCacDescripcion, D.AdmstsCodigo, D.IniStpCodigo, D.IniStpDescripcion, IniIniNumLegislaturaVigente
FROM mIniIni AS A 
INNER JOIN mIniCac AS B ON a.IniCacCodigo=B.IniCacCodigo 
INNER JOIN mIniTip AS C ON a.IniTipCodigo=C.IniTipCodigo 
INNER JOIN mIniStp AS D ON a.IniStpCodigo=D.IniStpCodigo 
WHERE IniIniFechaConteoLeg BETWEEN '20150816' AND '20160112' 
AND IniIniNumLegislaturaVigente = 2  AND A.IniCacCodigo = 2 
AND  RTRIM(c.IniTipDescripcion)='Proyecto de Ley' AND RTRIM(d.IniStpDescripcion)='Proyecto de Ley' 
ORDER BY A.IniIniNumero ASC

SELECT *
FROM mIniTip
WHERE IniTipDescripcion = 'Proyecto de Ley'

SELECT *
FROM mIniStp
WHERE IniStpDescripcion = 'Proyecto de Ley'

--Iniciativas -mIniIni- 'Perimidas' (3 -mIniCac-)
SELECT TOP(100) A.AdmEstCodigo, A.IniIniCodigoSis, A.IniIniSecuencia, A.IniIniNumero, A.IniIniDescripcion, A.IniIniFechaConteoLeg, A.IniIniNumLegislaturaVigente, A.IniCacCodigo, B.IniCacDescripcion
FROM mIniIni AS A 
INNER JOIN mIniCac AS B ON A.IniCacCodigo = B.IniCacCodigo
INNER JOIN dAdmEst AS C ON A.AdmEstCodigo = C.AdmEstCodigo
WHERE A.IniIniFechaConteoLeg BETWEEN '20150227' AND '20150726' AND A.IniIniNumLegislaturaVigente = 2 --AND A.IniCacCodigo = 3 AND C.AdmEstCodigo = 72
ORDER BY IniIniNumero ASC
GO
--

--Movimientos para el reporte: Listado de iniciativas
CREATE PROCEDURE Spc_mIniIniHist_Get(@FechaDesde DATETIME, @FechaHasta DATETIME)
AS
SET NOCOUNT ON
BEGIN
	SELECT A.IniIniNumero, A.IniIniDescripcion, A.IniIniProponentes, E.IniHisCodigo, E.AdmEstCodigo, F.AdmEstDescripcion, E.IniHisFechainicio
	FROM mIniIni a 
	INNER JOIN mIniCac b ON a.IniCacCodigo = b.IniCacCodigo 
	INNER JOIN mIniTip c ON a.IniTipCodigo = c.IniTipCodigo 
	INNER JOIN mIniStp d ON a.IniStpCodigo = d.IniStpCodigo 
	INNER JOIN dIniHis e ON a.IniIniCodigoSis = e.IniIniCodigoSis
	INNER JOIN dAdmEst f ON e.AdmEstCodigo = f.AdmEstCodigo
	WHERE IniIniNumLegislaturaVigente = 3 AND IniIniFechaConteoLeg BETWEEN @FechaDesde AND @FechaHasta
	AND RTRIM(C.IniTipDescripcion)='Proyecto de Ley' AND RTRIM(D.IniStpDescripcion)='Proyecto de Ley' --AND IniCacDescripcion ='Vigente' --INCORPORAR EN PRODUCCIÓN--
	ORDER BY A.IniIniNumero ASC
END
GO
--

CREATE FUNCTION Fnc_AdmParReports_Get(@AdmParDescripcion VARCHAR(8000))
RETURNS VARCHAR(255)
AS
BEGIN
	DECLARE @ReportPath VARCHAR(255) = (SELECT AdmParString FROM mAdmPar WHERE AdmParDescripcion = @AdmParDescripcion)
	RETURN @ReportPath
END
GO
--

SELECT dbo.Fnc_AdmParReports_Get('Cierre de legislatura')

EXEC Spc_mIniIniHist_Get '20150816', '20160112'
--

CREATE PROCEDURE Spc_dcomInv_Put
(
	@Type CHAR(1),
	@ComActCodigoSis INT,
	@ComInvSecuencia SMALLINT = NULL,
	@ComInvNombre VARCHAR(65), 
	@ComInvUsuario VARCHAR(15)
)
AS SET NOCOUNT ON;
BEGIN

IF @Type = 'D' --Delete
BEGIN
	DELETE
	FROM dComInv
	WHERE ComActCodigoSis = @ComActCodigoSis AND ComInvSecuencia = @ComInvSecuencia
END

ELSE IF @Type = 'I' --Insert
BEGIN

--Obtener el 
DECLARE @ComInvSeqNextNumber SMALLINT = (SELECT ISNULL(MAX(ComInvSecuencia),0) + 1 AS MaxInv FROM dcomInv WHERE ComActCodigoSis = @ComActCodigoSis)

	BEGIN TRAN _Insert_dcomInv

		INSERT INTO dcomInv(ComActCodigoSis, ComInvSecuencia, ComInvNombre, ComInvUsuario, ComInvFechaReg)
		VALUES (@ComActCodigoSis, @ComInvSeqNextNumber, @ComInvNombre, @ComInvUsuario, DEFAULT)
	
	COMMIT TRAN _Insert_dcomInv
END
ELSE IF @Type = 'U' --Update
BEGIN
	UPDATE dcomInv
	SET 
	 ComInvNombre =			@ComInvNombre		
	,ComInvUsuario =		@ComInvUsuario
	WHERE ComActCodigoSis =	@ComActCodigoSis AND ComInvSecuencia = @ComInvSecuencia
END
END
GO
--