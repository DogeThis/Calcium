
using System.Linq;
using UnityEngine;
using UTJ;
using UTJ.GameObjectExtensions;

public class CrawlBundlePrefab
{
    public static void Crawl(GameObject prefabGameObject, GameObject meshEditorGameObject)
    {
        // Find the objects in the tree that have the SpringBone component on them
        var springBones = prefabGameObject.GetComponentsInChildren<SpringBone>();
        Debug.Log(springBones.Length);
        foreach (var springBone in springBones)
        {
            Debug.Log(springBone.name);
            // Find the object in meshEditorGameObject hierarchy has the same name as the springBone
            // (Could be one of the deep children)
            var meshEditorObject = meshEditorGameObject.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == springBone.name)?.gameObject;
            if (meshEditorObject == null)
            {
                Debug.Log("Could not find " + springBone.name + " in meshEditorGameObject");
                continue;
            }
            else
            {
                Debug.Log("Found long list sibling " + springBone.name + " in meshEditorGameObject");
                DeepCopySpringBone(springBone, meshEditorObject);
            }
        }
    }

    // Returns a deep copy of the spring bone.
    static void DeepCopySpringBone(SpringBone original, GameObject target)
    {
        var newSpringBone = target.AddComponent<SpringBone>();
        newSpringBone.radius = original.radius;
        newSpringBone.angularStiffness = original.angularStiffness;
        newSpringBone.capsuleColliders = original.capsuleColliders; // TODO deep copy the spring colliders
        newSpringBone.dragForce = original.dragForce;
        newSpringBone.jobColliders = original.jobColliders; // TODO deep copy these
    }

}


