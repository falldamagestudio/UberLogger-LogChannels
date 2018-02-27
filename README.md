# UberLogger-LogChannels

# Features

Get compile time validation of channel names: channel names are available via IntelliSense.

Eliminate calls to muted channels at compile time. Logging to muted channels will have zero runtime cost. Any logic used to create the log string will also be eliminated.

Set channel log filter levels individually: disable Message for some channel, leave it on for others. This allows you to place detailed logging in your different systems, ready to be activated for individual systems when it is time to investigate something in them.

# Usage

## Create channel definitions

Create a new asset of type LogChannels. Add a few enabled configurations, and a few channels which you would like to have exist. ![Example configuration](/AssetInspectorExample.png?raw=true)

## Use the channels

Instead of this:

```
Debug.Log("Wave Spawn Cinematic starts");
...
Debug.LogWarning(string.Format("Unit {0} stuck behind obstacle - teleporting it to its final destination", unit.name));
...
Debug.LogError(string.Format("Cannot serialize reference to {0}", asset.name));
```

Do this:

```
LogChannels.WaveSpawnCinematic.Message("Start");
...
LogChannels.UnitMovementDebug.LogWarning(string.Format("Unit {0} stuck behind obstacle - teleporting it to its final destination", unit.name));
...
LogChannels.NetworkableSerialization.LogError(string.Format("Cannot serialize reference to {0}", asset.name));
```

## Mute/unmute channels

Adjust the Min Severity setting for the channel in the configuration asset, and regenerate the code.