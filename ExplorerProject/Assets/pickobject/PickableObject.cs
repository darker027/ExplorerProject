using UnityEngine;
using UnityEditor;

public interface IPickable
{
    bool isPick { get; }
    IPickable Pickup(Transform holderTransform);
    IPickable Putdown();
    float GetDistance(Vector3 position);
    Transform GetTransform();
    Transform GetHoldingOffset();

}

[RequireComponent(typeof(Collider))]
public class PickableObject : MonoBehaviour, IPickable
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Transform _pickableOffset;
    [SerializeField] private Transform _holdingOffset;
    [SerializeField] private float _pickableDistance;
    [SerializeField] private bool _isBeingPickup;
    [SerializeField] private Transform _holderTransform;

    private Collider _collider;


    public delegate void OnBreakDelegate();
    public OnBreakDelegate OnBreak;

    public bool isPick => _isBeingPickup;

    public Vector3 PickableOffset { get => _pickableOffset.position; }
    public float PickableDistance { get => _pickableDistance; set { _pickableDistance = (value < 0) ? 0 : value; } }

    private void Awake() 
    {
        _collider = this.GetComponent<Collider>();

    }

    private void Start()
    {
        PickableManager.Instance.Register(this);
    }
    private void OnDestroy() => PickableManager.Instance.Unregister(this);

    public IPickable Pickup(Transform holderTransform)
    {
        if (_isBeingPickup) return null;
        if (Vector3.Distance(_pickableOffset.position, holderTransform.position) > _pickableDistance) return null;
        _isBeingPickup = true;
        this.transform.GetComponent<Rigidbody>().isKinematic = true; // Not sure if we will animate object to floating, or using physic.
        if (_collider != null) _collider.isTrigger = true;
        _holderTransform = holderTransform;
        return this;
    }

    public IPickable Putdown()
    {
        if (_isBeingPickup == false) return this;
        _isBeingPickup = false;
        this.transform.GetComponent<Rigidbody>().isKinematic = false; // Not sure if we will animate object to floating, or using physic.
        if (_collider != null) _collider.isTrigger = false;
        return null;
    }

    public float GetDistance(Vector3 position) => Vector3.Distance(_pickableOffset.position, position);

    public Transform GetTransform() => _rootTransform;

    public Transform GetHoldingOffset() => _holdingOffset;




    public Transform GetHolderTransform() => _holderTransform;

}

#if UNITY_EDITOR

[CustomEditor(typeof(PickableObject))]
public class PickableObjectEditor : Editor
{
    private void OnSceneGUI() {
        PickableObject t = (target as PickableObject);

        EditorGUI.BeginChangeCheck();
        float radius = Handles.RadiusHandle(Quaternion.identity, t.PickableOffset, t.PickableDistance);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Pickable Distance");
            t.PickableDistance = radius;
        }
    }
}
#endif
