
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
            // Normalize strings to lower case and remove diacritics
            s1 = RemoveDiacritics(s1.ToLower());
            s2 = RemoveDiacritics(s2.ToLower());

            // Remove special characters from the strings
            s1 = RemoveSpecialCharacters(s1);
            s2 = RemoveSpecialCharacters(s2);

            if (string.IsNullOrEmpty(s1))
            {
                return string.IsNullOrEmpty(s2) ? 0 : s2.Length;
            }

            if (string.IsNullOrEmpty(s2))
            {
                return s1.Length;
            }

            // Ensure s1 is the shorter string to optimize space complexity
            if (s1.Length > s2.Length)
            {
                var temp = s1;
                s1 = s2;
                s2 = temp;
            }

            int m = s1.Length;
            int n = s2.Length;
            int[] currentRow = new int[m + 1];
            int[] previousRow = new int[m + 1];

            // Initialize the first row
            for (int i = 0; i <= m; i++)
            {
                previousRow[i] = i;
            }

            for (int j = 1; j <= n; j++)
            {
                currentRow[0] = j;

                for (int i = 1; i <= m; i++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    currentRow[i] = Math.Min(
                        Math.Min(previousRow[i] + 1, currentRow[i - 1] + 1),
                        previousRow[i - 1] + cost);
                }

                // Swap rows for the next iteration
                var tempRow = previousRow;
                previousRow = currentRow;
                currentRow = tempRow;
            }

            return previousRow[m];
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