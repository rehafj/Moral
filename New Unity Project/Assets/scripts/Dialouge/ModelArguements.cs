using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelArguements
{
    public string schema { get; set; }
    public List<Surfacevalue> Surfacevalues { get; set; }
}
    public class SurfaceValueObj
    {
        public string subvalue { get; set; }
        public string text { get; set; }
    }

    public class Surfacevalue
    {
        public string key { get; set; }
        public List<SurfaceValueObj> surfaceValueObj { get; set; }
    }


































//old code 
/*
    public string schema { get; set; }
    public Surfacevalues Surfacevalues { get; set; }
    // public string schema = "";
    //public List <SurfaceValues> Surfacevalues;*/
/*
public class BTrueTYourHeart
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class LoveIsForFools
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string startedAfamilyAtAyoungAge  { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class MoneyMaker
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string startedAfamilyAtAyoungAge  { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class Enviromentalist
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string startedAfamilyAtAyoungAge { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class EnviromentalistAnti
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class AnimalLover
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class AnimalLoverAnti
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class Teetotasler
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class TeetotaslerAnti
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class FamilyPerson
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string 

t { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class SchoolIsCool
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class SchoolIsDrool
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class SupportingComunities
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class LoverOfRisks
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class LandISWhereThehrtIS
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class CarrerAboveAll
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class FriendsAreTheJoyOFlife
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class Loner
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class YouthAreTheFuture
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class ProHiringFamily
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class SuchUncharactristicBehaviorOhMy
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class WeLiveForSpontaneity
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class AnAdventureWeSeek
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class NiaeveteIsFiction
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class ImmagretsWeGetTheJobDone
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class WeArewNothingIfWeAreNotReserved
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class AselfMadeShapeWeAspireToBe
{
    public string friendwithabestfriendsenemy { get; set; }
    public string InLoveWirhAnothersspouce { get; set; }
    public string IsYoungAndPregnant { get; set; }
    public string WillActOnLove { get; set; }
    public string isolated { get; set; }
}

public class Surfacevalues
{
    public BTrueTYourHeart BTrueTYourHeart { get; set; }
    public LoveIsForFools LoveIsForFools { get; set; }
    public MoneyMaker MoneyMaker { get; set; }
    public Enviromentalist Enviromentalist { get; set; }
    public EnviromentalistAnti EnviromentalistAnti { get; set; }
    public AnimalLover AnimalLover { get; set; }
    public AnimalLoverAnti AnimalLoverAnti { get; set; }
    public Teetotasler Teetotasler { get; set; }
    public TeetotaslerAnti TeetotaslerAnti { get; set; }
    public FamilyPerson FamilyPerson { get; set; }
    public SchoolIsCool SchoolIsCool { get; set; }
    public SchoolIsDrool schoolIsDrool { get; set; }
    public SupportingComunities SupportingComunities { get; set; }
    public LoverOfRisks LoverOfRisks { get; set; }
    public LandISWhereThehrtIS LandISWhereThehrtIS { get; set; }
    public CarrerAboveAll CarrerAboveAll { get; set; }
    public FriendsAreTheJoyOFlife FriendsAreTheJoyOFlife { get; set; }
    public Loner Loner { get; set; }
    public YouthAreTheFuture youthAreTheFuture { get; set; }
    public ProHiringFamily ProHiringFamily { get; set; }
    public SuchUncharactristicBehaviorOhMy suchUncharactristicBehaviorOhMy { get; set; }
    public WeLiveForSpontaneity WeLiveForSpontaneity { get; set; }
    public AnAdventureWeSeek AnAdventureWeSeek { get; set; }
    public NiaeveteIsFiction NiaeveteIsFiction { get; set; }
    public ImmagretsWeGetTheJobDone ImmagretsWeGetTheJobDone { get; set; }
    public WeArewNothingIfWeAreNotReserved WeArewNothingIfWeAreNotReserved { get; set; }
    public AselfMadeShapeWeAspireToBe AselfMadeShapeWeAspireToBe { get; set; }*/
//}

/*
 
 
 
 
     public string schema = "";
    public List <SurfaceValues> Surfacevalues;
}

public class SurfaceValues
{
    public List<KeyValuePair<string, SubValues>> subvalues { get; set; }
  
}
public class SubValues
{
    public KeyValuePair<string, string> subValues = new KeyValuePair<string, string>();
}


 
 */