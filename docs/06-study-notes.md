# Study Notes

## Purpose

이 문서는 실습 중 주고받은 대화에서 나온 핵심 개념을 다시 보기 쉽게 정리한 학습 노트입니다.  
명령어 자체는 [docs/02-efcore-scaffold-cli.md](/Users/newkimjiwon/project/scaffold-lab/docs/02-efcore-scaffold-cli.md)를 보고, 개념이 헷갈릴 때는 이 문서를 참고합니다.

## Scaffold Vs Migration

- 스캐폴딩은 `DB -> 코드` 흐름입니다.
- 마이그레이션은 `코드 -> DB` 흐름입니다.
- 스캐폴딩은 이미 존재하는 DB 구조를 읽어 `DbContext`와 엔터티 클래스를 생성합니다.
- 마이그레이션은 현재 코드 모델과 이전 `ModelSnapshot`을 비교해 DB 변경 이력을 만듭니다.

짧게 기억하기:

- DB가 원본이면 스캐폴딩
- 코드가 원본이면 마이그레이션

## Why Both Exist In This Project

이 저장소에서는 두 흐름을 모두 체험하기 위해 DB 파일을 나눠 봅니다.

- `sample.db`
  - DBeaver 또는 SQLite CLI로 직접 만든 DB
  - 스캐폴딩 실습용
- `migration-demo.db`
  - 마이그레이션으로 새로 생성하거나 변경을 적용해보는 DB
  - 코드 우선 실습용

이렇게 나누는 이유는 `sample.db`에 이미 테이블이 있어서, 초기 마이그레이션을 그대로 적용하면 `table already exists` 충돌이 나기 때문입니다.

## Why `sample.db` Failed On `database update`

`sample.db`는 이미 `Users`, `Posts` 테이블이 들어 있는 DB입니다.  
그런데 초기 마이그레이션 `InitialScaffoldedSchema`는 비어 있는 DB를 전제로 `CreateTable`을 실행합니다.

그래서 아래와 같은 충돌이 났습니다.

```text
SQLite Error 1: 'table "Users" already exists'
```

이건 마이그레이션이 잘못된 게 아니라, 적용 대상 DB의 출발점이 다르기 때문에 생기는 정상적인 충돌입니다.

## `string?` Means Nullable

예를 들어 아래 코드가 있으면:

```csharp
public string? Gender { get; set; }
```

의미는:

- C#에서 `Gender`는 `null`일 수 있음
- 마이그레이션에서는 `nullable: true`
- SQLite에서는 `TEXT NULL`

반대로 `NOT NULL`로 만들고 싶다면 보통 아래처럼 씁니다.

```csharp
public string Gender { get; set; } = null!;
```

## `Up` And `Down`

마이그레이션 파일의 두 메서드는 서로 짝입니다.

- `Up()`
  - 앞으로 적용할 변경
- `Down()`
  - 그 변경을 되돌릴 때 실행할 반대 작업

예를 들어 `Gender` 컬럼 추가 마이그레이션이면:

- `Up()`은 `AddColumn`
- `Down()`은 `DropColumn`

## Migration Files Meaning

- `20260603090917_InitialScaffoldedSchema.cs`
  - 실제 변경 이력 파일
- `20260603090917_InitialScaffoldedSchema.Designer.cs`
  - 그 시점의 모델 메타데이터
- `SampleContextModelSnapshot.cs`
  - 현재 최신 모델 기준선

날짜시간 prefix는 스냅샷 그 자체가 아니라, 변경 이력 파일의 순서를 관리하는 이름입니다.

## Why `--context` Was Needed

이 프로젝트에는 컨텍스트가 여러 개 있습니다.

- `EfCoreScaffoldLab.Models.SampleContext`
- `EfCoreScaffoldLab.Models.Annotations.SampleContext`
- `EfCoreScaffoldLab.Models.RawNames.sampleContext`

그래서 마이그레이션을 만들 때 기준 컨텍스트를 명확히 지정해야 했습니다.

```bash
dotnet tool run dotnet-ef migrations add AddGenderToUsers --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
```

현재 메인 작업 대상은 `Models` 아래의 기본 컨텍스트입니다.

## `Annotations` And `RawNames`

지금 단계에서 `Annotations`와 `RawNames`는 비교용 결과물입니다.

- `Annotations`
  - `--data-annotations` 옵션 결과 비교용
- `RawNames`
  - `--use-database-names` 옵션 결과 비교용

현재 마이그레이션 실습에서는 이 파일들을 수정하지 않고, `Models` 쪽만 수정하면 됩니다.

## Why Migrations Are Not "One Time"

마이그레이션은 한 번만 쓰는 기능이 아닙니다.  
코드 모델이 바뀔 때마다 새로운 버전 이력을 추가하는 방식입니다.

예:

1. `InitialScaffoldedSchema`
2. `AddGenderToUsers`
3. `MakeGenderRequired`
4. `AddSummaryToPosts`

즉 마이그레이션은 `DB 버전 이력`을 계속 누적해 가는 시스템입니다.

## How To Avoid Data Loss

마이그레이션이 자동으로 데이터 손실을 막아주지는 않습니다.  
안전한 변경 순서를 설계해야 합니다.

추천 패턴:

1. 새 컬럼을 nullable로 추가
2. 기존 데이터 채우기
3. 필요하면 `NOT NULL`로 바꾸기

짧게:

- expand
- migrate data
- contract

위험한 작업 예:

- 컬럼 바로 삭제
- nullable을 바로 `NOT NULL`로 변경
- 컬럼 rename을 삭제+재생성처럼 처리

## Entities Vs Stored Procedures

이 SQLite 실습에서는 프로시저를 거의 신경 쓰지 않아도 됩니다.  
SQLite는 SQL Server 같은 의미의 Stored Procedure 중심 DB가 아니기 때문입니다.

일반적인 경향:

- 엔티티 중심 개발
  - LINQ, `Add`, `SaveChanges`로 CRUD를 많이 처리
  - 프로시저 의존도가 낮아짐
- 레거시 DB 중심 개발
  - 프로시저 사용이 많을 수 있음

즉 엔티티를 쓴다고 프로시저를 아예 안 쓰는 건 아니지만, 보통은 덜 쓰게 됩니다.

## EF Core Vs SQL Performance

속도는 `C#이라서 느리다`, `SQL이라서 빠르다`로 단순 비교하기 어렵습니다.

실제로는 아래가 더 중요합니다.

- 생성된 SQL 품질
- DB 왕복 횟수
- 가져오는 데이터 양
- 인덱스
- 집계를 DB에서 하는지, 메모리에서 하는지

보통:

- 단순 CRUD는 EF Core로도 충분한 경우가 많음
- 복잡한 집계, 대량 배치, 특수 튜닝은 SQL/프로시저가 더 유리할 수 있음

즉 핵심은 언어보다 설계와 쿼리 방식입니다.

## Practical Rule Of Thumb

- 기존 DB를 코드로 가져오고 싶다
  - 스캐폴딩
- 새 앱을 코드 중심으로 설계한다
  - 마이그레이션
- 기존 DB를 계속 DBA나 외부 시스템이 관리한다
  - 스캐폴딩 중심 유지
- 코드로 변경 이력과 배포를 관리하고 싶다
  - 초기에만 스캐폴딩하고 이후 마이그레이션으로 전환

## Personal Summary

이 프로젝트에서 가장 중요한 한 줄 정리:

`sample.db`는 DB 우선 흐름을 이해하기 위한 실습 DB이고, `migration-demo.db`는 코드 우선 흐름을 이해하기 위한 실습 DB입니다.
