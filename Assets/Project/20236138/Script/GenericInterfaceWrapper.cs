using System;
using UnityEngine;

[Serializable]
public class GenericInterfaceWrapper<T1, T2> where T2 : UnityEngine.Object, T1
{
    T1 targetInterface;

    [SerializeField] T2 attachedObject;

    public T1 Interface
    {
        get
        {
            if (!attachedObject)
            {
#if UNITY_EDITOR
                Debug.LogError("No AttachedObject");
#endif
            }

            try
            {
                targetInterface ??= (T1)attachedObject;
            }
            catch (InvalidCastException e)
            {
#if UNITY_EDITOR
                Debug.LogError($"There is no correct script in {attachedObject}");
#endif
            }

            return targetInterface;
        }
    }
}