# Камера

## PlayerCameraFollow.cs

- Камера плавно следует за целью с помощью Lerp и смещения.

## CameraController.cs

- Камера отслеживает игрока через интерфейс IPlayer (DI).
- Плавное движение реализовано через Lerp и параметр сглаживания.

---
**Пример взаимодействия:**  
`CameraController` ↔ `PlayerCameraFollow`
