
using System;
using System.Linq;
using UnityEngine;
using UTJ;
using UTJ.Jobs;

public class CrawlBundlePrefab
{
    // Crawl over the prefab and transfer spring bone information to the mesh edit game object 
    public static void Crawl(GameObject prefabGameObject, GameObject meshEditGameObject)
    {
        HandleSpringJobManagers(prefabGameObject, meshEditGameObject);
        HandleSpringBones(prefabGameObject, meshEditGameObject);
        HandleSpringBonePivots(prefabGameObject, meshEditGameObject);
    }

    static void HandleSpringBonePivots(GameObject prefabGameObject, GameObject meshEditGameObject)
    {
        Debug.Log("Entering HandleSpringBonePivots");
        // Find the objects in the tree that have the SpringBonePivot component on them
        var springBonePivots = prefabGameObject.GetComponentsInChildren<SpringBonePivot>();
        Debug.Log("Found " + springBonePivots.Length + " spring bone pivots");
        foreach (var springBonePivot in springBonePivots)
        {
            // Find the object in meshEditGameObject hierarchy has the same name as the springBonePivot
            // (Could be one of the deep children)
            var meshEditObject = meshEditGameObject.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == springBonePivot.name)?.gameObject;
            
            // continue if we didn't find a matching object
            if (meshEditObject == null)
            {
                Debug.Log("Couldn't find matching object for springBonePivot: " + springBonePivot.name);
                continue;
            }
            // Instantiate a new SpringBonePivot on the meshEditObject
            Debug.Log("Found long list sibling " + springBonePivot.name + " in meshEditGameObject");
            meshEditObject.AddComponent<SpringBonePivot>();
        }
        
        Debug.Log("Exiting HandleSpringBonePivots");
    }

    static void HandleSpringJobManagers(GameObject prefabGameObject, GameObject meshEditGameObject)
    {
        Debug.Log("Entering HandleSpringJobManagers");
        // Find the objects in the tree that have the SpringJobManager component on them
        var springJobManagers = prefabGameObject.GetComponentsInChildren<SpringJobManager>();
        Debug.Log("Found " + springJobManagers.Length + " spring job managers");

        foreach (var springJobManager in springJobManagers)
        {
            // Find the object in meshEditGameObject hierarchy has the same name as the springJobManager
            // (Could be one of the deep children)
            var meshEditObject = meshEditGameObject.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == springJobManager.name)?.gameObject;
            Debug.Log(meshEditObject);
            if (meshEditObject == null)
            {
                Debug.Log("Could not find " + springJobManager.name + " in meshEditGameObject");
            }
            else
            {
                Debug.Log("Found long list sibling " + springJobManager.name);
                DeepCopySpringJobManager(springJobManager, meshEditObject);
            }
            
        }
        
        Debug.Log("Exiting HandleSpringJobManagers");
    }


    static void HandleSpringBones(GameObject prefabGameObject, GameObject meshEditGameObject)
    {
        // Find the objects in the tree that have the SpringBone component on them
        var springBones = prefabGameObject.GetComponentsInChildren<SpringBone>();
        Debug.Log("Number of spring bones in prefab: " + springBones.Length);
        
        foreach (var springBone in springBones)
        {
            Debug.Log("Examine spring bone: " + springBone.name);
            // Find the object in meshEditGameObject hierarchy has the same name as the springBone
            // (Could be one of the deep children)
            var meshEditObject = meshEditGameObject.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == springBone.name)?.gameObject;
            if (meshEditObject == null)
            {
                Debug.Log("Could not find " + springBone.name + " in meshEditGameObject");
            }
            else
            {
                Debug.Log("Found long list sibling " + springBone.name + " in meshEditGameObject");
                DeepCopySpringBone(springBone, meshEditObject, meshEditGameObject);
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
            // Find the object in meshEditGameObject hierarchy has the same name as the validChild
            // (Could be one of the deep children)
            var meshEditObject = target.GetComponentsInChildren<Transform>()
                .FirstOrDefault(c => c.gameObject.name == validChildName)?.gameObject;
            Debug.Log(meshEditObject);
            if (!meshEditObject)
            {
                // Was unable to find a matching object. Skip it.
                Debug.Log("Wasn't able to find a match for " + validChildName);
                continue;
            }
            // Push to the nsb's validChildren
            nsb.validChildren = nsb.validChildren.Append(meshEditObject.transform).ToArray();
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
        if (pivotNodeObject != null)
        {
            nsb.pivotNode = pivotNodeObject.transform;
        } else
        {
            Debug.Log("Wasn't able to find a match for " + pivotNodeName + " pivot");
        }
        nsb.angularStiffness = original.angularStiffness;
        nsb.yAngleLimits = original.yAngleLimits;
        nsb.zAngleLimits = original.zAngleLimits; 
        // nsb.lengthLimitTargets
        nsb.radius = original.radius;
        // TBD all the colliders (are they ever used?)
    }

    static void DeepCopySpringJobManager(SpringJobManager original, GameObject target)
    {
        // New spring job manager
        var nsjm = target.AddComponent<SpringJobManager>();
        nsjm.optimizeTransform = original.optimizeTransform;
        nsjm.isPaused = original.isPaused;
        nsjm.simulationFrameRate = original.simulationFrameRate;
        nsjm.dynamicRatio = original.dynamicRatio;
        nsjm.gravity = original.gravity;
        nsjm.bounce = original.bounce;
        nsjm.friction = original.friction;
        nsjm.time = original.time;
        nsjm.enableAngleLimits = original.enableAngleLimits;
        nsjm.enableCollision = original.enableCollision;
        nsjm.enableLengthLimits = original.enableLengthLimits;
        nsjm.collideWithGround = original.collideWithGround;
        nsjm.groundHeight = original.groundHeight;
        nsjm.windDisabled = original.windDisabled;
        nsjm.windInfluence = original.windInfluence;
        nsjm.windPower = original.windPower;
        nsjm.windDir = original.windDir;
        nsjm.distanceRate = original.distanceRate;
        nsjm.automaticReset = original.automaticReset;
        nsjm.resetDistance = original.resetDistance;
        nsjm.resetAngle = original.resetAngle;
    }
    
}


