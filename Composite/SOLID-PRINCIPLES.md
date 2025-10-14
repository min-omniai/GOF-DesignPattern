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
