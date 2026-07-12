// ====================================
// <copyright file="AeroEventAttribute.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;

namespace Aero.Events
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AeroEventAttribute : Attribute
    {
        public Type EventType { get; }
        
        public AeroEventAttribute(Type eventType)
        {
            EventType = eventType;
        }
    }
}