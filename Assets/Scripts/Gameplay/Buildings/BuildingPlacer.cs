using UnityEngine;
using Zenject;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask _placementLayer;
    [SerializeField] private Material _validPlacementMaterial;
    [SerializeField] private Material _invalidPlacementMaterial;
    
    private GameObject _currentBuilding;
    private bool _isPlacing;
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (!_isPlacing || _currentBuilding == null)
            return;
            
        UpdateBuildingPosition();
        UpdateBuildingRotation();
        UpdatePlacementValidity();
        
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceBuilding();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }
    
    public void StartPlacing(GameObject buildingPrefab)
    {
        if (_isPlacing)
            CancelPlacement();
            
        _currentBuilding = Instantiate(buildingPrefab);
        _isPlacing = true;
    }
    
    private void UpdateBuildingPosition()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _currentBuilding.transform.position = mousePosition;
    }
    
    private void UpdateBuildingRotation()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _currentBuilding.transform.Rotate(0, 0, 90f * Time.deltaTime);
        }
    }
    
    private void UpdatePlacementValidity()
    {
        bool isValid = IsValidPlacement();
        SetPlacementMaterial(isValid);
    }
    
    private bool IsValidPlacement()
    {
        // Check for collisions with other buildings or invalid terrain
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            _currentBuilding.transform.position,
            0.5f,
            _placementLayer
        );
        
        return colliders.Length == 0;
    }
    
    private void SetPlacementMaterial(bool isValid)
    {
        var renderers = _currentBuilding.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = isValid ? _validPlacementMaterial : _invalidPlacementMaterial;
        }
    }
    
    private void TryPlaceBuilding()
    {
        if (IsValidPlacement())
        {
            // Reset material to default
            var renderers = _currentBuilding.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material = null;
            }
            
            _isPlacing = false;
            _currentBuilding = null;
        }
    }
    
    private void CancelPlacement()
    {
        if (_currentBuilding != null)
        {
            Destroy(_currentBuilding);
        }
        
        _isPlacing = false;
        _currentBuilding = null;
    }
} 