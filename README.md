1. Open the scene called Scene.
2. Bring the asset bundle you want to import into StreamingAssets
3. Set the bundle name in `BoneCopier`
4. Drag in the "edit" mesh into the scene.
5. Drag the resulting gameobject into `BoneCopier`'s  "edited mesh"
6. Click "load prefab from assetbundle"
7. Click "crawl prefab"

Expectation:
the "edit" mesh will now have cloned `SpringBones`

TODO: Clone all other related SpringBone components

Things will get really messed up when you save becaused there's something strange about loading in bundles this way. BoneCopier and the cloned prefab (pink thing) needs to be deleted to save succesfully.
