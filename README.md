# UberLogger-LogChannels

Get compile time validation of channel names: channel names are available via IntelliSense.

Eliminate calls to muted channels at compile time. Logging to channels will have zero runtime cost.

# Usage

# Create channel definitions

Create a new asset of type LogChannels. Add a few enabled configurations, and a few channels which you would like to have exist. ![Example configuration](/AssetInspectorExample.png?raw=true)

# Use the channels

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

