using UnityEngine;
using Zenject;

public class CoinDropper : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _minCoins = 1;
    [SerializeField] private int _maxCoins = 3;
    [SerializeField] private float _dropRadius = 1f;

    [Inject] private DiContainer _container;

    public void DropCoins()
    {
        int coinCount = Random.Range(_minCoins, _maxCoins + 1);
        
        for (int i = 0; i < coinCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * _dropRadius;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            
            GameObject coinObject = _container.InstantiatePrefab(_coinPrefab, dropPosition, Quaternion.identity, null);
        }
    }
} 