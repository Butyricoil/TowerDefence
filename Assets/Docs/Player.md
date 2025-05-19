# Игрок

## PlayerController.cs

- Управляет основными действиями игрока: движение, прыжки, маунт, сбор монет.
- Использует Rigidbody2D для физики.
- Реализует интерфейс `IPlayer`.

## PlayerMovement.cs

- Обрабатывает ввод для движения и прыжков.
- Реализует ускорение и замедление.

## PlayerInputHandler.cs

- Получает ввод пользователя через InputSystem.
- Передаёт команды в PlayerController.

## PlayerConfig.cs

- ScriptableObject с параметрами игрока: скорость, сила прыжка, стартовые монеты.

## PlayerHealth.cs

- Управляет здоровьем игрока, обработка получения урона и смерти.

## PlayerInventory.cs

- Хранит и управляет предметами, собранными игроком.

---
**Пример взаимодействия:**  
`PlayerInputHandler` → `PlayerController` → `PlayerMovement`  
`PlayerController` ↔ `PlayerHealth`, `PlayerInventory`
