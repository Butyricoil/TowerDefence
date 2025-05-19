using UnityEngine;
using Zenject;
public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerController _playerPrefab;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        // Configs
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

        // Camera
        Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();

        // Player
        Container.Bind<IPlayer>().To<PlayerController>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
    }
}