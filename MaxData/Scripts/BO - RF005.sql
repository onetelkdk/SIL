--SQL SCRIPT: Created by Gregorio Ramírez [Development - MaxApp]
--Tables: mI, dComMbr, mIniIni
--Dependencies: mAdmCat, dAdmEst, dAdmDst, mAdmPar, mAdmFun, rFunUsr


/********************************** TABLES **********************************/
/********************************** TABLES **********************************/


/********************************** SEQUENCES **********************************/
/********************************** SEQUENCES **********************************/


/********************************** QUERIES **********************************/
/********************************** QUERIES **********************************/


/********************************** PROCEDURES *******************************/

CREATE FUNCTION Fnc_mComActNextSequence()
RETURNS VARCHAR(15)
AS
BEGIN

	/* OBTENEMOS ÚLTIMO REGISTRO EN TABLA */
	DECLARE 
	@ComActNumero VARCHAR(15) = (SELECT MAX(ComActNumero) FROM mComAct),
	@ComActCodigoSis INT =		(SELECT MAX(ComActCodigoSis)+1 FROM mComAct),
	@ComActSecuencia INT =		(SELECT MAX(ComActSecuencia)+1 FROM mComAct)

	/* OBTENEMOS SECUENCIA NUMÉRICA + 1 */
	SET @ComActNumero = (SELECT SUBSTRING(@ComActNumero,4,LEN(@ComActNumero))+1)

	/* VALORIZAMOS A •5• POSICIONES EL CAMPO: '@H_CIF' [IZQUIERDA EN 0] */
	SET @ComActNumero = (SELECT CONCAT('AC-',REPLICATE('0',(5 - LEN(@ComActNumero))) + CONVERT(VARCHAR, @ComActNumero)))

	RETURN @ComActNumero

END
GO
--

/* TEST  */
--SELECT dbo.Fnc_mComActNextSequence()

CREATE PROCEDURE Spc_mComAct_Put(@ComActNumero VARCHAR(15), @ComActTipo INT, @ComCfmId INT, @ComActFecha DATETIME, @AdmLetCodigo INT, @IniIniCodigoSis VARCHAR(50), @AdmPcoCodigo INT, @ComActTipoReunion INT, @ComActDescripcion VARCHAR(MAX), @ComActResultados VARCHAR(MAX), @ComActInvitados VARCHAR(MAX), @AdmSalCodigo INT, @ComActHoraConvoca TIME, @ComActHoraInicio TIME, @ComActHoraCierre TIME, @ComActFuncionarioCom INT, @AdmEstCodigo INT, @ComActUsuario VARCHAR(25), @IsDelete BIT = 0)
AS SET NOCOUNT ON;
BEGIN

IF @IsDelete = 1
BEGIN
	DELETE
	FROM mComAct
	WHERE ComActSecuencia = @ComActNumero
END

ELSE IF @IsDelete = 0 AND @ComActNumero = '0'
BEGIN
DECLARE @AdmSeqSecuencia INT = (SELECT AdmSeqSecuencia FROM mAdmSeq WHERE AdmCatCodigo = 5) + 1
--DECLARE @AdmpcCodigo SMALLINT = (SELECT AdmpcoCodigo FROM mAdmPco WHERE ) -POR ACLARAR-

	BEGIN TRAN _Insert_mComAct

		--CAMBIAR A /*IDENTITY*/
		INSERT INTO mComAct(ComActSecuencia, ComActNumero, ComActTipo, ComCfmId, ComActFecha, AdmLetCodigo, IniIniCodigoSis, AdmPcoCodigo, ComActTipoReunion, ComActDescripcion, ComActResultados, AdmSalCodigo, ComActHoraConvoca, ComActHoraInicio, ComActHoraCierre, ComActFuncionarioCom, AdmEstCodigo, ComActUsuario, ComActFechaReg)
			OUTPUT INSERTED.ComActNumero
		VALUES (@AdmSeqSecuencia, (SELECT DBO.Fnc_mComActNextSequence()), @ComActTipo, @ComCfmId, @ComActFecha, @AdmLetCodigo, @IniIniCodigoSis, @AdmPcoCodigo, @ComActTipoReunion, @ComActDescripcion, @ComActResultados, @AdmSalCodigo, @ComActHoraConvoca, @ComActHoraInicio, @ComActHoraCierre, @ComActFuncionarioCom, @AdmEstCodigo, @ComActUsuario, DEFAULT)

		IF @@ROWCOUNT != 0
		BEGIN
			UPDATE mAdmSeq
			SET AdmSeqSecuencia = (AdmSeqSecuencia) + 1
			WHERE AdmCatCodigo = 5

			--Inserción de invitados
			DECLARE 
			@ComActCodigoSis INT = (SELECT TOP(1) ComActCodigoSis FROM mComAct WHERE ComActNumero = @ComActNumero),
			@CountInvitados INT = (SELECT COUNT(SplitData) AS SplitData FROM dbo.Fnc_SplitString(@ComActInvitados,';')), 
			@CountItemInv INT = 0,
			@SplitComInvNombre VARCHAR(65)

			--Iteración de invitados por delimitador(;)
			WHILE(@CountInvitados != 0)
			BEGIN
				--Obtiene item 1 a 1
				SET @SplitComInvNombre = (SELECT SplitData FROM dbo.Fnc_SplitString(@ComActInvitados,';') ORDER BY SplitData ASC OFFSET @CountItemInv ROWS FETCH NEXT 1 ROWS ONLY)
				
				--Ingresa item 1 a 1
				EXEC Spc_dcomInv_Put 
				@Type = 'I',
				@ComActCodigoSis = @ComActCodigoSis, 
				@ComInvSecuencia = NULL, 
				@ComInvNombre = @SplitComInvNombre, 
				@ComInvUsuario = @ComActUsuario

				--Avanza el loop
				SET @CountItemInv += 1
				SET @SplitComInvNombre -= 1
			END

		END
	COMMIT TRAN _Insert_mComAct
END
ELSE IF @IsDelete = 0 AND @ComActNumero != '0'
BEGIN
	UPDATE mComAct
	SET 
	 ComActTipo =			@ComActTipo
	,ComCfmId =				@ComCfmId
	,ComActFecha =			@ComActFecha		
	,AdmLetCodigo =			@AdmLetCodigo
	,IniIniCodigoSis =		@IniIniCodigoSis
	,AdmPcoCodigo =			@AdmPcoCodigo
	,ComActTipoReunion =	@ComActTipoReunion
	,ComActDescripcion =	@ComActDescripcion
	,ComActResultados =		@ComActResultados	
	,AdmSalCodigo =			@AdmSalCodigo
	,ComActHoraConvoca =	@ComActHoraConvoca
	,ComActHoraInicio =		@ComActHoraInicio
	,ComActHoraCierre =		@ComActHoraCierre
	,ComActFuncionarioCom = @ComActFuncionarioCom
	,AdmEstCodigo =			@AdmEstCodigo
	,ComActUsuario =		@ComActUsuario
	WHERE ComActNumero =	@ComActNumero
END
END
GO
--

ALTER PROCEDURE Spc_mComAct_Get(@ComActFechaDesde DATETIME, @ComActFechaHasta DATETIME, @ComActNumero VARCHAR(15) = NULL)
AS SET NOCOUNT ON;
BEGIN
	SELECT ComActCodigoSis,ComActSecuencia,ComActNumero,ComActTipo,ComCfmId,CONVERT(VARCHAR(10),ComActFecha,103) AS ComActFecha,AdmLetCodigo,IniIniCodigoSis,AdmPcoCodigo,ComActTipoReunion,ComActDescripcion,ComActResultados,AdmSalCodigo,CONVERT(VARCHAR(5),ComActHoraConvoca,108) AS ComActHoraConvoca,CONVERT(VARCHAR(5),ComActHoraInicio,108) AS ComActHoraInicio,CONVERT(VARCHAR(5),ComActHoraCierre,108) AS ComActHoraCierre,ComActFuncionarioCom,AdmEstCodigo,ComActUsuario,ComActFechaReg, 
	SUBSTRING
	(
		(SELECT ComInvNombre +  ';' FROM dComInv AS B WHERE A.ComActCodigoSis = B.ComActCodigoSis FOR XML PATH (''))
		,0
		,LEN((SELECT ComInvNombre +  ';' FROM dComInv AS B WHERE A.ComActCodigoSis = B.ComActCodigoSis FOR XML PATH ('')))
	)  AS 'ComActInvitados'
	FROM mComAct AS A
	WHERE ComActNumero = ISNULL(@ComActNumero,ComActNumero) AND ComActFecha BETWEEN @ComActFechaDesde AND @ComActFechaHasta
	ORDER BY ComActNumero ASC
END
GO
--

CREATE PROCEDURE Spc_dComAct_Put(@ComActNumero VARCHAR(15), @AdmLegCodigo INT = NULL, @AdmFunCodigo INT = NULL, @ComActHoraLLegada TIME(7) = NULL, @ComActHoraSalida TIME(7) = NULL, @ComActPresente BIT = NULL, @ComActExcusa BIT = NULL, @AdmExcCodigo SMALLINT = NULL, @ComActDefinicionExc VARCHAR(MAX) = NULL, @ComActPerteneceComision BIT = NULL, @ComActUsuario VARCHAR(25) = NULL, @IsDelete BIT = 0)
AS SET NOCOUNT ON;
BEGIN

DECLARE @ComActCodigoSis INT = (SELECT TOP(1) ComActCodigoSis FROM mComAct WHERE ComActNumero = @ComActNumero)

IF @IsDelete = 1
BEGIN
		--ELIMINACIÓN DE REGISTROS ACTUALES
		DELETE
		FROM dComAct
		WHERE ComActCodigoSis = @ComActCodigoSis
END
ELSE IF @IsDelete = 0
BEGIN
	BEGIN TRAN _Insert_mComAct

		--CAMBIAR A /*IDENTITY*/
		--INGRESO DE NUEVO REGISTRO
		INSERT INTO dComAct(ComActCodigoSis, AdmLegCodigo, AdmFunCodigo, ComActHoraLLegada, ComActHoraSalida, ComActPresente, ComActExcusa, AdmExcCodigo, ComActDefinicionExc, ComActPerteneceComision, ComActUsuario, ComActFechaReg)
		VALUES (@ComActCodigoSis, @AdmLegCodigo, @AdmFunCodigo, @ComActHoraLLegada, @ComActHoraSalida, @ComActPresente, @ComActExcusa, @AdmExcCodigo, @ComActDefinicionExc, @ComActPerteneceComision, @ComActUsuario, DEFAULT)
	
	COMMIT TRAN _Insert_mComAct
END
END
GO
--

ALTER PROCEDURE Spc_dComAct_Get(@ComActCodigoSis INT)
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(1500);
	DECLARE @ParmDefinition NVARCHAR(500);


	SET @SQLString =  N'SELECT A.ComActCodigoSis, D.ComActNumero, A.AdmLegCodigo, C.FullName, A.AdmFunCodigo, B.AdmFunDescripcion';
	SET @SQLString += N' FROM dComAct AS A';
	SET @SQLString += N' INNER JOIN (SELECT AdmFunCodigo, AdmFunDescripcion, AdmCatCodigo FROM mAdmFun WHERE AdmCatCodigo = 3 AND AdmFunCodigo NOT IN(11,12)) AS B ON A.AdmFunCodigo = B.AdmFunCodigo';
	SET @SQLString += N' INNER JOIN (SELECT AdmLegCodigo, CONCAT(ISNULL(AdmlegNombres,''''),ISNULL(AdmlegApellido1,''''),ISNULL(AdmlegApellido2,'''')) AS FullName FROM mAdmLeg) AS C ON A.AdmLegCodigo = C.AdmLegCodigo';
	SET @SQLString += N' INNER JOIN mComAct AS D ON A.ComActCodigoSis = D.ComActCodigoSis';
	SET @SQLString += N' WHERE A.ComActCodigoSis = @Local_ComActCodigoSis';
	SET @ParmDefinition = N'@Local_ComActCodigoSis INT';

	EXECUTE sp_executesql @SQLString, @ParmDefinition,
						  @Local_ComActCodigoSis = @ComActCodigoSis;
END
GO
--

ALTER PROCEDURE Spc_rFunUsr_Get
AS SET NOCOUNT ON
BEGIN
	DECLARE @SQLString NVARCHAR(1500);
	DECLARE @ParmDefinition NVARCHAR(500);
	
	SET @SQLString = N'SELECT FunUsrCodigo,AdmFunCodigo,AdmUsrNombre,AdmPcoCodigo,FunUsrUserdata,AdmStsCodigo,FunUsrUsuario,FunUsrFechaReg';
	SET @SQLString += N' FROM rFunUsr';
	SET @SQLString += N' WHERE AdmFunCodigo = 12 ORDER BY AdmUsrNombre ASC';
	SET @ParmDefinition = N'';

	EXECUTE sp_executesql @SQLString, @ParmDefinition
END
GO
--

/********************************** PROCEDURES *******************************/


CREATE PROCEDURE [dbo].[Spc_mAdmSal_Get]
(
	@AdmCatCodigo int = null
)
AS
SET NOCOUNT ON
	BEGIN
	SELECT 
	   AdmSalCodigo
      ,AdmSalDescripcion
      ,AdmCatCodigo
      ,AdmSalUserdata
      ,AdmStsCodigo
      ,AdmSalUsuario
      ,AdmSalFechaReg
  FROM dbo.mAdmSal
  WHERE AdmCatCodigo = ISNULL(@AdmCatCodigo, AdmCatCodigo) 
  ORDER BY AdmSalDescripcion ASC

	END
