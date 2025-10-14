## 📌 SOLID 원칙 적용

### 1️⃣ S - 단일 책임 원칙 (Single Responsibility Principle)

**정의: 한 클래스는 하나의 책임만 가진다.**

**적용 사례**

**과제 1**
```csharp
// ❌ Before (SRP 위반):
public class Tank : MonoBehaviour
{
    void Move() { }
    void Shoot() { }
    void TakeDamage() { }
}
/*
→ 변경 이유 3가지: 이동/공격/체력

✅ After (SRP 준수):
TankMoveComponent    → 변경 이유: 이동 방식만
TankShootComponent   → 변경 이유: 공격 방식만
TankHealthComponent  → 변경 이유: 체력 시스템만
*/
```

**과제 2**
```csharp
추가 컴포넌트도 SRP 적용:
DamageFlashComponent    → 변경 이유: 시각 이펙트만
DeathEffectComponent    → 변경 이유: 사망 이펙트만
EnemyAIComponent        → 변경 이유: AI 로직만
AutoShootComponent      → 변경 이유: 자동 발사만
```

**효과**
- 한 기능 수정 시 다른 기능에 영향 없음
- 코드 재사용성 증가
- 테스트 용이

---

### 2️⃣ O - 개방-폐쇄 원칙 (Open-Closed Principle)

**정의: 확장에는 열려있고, 수정에는 닫혀있어야 한다.**

**적용 사례**

**과제 1**
```
새 기능 추가:
gameObject.AddComponent<JumpComponent>();
→ 기존 코드 수정 없이 확장 ✅
```

**과제 2**
```csharp
// ❌ Before (OCP 위반)
void OnTriggerEnter(Collider other)
{
    TankHealthComponent health = other.GetComponent<TankHealthComponent>();
    if (health != null) health.TakeDamage(10);
    // TankHealthComponent에만 의존
}

// ✅ After (OCP 준수)
void OnTriggerEnter(Collider other)
{
    if (other.TryGetComponent<IDamageable>(out var target))
    {
        target.TakeDamage(10);
    }
    // IDamageable 구현한 모든 타입 지원
}

// 확장 예시 (Bullet 코드 수정 없이):
public class ShieldComponent : MonoBehaviour, IDamageable
public class BuildingHealth : MonoBehaviour, IDamageable
public class BossHealth : MonoBehaviour, IDamageable
→ 모두 자동 지원 ✅
```

**효과**
- 새 타입 추가 시 기존 코드 안전
- 확장성 극대화
- 버그 발생 위험 감소

---

### 3️⃣ L - 리스코프 치환 원칙 (Liskov Substitution Principle)

**정의: 부모 타입을 자식 타입으로 바꿀 수 있어야 한다.**

**적용 사례**

**과제 2**
```csharp
// IDamageable 타입으로 선언
IDamageable target;

// 어떤 구현체든 교체 가능
target = GetComponent<TankHealthComponent>();  ✅
target = GetComponent<ShieldComponent>();      ✅
target = GetComponent<BuildingHealth>();       ✅

// 모두 동일하게 동작
target.TakeDamage(10);
```

**실제 사용**
```csharp
void OnTriggerEnter(Collider other)
{
    if (other.TryGetComponent<IDamageable>(out var target))
    {
        // 실제 타입이 뭐든 상관없음
        target.TakeDamage(damage);
        // TankHealth, Shield, Building 모두 동작 ✅
    }
}
```

**효과**
- 구현체 교체 자유로움
- 다형성 활용
- 유연한 설계

---

### 4️⃣ I - 인터페이스 분리 원칙 (Interface Segregation Principle)

**정의: 인터페이스는 작게 분리해야 한다.**

**적용 사례**

**과제 2**
```csharp
// ✅ ISP 준수 (작은 인터페이스)
public interface IDamageable
{
    void TakeDamage(int damage);  // 1개만
}

public interface IHealable
{
    void Heal(int amount);
}

public interface IStunnable
{
    void Stun(float duration);
}

// 필요한 것만 구현
public class TankHealthComponent : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage) { }
}

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    public void TakeDamage(int damage) { }
    public void Heal(int amount) { }
}
```

**만약 큰 인터페이스였다면? (안티패턴)**
```
// ❌ ISP 위반
public interface IEntity
{
    void TakeDamage(int damage);
    void Heal(int amount);
    void Stun(float duration);
    void Buff(string buffName);
}

// 건물은 Heal/Stun 필요 없는데 강제 구현
public class Building : MonoBehaviour, IEntity
{
    public void TakeDamage(int damage) { }  // 사용
    public void Heal(int amount) { throw new NotImplementedException(); }
    public void Stun(float duration) { throw new NotImplementedException(); }
    public void Buff(string buffName) { throw new NotImplementedException(); }
}
```

**효과**
- 불필요한 메서드 구현 강제 안 함
- 인터페이스 목적 명확
- 결합도 감소

---

### 5️⃣ D - 의존성 역전 원칙 (Dependency Inversion Principle)

**정의: 구체 클래스가 아닌 추상(인터페이스)에 의존해야 한다.**

**적용 사례**

**과제 1 vs 과제 2**
```csharp
// ❌ DIP 위반 (과제 1)
void OnTriggerEnter(Collider other)
{
    // 구체 클래스에 의존
    TankHealthComponent health = other.GetComponent<TankHealthComponent>();
    if (health != null) health.TakeDamage(damage);
}

// ✅ DIP 준수 (과제 2)
void OnTriggerEnter(Collider other)
{
    // 인터페이스에 의존
    if (other.TryGetComponent<IDamageable>(out var target))
    {
        target.TakeDamage(damage);
    }
}
```

**이벤트 시스템에서도 DIP**
```csharp
// 발행자는 구독자를 모름 (추상에 의존)
public class TankHealthComponent : MonoBehaviour
{
    public event Action<int, int> OnDamaged;
    
    public void TakeDamage(int damage)
    {
        OnDamaged?.Invoke(currentHp, maxHp);
        // 누가 구독하는지 몰라도 됨
    }
}

// 구독자
public class DamageFlashComponent : MonoBehaviour
{
    void Start()
    {
        GetComponent<TankHealthComponent>().OnDamaged += Flash;
        // 새로 추가해도 TankHealthComponent 수정 안 함
    }
}
```

**의존 관계 다이어그램**

**Before (높은 결합)**
```
Bullet → TankHealthComponent
(구체 클래스에 직접 의존)
```

**After (낮은 결합)**
```
Bullet → IDamageable ← TankHealthComponent
                    ← ShieldComponent
                    ← BuildingHealth
(인터페이스에 의존, 구현체 교체 가능)
```

**효과**
- 느슨한 결합
- 테스트 용이 (Mock 객체)
- 확장성/유지보수성 증가

---

### SOLID 적용 요약표
| 원칙 | 과제 1 | 과제 2 | 핵심 적용 |
|------|--------|--------|----------|
| **S** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | 컴포넌트 분리 |
| **O** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | 인터페이스 확장 |
| **L** | - | ⭐⭐⭐⭐⭐ | 구현체 교체 |
| **I** | - | ⭐⭐⭐⭐⭐ | 작은 인터페이스 |
| **D** | - | ⭐⭐⭐⭐⭐ | 추상 의존 |
