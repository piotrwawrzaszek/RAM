using System;
using System.Globalization;
using System.Windows.Data;
using RAM.Domain.Helpers;
using RAM.Domain.Helpers.Extensions;

namespace RAM.Infrastructure.Converters
{
    public class InstructionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is Instruction instruction) || instruction == Instruction.Empty)
                return string.Empty;
            return instruction.ConvertToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is string instruction) || instruction == string.Empty)
                return Instruction.Empty;
            return instruction.ConverToEnum<Instruction>();
        }
    }
}