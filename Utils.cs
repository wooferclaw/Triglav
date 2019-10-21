using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Triglav
{
    public static class Utils
    {
        public static string SoundBankPattern = @"\[[^\[\]]+\]";

        public static readonly JsonSerializerSettings ConverterSettingsSnake = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static readonly JsonSerializerSettings ConverterSettingsCamel = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        
        public static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(a))
            {
                return b.Length;
            }
            if (string.IsNullOrEmpty(b))
            {
                return a.Length;
            }
            var lengthA = a.Length;
            var lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (var i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (var j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (var i = 1; i <= lengthA; i++)
            for (var j = 1; j <= lengthB; j++)
            {
                var cost = b[j - 1] == a[i - 1] ? 0 : 1;
                distances[i, j] = Math.Min
                (
                    Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost
                );
            }
            return distances[lengthA, lengthB];
        }

        public static double LevenshteinRatio(string a, string b)
        {
            var maxLen = Math.Max(a.Length, b.Length);
            if (maxLen == 0)
            {
                return 1.0;
            }

            var levDist = LevenshteinDistance(a, b);
            return (double)levDist / maxLen;
        }

        public static bool CheckTokens(IEnumerable<string> tokens, params string[] expected)
        {
            return expected.Any(expectedString =>
            {
                var expectedTokens = expectedString.Split(" ");
                return expectedTokens.All(tokens.ContainsStartWith);
            });
        }
        
        public static bool ContainsStartWith(this IEnumerable<string> list, string start)
        {
            return list.Any(element => element.ToLower().Trim().StartsWith(start));
        }
        
        public static void CheckLocale(Layer layer, Locale locale)
        {
            Locale[] supportedLocales;
            switch (layer)
            {
                case Layer.Alice:
                    supportedLocales = new[] {Locale.Ru};
                    break;
                case Layer.Alexa:
                    supportedLocales = new[] {Locale.En};
                    break;
                case Layer.Telegram:
                    supportedLocales = new[] {Locale.Ru, Locale.En};
                    break;
                case Layer.VK:
                    supportedLocales = new[] {Locale.Ru, Locale.En};
                    break;
                case Layer.Facebook:
                    supportedLocales = new[] {Locale.Ru, Locale.En};
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }

            if (!supportedLocales.Contains(locale))
            {
                throw new ArgumentOutOfRangeException(nameof(locale), "Locale is not supported on this layer");
            }
        }
    }
}