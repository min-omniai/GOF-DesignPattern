## 📌 Unity 컴포넌트 패턴 학습 정리

### 프로젝트 개요

- **목표**: 컴포넌트 패턴과 SOLID 원칙을 Unity로 학습

- **완료 과제**: 과제 1 (신입), 과제 2 (미들)

- **핵심 학습**: 상속 대신 조립, 인터페이스 기반 설계, 이벤트 통신

---

### 📚 과제1 - 기본 탱크 시스템 (신입 레벨)

**구현 내용**

**1. TankMoveComponent.cs**
```
기능:
- WASD 입력으로 이동/회전
- Rigidbody 기반 물리 이동
- 속도/회전 속도 조절 가능

💬 책임: 이동만 담당
```

**2. TankShootComponent.cs**
```
기능:
- 스페이스바 입력으로 총알 발사
- FirePoint에서 Bullet 프리팹 생성
- 쿨다운 시스템 (연사 제한)

💬 책임: 발사만 담당
```

**3. TankHealthComponent.cs**
```
기능:
- 체력 관리 (현재/최대)
- TakeDamage(int) 메서드로 데미지 처리
- 체력 0 되면 GameObject 파괴

💬 책임: 체력 관리만 담당
```

**4. Bullet.cs**
```
기능:
- 발사 방향으로 직진
- 적 충돌 시 데미지 및 자기 파괴
- 일정 시간 후 자동 파괴 (메모리 관리)

💬 책임: 투사체 동작만 담당
```

---

### 프로젝트 구조
```
Assets/
├─ Scripts/
│  ├─ Components/
│  │  ├─ Movement/
│  │  │  └─ TankMoveComponent.cs
│  │  ├─ Combat/
│  │  │  └─ TankShootComponent.cs
│  │  └─ Health/
│  │     └─ TankHealthComponent.cs
│  └─ Projectiles/
│     └─ Bullet.cs
└─ Prefabs/
   └─ Bullet.prefab
```

### GameObject 구성
```
PlayerTank
├─ TankMoveComponent
├─ TankShootComponent
├─ TankHealthComponent
├─ Rigidbody
├─ Collider
└─ FirePoint (Transform)
```
