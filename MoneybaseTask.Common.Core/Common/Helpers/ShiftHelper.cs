// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ShiftHelper.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Common.Core.Common.Helpers;

public static class ShiftHelper
{
    public static Shift GetCurrentShift()
        => DateTime.UtcNow.Hour switch
        {
            >= 7 and < 15 => Shift.Morning,
            >= 15 and < 23 => Shift.Afternoon,
            _ => Shift.Night
        };
    
    public static bool IsCurrentShiftDuringOfficeHours()
        => GetCurrentShift() switch
        {
            Shift.Morning => true,
            Shift.Afternoon => true,
            Shift.Night => false,
            _ => false
        };
}