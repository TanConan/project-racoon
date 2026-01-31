using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FaceMask : MonoBehaviour
{
    public ActiveMasks thisMask;
    public float speed;
    public float rotateSpeed;
    public float collectDistance;

    private bool collect;

    public void Update()
    {
        if (!collect) return;
        transform.SetPositionAndRotation(
            Vector3.MoveTowards(transform.position, Camera.main.transform.position, speed * Time.deltaTime),
            Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, rotateSpeed * Time.deltaTime)
        );

        if (Vector3.Distance(Camera.main.transform.position, transform.position) <= collectDistance)
        {
            FindFirstObjectByType<PlayerLook>().UnlockMask(thisMask);
            Destroy(gameObject);
        }
    }

    public void OnToggleMask()
    {
        collect = true;
    }
}
