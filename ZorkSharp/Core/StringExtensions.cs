using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorkSharp.Core
{
    public static class StringExtensions
    {
        private static readonly HashSet<string> UsesAn = new(StringComparer.OrdinalIgnoreCase)
        {
            "honest", "hour", "honor", "heir"
        };

        private static readonly HashSet<string> UsesA = new(StringComparer.OrdinalIgnoreCase)
        {
            "university", "unicorn", "uniform", "one"
        };

        extension(string name)
        {
            public string WithIndefiniteArticle()
            {
                if (string.IsNullOrWhiteSpace(name))
                    return name;

                string firstWord = name.Split(' ')[0];

                if (UsesAn.Contains(firstWord))
                    return $"an {name}";
                if (UsesA.Contains(firstWord))
                    return $"a {name}";

                char firstChar = char.ToLower(name[0]);
                string article = "aeiou".Contains(firstChar) ? "an" : "a";
                return $"{article} {name}";
            }

            public string WithDefiniteArticle()
            {
                if (string.IsNullOrWhiteSpace(name))
                    return name;

                return $"the {name}";
            }
        }
    }
}
