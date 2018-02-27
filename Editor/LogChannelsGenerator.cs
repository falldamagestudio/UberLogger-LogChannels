using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogChannelsGenerator
{
    public static Dictionary<LogChannelsConfig.Configuration, string> ConfigurationToString = new Dictionary<LogChannelsConfig.Configuration, string>
        {
            { LogChannelsConfig.Configuration.Editor, "UNITY_EDITOR" },
            { LogChannelsConfig.Configuration.Development, "DEVELOPMENT_BUILD" },
            { LogChannelsConfig.Configuration.Release, "NOT_DEFINED" },
        };

    public struct SeverityStrings
    {
        public readonly string CallMethod;
        public readonly string LogMethod;

        public SeverityStrings(string callMethod, string logMethod)
        {
            CallMethod = callMethod;
            LogMethod = logMethod;
        }
    }

    /// <summary>
    /// Unity config
    /// </summary>

    public static Dictionary<LogChannelsConfig.Severity, SeverityStrings> unitySeverityToStrings = new Dictionary<LogChannelsConfig.Severity, SeverityStrings>
        {
            { LogChannelsConfig.Severity.Message, new SeverityStrings("Message", "UnityEngine.Debug.Log") },
            { LogChannelsConfig.Severity.Warning, new SeverityStrings("Warning", "UnityEngine.Debug.LogWarning") },
            { LogChannelsConfig.Severity.Error, new SeverityStrings("Error", "UnityEngine.Debug.LogError") },
        };

    private static string unityMethodCallTemplate =
        "namespace {0}\n" +
        "{{\n" +
        "    public partial class {1}\n" +
        "    {{\n" +
        "        [{2}]\n" +
        "        public static void {3}(string message)\n" +
        "        {{\n" +
        "            {4}(message);\n" +
        "        }}\n" +
        "    }}\n" +
        "}}\n" +
        "\n";

    /// <summary>
    /// UberLogger config
    /// </summary>

    public static Dictionary<LogChannelsConfig.Severity, SeverityStrings> uberLoggerSeverityToStrings = new Dictionary<LogChannelsConfig.Severity, SeverityStrings>
        {
            { LogChannelsConfig.Severity.Message, new SeverityStrings("Message", "UberDebug.LogChannel") },
            { LogChannelsConfig.Severity.Warning, new SeverityStrings("Warning", "UberDebug.LogWarningChannel") },
            { LogChannelsConfig.Severity.Error, new SeverityStrings("Error", "UberDebug.LogErrorChannel") },
        };

    private static string uberLoggerMethodCallTemplate =
        "namespace {0}\n" +
        "{{\n" +
        "    public partial class {1}\n" +
        "    {{\n" +
        "        [{2}]\n" +
        "        [UberLogger.StackTraceIgnore]\n" +
        "        public static void {3}(string message)\n" +
        "        {{\n" +
        "            {4}(\"{1}\", message);\n" +
        "        }}\n" +
        "    }}\n" +
        "}}\n" +
        "\n";

    private struct APIFormat
    {
        public readonly Dictionary<LogChannelsConfig.Severity, SeverityStrings> SeverityToStrings;
        public readonly string MethodCallTemplate;

        public APIFormat(Dictionary<LogChannelsConfig.Severity, SeverityStrings> severityToStrings, string methodCallTemplate)
        {
            SeverityToStrings = severityToStrings;
            MethodCallTemplate = methodCallTemplate;
        }
    }

    private static Dictionary<LogChannelsConfig.Logger, APIFormat> apiToFormat = new Dictionary<LogChannelsConfig.Logger, APIFormat>
    {
        { LogChannelsConfig.Logger.Unity, new APIFormat(unitySeverityToStrings, unityMethodCallTemplate) },
        { LogChannelsConfig.Logger.UberLogger, new APIFormat(uberLoggerSeverityToStrings, uberLoggerMethodCallTemplate) },
    };

    public static string Generate(LogChannelsConfig.Logger outputAPI, string nameSpace, string conditionals, string channel, LogChannelsConfig.Severity severity)
    {
        APIFormat apiFormat = apiToFormat[outputAPI];
        SeverityStrings severityStrings = apiFormat.SeverityToStrings[severity];
        string methodCallTemplate = apiFormat.MethodCallTemplate;

        string formattedTemplate = string.Format(methodCallTemplate, nameSpace, channel, conditionals, severityStrings.CallMethod, severityStrings.LogMethod);
        return formattedTemplate;
    }

    public static string Generate(LogChannelsConfig.Logger outputAPI, string nameSpace, List<LogChannelsConfig.Configuration> enabledConfigurations, string channel, LogChannelsConfig.Severity severity, bool severityEnabled)
    {
        string conditionals = "";
        if (severityEnabled && (enabledConfigurations.Count > 0))
            conditionals = string.Join(", ", enabledConfigurations.Select(configuration => string.Format("System.Diagnostics.Conditional(\"{0}\")", ConfigurationToString[configuration])).ToArray());
        else
            conditionals = "System.Diagnostics.Conditional(\"NOT_DEFINED\")";

        return Generate(outputAPI, nameSpace, conditionals, channel, severity);
    }

    public static string Generate(LogChannelsConfig.Logger outputAPI, string nameSpace, List<LogChannelsConfig.Configuration> enabledConfigurations, List<LogChannelsConfig.ChannelAndMinSeverity> channelsAndMinSeverities)
    {
        string code = "";
        foreach (LogChannelsConfig.ChannelAndMinSeverity channelAndMinSeverity in channelsAndMinSeverities)
            foreach (LogChannelsConfig.Severity severity in Enum.GetValues(typeof(LogChannelsConfig.Severity)))
                code += Generate(outputAPI, nameSpace, enabledConfigurations, channelAndMinSeverity.Channel, severity, (int) severity >= (int) channelAndMinSeverity.MinSeverity);

        return code;
    }
}
