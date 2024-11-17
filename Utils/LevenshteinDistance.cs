
using System;
using System.Text;
using System.Globalization;

namespace CoeusV2.Utils
{
    public static class LevenshteinDistance
    {
        // Computes the Levenshtein distance between two strings with additional features
        public static int Compute(string s1, string s2)
        {
            s1 = NormalizeString(s1);
            s2 = NormalizeString(s2);

            if (string.IsNullOrEmpty(s1)) return string.IsNullOrEmpty(s2) ? 0 : s2.Length;
            if (string.IsNullOrEmpty(s2)) return s1.Length;

            if (s1.Length > s2.Length) (s1, s2) = (s2, s1);

            int m = s1.Length;
            int n = s2.Length;
            int[] currentRow = new int[m + 1];
            int[] previousRow = new int[m + 1];

            for (int i = 0; i <= m; i++) previousRow[i] = i;

            for (int j = 1; j <= n; j++)
            {
                currentRow[0] = j;
                for (int i = 1; i <= m; i++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    currentRow[i] = Math.Min(Math.Min(previousRow[i] + 1, currentRow[i - 1] + 1), previousRow[i - 1] + cost);
                }
                (previousRow, currentRow) = (currentRow, previousRow);
            }

            return previousRow[m];
        }

        private static string NormalizeString(string input)
        {
            return RemoveSpecialCharacters(RemoveDiacritics(input.ToLower()));
        }

        // Helper method to remove special characters from a string
        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        // Helper method to remove diacritics from a string
        private static string RemoveDiacritics(string str)
        {
            string normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}