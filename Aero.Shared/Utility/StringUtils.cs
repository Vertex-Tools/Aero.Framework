// ====================================
// <copyright file="StringUtils.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Text;

namespace Aero.Shared.Utility
{
    public class StringUtils
    {
        private static readonly Random _random = new();

        public static string GenerateRandomPlate(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // <- All Chars for the template
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}