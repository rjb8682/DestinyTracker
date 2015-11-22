using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DestinyTracker.Models
{
    public class User
    {
        [JsonProperty("membershipId")]
        public string MemberId { get; set; }
        [JsonProperty("clanName")]
        public string ClanName { get; set; }
        [JsonProperty("clanTag")]
        public string ClanTag { get; set; }
        [JsonProperty("grimoireScore")]
        public int Grimoire { get; set; }
        [JsonProperty("characters")]
        public List<Character> Characters { get; set; }
    }

    public class Character
    {
        [JsonProperty("characterBase")]
        public CharacterBase CharacterBase { get; set; }

        [JsonProperty("emblemPath")]
        public string EmblemUrlEnd { get; set; }
        public string EmblemUrl => "http://www.bungie.net" + EmblemUrlEnd;

        [JsonProperty("backgroundPath")]
        public string BackgroundPathEnd { get; set; }
        public string BackgroundUrl => "http://www.bungie.net" + BackgroundPathEnd;

        [JsonProperty("characterLevel")]
        public int LightLevel { get; set; }
    }

    public class CharacterBase
    {
        [JsonProperty("characterId")]
        public string CharacterId { get; set; }
        [JsonProperty("minutesPlayedTotal")]
        public string MinutesPlayedTotal { get; set; }

        [JsonProperty("raceHash")]
        public double RaceHash { get; set; }
        public string Race
        {
            get
            {
                if (RaceHash - 898834093 < 0.1)
                {
                    return "Exo";
                }
                return RaceHash - 3887404748 < 0.1 ? "Human" : "Awoken";
            }
        }
        [JsonProperty("genderType")]
        public int GenderType { get; set; }
        public string Gender => GenderType == 0 ? "Male" : "Female";

        [JsonProperty("classHash")]
        public double ClassHash { get; set; }
        public string Class
        {
            get
            {
                if (ClassHash - 671679327 < 0.1)
                {
                    return "Hunter";
                }
                return Math.Abs(ClassHash - 3655393761) < 0.1 ? "Titan" : "Warlock";
            }
        }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }

    public class Stats
    {
        [JsonProperty("STAT_DEFENSE")]
        public Stat Defense { get; set; }
        [JsonProperty("STAT_INTELLECT")]
        public Stat Intellect { get; set; }
        [JsonProperty("STAT_DISCIPLINE")]
        public Stat Discipline { get; set; }
        [JsonProperty("STAT_STRENGTH")]
        public Stat Strength { get; set; }
        [JsonProperty("STAT_LIGHT")]
        public Stat Light { get; set; }
        [JsonProperty("STAT_ARMOR")]
        public Stat Armor { get; set; }
        [JsonProperty("STAT_AGILITY")]
        public Stat Agility { get; set; }
        [JsonProperty("STAT_RECOVERY")]
        public Stat Recovery { get; set; }
        [JsonProperty("STAT_OPTICS")]
        public Stat Optics { get; set; }
    }

    public class Stat
    {
        [JsonProperty("statHash")]
        public double StatHash { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class ActivityStats
    {
        [JsonProperty("story")]
        public Activity Story;
        [JsonProperty("raid")]
        public Activity Raid;
        [JsonProperty("patrol")]
        public Activity Patrol;
        [JsonProperty("allPvP")]
        public Activity AllPvP;
        [JsonProperty("allStrikes")]
        public Activity AllStrikes;
    }

    public class Activity
    {
        [JsonProperty("allTime")]
        public ActivityStatList Stats { get; set; }
    }

    public class ActivityStatList
    {
        [JsonProperty("activitiesCleared")]
        public ActivityStat ActivitiesCleared { get; set; }
        [JsonProperty("weaponKillsSuper")]
        public ActivityStat WeaponKillsSuper { get; set; }
        [JsonProperty("activitiesEntered")]
        public ActivityStat ActivitiesEntered { get; set; }
        [JsonProperty("weaponKillsMelee")]
        public ActivityStat WeaponKillsMelee { get; set; }
        [JsonProperty("weaponKillsGrenade")]
        public ActivityStat WeaponKillsGrenade { get; set; }
        [JsonProperty("abilityKills")]
        public ActivityStat AbilityKills { get; set; }
        [JsonProperty("assists")]
        public ActivityStat Assists { get; set; }
        [JsonProperty("totalDeathDistance")]
        public ActivityStat TotalDeathDistance { get; set; }
        [JsonProperty("averageDeathDistance")]
        public ActivityStat AverageDeathDistance { get; set; }
        [JsonProperty("totalKillDistance")]
        public ActivityStat TotalKillDistance { get; set; }
        [JsonProperty("kills")]
        public ActivityStat Kills { get; set; }
        [JsonProperty("averageKillDistance")]
        public ActivityStat AverageKillDistance { get; set; }
        [JsonProperty("secondsPlayed")]
        public ActivityStat SecondsPlayed { get; set; }
        [JsonProperty("deaths")]
        public ActivityStat Deaths { get; set; }
        [JsonProperty("averageLifespan")]
        public ActivityStat AverageLifespan { get; set; }
        [JsonProperty("bestSingleGameKills")]
        public ActivityStat BestSingleGameKills { get; set; }
        [JsonProperty("killsDeathsRatio")]
        public ActivityStat KillsDeathsRatio { get; set; }
        [JsonProperty("killsDeathsAssists")]
        public ActivityStat KillsDeathsAssists { get; set; }
        [JsonProperty("objectivesCompleted")]
        public ActivityStat ObjectivesCompleted { get; set; }
        [JsonProperty("precisionKills")]
        public ActivityStat PrecisionKills { get; set; }
        [JsonProperty("resurrectionsPerformed")]
        public ActivityStat ResurrectionsPerformed { get; set; }
        [JsonProperty("resurrectionsReceived")]
        public ActivityStat ResurrectionsReceived { get; set; }
        [JsonProperty("suicides")]
        public ActivityStat Suicides { get; set; }
        [JsonProperty("weaponKillsAutoRifle")]
        public ActivityStat WeaponKillsAutoRifle { get; set; }
        [JsonProperty("weaponKillsFusionRifle")]
        public ActivityStat WeaponKillsFusionRifle { get; set; }
        [JsonProperty("weaponKillsHandCannon")]
        public ActivityStat WeaponKillsHandCannon { get; set; }
        [JsonProperty("weaponKillsMachinegun")]
        public ActivityStat WeaponKillsMachinegun { get; set; }
        [JsonProperty("weaponKillsPulseRifle")]
        public ActivityStat WeaponKillsPulseRifle { get; set; }
        [JsonProperty("weaponKillsRocketLauncher")]
        public ActivityStat WeaponKillsRocketLauncher { get; set; }
        [JsonProperty("weaponKillsScoutRifle")]
        public ActivityStat WeaponKillsScoutRifle { get; set; }
        [JsonProperty("weaponKillsShotgun")]
        public ActivityStat WeaponKillsShotgun { get; set; }
        [JsonProperty("weaponKillsSniper")]
        public ActivityStat WeaponKillsSniper { get; set; }
        [JsonProperty("weaponKillsSubmachinegun")]
        public ActivityStat WeaponKillsSubmachinegun { get; set; }
        [JsonProperty("weaponKillsRelic")]
        public ActivityStat WeaponKillsRelic { get; set; }
        [JsonProperty("weaponKillsSideArm")]
        public ActivityStat WeaponKillsSideArm { get; set; }
        [JsonProperty("weaponBestType")]
        public ActivityStat WeaponBestType { get; set; }
        [JsonProperty("allParticipantsCount")]
        public ActivityStat AllParticipantsCount { get; set; }
        [JsonProperty("allParticipantsTimePlayed")]
        public ActivityStat AllParticipantsTimePlayed { get; set; }
        [JsonProperty("longestKillSpree")]
        public ActivityStat LongestKillSpree { get; set; }
        [JsonProperty("longestSingleLife")]
        public ActivityStat LongestSingleLife { get; set; }
        [JsonProperty("mostPrecisionKills")]
        public ActivityStat MostPrecisionKills { get; set; }
        [JsonProperty("orbsDropped")]
        public ActivityStat OrbsDropped { get; set; }
        [JsonProperty("orbsGathered")]
        public ActivityStat OrbsGathered { get; set; }
        [JsonProperty("publicEventsCompleted")]
        public ActivityStat PublicEventsCompleted { get; set; }
        [JsonProperty("publicEventsJoined")]
        public ActivityStat PublicEventsJoined { get; set; }
        [JsonProperty("remainingTimeAfterQuitSeconds")]
        public ActivityStat RemainingTimeAfterQuitSeconds { get; set; }
        [JsonProperty("totalActivityDurationSeconds")]
        public ActivityStat TotalActivityDurationSeconds { get; set; }
        [JsonProperty("fastestCompletion")]
        public ActivityStat FastestCompletion { get; set; }
    }

    public class ActivityStat
    {
        [JsonProperty("statId")]
        public string StatId { get; set; }
        [JsonProperty("basic")]
        public AStatStruct Basic { get; set; }
    }

    public class AStatStruct
    {
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }
    }
}
