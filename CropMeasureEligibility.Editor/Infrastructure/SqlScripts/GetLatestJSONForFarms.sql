/*
	Dohvati za definirane farme posljednji JSON u 2023 godini
*/

USE CeresPayments

DROP TABLE If EXISTS #Farms;
DROP TABLE IF EXISTS #LatestIdByFarm;

CREATE TABLE #Farms (
	FarmId INT NOT NULL
);

CREATE TABLE #LatestIdByFarm (
	FarmId INT NOT NULL,
	LatestId INT NOT NULL
);

-- potrebno popuniti
INSERT INTO #Farms
VALUES(81),(93)


--zadnji barkod za korisnika
INSERT INTO #LatestIdByFarm(
	FarmId,
	LatestId
)
SELECT AC.FarmId, 
	   MAX(AC.Id) LatestId
FROM
    Payments.ActionContext AS AC
	INNER JOIN #Farms F
	ON AC.FarmId = F.FarmId
WHERE AC.Year = 2023
    AND AC.Active = 1
	AND AC.IsInProgress = 0
GROUP BY AC.FarmId

SELECT  AC.FarmId, 
		AC.ActionContextId,
		ACC.CropMeasuresEligibility
FROM Payments.ActionContext AS AC
	INNER JOIN #LatestIdByFarm AS LIBF
		ON AC.Id = LIBF.LatestId
	AND AC.FarmId = LIBF.FarmId
	INNER JOIN Payments.[ActionContext.Crop] AS ACC
	ON ACC.ActionContextId = AC.ActionContextId
WHERE AC.[Year] = 2023

--identifieri koji se promatraju
SELECT ACCRI.ActionContextId, ACCRI.Identifier
FROM Payments.[ActionContext.CropRequestItem] AS ACCRI
WHERE ACCRI.ActionContextId IN (
	SELECT   
		AC.ActionContextId
	FROM Payments.ActionContext AS AC
		INNER JOIN #LatestIdByFarm AS LIBF
			ON AC.Id = LIBF.LatestId
		AND AC.FarmId = LIBF.FarmId
		INNER JOIN Payments.[ActionContext.Crop] AS ACC
		ON ACC.ActionContextId = AC.ActionContextId
	WHERE AC.[Year] = 2023
)
AND ACCRI.Active = 1
AND ACCRI.LandUseId NOT IN (900, 910)
AND ACCRI.Surface > 1.00;