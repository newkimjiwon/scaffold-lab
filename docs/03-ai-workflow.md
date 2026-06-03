# AI Workflow

## Purpose

이 문서는 이 저장소에서 AI가 코드를 만들고 수정할 때 따라야 할 운영 기준입니다.  
세션이 바뀌더라도 이 문서를 읽으면 같은 방식으로 이어서 작업할 수 있게 만드는 것이 목표입니다.

## Core Rules

- 코드를 만들기 전 현재 `docs/` 내용을 먼저 확인합니다.
- 기술 판단의 근거가 필요하면 `docs/05-reference-links.md`의 공식 링크를 우선 확인합니다.
- 큰 작업 전에는 문서 기준과 폴더 구조를 먼저 맞춥니다.
- 실습이 끝나면 결과를 `docs/04-progress-log.md`에 기록합니다.
- 실험성 코드와 학습 메모를 분리합니다.
- 기존 파일을 덮어쓸 때는 목적과 영향 범위를 분명히 남깁니다.

## Session Start Checklist

- `README.md`와 `docs/`를 읽고 현재 목표를 파악한다.
- 필요하면 `docs/05-reference-links.md`에서 공식 문서 출처를 확인한다.
- 아직 생성되지 않은 코드와 이미 생성된 코드를 구분한다.
- 이번 세션의 작업 목표를 한 줄로 정리한다.
- 필요한 경우 실습 로그의 마지막 항목을 확인한다.

## Session End Checklist

- 어떤 명령을 실행했는지 정리한다.
- 어떤 파일이 생성되거나 변경되었는지 기록한다.
- 배운 점이나 주의점을 로그에 남긴다.
- 다음 세션의 추천 시작점을 한 줄로 적는다.

## Documentation Update Rules

- 프로젝트 목적이 바뀌면 `README.md`와 `docs/01-project-overview.md`를 함께 갱신합니다.
- 실행 명령이 바뀌면 `docs/02-efcore-scaffold-cli.md`를 먼저 갱신합니다.
- 작업 방식이 바뀌면 `docs/03-ai-workflow.md`를 갱신합니다.
- 공식 참고 문서가 추가되면 `docs/05-reference-links.md`를 갱신합니다.
- 실습 결과는 항상 `docs/04-progress-log.md`에 남깁니다.

## Prompt Template For Future AI Sessions

아래 프롬프트를 기반으로 다음 작업을 이어갈 수 있습니다.

```text
docs 폴더를 먼저 읽고 현재 상태를 파악한 뒤 이어서 작업해줘.
이번 목표는 {여기에 목표 입력}.
작업이 끝나면 관련 문서와 progress log도 함께 업데이트해줘.
```

## Suggested Next Tasks For AI

- .NET 콘솔 프로젝트 실제 생성
- SQLite 샘플 DB 생성
- 첫 EF Core 스캐폴딩 실행
- 옵션별 생성 코드 비교 문서화
- 생성된 코드 구조 설명 정리
