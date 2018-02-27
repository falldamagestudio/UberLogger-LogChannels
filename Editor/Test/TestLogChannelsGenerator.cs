using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    [Category("Code")]
    public class TestLogChannelsGenerator
    {
        [Test]
        public void TestGenerateSingleEntryPoint()
        {
            string expectedResult =
                "namespace LogChannel\n" +
                "{\n" +
                "    public partial class Channel\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Message(string message)\n" +
                "        {\n" +
                "            UberDebug.LogChannel(\"Channel\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n";

            Assert.AreEqual(expectedResult, LogChannelsGenerator.Generate(LogChannelsConfig.Logger.UberLogger, "LogChannel", new List<LogChannelsConfig.Configuration> { LogChannelsConfig.Configuration.Editor, LogChannelsConfig.Configuration.Development }, "Channel", LogChannelsConfig.Severity.Message, true));
        }

        [Test]
        public void TestGenerateSingleChannel()
        {
            string expectedResult =

                // Channel 1

                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        public static void Message(string message)\n" +
                "        {\n" +
                "            UnityEngine.Debug.Log(message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        public static void Warning(string message)\n" +
                "        {\n" +
                "            UnityEngine.Debug.LogWarning(message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        public static void Error(string message)\n" +
                "        {\n" +
                "            UnityEngine.Debug.LogError(message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n";

            Assert.AreEqual(expectedResult, LogChannelsGenerator.Generate(
                LogChannelsConfig.Logger.Unity,
                "LogChannels",
                new List<LogChannelsConfig.Configuration> { LogChannelsConfig.Configuration.Editor, LogChannelsConfig.Configuration.Development },
                new List<LogChannelsConfig.ChannelAndMinSeverity> {
                    new LogChannelsConfig.ChannelAndMinSeverity("Channel1", LogChannelsConfig.MinSeverity.Warning),
                }));
        }

        [Test]
        public void TestGenerateMultipleChannels()
        {
            string expectedResult =

                // Channel 1

                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Message(string message)\n" +
                "        {\n" +
                "            UberDebug.LogChannel(\"Channel1\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Warning(string message)\n" +
                "        {\n" +
                "            UberDebug.LogWarningChannel(\"Channel1\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel1\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Error(string message)\n" +
                "        {\n" +
                "            UberDebug.LogErrorChannel(\"Channel1\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +

            // Channel 2

                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel2\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Message(string message)\n" +
                "        {\n" +
                "            UberDebug.LogChannel(\"Channel2\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel2\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Warning(string message)\n" +
                "        {\n" +
                "            UberDebug.LogWarningChannel(\"Channel2\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel2\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Error(string message)\n" +
                "        {\n" +
                "            UberDebug.LogErrorChannel(\"Channel2\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +

                // Channel 3

                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel3\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Message(string message)\n" +
                "        {\n" +
                "            UberDebug.LogChannel(\"Channel3\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel3\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"NOT_DEFINED\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Warning(string message)\n" +
                "        {\n" +
                "            UberDebug.LogWarningChannel(\"Channel3\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "namespace LogChannels\n" +
                "{\n" +
                "    public partial class Channel3\n" +
                "    {\n" +
                "        [System.Diagnostics.Conditional(\"UNITY_EDITOR\"), System.Diagnostics.Conditional(\"DEVELOPMENT_BUILD\")]\n" +
                "        [UberLogger.StackTraceIgnore]\n" +
                "        public static void Error(string message)\n" +
                "        {\n" +
                "            UberDebug.LogErrorChannel(\"Channel3\", message);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n";

            Assert.AreEqual(expectedResult, LogChannelsGenerator.Generate(
                LogChannelsConfig.Logger.UberLogger,
                "LogChannels",
                new List<LogChannelsConfig.Configuration> { LogChannelsConfig.Configuration.Editor, LogChannelsConfig.Configuration.Development },
                new List<LogChannelsConfig.ChannelAndMinSeverity> {
                    new LogChannelsConfig.ChannelAndMinSeverity("Channel1", LogChannelsConfig.MinSeverity.Message),
                    new LogChannelsConfig.ChannelAndMinSeverity("Channel2", LogChannelsConfig.MinSeverity.None),
                    new LogChannelsConfig.ChannelAndMinSeverity("Channel3", LogChannelsConfig.MinSeverity.Error),
                }));
        }
    }
}