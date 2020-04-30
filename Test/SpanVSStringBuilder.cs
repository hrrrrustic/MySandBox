using System;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Test
{
    public class SpanVSStringBuilder
    {
        private const string FullName = "Steve J Gordon";

        public string GetLastNameUsingSubstring(string fullName)
        {
            var lastSpaceIndex = fullName.LastIndexOf(" ", StringComparison.Ordinal);

            return lastSpaceIndex == -1
                ? string.Empty
                : fullName.Substring(lastSpaceIndex + 1);
        }
        public ReadOnlySpan<char> GetLastNameWithSpan(ReadOnlySpan<char> fullName)
        {
            var lastSpaceIndex = fullName.LastIndexOf(' ');

            return lastSpaceIndex == -1
                ? ReadOnlySpan<char>.Empty
                : fullName.Slice(lastSpaceIndex + 1);
        }

        public string GetLastName(string fullName)
        {
            var names = fullName.Split(" ");

            var lastName = names.LastOrDefault();

            return lastName ?? string.Empty;
        }
        public string GetLastNameWithStringBuilder(string fullName)
        {
            StringBuilder builder = new StringBuilder(fullName);
            int count = 0;
            int ind = -1;
            for (int i = 0; i < builder.Length; i++)
            {
                if (builder[i] == ' ')
                {
                    if (count == 2)
                    {
                        ind = i;
                        break;
                    }
                    count++;
                }
            }
            return ind == -1 ? String.Empty : builder.ToString(ind, builder.Length - 1);
        }

        [Benchmark]
        public void GetLastName()
        {
            for (int i = 0; i < 1000; i++)
            {
                GetLastName(FullName);
            }
        }

        [Benchmark]
        public void GetLastNameUsingSubstring()
        {
            for (int i = 0; i < 1000; i++)
            {
                GetLastNameUsingSubstring(FullName);
            }
        }

        [Benchmark]
        public void GetLastNameWithSpan()
        {
            for (int i = 0; i < 1000; i++)
            {
                GetLastNameWithSpan(FullName);
            }
        }
        [Benchmark]
        public void GetLastNameWithStringBuilder()
        {
            for (int i = 0; i < 1000; i++)
            {
                GetLastNameWithStringBuilder(FullName);
            }
        }
    }
}