namespace CropMeasureEligibility.Editor.Common
{
	public enum RequestDocumentTypeEnum
	{
		ListA = 1,
		ListB = 2,
		ListC = 3,
		ListD = 4,
		ListE = 5,
		ListF = 6,
		ListG = 7
	}

	public enum CategoryTypeEnum
	{
		/// <summary>
		/// List C
		/// </summary>
		SheetC = 1,

		/// <summary>
		/// List D
		/// </summary>
		SheetD = 2
	}

	public enum CommonMeasurePillarEnum
	{
		/// <summary>
		/// I Zahtjevi za izravnu potporu,
		/// IZRAVNA PLAĆANJA
		/// </summary>
		DirectPayments = 2,

		/// <summary>
		/// II Zahtjevi za potporu za mjere ruralnog razvoja,
		/// MJERE RURALNOG RAZVOJA
		/// </summary>
		RuralDevelopment = 3,

		/// <summary>
		/// Van sustava poticaja
		/// </summary>
		OutsideIncentiveSystem = 4,

		/// <summary>
		/// Intervencije za ruralni razvoj (Strateški plan 2023.-2027.)
		/// </summary>
		RuralInterventions = 5
	}

	public enum CommonRequestDocumentDeclarationEnum
	{
		/// <summary>
		/// livade i pašnjaci
		/// </summary>
		OPLP = 1,

		/// <summary>
		/// ostale vrste korištenja zemljišta
		/// </summary>
		OVKZ = 2,

		/// <summary>
		/// šećerna repa
		/// </summary>
		SugerBeat = 3,

		/// <summary>
		/// duhan
		/// </summary>
		Tobacco = 4,

		/// <summary>
		/// maslinovo ulje
		/// </summary>
		OliveOil = 5,

		/// <summary>
		/// krave dojilje
		/// </summary>
		Cows = 6,

		/// <summary>
		/// mliječne krave
		/// </summary>
		DairyCow = 7,

		/// <summary>
		/// ovce i koze
		/// </summary>
		SheapAndGoat = 8,

		/// <summary>
		/// rasplodne krmače
		/// </summary>
		Sow = 9,

		/// <summary>
		/// mlijeko kravlje
		/// </summary>
		CowMilk = 10,

		/// <summary>
		/// mlijeko ovčje
		/// </summary>
		SheepMilk = 11,

		/// <summary>
		/// mlijeko kozje
		/// </summary>
		GoatMilk = 12,

		/// <summary>
		/// ekološka poljoprivredna proizvodnja
		/// </summary>
		Eco = 13,

		/// <summary>
		/// očuvanje izvornih i zaštičenih pasmina
		/// </summary>
		ProtectedBreed = 14,

		/// <summary>
		/// teži uvjeti gospodarenja u poljoprivredi
		/// </summary>
		Heavy = 15,

		/// <summary>
		/// integrirana poljoprivredna proizvodnja
		/// </summary>
		IntegratedProduction = 16,

		/// <summary>
		/// Tov goveda
		/// </summary>
		Bovidae = 17,

		/// <summary>
		/// pravo na regionalno plaćanje
		/// </summary>
		RegionalPE = 18,

		/// <summary>
		/// Specifično plaćanje
		/// </summary>
		SpecificPayment = 19,

		/// <summary>
		/// Dopunska premija za ovce i koze
		/// </summary>
		AdditionalSheepAndGoat = 20,

		/// <summary>
		/// Očuvanje izvornih i zaštićenih vrsta i kultivara poljoprivrednog bilja
		/// </summary>
		ProtectedCultivar = 21,

		/// <summary>
		/// Pravo na plaćanje iz posebne nacionalne rezerve za razminirano zemljište
		/// </summary>
		RegionalMining = 22,

		/// <summary>
		/// Novo za Korištenje prava na plaćanja
		/// </summary>
		UsingRegionalPE = 29,

		/// <summary>
		/// Dodjela prava na plaćanje iz nacionalne rezerve za nove korisnike
		/// </summary>
		NationalReserve = 30,

		/// <summary>
		/// Korištenje prava na premije za ovce i koze
		/// </summary>
		UsingSheepAndGoat = 31,

		/// <summary>
		/// Korištanje prava na primije za krava dojilje
		/// </summary>
		UsingCows = 32,

		/// <summary>
		/// Potpora za unaprjeđenje kvalitete mlijeka
		/// </summary>
		CowMilkQuality = 33,

		/// <summary>
		/// Potpora za unaprjeđenje kvalitete goveđeg mesa
		/// </summary>
		BeefMeat = 34,

		/// <summary>
		/// Potpora za unaprjeđenje kvalitete janjećeg i jarećeg mesa
		/// </summary>
		LambMeat = 35,

		/// <summary>
		/// Proizvodno vezana potpora za krave u proizvodnji mlijeka
		/// </summary>
		CowsInMilkProduction = 36,

		/// <summary>
		/// Mladi poljoprivrednici
		/// </summary>
		YoungFarmers = 41,

		/// <summary>
		/// Povrće
		/// </summary>
		Vegetables = 42,

		/// <summary>
		/// Voće
		/// </summary>
		Fruit = 43,

		/// <summary>
		/// Proteinski usjevi
		/// </summary>
		ProteinCrops = 44,

		/// <summary>
		/// Travnjaci velike prirodne vrijednosti
		/// </summary>
		HighNaturalValueLawns = 45,

		/// <summary>
		/// Kosac (Crex crex)
		/// </summary>
		CornCrake = 46,

		/// <summary>
		/// Leptiri
		/// </summary>
		Butterflies = 47,

		/// <summary>
		/// Poljske trake
		/// </summary>
		FieldLanes = 48,

		/// <summary>
		/// Ekstenzivni voćnjaci
		/// </summary>
		ExtensiveOrchard = 49,

		/// <summary>
		/// Ekstenzivni maslinik
		/// </summary>
		ExtensiveOliveGrove = 50,

		/// <summary>
		/// EKO-prijelaz
		/// </summary>
		EcoTransition = 51,

		/// <summary>
		/// gorsko-planinska područja
		/// </summary>
		MountainAreas = 52,

		/// <summary>
		/// prirodna ograničenja
		/// </summary>
		NaturalLimitations = 53,

		/// <summary>
		/// specifična ograničenja
		/// </summary>
		SpecificLimitations = 54,

		/// <summary>
		/// Obrada tla s nagibom
		/// </summary>
		SlopeCultivation = 55,

		/// <summary>
		/// Zatravnjivanje trajnih nasada
		/// </summary>
		PermanentPlantationGrassing = 56,

		/// <summary>
		/// Program za male poljoprivrednike
		/// </summary>
		YoungFarmersProgram = 57,

		/// <summary>
		/// Preraspodijeljeno plaćanje
		/// </summary>
		ReallocatedPayment = 58,

		/// <summary>
		/// Zeleno plaćanje
		/// </summary>
		GreenPayment = 59,

		/// <summary>
		/// Ekološki uzgoj
		/// </summary>
		EcoFarming = 60,

		/// <summary>
		/// Plaćanja područjima s prirodnim ograničenjima ili ostalim posebnim ograničenjima
		/// </summary>
		NaturalAndOtherSpecialLimitations = 61,

		/// <summary>
		/// Očuvanje suhozida
		/// </summary>
		Drywall = 62,

		/// <summary>
		/// Očuvanje živica
		/// </summary>
		Hedge = 63,

		/// <summary>
		/// Dobrobit životinja u govedarstvu
		/// </summary>
		BovineWelfare = 64,

		/// <summary>
		/// Dobrobit životinja u svinjogojstvu
		/// </summary>
		PigWelfare = 65,

		/// <summary>
		/// Dobrobit životinja u peradarstvu
		/// </summary>
		PoultryWelfare = 66,

		/// <summary>
		/// Korištenje klopki
		/// </summary>
		TrapsUsage = 67,

		/// <summary>
		/// Metoda konfuzije štetnika
		/// </summary>
		PestConfusionMethod = 68,

		/// <summary>
		/// Poboljšano održavanje međurednog prostora
		/// </summary>
		InterspaceMaintaining = 69,

		/// <summary>
		/// Primjena ekoloških gnojiva
		/// </summary>
		EcologicalFertilizersApplication = 70,

		/// <summary>
		/// Mehaničko uništavanje korova
		/// </summary>
		MechanicalWeedsDestruction = 71,

		/// <summary>
		/// Dobrobit životinja u kozarstvu
		/// </summary>
		GoatWelfare = 73,

		/// <summary>
		/// Dobrobit životinja u ovčarstvu
		/// </summary>
		SheepWelfare = 74,

		/// <summary>
		/// Uporabe stajskog gnoja na oranicnim povrsinama (PUS)
		/// </summary>
		UseOfManuresOnArableLand = 75,

		/// <summary>
		/// 26.01. Dodatna preraspodijeljena potpora dohotku za održivost
		/// </summary>
		AdditionalReallocatedPayment = 76,

		/// <summary>
		/// 31.01. Intenzivirana raznolikost usjeva
		/// </summary>
		IntensifiedCropDiversity = 77,

		/// <summary>
		/// 31.02. Ekstenzivno gospodarenje pašnjacima
		/// </summary>
		ExtensivePastures = 78,

		/// <summary>
		/// 31.03. Intenzivirano održavanje ekološki značajnih površina
		/// </summary>
		IntensifiedEfa = 79,

		/// <summary>
		/// 31.04. Uporaba stajskog gnoja na oraničnim površina
		/// </summary>
		ManureOnArableSurface = 80,

		/// <summary>
		/// 31.05. Minimalni udio leguminoza od 20% unutar poljoprivrednih površina
		/// </summary>
		MinimalLegumes = 81,

		/// <summary>
		/// 31.06. Konzervacijska poljoprivreda
		/// </summary>
		ConservationAgriculture = 82,

		/// <summary>
		/// 31.07. Očuvanje travnjaka velike prirodne vrijednosti (TVPV)
		/// </summary>
		HighNaturalValueLawnsPreservation = 83,

		/// <summary>
		/// 32.01.02. Proizvodno vezana plaćanja za prvotelke
		/// </summary>
		FirstBreedCowsPayment = 84,

		/// <summary>
		/// 32.09. Proizvodno vezana plaćanja za sjeme
		/// </summary>
		SeedPayment = 85,

		/// <summary>
		/// 71.01.01. - Plaćanja u gorsko planinskim područjima (GPP)
		/// </summary>
		MountainousAreaPayments = 86,

		/// <summary>
		/// 71.01.02. - Plaćanja u područjima sa značajnim prirodnim ograničenjima (ZPP)
		/// </summary>
		SignificantNatualConstraintsAreaPayments = 87,

		/// <summary>
		/// 71.01.03. - Plaćanja u područjima s posebnim ograničenjima (PPO)
		/// </summary>
		SpecificRestrictionsAreaPayments = 88,

		/// <summary>
		/// Očuvanje ugroženih izvornih i zaštićenih pasmina domaćih životinja
		/// </summary>
		NativeAndProtectedDomesticAnimals = 89,

		/// <summary>
		/// Pilot mjera za zaštitu kosca (Crex crex)
		/// </summary>
		ProtectedCornCrake = 90,

		/// <summary>
		/// Pilot mjera za zaštitu leptira
		/// </summary>
		ProtectedButterflies = 91,

		/// <summary>
		/// Uspostava poljskih traka
		/// </summary>
		FieldStripes = 92,

		/// <summary>
		/// Održavanje ekstenzivnih voćnjaka
		/// </summary>
		ExtensiveOrchards = 93,

		/// <summary>
		/// Održavanje ekstenzivnih maslinika
		/// </summary>
		ExtensiveOliveGroves = 94,

		/// <summary>
		/// Ekološki uzgoj
		/// </summary>
		EcologicalFarming = 95,

		/// <summary>
		/// Očuvanje suhozida
		/// </summary>
		DrywallPreservation = 96,

		/// <summary>
		/// Očuvanje živica
		/// </summary>
		HedgePreservation = 97,

		/// <summary>
		/// Korištenje feromonskih, vizualnih i hranidbenih klopki
		/// </summary>
		PheromoneVisualFoodTraps = 98,

		/// <summary>
		/// Metoda konfuzije štetnika u višegodišnjim nasadima
		/// </summary>
		PestConfusionInPlantations = 99,

		/// <summary>
		/// Mehaničko uništavanje korova unutar redova višegodišnjih nasada
		/// </summary>
		MechanicalWeedsDestructionInPlantations = 100,

		/// <summary>
		/// 31.08. Primjena ekoloških gnojiva u trajnim nasadima
		/// </summary>
		EcologicalFertilizersApplicationOnPermanentPlantation = 101
	}

}
