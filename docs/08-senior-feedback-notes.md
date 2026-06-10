# Senior Feedback Notes

## Purpose

이 문서는 최근 피드백에서 배운 내용을 따로 정리한 보완 노트입니다.  
기존 [docs/06-study-notes.md](/Users/newkimjiwon/project/scaffold-lab/docs/06-study-notes.md)가 개념 중심 정리라면, 이 문서는 실제 실무 관점에서 왜 그렇게 설계해야 하는지에 더 집중합니다.

## 1. `Annotations`, `RawNames`를 나눈 이유를 다시 이해하기

처음에는 `Models/Annotations`, `Models/RawNames`를 일종의 스냅샷 보관소처럼 생각하기 쉬웠습니다.  
하지만 시니어 피드백의 핵심은 아래에 가깝습니다.

- 이 폴더들은 `백업용`이라기보다 `옵션별 스캐폴딩 결과 비교용`이다.
- 한쪽 스캐폴딩 결과를 수정해 두고 나중에 덮어쓰기 방지용으로 쓰는 구조는 아니다.
- 스캐폴딩을 다시 돌릴 때 덮어써도 괜찮은 영역과, 내가 직접 관리할 영역을 분리해서 생각해야 한다.

즉 `Annotations`와 `RawNames`의 본질은:

- `Annotations`
  - `--data-annotations` 옵션을 썼을 때 결과가 어떻게 달라지는지 비교
- `RawNames`
  - `--use-database-names` 옵션을 썼을 때 이름이 어떻게 유지되는지 비교

정리하면 이 둘은 `실험 결과 보관본`에 가깝고, `내 커스텀 코드 보호 전략` 그 자체는 아닙니다.

## 2. 진짜로 보호해야 하는 것은 "내가 손으로 쓴 코드"

스캐폴딩 코드는 다시 생성될 수 있는 코드입니다.  
그래서 내가 수정할 파일은 스캐폴딩 결과와 섞이지 않게 관리하는 쪽이 안전합니다.

시니어 피드백을 실무적으로 풀면:

- 생성 코드 영역
  - 스캐폴딩이 다시 덮어써도 되는 영역
- 수기 작성 영역
  - 비즈니스 로직, 확장 로직, 별도 모델, 매핑 코드처럼 내가 유지해야 하는 영역

여기서 중요한 포인트는 `namespace`와 파일 역할을 분리해서, "이 파일은 생성물", "이 파일은 내가 관리"를 명확히 만드는 것입니다.

## 3. 실무에서 자주 쓰는 두 가지 방향

### 방향 A. 스캐폴딩 코드는 생성물로 두고, 직접 수정은 최소화

- 스캐폴딩 결과는 전용 폴더/namespace에 둔다.
- 애플리케이션에서 직접 쓰는 로직은 별도 파일에 둔다.
- 필요하면 서비스, DTO, 매퍼 계층에서 분리한다.

예를 들어:

- 생성 코드 namespace
  - `EfCoreScaffoldLab.Models.Scaffolded`
- 수기 코드 namespace
  - `EfCoreScaffoldLab.Models`
  - `EfCoreScaffoldLab.Services`
  - `EfCoreScaffoldLab.Features.Users`

이 방식의 장점은 스캐폴딩을 다시 돌려도 생성물과 수기 코드가 뒤섞이지 않는다는 점입니다.

### 방향 B. `partial class`로 확장하되, 생성 멤버는 복제하지 않기

현재 스캐폴딩 결과를 보면 `User`, `Post`, `SampleContext`가 `partial`로 생성됩니다.  
이 말은 같은 클래스 이름과 같은 namespace로 파일을 하나 더 만들어 확장할 수 있다는 뜻입니다.

예:

```csharp
namespace EfCoreScaffoldLab.Models;

public partial class User
{
    public string DisplayLabel => $"{DisplayName} <{Email}>";
}
```

이때 주의할 점:

- `partial` 확장은 가능하지만, 스캐폴딩된 속성을 다시 복사해서 넣는 방식은 아니다.
- 같은 클래스에 같은 멤버를 다시 선언하면 충돌 난다.
- 즉 "스캐폴딩에 적힌 내용은 빼고", 추가 로직만 넣는 식으로 관리해야 한다.

그래서 피드백에서 나온 `namespace를 고쳐야 한다`는 말은, 생성 코드와 수기 코드를 구분 가능한 구조로 잡으라는 뜻으로 이해하면 좋습니다.

## 4. 이번 프로젝트 기준으로 해석하면

현재 저장소에서는:

- `Models`
  - 메인 작업 대상
- `Models/Annotations`
  - 옵션 비교용
- `Models/RawNames`
  - 옵션 비교용

여기서 추가로 기억할 점은:

- `Annotations`, `RawNames`는 비교 실험용이므로 "여기에 수정본을 저장해 두자"는 발상과는 다르다.
- 스캐폴딩을 반복할 계획이라면, 생성 코드와 수기 코드를 분리하는 규칙을 별도로 정해야 한다.
- 수기 로직을 엔터티에 붙이고 싶다면 `partial class`를 활용하되, 생성된 멤버는 다시 쓰지 않는다.

## 5. `NOT NULL` 마이그레이션은 데이터 이관 단계가 꼭 필요하다

두 번째 피드백은 마이그레이션을 만들 때 아주 중요합니다.

상황:

- 새 컬럼을 추가하거나
- 기존 nullable 컬럼을 `NOT NULL`로 바꾸고 싶은데
- 이미 데이터가 들어 있는 테이블이라면

그냥 `NOT NULL`로 바꾸면 기존 행에서 문제가 생길 수 있습니다.  
기존 데이터에는 그 컬럼 값이 아직 없기 때문입니다.

예를 들어 `Users.Gender`가 기존에는 없었는데, 갑자기 `NOT NULL` 컬럼으로 추가하면:

- 기존 레코드는 `Gender` 값이 비어 있음
- DB는 `NULL`을 허용하지 않음
- 그래서 마이그레이션이 실패하거나, 임의 값 채우기 로직이 필요해짐

## 6. 안전한 패턴: nullable로 추가 -> 데이터 채우기 -> not null로 변경

이건 실무에서 자주 쓰는 기본 패턴입니다.

1. 컬럼을 우선 nullable로 추가한다.
2. 기존 데이터에 대해 적절한 기본값을 채운다.
3. 그 다음 `NOT NULL`로 바꾼다.

현재 프로젝트의 `AddGenderToUsers` 마이그레이션도 이 흐름으로 이해할 수 있습니다.

```csharp
migrationBuilder.AddColumn<string>(
    name: "Gender",
    table: "Users",
    type: "TEXT",
    nullable: true);

migrationBuilder.Sql(
    "UPDATE Users SET Gender = 'Unknown' WHERE Gender IS NULL");

migrationBuilder.AlterColumn<string>(
    name: "Gender",
    table: "Users",
    type: "TEXT",
    nullable: false,
    oldClrType: typeof(string),
    oldType: "TEXT",
    oldNullable: true);
```

핵심은 가운데 `UPDATE`입니다.  
EF Core가 기존 데이터에 무슨 값을 넣을지 자동으로 정해주지 않기 때문에, 이 단계는 개발자가 직접 책임지고 작성해야 합니다.

## 7. 왜 "임의의 값"도 설계가 필요한가

기존 데이터를 채울 때 `"Unknown"` 같은 값을 넣는 건 단순한 편의가 아니라 하나의 정책입니다.

생각해야 할 점:

- 정말 `"Unknown"`이 맞는가
- `0`, `-1`, `"N/A"` 같은 센티널 값이 더 맞는가
- 도메인상 기본값을 만들어도 되는가
- 차라리 한동안 nullable 상태로 두고 운영 데이터 이관을 먼저 해야 하는가

즉 `NOT NULL` 변경은 단순 문법 문제가 아니라, 기존 데이터를 어떤 의미로 해석할지 결정하는 작업입니다.

## 8. 이 피드백으로 정리한 실전 규칙

- `Annotations`, `RawNames`는 비교용 결과물로 이해한다.
- 생성 코드 보호와 수기 코드 보호는 별도 전략으로 가져간다.
- 스캐폴딩을 반복할 예정이면 생성 영역과 수기 영역을 폴더/namespace 기준으로 나눈다.
- 엔터티를 확장할 때는 `partial class`를 쓰되, 생성된 멤버는 다시 선언하지 않는다.
- `NOT NULL` 마이그레이션은 기존 데이터 처리 전략 없이 바로 들어가면 위험하다.
- 안전한 순서는 `nullable 추가 -> 데이터 채우기 -> not null 변경`이다.

## Personal Summary

이번 피드백에서 가장 중요했던 한 줄 정리:

`Annotations`와 `RawNames`는 수정본 백업 폴더가 아니라 비교용 결과물이고, 실제로 지켜야 하는 것은 스캐폴딩 생성물과 내가 직접 쓴 코드를 구조적으로 분리하는 습관입니다.  
또한 `NOT NULL` 마이그레이션은 스키마 변경만이 아니라 기존 데이터를 어떤 값으로 이관할지까지 직접 설계해야 합니다.
