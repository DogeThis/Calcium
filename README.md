1. Open the scene called Scene.
2. Drag in the "edit" mesh into the scene.
3. Drag the resulting gameobject into `BoneCopier`'s  "edited mesh"
4. Click "load prefab from assetbundle"
5. Click "crawl prefab"

Expectation:
the "edit" mesh will now have cloned `SpringBones`

TODO: Clone all other related SpringBone components

Things will get really messed up when you save becaused there's something strange about loading in bundles this way. BoneCopier and the cloned prefab (pink thing) needs to be deleted to save succesfully.
