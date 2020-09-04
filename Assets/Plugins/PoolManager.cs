/*
    PoolManager for Unity3D
    Author: Kenneth "MrSpectacle" Vassbakk (kenneth.vassbakk@gmail.com)
    Version: 1.1

    Usage:
        There's no special need for setup.

        Instead of using GameObject.Instantiate();
        Use: PoolManager.Spawn(GameObject prefab, Vector3 Position, Quaternion Rotation);

        Instead of using GameObject.Destroy();
        Use: PoolManager.DeSpawn(GameObject prefab)
        
        - IMPORTANT: Start() {} will only run the first time an object is instantiated, 
            if you want to run something (i.e. reset HP, or anything you'd normally place in start) use OnEnable(); instead
            as well as OnDisable() for when the object is removed.

        Extra:
            You can preload a set number of GameObjects in the pool using:
            PoolManager.Preload(GameObject prefab, int Quantity);

            If for some reason you want to decrease the number of objects in a pool you can use:
            PoolManager.Clear(GameObject prefab, int NumberOfRemainingObjects);

            If for some reason you want to remove all objects in a pool you can use:
            PoolManager.Clear(GameObject prefab);

            If for some reason you want to remove all pools in the PoolManager, use:
            PoolManager.Clear();

    Variables:
    If you want to change the name of the GameObject in the scene that holds all the pooled objects,
    change the: private const string PARENT_NAME variable.

    Notes:
        - Using the Clear() functions will not remove any active objects, only pooled objects that are hidden.

    CHANGELOG
        1.1 28.08.2020 - UNITY_EDITOR
        Changed nesting functionality so that it only runs in the Editor. We do not need nesting of objects in the
        runtime environment.

        1.0 - Initial Version
*/


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PoolManager
{
    // You can avoid resizing the size of the list by setting this to a number equal or greater than
    // What you expect mos of your pool sizes to be, you can also use Preload() to set the initial size of a pool -- this can be handy.
    private const int DEFAULT_POOL_SIZE = 3;

    // A dictionary of all our _pools
    private static Dictionary<GameObject, Pool> _pools;

#if UNITY_EDITOR
    // Parent Object  created in the Unity Editor scene inspector.
    private const string PARENT_NAME = "PoolManager";

    // The parent which holds all the instantiated objects. 
    private static Transform _parent;
#endif

    /// <summary>
    /// Initialize our dictionary.
    /// </summary>
    /// <param name="prefab">GameObject Prefab</param>
    /// <param name="quantity">int Quantity</param>
    private static void Init(GameObject prefab = null, int quantity = DEFAULT_POOL_SIZE)
    {
        // If we currently have no pools, we need to initialize
        if (_pools == null)
        {
            _pools = new Dictionary<GameObject, Pool>();

#if UNITY_EDITOR
            if (_parent == null)
            {
                var p = GameObject.Find(PARENT_NAME) ?? new GameObject(PARENT_NAME);

                _parent = p.transform;
            }
#endif
        }

        // If we're not storing stuff, no need to continue from here.
        if (prefab == null || _pools.ContainsKey(prefab)) return;

        _pools[prefab] = new Pool(prefab, quantity);

#if UNITY_EDITOR
        if (_parent == null) return;
        var nestParent = _parent.Find(prefab.name + " Pool");

        if (nestParent == null)
        {
            var obj = new GameObject {name = prefab.name + " Pool"};
            obj.transform.SetParent(_parent);
            obj.transform.position = Vector3.zero;
            nestParent = obj.transform;
        }

        _pools[prefab].Parent = nestParent;
#endif
    }

    /// <summary>
    /// If you want to preload a few copies of an object at the start of a scene,
    /// you can use this. Really not needed unless your going from zero instances to more than ten.
    /// </summary>
    /// <param name="prefab">GameObject Prefab</param>
    /// <param name="quantity">int Quantity</param>
    public static void Preload(GameObject prefab, int quantity = 1)
    {
        // Initialize the prefab into our dictionary of _pools.
        // Does nothing if it's already in there.
        Init(prefab, quantity);

        // Make an array to grab the objects we're about to pre spawn
        var objs = new GameObject[quantity];
        for (var i = 0; i < quantity; i++) objs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);

        // Now despawn them all!
        for (var i = 0; i < quantity; i++) DeSpawn(objs[i]);
    }

    /// <summary>
    /// Method to clear the entire PoolManager
    /// </summary>
    public static void Clear()
    {
        while (_pools.Count > 0) _pools.FirstOrDefault().Value.Clear(0);

        if (_pools.Count == 0) _pools = null;

#if UNITY_EDITOR
        if (_parent.childCount != 0) return;
        Object.Destroy(_parent.gameObject);
        _parent = null;
#endif
    }

    /// <summary>
    /// A method to reduce the amount of pooled objects in a pool
    /// will remove available objects until it reaches the Qty.
    /// Pooled objects that are in use are ignored.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="quantity"></param>
    public static void Clear(GameObject prefab, int quantity)
    {
        if (_pools.ContainsKey(prefab))
            _pools[prefab].Clear(quantity);
    }

    /// <summary>
    /// Clear an entire object pool
    /// </summary>
    /// <param name="prefab">GameObject Prefab</param>
    public static void Clear(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab))
            _pools[prefab].Clear(0);
    }

    /// <summary>
    /// Removes a pool from the PoolManager
    /// </summary>
    /// <param name="prefab">GameObject prefab</param>
    private static void Dispose(GameObject prefab)
    {
        if (_pools[prefab].Parent.childCount == 0)
        {
            _pools[prefab].Parent.SetParent(null);
            Object.Destroy(_pools[prefab].Parent.gameObject);
        }

        _pools.Remove(prefab);
    }

    /// <summary>
    /// Spawns a copy of the specified prefab (instantiating one if required).
    /// NOTE: Remember that Awake() and/or Start() will only run on the very first spawn,
    ///       and that it's variables wont be reset. Use OnEnable() and OnDisable() instead.
    /// </summary>
    /// <param name="prefab">GameObject Prefab</param>
    /// <param name="position">Vector3 Position</param>
    /// <param name="rotation">Quaternion Rotation</param>
    /// <returns>Returns pooled Object</returns>
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // Initialize the prefab to the _pools.
        // Does nothing if it's already in there.
        if (prefab == null)
            throw new Exception("PoolManager - Attempted to initialize a null GameObject");

        Init(prefab);
        return _pools[prefab].Spawn(position, rotation);
    }


    /// <summary>
    /// Despawns a object in the pool.
    /// If the item is not created from a pool, it will be destroyed.
    /// </summary>
    /// <param name="obj"></param>
    public static void DeSpawn(GameObject obj)
    {
        var poolItem = obj.GetComponent<PoolItem>();
        if (poolItem)
        {
            poolItem.myPool.DeSpawn(obj);
        }
        else
        {
            Debug.Log("PoolManager - Object '" + obj.name + "' was not spawned from a pool. Destroying it instead.");
            Object.Destroy(obj);
        }
    }

    /// <summary>
    /// The Pool class is created for each different type of GameObject that is 
    /// put into the PoolManager.
    /// </summary>
    public class Pool
    {
        // We append an ID to the spawned GameObjects
        // Just for organizational benefits.
        private int _nextId;

        // The structure containing our available objects.
        // Using stack instead of a list, because we'll never need to pluck
        // an object from the start or middle of the array.
        // We'll always just grab the last one, which eliminates
        // any need to shuffle the objects around in memory
        private readonly Stack<GameObject> _available;

        // The prefab we are pooling
        private readonly GameObject _prefab;

        // The parent we should be setting our spawned objects to.
        public Transform Parent { get; set; }

        // Constructor
        public Pool(GameObject prefab, int initialQuantity)
        {
            _prefab = prefab;

            // Create the stack of available GameObjects.
            _available = new Stack<GameObject>(initialQuantity);
        }

        /// <summary>
        /// Spawn an object from our pool,
        /// or generate a new one.
        /// </summary>
        /// <param name="position">Vector3 Position</param>
        /// <param name="rotation">Quaternion Rotation</param>
        /// <returns>GameObject Pooled Object</returns>
        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            while (true)
            {
                GameObject obj;

                if (_available.Count == 0)
                {
                    // We don't have an available object in our pool
                    // so we have to instantiate a new one.
                    obj = Object.Instantiate(_prefab, position, rotation);
                    obj.name = _prefab.name + " (" + _nextId++ + ")";

                    // we add a PoolItem component so we know which pool we belong to
                    obj.AddComponent<PoolItem>().myPool = this;

                    // Set the parent of the object, if we have one.
                    if (Parent) obj.transform.SetParent(Parent);
                }
                else
                {
                    // We have an available object!
                    obj = _available.Pop();

                    if (obj == null)
                        continue;
                }

                // Time to set the properties of the object, and set it to active!
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        /// <summary>
        /// DeSpawns an object in our pool,
        /// setting it to disabled.
        /// </summary>
        /// <param name="obj">GameObject obj</param>
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public void DeSpawn(GameObject obj)
        {
            obj.SetActive(false);

#if UNITY_EDITOR
            // If  we have a parent, lets  reset this back.
            obj.transform.SetParent(Parent ? Parent : null);
#else
                // We're not in the editor, return to zero.
                obj.transform.SetParent(null);
#endif
            // Push the object back into the available stack.
            _available.Push(obj);
        }

        /// <summary>
        /// Removes objects from pool until it has less than Qty
        /// </summary>
        /// <param name="quantity">Int Quantity</param>
        public void Clear(int quantity)
        {
            while (_available.Count > quantity)
            {
                var obj = _available.Pop();

                // Removing the parent of the object, so that (if no children) we can remove the parent.
                // This won't be able to happen otherwise due to GarbageCollection.
                obj.transform.SetParent(null);
                Object.Destroy(obj);
            }

            // If the quantity was set to zero, destroy this pool.
            if (quantity == 0) Dispose(_prefab);
        }
    }

    /// <summary>
    /// This class is added to spawned items, so that we know which pool
    /// the item belongs to.
    /// </summary>
    public class PoolItem : MonoBehaviour
    {
        public Pool myPool;
    }
}