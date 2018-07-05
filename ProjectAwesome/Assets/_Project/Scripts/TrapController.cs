using UnityEngine;

public class TrapController : MonoBehaviour, IAbility
{
    public GameObject m_TrapPrefab;

    public void Do()
    {
        PlaceTrap(transform.position, transform.rotation);
    }

    public void PlaceTrap(Vector3 position, Quaternion rotation)
    {
        Instantiate(m_TrapPrefab,position,rotation);
    }
}
