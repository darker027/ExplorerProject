using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoSingleton<PickableManager>
{
    [SerializeField] private List<IPickable> _pickables = new List<IPickable>();
    public void Register(IPickable pickable) => _pickables.Add(pickable);
    public void Unregister(IPickable pickable) => _pickables.Remove(pickable); 

    public IPickable AttemptPickup(Transform holderTransform)
    {
        int closest = ClosestPickableIndex(holderTransform.position);
        if (closest == -1) return null;
        return _pickables[closest].Pickup(holderTransform);
    }

    private int ClosestPickableIndex(Vector3 position)
    {
        float closestDist = float.MaxValue;
        int index = -1;
        for (int i = 0; i < _pickables.Count; i++)
        {
            float dist = _pickables[i].GetDistance(position);
            if (closestDist > dist)
            {
                closestDist = dist;
                index = i;
            }
        }

        if (index == -1) return -1;
        return index;
    }
}
