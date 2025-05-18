using UnityEngine;
using Zenject;


// Важно: Player должен быть только в виде префаба, не размещайте его на сцене вручную!
public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerController _playerPrefab;
    
    public override void InstallBindings()
    {
        // Configs
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        
        // Player
        Container.Bind<IPlayer>().To<PlayerController>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
        
        // Camera
        Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
    }
} 