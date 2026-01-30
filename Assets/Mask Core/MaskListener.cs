using UnityEngine;

public abstract class MaskListener : MonoBehaviour
{
    protected static Mask mask;

    void Awake()
    {
        if (mask == null)
        {
            mask = Resources.Load<Mask>("Mask");
        }
    }

    void OnEnable()
    {
        mask.maskChangedEvent.AddListener(MaskChange);
    }

    // Update is called once per frame
    void OnDisable()
    {
        mask.maskChangedEvent.RemoveListener(MaskChange);
    }

    public abstract void MaskChange(Masks mask);
}
