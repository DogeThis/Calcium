1. Start by putting the bundle you want to use in the StreamingAssets folder.
1. Then make sure the target FBX you want to input to, is somewhere in your project, with the usual import settings shenanigans (scaling?)
1. Load the "Scene" file.
1. Ensure that the bundle's name from step 1, is in the Path To Asset Bundle (really, this is just the "name" of asset bundle")
1. Click Load prefab button
(expectation: pink blob appears)
1. Drag the FBX into the scene
1. Then drag the FBX game object into Edited Mesh slot.
Click "crawl"
1. (expectation: at the point, the spring bone information is now on the edited mesh gameobject in the scene)
1. Click "Unload all...")
1. Save the scene.
1. Build the project.
1. Open (don't move) the level0 in your favorite external bundle manager.

Expectation:
the "edit" mesh will now have cloned `SpringBones`

Things will get really messed up when you save becaused there's something strange about loading in bundles this way. BoneCopier and the cloned prefab (pink thing) needs to be deleted to save succesfully.
