# Башни

## TowerController.cs

- Управляет логикой башни: поиск целей, стрельба, апгрейд.

## TowerSpawner.cs

- Позволяет размещать башни на сцене игроком.

## TowerUpgrade.cs

- Реализует улучшения башен.

---
**Пример взаимодействия:**  
`TowerSpawner` → `TowerController` ↔ `TowerUpgrade`
