// ====================================
// <copyright file="ConfigBuilder.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Aero.Core;

public class ConfigBuilder
{
    /// <summary>
    /// Yaml Deserializer.
    /// </summary>
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    
    /// <summary>
    /// Yaml Serializer.
    /// </summary>
    private static readonly ISerializer Serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    /// <summary>
    /// Loads a config file.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static object LoadConfig(Type type, string path)
    {
        if (!File.Exists(path))
        {
            var instance = Activator.CreateInstance(type);
            SaveConfig(instance, path);
            return instance;
        }
        var yaml = File.ReadAllText(path);
        return Deserializer.Deserialize(yaml, type);
    }

    /// <summary>
    /// Saves a config file.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="path"></param>
    public static void SaveConfig(object config, string path)
    {
        var yaml = Serializer.Serialize(config);
        File.WriteAllText(path, yaml);
    }
}