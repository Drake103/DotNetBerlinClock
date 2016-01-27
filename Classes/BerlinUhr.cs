using System;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    internal sealed class BerlinUhr
    {
        private const int HoursInFirstRowCell = 5;
        private const int FirstRowCellCountMax = 4;

        private const int SecondRowCellCountMax = 4;

        private const int MinutesInThirdRowCell = 5;
        private const int ThirdRowCellCountMax = 11;

        private const int ForthRowMaxCellCountMax = 4;

        private readonly int _hours;
        private readonly int _minutes;
        private readonly int _seconds;

        public BerlinUhr(int hours, int minutes, int seconds)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }
        
        /// <summary>
        /// Returns string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            // Lamp part
            sb.AppendLine(getLamp());

            // First row part
            int firstRowCellCount;
            sb.AppendLine(getFirstRow(out firstRowCellCount));

            // Second row part
            sb.AppendLine(getSecondRow(firstRowCellCount));

            // Third row part
            int thirdRowCellCount;
            sb.AppendLine(getThirdRow(out thirdRowCellCount));

            // Forth row part
            sb.Append(getFourthRow(thirdRowCellCount));

            return sb.ToString();
        }


        private string getLamp()
        {
            bool secondsNumIsEven = _seconds % 2 == 0;

            return secondsNumIsEven
                ? CellBackground.Yellow
                : CellBackground.Empty;
        }

        private string getFirstRow(out int aFirstRowCellCount)
        {
            aFirstRowCellCount = _hours / HoursInFirstRowCell;

            var sb = new StringBuilder();

            appendValue(sb, CellBackground.Red, aFirstRowCellCount);
            appendValue(sb, CellBackground.Empty, FirstRowCellCountMax - aFirstRowCellCount);

            return sb.ToString();
        }

        private string getSecondRow(int aFirstRowCellCount)
        {
            var sb = new StringBuilder();
            int secondRowCellsCount = _hours - aFirstRowCellCount * HoursInFirstRowCell;

            appendValue(sb, CellBackground.Red, secondRowCellsCount);
            appendValue(sb, CellBackground.Empty, SecondRowCellCountMax - secondRowCellsCount);

            return sb.ToString();
        }

        private string getThirdRow(out int thirdRowCellCount)
        {
            var sb = new StringBuilder();
            thirdRowCellCount = _minutes / MinutesInThirdRowCell;

            for (int i = 0; i < thirdRowCellCount; i++)
            {
                bool isEveryThird = (i + 1) % 3 == 0;

                sb.Append(isEveryThird ? CellBackground.Red : CellBackground.Yellow);
            }

            appendValue(sb, CellBackground.Empty, ThirdRowCellCountMax - thirdRowCellCount);
            return sb.ToString();
        }

        private string getFourthRow(int aThirdRowCellCount)
        {
            var sb = new StringBuilder();
            int forthRowCellCount = _minutes - aThirdRowCellCount * MinutesInThirdRowCell;

            appendValue(sb, CellBackground.Yellow, forthRowCellCount);
            appendValue(sb, CellBackground.Empty, ForthRowMaxCellCountMax - forthRowCellCount);

            return sb.ToString();
        }


        private static class CellBackground
        {
            public const string Empty = "O";
            public const string Yellow = "Y";
            public const string Red = "R";
        }

        /// <summary>
        /// Appends aVal to aSb string builder aTimes times. 
        /// </summary>
        /// <param name="aSb">String builder append to</param>
        /// <param name="aVal">Value to append</param>
        /// <param name="aTimes">How many times to append</param>
        private static void appendValue(StringBuilder aSb, string aVal, int aTimes)
        {
            for (int i = 0; i < aTimes; i++)
            {
                aSb.Append(aVal);
            }
        } 
    }
}