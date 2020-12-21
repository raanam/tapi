using System;
using System.ComponentModel.DataAnnotations;

namespace TrolleyApi.Sort
{
    public class SortOptionValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var parsedValue = value as string;
            return Enum.IsDefined(typeof(SortOptions), parsedValue?.Trim()?.ToUpperInvariant());
        }
    }
}
