using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[CustomEditor(typeof(LogChannelsConfig))]
public class LogChannelsEditor : Editor {

#if false
    private bool isConfigEnabled(LogChannelsConfig logChannelsConfig, LogChannelsGenerator.Configuration configuration)
    {
        return logChannelsConfig.EnabledConfigurations.Contains(configuration);
    }

    private void setConfigEnabled(LogChannelsConfig logChannelsConfig, LogChannelsGenerator.Configuration configuration, bool enabled)
    {
        if (enabled && !isConfigEnabled(logChannelsConfig, configuration))
            logChannelsConfig.EnabledConfigurations.Add(configuration);
        else if (!enabled && isConfigEnabled(logChannelsConfig, configuration))
            logChannelsConfig.EnabledConfigurations.Remove(configuration);
    }
#endif
    public override void OnInspectorGUI()
    {
        LogChannelsConfig logChannelsConfig = (LogChannelsConfig)target;

        serializedObject.Update();
#if false

        setConfigEnabled(logChannelsConfig, LogChannelsGenerator.Configuration.Editor, EditorGUILayout.Toggle("Editor logging", isConfigEnabled(logChannelsConfig, LogChannelsGenerator.Configuration.Editor)));
        setConfigEnabled(logChannelsConfig, LogChannelsGenerator.Configuration.Development, EditorGUILayout.Toggle("Development build logging", isConfigEnabled(logChannelsConfig, LogChannelsGenerator.Configuration.Development)));
#endif
        DrawDefaultInspector();

        if (GUILayout.Button("Generate code"))
        {
            string configAssetPath = AssetDatabase.GetAssetPath(logChannelsConfig);
            string configDirectory = Path.GetDirectoryName(configAssetPath);
            string generatedFilePath = Path.Combine(configDirectory, logChannelsConfig.GeneratedSourceFile);

            string generatedContentWithLF = LogChannelsGenerator.Generate(logChannelsConfig.OutputAPI, logChannelsConfig.NameSpace, logChannelsConfig.EnabledConfigurations, logChannelsConfig.Channels);
            string generatedContent = generatedContentWithLF.Replace("\n", Environment.NewLine);

            string originalContent = "";
            try
            {
                originalContent = File.ReadAllText(generatedFilePath);
            }
            catch
            {
            }

            if (originalContent != generatedContent)
            {
                File.WriteAllText(generatedFilePath, generatedContent);
                AssetDatabase.Refresh();
            }

        }

        serializedObject.ApplyModifiedProperties();
    }

}
