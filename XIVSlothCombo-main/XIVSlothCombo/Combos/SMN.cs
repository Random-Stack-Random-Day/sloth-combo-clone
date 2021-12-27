using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVSlothComboPlugin.Combos
{
    internal static class SMN
    {
        public const byte ClassID = 15;
        public const byte JobID = 27;

        public const float CooldownThreshold = 0.5f;

        public const uint
            // summons
            SummonRuby = 25802,
            SummonTopaz = 25803,
            SummonEmerald = 25804,

            SummonIfrit = 25805,
            SummonTitan = 25806,
            SummonGaruda = 25807,

            SummonIfrit2 = 25838,
            SummonTitan2 = 25839,
            SummonGaruda2 = 25840,

            SummonCarbuncle = 25798,

            // summon abilities
            Gemshine = 25883,
            PreciousBrilliance = 25884,
            DreadwyrmTrance = 3581,

            // summon ruins
            RubyRuin1 = 25808,
            RubyRuin2 = 25811,
            RubyRuin3 = 25817,
            TopazRuin1 = 25809,
            TopazRuin2 = 25812,
            TopazRuin3 = 25818,
            EmeralRuin1 = 25810,
            EmeralRuin2 = 25813,
            EmeralRuin3 = 25819,

            // summon outbursts
            RubyOutburst = 25814,
            TopazOutburst = 25815,
            EmeraldOutburst = 25816,

            // summon single targets
            RubyRite = 25823,
            TopazRite = 25824,
            EmeraldRite = 25825,

            // summon aoes
            RubyCata = 25832,
            TopazCata = 25833,
            EmeraldCata = 25834,

            // summon astral flows
            CrimsonCyclone = 25835, // dash
            CrimsonStrike = 25885, // melee
            MountainBuster = 25836,
            Slipstream = 25837,

            // demisummons
            SummonBahamut = 7427,
            SummonPhoenix = 25831,

            // demisummon abilities
            AstralImpulse = 25820, // single target bahamut gcd
            AstralFlare = 25821, // aoe bahamut gcd
            Deathflare = 3582, // damage ogcd bahamut
            EnkindleBahamut = 7429,

            FountainOfFire = 16514, // single target phoenix gcd
            BrandOfPurgatory = 16515, // aoe phoenix gcd
            Rekindle = 25830, // healing ogcd phoenix
            EnkindlePhoenix = 16516,

            // shared summon abilities
            AstralFlow = 25822,

            // summoner gcds
            Ruin = 163,
            Ruin2 = 172,
            Ruin3 = 3579,
            Ruin4 = 7426,
            Tridisaster = 25826,

            // summoner AoE
            RubyDisaster = 25827,
            TopazDisaster = 25828,
            EmeraldDisaster = 25829,

            // summoner ogcds
            EnergyDrain = 16508,
            Fester = 181,
            EnergySiphon = 16510,
            Painflare = 3578,

            // revive
            Resurrection = 173,

            // buff 
            SearingLight = 25801;

        public static class Buffs
        {
            public const short
                FurtherRuin = 2701,
                GarudasFavor = 2725,
                TitansFavor = 2853,
                IfritsFavor = 2724,
                EverlastingFlight = 16517;
        }

        public static class Levels
        {
            public const byte
                Painflare = 52,
                Ruin3 = 54,
                EnhancedEgiAssault = 74,
                Ruin4 = 62,
                EnergyDrain = 10,
                EnergySyphon = 52,
                EnhancedFirebirdTrance = 80,
                OutburstMastery2 = 82,
                Slipstream = 86,
                MountainBuster = 86,

                Bahamut = 70,
                Phoenix = 80,

                // summoner ruins lvls
                RubyRuin1 = 22,
                RubyRuin2 = 30,
                RubyRuin3 = 54,
                TopazRuin1 = 22,
                TopazRuin2 = 30,
                TopazRuin3 = 54,
                EmeralRuin1 = 22,
                EmeralRuin2 = 30,
                EmeralRuin3 = 54;
        }
    }

    internal class SummonerEDFesterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerEDFesterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Fester)
            {
                var gauge = GetJobGauge<SMNGauge>();
                var furtherRuin = HasEffect(SMN.Buffs.FurtherRuin);
                var edrainCD = GetCooldown(SMN.EnergyDrain);
                if (level >= SMN.Levels.EnergyDrain && !gauge.HasAetherflowStacks)
                    return SMN.EnergyDrain;
                if (furtherRuin && edrainCD.IsCooldown && !gauge.HasAetherflowStacks && IsEnabled(CustomComboPreset.SummonerFesterPainflareRuinFeature))
                    return SMN.Ruin4;
            }

            return actionID;
        }
    }

    internal class SummonerESPainflareCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerESPainflareCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Painflare)
            {
                var gauge = GetJobGauge<SMNGauge>();
                var furtherRuin = HasEffect(SMN.Buffs.FurtherRuin);
                var energysyphonCD = GetCooldown(SMN.EnergySiphon);
                if (level >= SMN.Levels.EnergySyphon && !gauge.HasAetherflowStacks)
                    return SMN.EnergySiphon;
                if (furtherRuin && energysyphonCD.IsCooldown && !gauge.HasAetherflowStacks && IsEnabled(CustomComboPreset.SummonerFesterPainflareRuinFeature))
                    return SMN.Ruin4;
            }

            return actionID;
        }
    }

    internal class SummonerMainComboFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerMainComboFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Ruin3 || actionID == SMN.Ruin2 || actionID == SMN.Ruin)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (IsEnabled(CustomComboPreset.SimpleSummoner))
                {
                    var bahaCD = GetCooldown(SMN.SummonBahamut);
                    var phoenixCD = GetCooldown(SMN.SummonPhoenix);
                    var incombat = HasCondition(Dalamud.Game.ClientState.Conditions.ConditionFlag.InCombat);
                    var buffCD = GetCooldown(SMN.SearingLight);

                    if (IsEnabled(CustomComboPreset.BuffOnSimpleSummoner) && gauge.IsBahamutReady && !bahaCD.IsCooldown && !buffCD.IsCooldown && incombat)
                        return SMN.SearingLight;

                    // Egis
                    if (gauge.IsTitanReady && !gauge.IsGarudaAttuned && !gauge.IsIfritAttuned && gauge.SummonTimerRemaining == 0 && bahaCD.IsCooldown && phoenixCD.IsCooldown && incombat)
                        return SMN.SummonTitan;
                    if (gauge.IsGarudaReady && !gauge.IsTitanAttuned && !gauge.IsIfritAttuned && gauge.SummonTimerRemaining == 0 && bahaCD.IsCooldown && phoenixCD.IsCooldown && incombat)
                        return SMN.SummonGaruda;
                    if (gauge.IsIfritReady && !gauge.IsGarudaAttuned && !gauge.IsTitanAttuned && gauge.SummonTimerRemaining == 0 && bahaCD.IsCooldown && phoenixCD.IsCooldown && incombat)
                        return SMN.SummonIfrit;

                    // Demi
                    if (gauge.IsBahamutReady && !gauge.IsPhoenixReady && gauge.AttunmentTimerRemaining == 0 && gauge.SummonTimerRemaining == 0 && incombat && !bahaCD.IsCooldown && buffCD.IsCooldown )
                        return SMN.SummonBahamut;
                    if (gauge.IsPhoenixReady && !gauge.IsBahamutReady && gauge.AttunmentTimerRemaining == 0 && gauge.SummonTimerRemaining == 0 && incombat && !phoenixCD.IsCooldown)
                        return SMN.SummonPhoenix;


                }
                if (IsEnabled(CustomComboPreset.SummonerLazyFesterFeature))
                {
                    var energyDrainCD = GetCooldown(SMN.EnergyDrain);
                    var astralimpulseCD = GetCooldown(SMN.AstralImpulse);
                    var fofCD = GetCooldown(SMN.FountainOfFire);
                    if (lastComboMove == SMN.AstralImpulse && gauge.HasAetherflowStacks && astralimpulseCD.CooldownRemaining > 0.99)
                        return SMN.Fester;
                    if (lastComboMove == SMN.AstralImpulse && gauge.HasAetherflowStacks && astralimpulseCD.CooldownRemaining > 0.2)
                        return SMN.Fester;
                    if ((lastComboMove == SMN.AstralImpulse && !gauge.HasAetherflowStacks && !energyDrainCD.IsCooldown && astralimpulseCD.CooldownRemaining > 0.95) || (lastComboMove == SMN.FountainOfFire && !gauge.HasAetherflowStacks && !energyDrainCD.IsCooldown && astralimpulseCD.CooldownRemaining > 0.95))
                        return SMN.EnergyDrain;
                }
                if (IsEnabled(CustomComboPreset.SummonerSingleTargetDemiFeature))
                {
                    var smnBahamut = GetCooldown(SMN.SummonBahamut);
                    var smnPhoenix = GetCooldown(SMN.SummonPhoenix);
                    var astralCD = GetCooldown(SMN.AstralImpulse);
                    var deathflare = GetCooldown(SMN.Deathflare);
                    var fountainfireCD = GetCooldown(SMN.FountainOfFire);
                    var enkindleBahamut = GetCooldown(SMN.EnkindleBahamut);
                    var enkindlePhoenix = GetCooldown(SMN.EnkindlePhoenix);
                    var rekindle = GetCooldown(SMN.Rekindle);

                    // Bahamut
                    if (level >= SMN.Levels.Bahamut && lastComboMove == SMN.AstralImpulse && !deathflare.IsCooldown && !gauge.HasAetherflowStacks && astralCD.CooldownRemaining > 0.99)
                        return SMN.Deathflare;
                    if (level >= SMN.Levels.Bahamut && lastComboMove == SMN.AstralImpulse && !deathflare.IsCooldown && !gauge.HasAetherflowStacks && astralCD.CooldownRemaining > 0.3)
                        return SMN.Deathflare;
                    if (level >= SMN.Levels.Bahamut && lastComboMove == SMN.AstralImpulse && deathflare.IsCooldown && !enkindleBahamut.IsCooldown && !gauge.HasAetherflowStacks && astralCD.CooldownRemaining > 0.99)
                        return SMN.EnkindleBahamut;
                    if (level >= SMN.Levels.Bahamut && lastComboMove == SMN.AstralImpulse && deathflare.IsCooldown && !enkindleBahamut.IsCooldown && !gauge.HasAetherflowStacks && astralCD.CooldownRemaining > 0.3)
                        return SMN.EnkindleBahamut;
                    // Phoenix
                    if (level >= SMN.Levels.Phoenix && lastComboMove == SMN.FountainOfFire && !enkindlePhoenix.IsCooldown && fountainfireCD.CooldownRemaining > 0.99)
                        return SMN.EnkindlePhoenix;
                    if (level >= SMN.Levels.Phoenix && lastComboMove == SMN.FountainOfFire && !enkindlePhoenix.IsCooldown && fountainfireCD.CooldownRemaining > 0.3)
                        return SMN.EnkindlePhoenix;
                    if (level >= SMN.Levels.Phoenix && lastComboMove == SMN.FountainOfFire && !rekindle.IsCooldown && fountainfireCD.CooldownRemaining > 0.3)
                        return SMN.Rekindle;
                }
                if (gauge.IsGarudaAttuned && HasEffect(SMN.Buffs.GarudasFavor) && IsEnabled(CustomComboPreset.SummonerGarudaUniqueFeature))
                    return SMN.Slipstream;
                else if (HasEffect(SMN.Buffs.TitansFavor) && lastComboMove == SMN.TopazRite && IsEnabled(CustomComboPreset.SummonerTitanUniqueFeature))
                    return SMN.MountainBuster;
                else if (gauge.IsIfritAttuned && HasEffect(SMN.Buffs.IfritsFavor) && IsEnabled(CustomComboPreset.SummonerIfritUniqueFeature))
                    return SMN.CrimsonCyclone;
                else if (gauge.IsIfritAttuned && !HasEffect(SMN.Buffs.IfritsFavor) && lastComboMove == SMN.CrimsonCyclone && IsEnabled(CustomComboPreset.SummonerIfritUniqueFeature))
                    return SMN.CrimsonStrike;

                if (IsEnabled(CustomComboPreset.SummonerEgiAttacksFeature))
                {
                    // low level 1-29
                    if (gauge.IsGarudaAttuned && level <= 29)
                        return SMN.EmeralRuin1;
                    else if (gauge.IsTitanAttuned && level <= 29)
                        return SMN.TopazRuin1;
                    else if (gauge.IsIfritAttuned && level <= 29)
                        return SMN.RubyRuin1;

                    // low level 30-53
                    if (gauge.IsGarudaAttuned && level >= 30 && level <= 53)
                        return SMN.EmeralRuin2;
                    else if (gauge.IsTitanAttuned && level >= 30 && level <= 53)
                        return SMN.TopazRuin2;
                    else if (gauge.IsIfritAttuned && level >= 30 && level <= 53)
                        return SMN.RubyRuin2;

                    // low level 54-71
                    if (gauge.IsGarudaAttuned && level >= 30 && level <= 71)
                        return SMN.EmeralRuin3;
                    else if (gauge.IsTitanAttuned && level >= 30 && level <= 71)
                        return SMN.TopazRuin3;
                    else if (gauge.IsIfritAttuned && level >= 30 && level <= 71)
                        return SMN.RubyRuin3;

                    if (gauge.IsGarudaAttuned && level >= 72)
                        return SMN.EmeraldRite;
                    else if (gauge.IsTitanAttuned && level >= 72)
                        return SMN.TopazRite;
                    else if (gauge.IsIfritAttuned && level >= 72)
                        return SMN.RubyRite;
                }

                if (IsEnabled(CustomComboPreset.SummonerRuin4ToRuin3Feature))
                {
                    if (level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && HasEffect(SMN.Buffs.FurtherRuin))
                        return SMN.Ruin4;
                }
                if (IsEnabled(CustomComboPreset.SummonerCarbuncleSummonFeature))
                {
                    var carbyPresent = Service.BuddyList.PetBuddyPresent;
                    if (!carbyPresent && gauge.SummonTimerRemaining == 0 && gauge.Attunement == 0 && gauge.AttunmentTimerRemaining == 0)
                            return SMN.SummonCarbuncle;
                }
            }

            return OriginalHook(actionID);
        }
    }

    internal class SummonerAOEComboFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerAOEComboFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Tridisaster)
            {
                var gauge = GetJobGauge<SMNGauge>();

                if (IsEnabled(CustomComboPreset.SummonerLazyFesterFeature))
                {
                    var energyDrainCD = GetCooldown(SMN.EnergyDrain);
                    var energySiphonCD = GetCooldown(SMN.EnergySiphon);
                    var astralimpulseCD = GetCooldown(SMN.AstralImpulse);
                    var astralflareCD = GetCooldown(SMN.AstralFlare);
                    var fofCD = GetCooldown(SMN.FountainOfFire);
                    if (lastComboMove == SMN.AstralFlare && gauge.HasAetherflowStacks && astralflareCD.CooldownRemaining > 0.99)
                        return SMN.Painflare;
                    if (lastComboMove == SMN.AstralFlare && gauge.HasAetherflowStacks && astralflareCD.CooldownRemaining > 0.2)
                        return SMN.Painflare;
                    if ((lastComboMove == SMN.AstralFlare && !gauge.HasAetherflowStacks && !energySiphonCD.IsCooldown && astralflareCD.CooldownRemaining > 0.95) || (lastComboMove == SMN.BrandOfPurgatory && !gauge.HasAetherflowStacks && !energySiphonCD.IsCooldown && astralflareCD.CooldownRemaining > 0.95))
                        return SMN.EnergySiphon;
                }
                if (IsEnabled(CustomComboPreset.SummonerAOEDemiFeature))
                {
                    var smnBahamut = GetCooldown(SMN.SummonBahamut);
                    var smnPhoenix = GetCooldown(SMN.SummonPhoenix);

                    var astralflareCD = GetCooldown(SMN.AstralFlare);
                    var deathflare = GetCooldown(SMN.Deathflare);
                    var brandofpurgaCD = GetCooldown(SMN.BrandOfPurgatory);
                    var enkindleBahamut = GetCooldown(SMN.EnkindleBahamut);
                    var enkindlePhoenix = GetCooldown(SMN.EnkindlePhoenix);
                    var rekindle = GetCooldown(SMN.Rekindle);

                    if (level >= SMN.Levels.Bahamut && lastComboMove == SMN.AstralFlare && !deathflare.IsCooldown && !deathflare.IsCooldown && astralflareCD.CooldownRemaining > 0.7)
                        return SMN.Deathflare;
                    else if (level >= SMN.Levels.Phoenix && lastComboMove == SMN.BrandOfPurgatory && !rekindle.IsCooldown && brandofpurgaCD.CooldownRemaining > SMN.CooldownThreshold)
                        return SMN.Rekindle;
                    else if (level >= SMN.Levels.Bahamut && (lastComboMove == SMN.Ruin4 || lastComboMove == SMN.Ruin3 || lastComboMove == SMN.Ruin2 || lastComboMove == SMN.Tridisaster) && (smnPhoenix.CooldownRemaining > 40 || smnBahamut.CooldownRemaining > 40) && !enkindleBahamut.IsCooldown && IsEnabled(CustomComboPreset.SummonerEnkindleWeave) && astralflareCD.CooldownRemaining > SMN.CooldownThreshold)
                        return SMN.EnkindleBahamut;
                    else if (level >= SMN.Levels.Bahamut &&
                        (lastComboMove == SMN.AstralFlare || lastComboMove == SMN.SummonBahamut)
                        && !enkindleBahamut.IsCooldown && astralflareCD.CooldownRemaining > SMN.CooldownThreshold)
                        return SMN.EnkindleBahamut;
                    else if (level > SMN.Levels.Phoenix &&
                        (lastComboMove == SMN.BrandOfPurgatory || lastComboMove == SMN.SummonPhoenix)
                        && !enkindlePhoenix.IsCooldown && brandofpurgaCD.CooldownRemaining > SMN.CooldownThreshold)
                        return SMN.EnkindlePhoenix;
                }

                if (gauge.IsGarudaAttuned && HasEffect(SMN.Buffs.GarudasFavor) && IsEnabled(CustomComboPreset.SummonerGarudaUniqueFeature))
                    return SMN.Slipstream;
                else if (HasEffect(SMN.Buffs.TitansFavor) && lastComboMove == SMN.TopazCata && IsEnabled(CustomComboPreset.SummonerTitanUniqueFeature))
                    return SMN.MountainBuster;
                else if (gauge.IsIfritAttuned && HasEffect(SMN.Buffs.IfritsFavor) && IsEnabled(CustomComboPreset.SummonerIfritUniqueFeature))
                    return SMN.CrimsonCyclone;
                else if (gauge.IsIfritAttuned && !HasEffect(SMN.Buffs.IfritsFavor) && lastComboMove == SMN.CrimsonCyclone && IsEnabled(CustomComboPreset.SummonerIfritUniqueFeature))
                    return SMN.CrimsonStrike;

                if (IsEnabled(CustomComboPreset.SummonerEgiAttacksFeature))
                {
                    // low level 1-29
                    if (gauge.IsGarudaAttuned && level >= 26 && level <= 73)
                        return SMN.EmeraldOutburst;
                    else if (gauge.IsTitanAttuned && level >= 26 && level <= 73)
                        return SMN.TopazOutburst;
                    else if (gauge.IsIfritAttuned && level >= 26 && level <= 73)
                        return SMN.RubyOutburst;

                    if (gauge.IsGarudaAttuned && level >= 82)
                        return SMN.EmeraldCata;
                    else if (gauge.IsGarudaAttuned && level <= 81)
                        return SMN.EmeraldDisaster;
                    else if (gauge.IsTitanAttuned && level >= 82)
                        return SMN.TopazCata;
                    else if (gauge.IsTitanAttuned && level <= 81)
                        return SMN.TopazDisaster;
                    else if (gauge.IsIfritAttuned && level >= 82)
                        return SMN.RubyCata;
                    else if (gauge.IsIfritAttuned && level <= 81)
                        return SMN.RubyDisaster;
                }

                if (IsEnabled(CustomComboPreset.SummonerRuin4ToTridisasterFeature))
                {
                    if (level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && HasEffect(SMN.Buffs.FurtherRuin))
                        return SMN.Ruin4;
                }
            }

            return actionID;
        }
    }
}