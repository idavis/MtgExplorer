using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MtgExplorer.Entities;

namespace MtgExplorer.Mtg
{
    /// <summary>
    /// TODO: Create custom ability creation
    ///     Board wipe
    ///     Counter X where X is a type with 0-* subtypes
    ///     {T}: Add N to your mana pool where N is come color
    ///     Cost : Put a +- X / +- Y counter on type subtype
    ///     Unblockable
    ///     Must attack each turn if possible - Potential names: Wave Crashing, Relentless Assault
    ///     Identify battalion that effects the creature itself vs all attaching creatures.
    ///     Deal damage to creatue/player/planeswalker
    ///     Destroy creatue/player/planeswalker/land/artifact
    ///     Whenever {Card Name} attacks, side effect
    ///     Whenever {Card Name} attacks, pay Cost. Effect X
    ///     Whenever {Card Name} attacks, you may pay Cost. If you do, Effect X, see Hellkite Charger
    ///     When {Card Name} enters the battlefield, add {X} to your mana pool
    /// </summary>
    public static class AbilityRecognizer
    {
        public static readonly IEnumerable<IAbilityMatcher> Abilities = new IAbilityMatcher[]
            {
                new EntersTheBattlefieldAbilityGainXLife(),
                new FlyingAbilityMatcher(),
                new AddSingleManaToYourManaPool()
            };

        public static bool IsRecognized(string ability, CardInstance card)
        {
            foreach (var abilityMatcher in Abilities)
            {
                if (abilityMatcher.Matches(ability, card))
                {
                    return true;
                }
            }
            return false;
        }

        private const string MatchedParenRegexString = @"\(.*?\)";

        public static string[] GetAbilities(CardInstance card)
        {
            var abilities = new List<string>();
            foreach (var value in card.OracleCardText)
            {
                var test = IsRecognized(value, card);
                if (test)
                {
                    Console.WriteLine();
                }
                string local = Regex.Replace(value, MatchedParenRegexString, string.Empty).Trim();

                var found = AbilityContainer.AbilityWords.Where(item => local.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0);
                abilities.AddRange(found);

                found = AbilityContainer.KeywordAbilities.Where(item => local.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0);
                abilities.AddRange(found);

                found = AbilityContainer.CyclingAbilities.Where(item => local.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0);
                abilities.AddRange(found);

                found = AbilityContainer.CyclingAbility.Where(item => local.IndexOf(item, StringComparison.Ordinal) >= 0);
                abilities.AddRange(found);
            }
            var result = abilities.Distinct().ToArray();
            return result;
        }
    }

    public interface IGainLifeAbility
    {
        int LifeGain { get; set; }
    }

    public interface IKeywordAbility
    {
        string Keyword { get; }
    }

    public class FlyingAbility : IKeywordAbility
    {
        public string Keyword { get { return "Flying"; } }
    }

    public interface IAbilityMatcher
    {
        bool Matches(string text, CardInstance card);
    }

    public class FlyingAbilityMatcher : ParsedAbility
    {
        public override bool Matches(string text, CardInstance card)
        {
            return text.Trim().Equals("flying", StringComparison.OrdinalIgnoreCase);
        }
    }

    public abstract class ParsedAbility : IAbilityMatcher
    {
        public abstract bool Matches(string text, CardInstance card);
    }

    public class EntersTheBattlefieldAbility : ParsedAbility
    {
        public override bool Matches(string text, CardInstance card)
        {
            var pieces = text.Split(new[]{','}, StringSplitOptions.RemoveEmptyEntries);
            if (pieces.Length != 2)
                return false;
            string matching = string.Format("When {0} enters the battlefield", card.Name);
            return pieces[0].StartsWith(matching);
        }
    }

    public class EntersTheBattlefieldAbilityGainXLife : EntersTheBattlefieldAbility, IGainLifeAbility
    {
        public virtual bool Matches(string text, string cardName)
        {
            var pieces = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (pieces.Length != 2)
                return false;
            var match = Regex.Match(@"you gain (<x>\d+) life", pieces[1]);
            if (match.Success)
            {
                LifeGain = int.Parse(match.Groups["x"].Value);
            }
            return match.Success;
        }

        public int LifeGain { get; set; }
    }

    public class AddSingleManaToYourManaPool : EntersTheBattlefieldAbility
    {
        public override bool Matches(string text, CardInstance card)
        {
            if (!base.Matches(text, card))
            {
                return false;
            }
            // TODO: Use regex to match a value.
            string matching = string.Format("add {0} to your mana pool", null);
            return text.StartsWith(matching);
        }
    }
}