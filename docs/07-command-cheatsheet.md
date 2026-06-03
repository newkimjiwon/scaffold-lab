# Command Cheatsheet

## Purpose

이 문서는 이 프로젝트에서 실제로 사용한 터미널 명령어를 빠르게 다시 찾기 위한 치트시트입니다.  
개념 설명은 [docs/06-study-notes.md](/Users/newkimjiwon/project/scaffold-lab/docs/06-study-notes.md), 전체 흐름은 [docs/02-efcore-scaffold-cli.md](/Users/newkimjiwon/project/scaffold-lab/docs/02-efcore-scaffold-cli.md)를 참고합니다.

## Start Position

프로젝트 루트:

```bash
cd /Users/newkimjiwon/project/scaffold-lab
```

실습 프로젝트 폴더:

```bash
cd /Users/newkimjiwon/project/scaffold-lab/src/EfCoreScaffoldLab
```

## Session Setup

`EfCoreScaffoldLab` 폴더 안에서 자주 쓴 세션 설정입니다.

```bash
export DOTNET_CLI_HOME=../../.dotnet_cli_home
export PATH=../../.dotnet:$PATH
dotnet --version
```

예상 결과:

```text
10.0.300
```

## Basic Checks

현재 위치 확인:

```bash
pwd
```

파일 목록 확인:

```bash
ls -la
```

변경 상태 확인:

```bash
git status --short
```

변경 요약 확인:

```bash
git diff --stat
```

## Local .NET / Tooling

로컬 도구 복원:

```bash
dotnet tool restore
```

로컬 `dotnet-ef` 버전 확인:

```bash
dotnet tool run dotnet-ef --version
```

프로젝트 실행:

```bash
dotnet run
```

## Scaffold Commands

기본 스캐폴딩:

```bash
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --force
```

`Data Annotations` 버전:

```bash
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models/Annotations --data-annotations --force
```

DB 이름 유지 버전:

```bash
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models/RawNames --use-database-names --force
```

## Migration Commands

초기 마이그레이션 생성:

```bash
dotnet tool run dotnet-ef migrations add InitialScaffoldedSchema --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
```

`Gender` 컬럼 추가 예시 마이그레이션 생성:

```bash
dotnet tool run dotnet-ef migrations add AddGenderToUsers --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
```

가장 최근 마이그레이션 제거:

```bash
dotnet tool run dotnet-ef migrations remove --context EfCoreScaffoldLab.Models.SampleContext
```

새 DB에 마이그레이션 적용:

```bash
dotnet dotnet-ef database update --context EfCoreScaffoldLab.Models.SampleContext --connection "Data Source=migration-demo.db"
```

## SQLite Commands

`sample.db`의 `Users` 스키마 확인:

```bash
sqlite3 sample.db ".schema Users"
```

`migration-demo.db`의 `Users` 스키마 확인:

```bash
sqlite3 migration-demo.db ".schema Users"
```

테이블 목록 확인:

```bash
sqlite3 migration-demo.db ".tables"
```

마이그레이션 이력 확인:

```bash
sqlite3 migration-demo.db "select MigrationId, ProductVersion from __EFMigrationsHistory order by MigrationId;"
```

기존 DB에 컬럼 직접 추가:

```bash
sqlite3 sample.db "ALTER TABLE Users ADD COLUMN Gender TEXT;"
```

## File Inspection

`User.cs` 보기:

```bash
sed -n '1,200p' Models/User.cs
```

마이그레이션 파일 보기:

```bash
sed -n '1,200p' Migrations/*_AddGenderToUsers.cs
```

생성된 파일 목록 보기:

```bash
find Migrations -maxdepth 1 -type f | sort
```

## Practical Command Flows

### Flow A. DB 우선 스캐폴딩

```bash
cd /Users/newkimjiwon/project/scaffold-lab/src/EfCoreScaffoldLab
export DOTNET_CLI_HOME=../../.dotnet_cli_home
export PATH=../../.dotnet:$PATH
sqlite3 sample.db "ALTER TABLE Users ADD COLUMN Gender TEXT;"
dotnet tool run dotnet-ef dbcontext scaffold "Data Source=sample.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --force
sed -n '1,200p' Models/User.cs
```

### Flow B. 코드 우선 마이그레이션

```bash
cd /Users/newkimjiwon/project/scaffold-lab/src/EfCoreScaffoldLab
export DOTNET_CLI_HOME=../../.dotnet_cli_home
export PATH=../../.dotnet:$PATH
dotnet tool restore
dotnet tool run dotnet-ef migrations add AddGenderToUsers --context EfCoreScaffoldLab.Models.SampleContext --output-dir Migrations
dotnet dotnet-ef database update --context EfCoreScaffoldLab.Models.SampleContext --connection "Data Source=migration-demo.db"
sqlite3 migration-demo.db ".schema Users"
```

## Reminder

- `sample.db`는 DB 우선 실습용입니다.
- `migration-demo.db`는 마이그레이션 실습용입니다.
- `sample.db`에 초기 마이그레이션을 바로 적용하면 `table already exists` 충돌이 날 수 있습니다.
