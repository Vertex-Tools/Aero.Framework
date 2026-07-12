// ====================================
// <copyright file="EventManager.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Reflection;
using Aero.API.Features;
using Avian.Event;

namespace Aero.Events
{
    public class EventManager
    {
        public void RegisterEvent(Dispatcher dispatcher)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var asm in  assemblies)
            {
                foreach (var type in asm.GetTypes())
                {
                    foreach (var method in type.GetMethods())
                    {
                        var attribute = method.GetCustomAttribute<AeroEventAttribute>();
                        if (attribute == null)
                            continue;
                        var eventType = attribute.EventType;
                        try
                        {
                            var actionType = typeof(Action<>).MakeGenericType(eventType);
                            object delegateInstance;
                            if (method.IsStatic)
                            {
                                delegateInstance = Delegate.CreateDelegate(actionType, method);
                            }
                            else
                            {
                                var instance = Activator.CreateInstance(type);
                                delegateInstance = Delegate.CreateDelegate(actionType, instance, method);
                            }

                            var subscribeMethod = dispatcher.GetType()
                                .GetMethod("Subscribe", new[] { actionType });
                            if (subscribeMethod != null)
                            {
                                subscribeMethod.Invoke(dispatcher, new[] { delegateInstance });
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error($"[Aero - EventManager] -> Error: {e.Message}");
                        }
                    }
                }
            }
        }
    }
}