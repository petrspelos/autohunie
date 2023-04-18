using System.Drawing;
using System.Text;

namespace AutoHunie.Core.Entities;

public class GameBoard
{
    public const int NumberOfColumns = 9;
    public const int NumberOfRows = 7;

    Token[,] _tokens;

    public GameBoard(int numOfColumns = NumberOfColumns, int numOfRows = NumberOfRows)
    {
        _tokens = new Token[numOfColumns, numOfRows];
    }

    public int Width => _tokens.GetLength(0);

    public int Height => _tokens.GetLength(1);

    public Token[] GetRow(int x)
    {
        if (x < 0 || x >= Height)
            throw new InvalidOperationException($"The row {x} is outside of the game board bounds ({Width}x{Height})");

        var result = new Token[Width];

        for (var i = 0; i < Width; i++)
        {
            result[i] = _tokens[i, x];
        }

        return result;
    }

    public void SetRow(int x, Token[] tokens)
    {
        if (tokens.Length != Width)
            throw new InvalidOperationException($"Cannot set a row of width {Width} to width of {tokens.Length}");

        for (var i = 0; i < Width; i++)
            _tokens[i, x] = new Token(tokens[i].Type);
    }

    public Token[] GetColumn(int y)
    {
        if (y < 0 || y >= Width)
            throw new InvalidOperationException($"The column {y} is outside of the game board bounds ({Width}x{Height})");

        var result = new Token[Height];

        for (var i = 0; i < Height; i++)
        {
            result[i] = _tokens[y, i];
        }

        return result;
    }

    public void SetColumn(int y, Token[] tokens)
    {
        if (tokens.Length != Height)
            throw new InvalidOperationException($"Cannot set a column of height {Height} to height of {tokens.Length}");

        for (var i = 0; i < Height; i++)
        {
            _tokens[y, i] = tokens[i] is null ? null! : new Token(tokens[i].Type);
        }
    }

    public Token GetToken(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            throw new InvalidOperationException($"The position ({x}x{y}) is outside of the game board bounds ({Width}x{Height})");

        return _tokens[x, y];
    }

    public void SetToken(int x, int y, Token token)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            throw new InvalidOperationException($"The position ({x}x{y}) is outside of the game board bounds ({Width}x{Height})");

        _tokens[x, y] = token;
    }

    public Token GetTokenSafe(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return new Token(TokenType.Unknown);

        return _tokens[x, y];
    }

    public IEnumerable<Tuple<Point, Token>> Where(Func<Token, int, int, bool> predicate)
    {
        var result = new List<Tuple<Point, Token>>();

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (predicate?.Invoke(_tokens[x, y], x, y) ?? false)
                    result.Add(new(new Point(x, y), _tokens[x, y]));
            }
        }

        return result;
    }

    public GameBoard Clone()
    {
        var clone = new GameBoard(Width, Height);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                clone.SetToken(x, y, _tokens[x, y]);
            }
        }

        return clone;
    }

    public void SlideToken(int x, int y, SlideDirection direction, int source, int destination)
    {
        var line = direction == SlideDirection.Horizontal ? GetRow(y) : GetColumn(x);

        var isFlipped = false;

        if (destination < source)
        {
            line = line.Reverse().ToArray();
            source = (line.Length - 1) - source;
            destination = (line.Length - 1) - destination;
            isFlipped = true;
        }

        var result = new Token[line.Length];

        var sourceValue = line[source];

        for (var i = 0; i < line.Length; i++)
        {
            var value = line[i];

            if (i < source || i > destination)
            {
                result[i] = line[i];
            }
            else
            {
                if (i == destination)
                    result[i] = sourceValue;
                else
                    result[i] = line[i + 1];
            }
        }

        if (isFlipped)
            result = result.Reverse().ToArray();

        if (direction == SlideDirection.Horizontal)
            SetRow(y, result);
        else
            SetColumn(x, result);
    }

    public void Draw()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _tokens[x, y].Draw();
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                sb.Append(_tokens[x, y].ToString());
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public bool IsInBounds(Point p) 
        => p.X >= 0 &&
            p.X < Width &&
            p.Y >= 0 &&
            p.Y < Height;

    public bool HasMatch()
    {
        return Where((token, x, y) =>
        {
            return 
                (GetTokenSafe(x + 1, y) == token && GetTokenSafe(x - 1, y) == token) ||
                (GetTokenSafe(x, y + 1) == token && GetTokenSafe(x, y - 1) == token);
        }).Any();
    }

    public int SimulateForward()
    {
        int score = 0;

        var matches = Where((token, x, y) =>
        {
            return
                token != new Token(TokenType.Unknown) &&
                ((GetTokenSafe(x + 1, y) == token && GetTokenSafe(x - 1, y) == token) ||
                (GetTokenSafe(x, y + 1) == token && GetTokenSafe(x, y - 1) == token) ||
                (GetTokenSafe(x - 1, y) == token && GetTokenSafe(x - 2, y) == token) || //two on left
                (GetTokenSafe(x + 1, y) == token && GetTokenSafe(x + 2, y) == token) || //two on right
                (GetTokenSafe(x, y - 1) == token && GetTokenSafe(x, y - 2) == token) || //two above
                (GetTokenSafe(x, y + 1) == token && GetTokenSafe(x, y + 2) == token)); //two bellow
        });

        foreach (var (pos, token) in matches)
        {
            // add to score
            // TODO: different points for different types (+ settings for these)
            if (token == new Token(TokenType.BrokenHeart))
                score -= 5;
            else if (token == new Token(TokenType.Stamina))
                score += 2;
            else if (token == new Token(TokenType.Romance))
                score += 2;
            else if (token == new Token(TokenType.Unknown))
                throw new InvalidOperationException("We're popping unknowns?");
            else
                score += 5;

            _tokens[pos.X, pos.Y] = null!;
        }

        for (var i = 0; i < Width; i++)
        {
            var column = GetColumn(i);
            var nullCount = column.Count(t => t is null);

            if (nullCount > 0)
            {
                for (var j = 0; j < nullCount; j++)
                {
                    var last = column.ToList().LastIndexOf(null!);
                    SlideToken(i, last, SlideDirection.Vertical, last, 0);
                }
            }
        }

        var nulls = Where((token, x, y) => token is null);
        foreach (var (pos, token) in nulls)
        {
            SetToken(pos.X, pos.Y, new Token(TokenType.Unknown));
        }

        return score;
    }

    public static bool operator ==(GameBoard obj1, GameBoard obj2) => obj1 is not null && obj1.Equals(obj2);

    public static bool operator !=(GameBoard obj1, GameBoard obj2) => obj1 is null || !obj1.Equals(obj2);

    public override bool Equals(object? obj) => Equals(obj as GameBoard);

    public bool Equals(GameBoard? other)
    {
        if (other is null || other.Width != this.Width || other.Height != this.Height)
            return false;

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (this._tokens[x, y] != other.GetToken(x, y))
                    return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(Width, Height);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                hash = HashCode.Combine(hash, this._tokens[x, y]);
            }
        }

        return hash;
    }

    public GameBoard ApplyMove(GameMove move)
    {
        var direction = move.FromX == move.ToX ? SlideDirection.Vertical : SlideDirection.Horizontal;

        var source = direction == SlideDirection.Horizontal ? move.FromX : move.FromY;
        var destionation = direction == SlideDirection.Horizontal ? move.ToX : move.ToY;

        SlideToken(move.FromX, move.FromY, direction, source, destionation);

        return this;
    }
}
