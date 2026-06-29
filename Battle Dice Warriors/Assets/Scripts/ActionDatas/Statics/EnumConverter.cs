using System;
using System.Collections.Generic;

public static class EnumConverter<T>
{
    /// <summary>
    /// Creates an enum of type MovementType.MovementKey from a list of enums.
    /// </summary>
    /// <param name="enums"></param>
    /// <returns></returns>
    public static T CreateEnumFrom(List<object> enums)
    {
        string enumText = "";

        for (int i = 0; i < enums.Count; i++)
        {
            string text = enums[i].ToString();
            enumText += text;

            if (i < enums.Count - 1)
                enumText += "_";
        }

        return (T)Enum.Parse(typeof(T), enumText, true);
    }

    public static string CreateStringFromEnum(AllowedDiceNumber allowedDiceNumber)
    {
        string enumText = allowedDiceNumber.ToString();
        enumText = enumText.Replace("D", "");
        return enumText.Replace("_", "-");
    }
}
