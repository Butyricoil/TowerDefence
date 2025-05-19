# Инфраструктура

## GameInstaller.cs

- Инсталлер Zenject: связывает зависимости для внедрения.

## GameManager.cs

- Управляет состоянием игры: старт, пауза, победа, поражение.

## LevelLoader.cs

- Загружает и переключает уровни.

---
**Пример взаимодействия:**  
`GameInstaller` → `GameManager`, `LevelLoader`
