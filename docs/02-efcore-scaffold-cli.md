# EF Core Scaffold CLI

## Goal

맥북 터미널에서 바로 복사해서 실행할 수 있도록, SQLite 파일 생성부터 EF Core 스캐폴딩까지의 최소 실습 명령어를 정리합니다.

## Assumptions

- 저장소 루트는 `scaffold-lab`입니다.
- 실습용 .NET 프로젝트는 `src/EfCoreScaffoldLab`에 생성합니다.
- SQLite DB 파일은 `DBeaver` 또는 `sqlite3`로 준비합니다.
- `dotnet`이 시스템에 없으면 저장소 로컬 SDK 방식으로 설치할 수 있습니다.

## 0. Optional: Local .NET SDK Install

이 저장소에서는 전역 설치 대신 프로젝트 안에 로컬 SDK를 두는 방식도 사용할 수 있습니다.

공식 참고 문서:
- [docs/05-reference-links.md](/Users/newkimjiwon/project/scaffold-lab/docs/05-reference-links.md)

```bash
curl -fsSL https://dot.net/v1/dotnet-install.sh -o .dotnet-install.sh
bash .dotnet-install.sh --channel 10.0 --install-dir ./.dotnet
```

이후 문서의 `dotnet` 명령은 아래처럼 바꿔서 실행할 수 있습니다.

```bash
./.dotnet/dotnet --info
```

## 0.5. Recommended: Session Environment Setup

로컬 SDK 방식을 계속 사용할 때는 매번 명령 앞에 긴 경로를 붙이는 대신, 현재 터미널 세션에 환경변수를 먼저 설정하는 편이 훨씬 편합니다.

`src/EfCoreScaffoldLab` 폴더 안에 들어와 있다면 아래 두 줄을 먼저 실행합니다.

```bash
export DOTNET_CLI_HOME=../../.dotnet_cli_home
export PATH=../../.dotnet:$PATH
```

정상 적용 확인:

```bash
dotnet --version
```

예상 결과:

```text
10.0.300
```

이 설정은 현재 터미널 세션에서만 유지됩니다.  
터미널을 새로 열면 다시 실행해야 합니다.

이후에는 아래처럼 짧은 명령으로 진행할 수 있습니다.

```bash
dotnet tool restore
dotnet tool run dotnet-ef --version
dotnet run
```

## 1. Project Create

전역 `dotnet`이 있으면 그대로 실행합니다.

```bash
mkdir -p src
dotnet new console -o src/EfCoreScaffoldLab
cd src/EfCoreScaffoldLab
```

로컬 SDK를 쓰는 경우에는 아래처럼 실행합니다.

```bash
mkdir -p src/EfCoreScaffoldLab
cd src/EfCoreScaffoldLab
DOTNET_CLI_HOME=$PWD/../../.dotnet_cli_home ../../.dotnet/dotnet new console --force
```

## 2. Package Install

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

로컬 SDK를 쓰는 경우 예시는 아래와 같습니다.

```bash
DOTNET_CLI_HOME=$PWD/../../.dotnet_cli_home ../../.dotnet/dotnet add package Microsoft.EntityFrameworkCore.Design
DOTNET_CLI_HOME=$PWD/../../.dotnet_cli_home ../../.dotnet/dotnet add package Microsoft.EntityFrameworkCore.Sqlite
DOTNET_CLI_HOME=$PWD/../../.dotnet_cli_home ../../.dotnet/dotnet new tool-manifest
DOTNET_CLI_HOME=$PWD/../../.dotnet_cli_home ../../.dotnet/dotnet tool install dotnet-ef
```

이미 `export DOTNET_CLI_HOME=../../.dotnet_cli_home`와 `export PATH=../../.dotnet:$PATH`를 적용했다면 아래처럼 더 짧게 실행할 수 있습니다.

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

이미 `dotnet-ef`가 설치되어 있다면 마지막 명령은 실패할 수 있습니다.  
그 경우에는 아래 명령으로 버전만 확인하면 됩니다.

```bash
dotnet ef --version
```

로컬 도구 매니페스트를 사용하는 경우:

```bash
dotnet tool run dotnet-ef --version
```

## 3. SQLite File Create

현재 학습 흐름에서는 `DBeaver + SQLite`를 기본 도구 조합으로 사용합니다.

### Option A. DBeaver로 생성

1. DBeaver에서 새 연결 생성
2. 데이터베이스 타입으로 `SQLite` 선택
3. DB 파일 경로를 `src/EfCoreScaffoldLab/sample.db`로 지정
4. 연결 후 SQL Editor에서 아래 SQL 실행

```sql
CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL,
    DisplayName TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    PhoneNumber TEXT
);

CREATE TABLE Posts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Content TEXT,
    CreatedAt TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### Option B. sqlite3로 생성

`sqlite3`가 설치되어 있다면 아래 명령으로 샘플 DB와 테이블을 바로 만들 수 있습니다.

```bash
sqlite3 sample.db
```

SQLite 프롬프트가 열리면 아래 SQL을 붙여넣습니다.

```sql
CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL,
    DisplayName TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    PhoneNumber TEXT
);

CREATE TABLE Posts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Content TEXT,
    CreatedAt TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

INSERT INTO Users (Email, DisplayName, CreatedAt)
VALUES ('alice@example.com', 'Alice', '2026-06-03');

INSERT INTO Posts (UserId, Title, Content, CreatedAt)
VALUES (1, 'First Post', 'Hello EF Core Scaffold', '2026-06-03');

.tables
.schema Users
.quit
```

## 4. First Scaffold

가장 기본형입니다.

```bash
dotnet ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models
```

로컬 도구 방식:

```bash
../../.dotnet/dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models
```

세션 환경변수를 이미 설정했다면 아래처럼 실행합니다.

```bash
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models
```

## 5. Variation A: Data Annotations

```bash
dotnet ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models/Annotations --data-annotations --force
```

## 6. Variation B: Use Database Names

```bash
dotnet ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models/RawNames --use-database-names --force
```

## 7. Compare Results

생성 파일을 빠르게 확인할 때는 아래 명령이 편합니다.

```bash
find Models -maxdepth 3 -type f | sort
```

```bash
rg "class |DbSet|OnModelCreating|\\[Key\\]|\\[Required\\]" Models
```

## 8. First Migration

스캐폴딩된 기본 컨텍스트를 기준으로 마이그레이션 파일도 생성할 수 있습니다.

이 프로젝트는 비교용 컨텍스트가 여러 개 있어서 `--context`를 지정하는 편이 안전합니다.

```bash
dotnet tool restore
dotnet tool run dotnet-ef migrations add InitialScaffoldedSchema --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
```

생성 결과:

- `Migrations/{timestamp}_InitialScaffoldedSchema.cs`
- `Migrations/{timestamp}_InitialScaffoldedSchema.Designer.cs`
- `Migrations/SampleContextModelSnapshot.cs`

주의:

- 현재 DB를 먼저 바꾸고 나서 처음 마이그레이션을 만들면, 그 마이그레이션은 `PhoneNumber`만의 변경분이 아니라 현재 모델 전체를 기준으로 한 첫 기준선이 됩니다.
- 즉 지금 예시에서는 `Users`, `Posts`, `PhoneNumber`가 모두 포함된 초기 마이그레이션이 생성됩니다.
- 실제 변경분만 따로 보고 싶다면 DB 변경 전에 기준 마이그레이션을 먼저 만들어두는 흐름이 더 적합합니다.

생성된 마이그레이션 코드 확인:

```bash
find Migrations -maxdepth 1 -type f | sort
sed -n '1,240p' Migrations/*_InitialScaffoldedSchema.cs
sed -n '1,240p' Migrations/SampleContextModelSnapshot.cs
```


## 9. What To Observe

- `DbContext` 파일 이름과 위치
- 엔터티 클래스 이름 변환 방식
- `OnModelCreating` 내부 Fluent API 생성 여부
- `[Key]`, `[Required]` 같은 특성 부여 여부
- 외래 키와 탐색 속성 생성 형태
- 마이그레이션의 `Up` 메서드가 어떤 SQL 동작을 표현하는지
- `ModelSnapshot`이 현재 모델 기준선을 어떻게 저장하는지

## 10. Common Cleanup

다시 실습할 때 생성물만 지우고 싶다면 아래 정도만 사용합니다.

```bash
rm -rf Models
```

마이그레이션만 다시 만들고 싶다면 아래도 참고합니다.

```bash
dotnet tool run dotnet-ef migrations remove --context EfCoreScaffoldLab.Models.SampleContext
```

## 11. Log After Each Run

실습을 한 번 끝낼 때마다 아래 내용을 [docs/04-progress-log.md](/Users/newkimjiwon/project/scaffold-lab/docs/04-progress-log.md)에 남깁니다.

- 실행 날짜
- 사용한 명령어
- 생성된 폴더
- 관찰한 차이점
- 다음 실험 아이디어

공식 근거가 필요하면 [docs/05-reference-links.md](/Users/newkimjiwon/project/scaffold-lab/docs/05-reference-links.md)를 함께 확인합니다.

## 12. Quick Start In This Project

지금 저장소에서 가장 자주 쓰게 되는 실제 시작 순서입니다.

```bash
cd /Users/newkimjiwon/project/scaffold-lab/src/EfCoreScaffoldLab
export DOTNET_CLI_HOME=../../.dotnet_cli_home
export PATH=../../.dotnet:$PATH
dotnet --version
dotnet tool restore
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --force
dotnet tool run dotnet-ef migrations add InitialScaffoldedSchema --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
dotnet run
```
