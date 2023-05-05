﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace XNVSU0_HFT_202231.Logic
{
    static class CustomAttributeExtension
    {
        public static string GetDisplayName(this PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return prop.Name;
            return attribute.DisplayName;
        }
        public static string GetDisplayName(this Type type)
        {
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return type.Name;
            return attribute.DisplayName;
        }
        public static string GetDisplayName(this object obj)
        {
            var type = obj.GetType();
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return type.Name;
            return attribute.DisplayName;
        }
    }
}
