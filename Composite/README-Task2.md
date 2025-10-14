## 📌 Unity 컴포넌트 패턴 학습 정리

### 프로젝트 개요

- **목표**: 컴포넌트 패턴과 SOLID 원칙을 Unity로 학습

- **완료 과제**: 과제 1 (신입), 과제 2 (미들)

- **핵심 학습**: 상속 대신 조립, 인터페이스 기반 설계, 이벤트 통신

---

### 📚 과제 2 - 인터페이스 + 이벤트 + AI (미들 레벨)

**구현 내용**

**1. IDamageable 인터페이스**
```csharp
public interface IDamageable
{
    void TakeDamage(int damage);
}

목적: 데미지 처리 추상화
효과: 어떤 타입이든 공격 가능 (확장성)
```

**2. TankHealthComponent (개선)**
```
추가 사항:
- IDamageable 인터페이스 구현
- OnDamaged 이벤트 (int currentHp, int maxHp)
- OnDied 이벤트
- 프로퍼티로 외부 읽기 제공

효과: 느슨한 결합, 이벤트 기반 통신
```

**3. DamageFlashComponent**
```
기능:
- OnDamaged 이벤트 구독
- 피격 시 Material 색상 빨간색 변경
- 0.2초 후 원래 색으로 복구

💬 책임: 시각 피드백만 담당
```

**4. DeathEffectComponent**
```
기능:
- OnDied 이벤트 구독
- 사망 위치에 폭발 이펙트 생성
- 이펙트는 자동 파괴

💬 책임: 사망 이펙트만 담당
```

**5. EnemyAIComponent**
```
기능:
- 플레이어 탐지 (detectionRange)
- 거리 기반 행동 전환:
  * 15m 안: 추적
  * 8m 안: 정지 + 조준
- Transform 기반 이동/회전

💬 책임: AI 로직만 담당
```

**6. AutoShootComponent**
```
기능:
- 플레이어와의 거리 체크
- 사거리 안에 있으면 자동 발사
- 쿨다운 관리

💬 책임: 자동 발사만 담당
```
