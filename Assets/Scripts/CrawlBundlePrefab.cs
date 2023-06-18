
using System;
using System.Linq;
using UnityEngine;
using UTJ;

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
                DeepCopySpringBone(springBone, meshEditorObject, meshEditorGameObject);
            }
        }
    }

    // Returns a deep copy of the spring bone.
    static void DeepCopySpringBone(SpringBone original, GameObject target, GameObject targetRoot)
    {
        // New spring bone
        var nsb = target.AddComponent<SpringBone>();
        // use reflection and check if enabledJobSystem is True on the original SpringBone
        var originalEnabledJobField = original.GetType().GetField("enabledJobSystem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var enabledJobSystem = (bool)originalEnabledJobField.GetValue(original);
        // if it is, then set enabledJobSystem to be True on the new SpringBone
        // use reflection and set enabledJobSystem to be equal to that of the original SpringBone
        var nsbEnabledJobField = nsb.GetType().GetField("enabledJobSystem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        nsbEnabledJobField.SetValue(nsb, enabledJobSystem);
        nsb.jobColliders = original.jobColliders; // TODO deep copy these?
        nsb.validChildren = Array.Empty<Transform>();
        foreach (var validChild in original.validChildren)
        {
            // Get the name of the valid child
            var validChildName = validChild.name;
            // Find the object in meshEditorGameObject hierarchy has the same name as the validChild
            // (Could be one of the deep children)
            var meshEditorObject = target.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == validChildName)?.gameObject;
            Debug.Log(meshEditorObject);
            // Push to the nsb's validChildren
            nsb.validChildren = nsb.validChildren.Append(meshEditorObject.transform).ToArray();
        }
        nsb.stiffnessForce = original.stiffnessForce;
        nsb.dragForce = original.dragForce;
        nsb.springForce = original.springForce;
        nsb.windInfluence = original.windInfluence;
        //pivot node
        // Get the name of the pivot node
        var pivotNodeName = original.pivotNode.name;
        // Find the object in targetRoot hierarchy has the same name as the pivotNode
        // (Most likely a sibling. But I am not sure. Start at root.)
        var pivotNodeObject = targetRoot.GetComponentsInChildren<Transform>()
            .FirstOrDefault(c => c.gameObject.name == pivotNodeName)?.gameObject;
        Debug.Log(pivotNodeObject);
        nsb.pivotNode = pivotNodeObject.transform;
        nsb.angularStiffness = original.angularStiffness;
        nsb.yAngleLimits = original.yAngleLimits;
        nsb.zAngleLimits = original.zAngleLimits; 
        // nsb.lengthLimitTargets
        nsb.radius = original.radius;
        // TBD all the colliders (are they ever used?)
    }
}


