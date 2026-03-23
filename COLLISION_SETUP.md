# Space Invaders — Collision Setup Checklist

Follow this checklist **exactly** in the Unity Editor to make collisions work.
The corrected C# scripts in the `Scripts/` folder handle the code side — this covers the **Editor setup** they depend on.

---

## 1. Create Custom Tags

Go to **Edit → Project Settings → Tags and Layers** (or click **any object's Tag dropdown → Add Tag**).

Create these tags:

| Tag Name      | Used On             |
|---------------|---------------------|
| `Invader`     | Invader prefab      |
| `EnemyBullet` | EnemyBullet prefab  |

> **Don't skip this.** The scripts use `CompareTag()` — if the tag doesn't exist, Unity throws an error.

---

## 2. Create Custom Layers

In the same **Tags and Layers** panel, add these User Layers:

| Layer # | Name           |
|---------|----------------|
| 6       | `Player`       |
| 7       | `Invader`      |
| 8       | `PlayerBullet` |
| 9       | `EnemyBullet`  |

---

## 3. Assign Tags and Layers to Objects

| Object/Prefab     | Tag            | Layer          |
|--------------------|----------------|----------------|
| **Player**         | *(default)*    | `Player`       |
| **Invader** prefab | `Invader`      | `Invader`      |
| **Bullet** prefab  | *(default)*    | `PlayerBullet` |
| **EnemyBullet** prefab | `EnemyBullet` | `EnemyBullet` |

---

## 4. Physics Components — What Goes Where

### Player (in scene)
- **SpriteRenderer** — Triangle shape, green colour
- **PolygonCollider2D** — ✅ `Is Trigger = true`
- **Rigidbody2D** — Body Type: `Kinematic`, Gravity Scale: `0`
- **PlayerController** script — drag Bullet prefab into the `bulletPrefab` slot

### Bullet prefab
- **SpriteRenderer** — Square shape, yellow colour, Scale (0.1, 0.4, 1)
- **BoxCollider2D** — ✅ `Is Trigger = true`
- **Rigidbody2D** — Body Type: `Kinematic`, Gravity Scale: `0`
- **Bullet** script

### EnemyBullet prefab
- **SpriteRenderer** — Square shape, red/orange colour, Scale (0.1, 0.4, 1)
- **BoxCollider2D** — ✅ `Is Trigger = true`
- **Rigidbody2D** — Body Type: `Kinematic`, Gravity Scale: `0`
- **EnemyBullet** script

### Invader prefab
- **SpriteRenderer** — Square shape, red colour, Scale (0.7, 0.7, 1)
- **BoxCollider2D** — ✅ `Is Trigger = true`
- **Rigidbody2D** — Body Type: `Kinematic`, Gravity Scale: `0`  ← **THE PLAN MISSED THIS**
- **EnemyShooter** script — drag EnemyBullet prefab into the `bulletPrefab` slot

---

## 5. Layer Collision Matrix

Go to **Edit → Project Settings → Physics 2D** and scroll to **Layer Collision Matrix**.

**Uncheck** these pairs (they should NOT collide):

| Pair                            | Why                                    |
|----------------------------------|----------------------------------------|
| PlayerBullet ↔ PlayerBullet     | Bullets shouldn't hit each other       |
| EnemyBullet ↔ EnemyBullet      | Same                                   |
| Player ↔ PlayerBullet           | Don't shoot yourself                   |
| Invader ↔ EnemyBullet           | Invaders don't shoot themselves        |
| PlayerBullet ↔ EnemyBullet     | Bullets shouldn't cancel each other    |

Leave everything else checked (defaults).

---

## 6. Scene Hierarchy Checklist

Your Hierarchy should contain:

```
Main Camera          — Background: black, Size: 5
GameManager          — Empty GameObject with GameManager script
InvaderGrid          — Empty GameObject at (-4.8, 2, 0) with InvaderGrid script
Player               — Triangle sprite at (0, -4, 0) with PlayerController script
Canvas               — (Phase 6 — skip until then)
EventSystem          — (auto-created with Canvas)
```

### Phase 6 Addition: VictoryPanel

When you reach Phase 6 (UI), create **three** panels inside the Canvas:
- **StartPanel** — "Press any key to start" (inactive by default)
- **GameOverPanel** — "GAME OVER" + Restart button (inactive by default)
- **VictoryPanel** — "YOU WIN!" + Restart button (inactive by default)

Drag all three panels into the UIManager's Inspector slots. Wire both Restart buttons to `GameManager → RestartGame()`.

---

## 7. Quick Test

Press **Play** and verify:
1. ✅ Player moves left/right
2. ✅ Spacebar fires yellow bullets upward
3. ✅ Bullets destroy red invader squares on contact
4. ✅ Invaders fire red bullets downward
5. ✅ Red bullets hitting the player reduce lives (check Console: no errors)
6. ✅ At 0 lives, game pauses (Game Over)
7. ✅ Destroying all invaders pauses the game (Victory)
8. ✅ Invaders reaching the player's level triggers Game Over

> **Common fix:** If bullets pass through things, double-check that `Is Trigger = true` is set on ALL colliders (Player, Bullet, EnemyBullet, Invader).
