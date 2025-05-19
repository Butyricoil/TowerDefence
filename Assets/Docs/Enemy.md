# Враги

## EnemyController.cs

- Управляет поведением врага: патрулирование, атака, получение урона.

## EnemySpawner.cs

- Спавнит врагов на сцене по заданным точкам и условиям.

## EnemyHealth.cs

- Управляет здоровьем врага.

---
**Пример взаимодействия:**  
`EnemySpawner` → `EnemyController` ↔ `EnemyHealth`
