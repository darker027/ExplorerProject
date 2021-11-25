using System.Collections;
using UnityEngine;


public class PickableController : MonoBehaviour
{

    private IPickable _pickable;
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Transform _holdingTransform;
    public bool IsHolding { get => _pickable != null; }

    public bool Pickup()
    {
        IPickable pickable = PickableManager.Instance.AttemptPickup(_rootTransform);

        if (pickable == null) return false;

        _pickable = pickable;
        Transform trans = _pickable.GetTransform();
        Transform holdingOffset = _pickable.GetHoldingOffset();
        trans.parent = _holdingTransform;
        trans.localPosition = holdingOffset.localPosition;
        trans.localRotation = holdingOffset.localRotation;
        return true;
        
    }

    public bool SetPickable(IPickable pickable)
    {
        pickable.Pickup(_rootTransform);
        _pickable = pickable;
        Transform trans = _pickable.GetTransform();
        Transform holdingOffset = _pickable.GetHoldingOffset();
        trans.parent = _holdingTransform;
        trans.localPosition = holdingOffset.localPosition;
        trans.localRotation = holdingOffset.localRotation;
        return true;
    }

    public bool PutDown()
    {
        if (_pickable == null) return false;

        IPickable pickable = _pickable.Putdown();

        if (pickable != null) return false;
        
        StartCoroutine(WaitToPutDown());

        return true;
    }



    IEnumerator WaitToPutDown()
    {
        yield return new WaitForEndOfFrame();

        //if the pickable trasform exists
        if (_pickable.GetTransform()) 
        {
            Transform trans = _pickable.GetTransform();
            trans.parent = null;
        }
        
        _pickable = null;
    }

    public GameObject GetHoldingItem() 
    {
        if (IsHolding == true) 
        {
            return _pickable.GetTransform().gameObject;
        }
        else
        {
            return null;
        }
    }
    public IPickable GetPickable() 
    {
        return _pickable;
    }


}
