using EfCoreScaffoldLab.Models;
using Microsoft.EntityFrameworkCore;

var workingDirectory = Directory.GetCurrentDirectory();
var databasePath = Path.Combine(workingDirectory, "sample.db");

Console.WriteLine($"Working directory: {workingDirectory}");
Console.WriteLine($"SQLite file: {databasePath}");

if (!File.Exists(databasePath))
{
    Console.WriteLine("sample.db 파일을 찾을 수 없습니다. 프로젝트 폴더에서 실행해 주세요.");
    return;
}

using var db = new SampleContext();

var users = db.Users
    .AsNoTracking()
    .Include(user => user.Posts)
    .OrderBy(user => user.Id)
    .ToList();

Console.WriteLine($"Users: {users.Count}");

foreach (var user in users)
{
    Console.WriteLine($"- {user.Id}: {user.DisplayName} <{user.Email}> / Posts: {user.Posts.Count}");
}
