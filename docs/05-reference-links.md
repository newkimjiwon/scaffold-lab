# Reference Links

## Purpose

이 파일은 이 학습 저장소에서 기준으로 삼는 공식 문서 링크를 모아두는 곳입니다.  
새 세션에서 AI가 작업을 이어갈 때도, 먼저 이 파일을 읽으면 어떤 문서를 우선 참고해야 하는지 바로 파악할 수 있습니다.

## Current Stack

- ORM: EF Core
- Database: SQLite
- DB Tool: DBeaver
- Main Workflow: `dotnet ef dbcontext scaffold`를 이용한 리버스 엔지니어링

## Primary References

### EF Core Scaffolding

- Microsoft Learn KR: EF Core 스캐폴딩(리버스 엔지니어링)
  https://learn.microsoft.com/ko-kr/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli
- Microsoft Learn EN: EF Core Scaffolding (Reverse Engineering)
  https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/

이 문서는 현재 학습의 가장 중요한 기준 문서입니다.  
특히 필수 인수, 연결 문자열, 공급자 이름, 명령줄 옵션을 확인할 때 우선 참고합니다.

### EF Core SQLite Provider

- Microsoft Learn KR: SQLite EF Core 데이터베이스 공급자
  https://learn.microsoft.com/ko-kr/ef/core/providers/sqlite/
- Microsoft Learn EN: SQLite Database Provider
  https://learn.microsoft.com/en-us/ef/core/providers/sqlite/

이 문서는 `Microsoft.EntityFrameworkCore.Sqlite` 패키지와 SQLite 공급자 특성을 확인할 때 참고합니다.

### EF Core Providers Overview

- Microsoft Learn KR: 데이터베이스 공급자
  https://learn.microsoft.com/ko-kr/ef/core/providers/
- Microsoft Learn EN: Database Providers
  https://learn.microsoft.com/en-us/ef/core/providers/

공급자 호환성이나 전체 그림이 필요할 때 참고합니다.

### DBeaver SQLite Docs

- DBeaver Docs: SQLite
  https://dbeaver.com/docs/dbeaver/Database-driver-SQLite/
- DBeaver Docs: Documentation Home
  https://dbeaver.com/docs/

이 문서는 DBeaver에서 SQLite 연결을 만들거나 DB 파일 경로를 설정할 때 참고합니다.

### .NET SDK Install

- Microsoft Learn EN: Install .NET on macOS
  https://learn.microsoft.com/en-us/dotnet/core/install/macos
- Microsoft Learn EN: dotnet-install scripts reference
  https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script

이 문서는 시스템 전역 설치 대신 로컬 SDK를 설치해야 할 때 참고합니다.

## How To Use These References

- 스캐폴딩 명령 자체는 EF Core Scaffolding 문서를 우선 본다.
- SQLite 패키지나 제약 사항은 SQLite Provider 문서를 본다.
- DBeaver 연결 설정은 DBeaver SQLite 문서를 본다.
- 한글 문서가 어색하거나 누락된 경우 영문 문서를 함께 비교한다.

## Notes

- 현재 시작 기준 문서는 사용자가 지정한 한국어 Microsoft Learn 스캐폴딩 문서입니다.
- 문서 링크는 2026-06-03 기준으로 확인했습니다.
- 이후 SQL Server, PostgreSQL 등 다른 DB로 확장하면 이 파일에 섹션을 추가합니다.
