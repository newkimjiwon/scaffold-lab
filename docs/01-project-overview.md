# Project Overview

## Purpose

이 저장소는 C#과 EF Core 스캐폴딩을 학습하기 위한 실습 공간입니다.  
특히 SQLite 파일을 기준으로 `DbContext`와 엔터티 클래스를 생성하고, 옵션에 따라 어떤 코드 차이가 생기는지 비교하는 데 초점을 둡니다.

## Study Goals

- EF Core 리버스 엔지니어링 기본 흐름 익히기
- SQLite 연결 문자열과 공급자 개념 이해하기
- `--data-annotations`, `--use-database-names`, `--force` 같은 옵션 차이 확인하기
- 생성된 엔터티와 `DbContext` 구조 읽는 연습하기
- 실습 내용을 문서와 로그로 남겨 다음 세션에서 바로 이어가기

## Working Scope

- 학습용 .NET 콘솔 프로젝트 생성
- SQLite 샘플 데이터베이스 준비
- EF Core 스캐폴딩 실행
- 생성 코드 검토 및 비교
- 필요 시 후속 실험 추가

## Suggested Folder Layout

```text
scaffold-lab/
├── README.md
├── docs/
│   ├── 01-project-overview.md
│   ├── 02-efcore-scaffold-cli.md
│   ├── 03-ai-workflow.md
│   ├── 04-progress-log.md
│   ├── 05-reference-links.md
│   └── 06-study-notes.md
└── src/
    └── EfCoreScaffoldLab/
```

## Conventions

- 실습용 앱 코드는 `src/` 아래에 둡니다.
- 문서는 `docs/` 아래에서 숫자 순서대로 읽히도록 유지합니다.
- 공식 문서 링크는 `docs/05-reference-links.md`에 누적 관리합니다.
- 대화 중 정리한 학습 개념은 `docs/06-study-notes.md`에 누적 관리합니다.
- 큰 작업을 할 때는 먼저 문서를 갱신하고, 그 다음 코드 변경을 진행합니다.
- 실습 결과 비교가 필요하면 출력 폴더를 분리합니다.

## Definition Of Done For One Experiment

- 실행한 명령어가 문서에 남아 있다.
- 생성된 코드 위치가 정리되어 있다.
- 어떤 옵션을 썼는지 기록되어 있다.
- 결과에서 배운 점이 로그에 남아 있다.

## Next Recommended Task

다음 세션에서는 [docs/02-efcore-scaffold-cli.md](/Users/newkimjiwon/project/scaffold-lab/docs/02-efcore-scaffold-cli.md)를 따라 `src/EfCoreScaffoldLab` 프로젝트와 `sample.db`를 실제로 만들고 첫 스캐폴딩을 수행합니다.
