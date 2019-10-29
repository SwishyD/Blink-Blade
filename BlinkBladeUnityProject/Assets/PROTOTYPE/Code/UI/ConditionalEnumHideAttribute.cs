using System;
using System.Collections;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalEnumHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField = "";

    public int EnumValue1 = 0;
    public int EnumValue2 = 0;

    public bool HideInInspector = false;
    public bool Inverse = false;
    
    public ConditionalEnumHideAttribute(string conditionalSourceField, int enumValue1)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.EnumValue1 = enumValue1;
        this.EnumValue2 = enumValue1;
        this.HideInInspector = false;
    }

    public ConditionalEnumHideAttribute(string conditionalSourceField, int enumValue1, int enumValue2, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.EnumValue1 = enumValue1;
        this.EnumValue2 = enumValue2;
        this.HideInInspector = hideInInspector;
    }
}
