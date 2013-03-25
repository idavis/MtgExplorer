namespace MtgExplorer.Mtg
{
    /// <summary>
    /// TODO: separate abilities that stack vs. redundant
    /// </summary>
    public class AbilityContainer
    {
        /// <summary>
        /// An ability word appears in italics at the beginning of some abilities. Ability words are
        /// similar to keywords in that they tie together cards that have similar functionality, but they have
        /// no special rules meaning and no individual entries in the Comprehensive Rules. The ability
        /// words are battalion, bloodrush, channel, chroma, domain, fateful hour, grandeur, hellbent,
        /// imprint, join forces, kinship, landfall, metalcraft, morbid, radiance, sweep, and threshold.
        /// </summary>
        public static readonly string[] AbilityWords = new []
            {
                "Battalion",
                "Bloodrush",
                "Channel",
                "Chroma",
                "Domain",
                "Fateful Hour",
                "Grandeur",
                "Hellbent",
                "Imprint",
                "Join Forces",
                "Kinship",
                "Landfall",
                "Metalcraft",
                "Morbid",
                "Radiance",
                "Sweep",
                "Threshold"
            };

        // Cycling [cost]
        // [Type]cycling [cost]
        // This type is usually a subtype (as in “mountaincycling”) but can be any card type, subtype, supertype, or combination thereof (as in “basic landcycling”).
        public static readonly string[] CyclingAbility =new []{ "Cycling"}; 

        public static readonly string[] CyclingAbilities = new []
            {
                "Landcycling",
                "Basic landcycling",
                "Mountaincycling",
                "Forestcycling",
                "Plainscycling",
                "Slivercycling",
                "Swampcycling",
                "Islandcycling",
                "Wizardcycling" // Vedalken Æthermage
            };

        public static readonly string[] KeywordAbilities = new []
            {
                "Deathtouch",
                "Defender",
                "Double Strike",
                "Enchant",
                "Equip", // Equip [cost]
                "First Strike",
                "Flash",
                "Flying",
                "Haste",
                "Hexproof",
                "Intimidate",
                "Landwalk", // need to check for prefixed supertypes (nonbasic, snow, etc)
                "Lifelink",
                "Protection",
                // written: Protection from [quality], or Protection from [quality A] and from [quality B], Protection from all [characteristic], Protection from everything
                "Reach",
                "Shroud",
                "Trample",
                "Vigilance",
                "Banding",
                "Rampage", // Rampage N
                "Cumulative Upkeep",
                // Cumulative upkeep {W} or {U}, Cumulative upkeep {B}, Cumulative upkeep sacrifice an island, Cumulative upkeep—Pay 1 life
                "Flanking",
                "Phasing",
                "Buyback", // Buyback [cost]
                "Shadow",
                "Echo", // Echo [cost]
                "Horsemanship",
                "Fading", // Fading N
                "Kicker",
                // Kicker [cost], The phrase "Kicker [cost 1] and/or [cost 2]" means the same thing as "Kicker [cost 1], kicker [cost 2]."
                // Multikicker is a variant of the kicker ability. “Multikicker [cost]” means “You may pay an additional [cost] any number of times as you cast this spell.” A multikicker cost is a kicker cost.
                "Flashback", // Flashback [cost]
                "Madness", // static ability while in hand plus Madness [cost]
                "Fear",
                "Morph",
                "Amplify",
                "Provoke",
                "Storm",
                "Affinity", // Affinity for [text]
                "Entwine", // Entwine [cost]
                "Modular", // Modular N
                "Sunburst",
                "Bushido", // Bushido N
                "Soulshift", // Soulshift N
                "Splice", // Splice onto [subtype] [cost]
                "Offering", // [Subtype] offering
                "Ninjutsu", // Ninjutsu [cost]
                "Epic",
                "Convoke",
                "Dredge", // Dredge N
                "Transmute", // Transmute [cost]
                "Bloodthirst", // Bloodthirst N
                "Haunt",
                "Replicate", // Replicate [cost]
                "Forecast", // Forecast — [Activated ability]
                "Graft", // Graft N
                "Recover", // Recover [cost]
                "Ripple", // Ripple N
                "Split Second",
                "Suspend", // Suspend N—[cost]
                "Vanishing", // Vanishing N
                "Absorb", // Absorb N
                "Aura Swap", // Aura swap [cost]
                "Delve",
                "Fortify", // Fortify [cost]
                "Frenzy", // Frenzy N
                "Gravestorm",
                "Poisonous", // Poisonous N
                "Transfigure", // Transfigure [cost]
                "Champion", // Champion an [object]
                "Changeling",
                "Evoke", // Evoke [cost]
                "Hideaway",
                "Prowl", // Prowl [cost]
                "Reinforce", // Reinforce N—[cost]
                "Conspire",
                "Persist",
                "Wither",
                "Retrace",
                "Devour", // Devour N
                "Exalted",
                "Unearth", // Unearth [cost]
                "Cascade",
                "Annihilator", // Annihilator N
                "Level Up", // Level Up [cost]
                "Rebound",
                "Totem Armor",
                "Infect",
                "Battle Cry",
                "Living Weapon",
                "Undying",
                "Miracle", // Miracle [cost]
                "Soulbond",
                "Overload", // Overload [cost]
                "Scavenge", // Scavenge [cost]
                "Unleash",
                "Cipher",
                "Evolve",
                "Extort"
            };
    }
}