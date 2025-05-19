using UnityEngine;

public class ResidentSpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _isOccupied;
    
    public bool IsOccupied => _isOccupied;
    public Vector3 Position => transform.position;

    public void SetOccupied(bool occupied)
    {
        _isOccupied = occupied;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isOccupied ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
} 