using System.Collections;

namespace AutoHunie.Tests.Data;

public class BoardRecognitionData : IEnumerable<object[]>
{
    private readonly List<object[]> _data;

    public BoardRecognitionData()
    {
        var boardsPath = @".\TestBoards";
        var files = Directory.GetFiles(boardsPath, "*.png", SearchOption.AllDirectories);
        _data = files.Select(file => new object[] { file, Path.ChangeExtension(file, ".csv") }).ToList();
    }

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
