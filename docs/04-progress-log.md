# Progress Log

## 2026-06-03

### Done

- 저장소 기본 소개용 `README.md`를 정리했다.
- AI 협업 기준 문서와 스캐폴딩 실습 가이드를 만들었다.
- `docs/` 중심 문서 구조를 도입했다.
- 공식 문서 링크를 보관할 레퍼런스 문서 구조를 추가했다.
- 저장소 로컬 `.NET SDK` 10.0.300을 설치했다.
- `src/EfCoreScaffoldLab` 콘솔 프로젝트를 생성했다.
- `Microsoft.EntityFrameworkCore.Design` 10.0.8 패키지를 설치했다.
- `Microsoft.EntityFrameworkCore.Sqlite` 10.0.8 패키지를 설치했다.
- 로컬 도구 매니페스트와 `dotnet-ef` 10.0.8을 설치했다.
- `sample-schema.sql`과 `sample.db`를 만들었다.
- 기본 스캐폴딩과 옵션별 스캐폴딩 결과를 `Models` 아래에 생성했다.
- 샘플 데이터를 읽어보는 `Program.cs` 실행 코드를 작성했다.
- `dotnet run`으로 샘플 사용자 2명과 게시글 관계가 정상 조회되는 것을 확인했다.

### Notes

- 시스템 전역 `dotnet`은 없었고, 대신 프로젝트 안의 로컬 SDK로 작업했다.
- `sample.db` 기준 기본 컨텍스트 이름은 `SampleContext`로 생성되었다.
- `--data-annotations` 결과는 `Models/Annotations`에 생성되었다.
- `--use-database-names` 결과는 `Models/RawNames`에 생성되었고 컨텍스트 이름이 `sampleContext`로 유지되었다.
- SQLite의 `TEXT` 날짜 값이 `DateOnly`로 매핑되는 것을 확인했다.
- 비교용 컨텍스트까지 모두 프로젝트에 포함되어 있어 빌드 시 `#warning` 메시지가 함께 출력된다.

### Next

- 생성된 모델 코드를 옵션별로 비교 정리
- 필요하면 `--table`, `--context`, `--no-onconfiguring` 옵션 실험 추가
- DBeaver에서 스키마를 바꾼 뒤 재스캐폴딩하면서 변화 관찰
