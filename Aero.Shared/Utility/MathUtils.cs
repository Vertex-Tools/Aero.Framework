// ====================================
// <copyright file="MathUtils.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.Shared.Utility
{
    /// <summary>
    /// Provides utility methods for mathematical calculations.
    /// </summary>
    public static class MathUtils
    {
        public static float CalculateDistance3D(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            float deltaX = x1 - x2;
            float deltaY = y1 - y2;
            float deltaZ = z1 - z2;
            
            return (float) System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
    // TODO: Add more math methods
}