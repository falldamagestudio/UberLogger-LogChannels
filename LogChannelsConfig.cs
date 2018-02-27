using System;
using System.Collections.Generic;
using UnityEngine;

public class LogChannelsConfig : ScriptableObject {

    public enum Logger { Unity, UberLogger };

    public enum Configuration { Editor, Development, Release };

    public enum Severity { Message, Warning, Error };

    public enum MinSeverity { Message, Warning, Error, None };

    [Serializable]
    public struct ChannelAndMinSeverity
    {
        [SerializeField]
        public string Channel;
        [SerializeField]
        public MinSeverity MinSeverity;

        public ChannelAndMinSeverity(string channel, MinSeverity minSeverity)
        {
            Channel = channel;
            MinSeverity = minSeverity;
        }
    }

    [SerializeField]
    public Logger OutputAPI;

    [SerializeField]
    public List<Configuration> EnabledConfigurations;

    [SerializeField]
    public List<ChannelAndMinSeverity> Channels;

    [SerializeField]
    public string NameSpace;

    [SerializeField]
    public string GeneratedSourceFile;
}
