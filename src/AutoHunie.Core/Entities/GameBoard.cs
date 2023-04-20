using System.Drawing;
using System.Text;

namespace AutoHunie.Core.Entities;

public record GameBoard
{
    public const int NumberOfColumns = 9;
    public const int NumberOfRows = 7;

    Token[,] _tokens;

    public GameBoard(int numOfColumns = NumberOfColumns, int numOfRows = NumberOfRows)
    {
        _tokens = new Token[numOfColumns, numOfRows];
    }

    public int Columns => _tokens.GetLength(0);

    public int Rows => _tokens.GetLength(1);

    public List<(int x, int y, int newX, int newY)> FindAllPossibleMoves()
    {
        if (HasMatch())
            throw new InvalidOperationException("Cannot search for moves in a board with matches");

        var possibleMoves = new List<(int x, int y, int newX, int newY)>();

        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int dist = 1; dist < Columns; dist++) // Check horizontal moves with larger distances
                {
                    if (x + dist < Columns)
                    {
                        SlideToken(x, y, x + dist, y);
                        if (HasMatch())
                        {
                            possibleMoves.Add((x, y, x + dist, y));
                        }
                        SlideToken(x + dist, y, x, y); // Move token back to original position
                    }
                }

                for (int dist = 1; dist < Rows; dist++) // Check vertical moves with larger distances
                {
                    if (y + dist < Rows)
                    {
                        SlideToken(x, y, x, y + dist);
                        if (HasMatch())
                        {
                            possibleMoves.Add((x, y, x, y + dist));
                        }
                        SlideToken(x, y + dist, x, y); // Move token back to original position
                    }
                }
            }
        }

        return possibleMoves;
    }

    public (int x, int y, int newX, int newY) FindBestMoveConsideringFuture(int searchDepth)
    {
        return DepthLimitedSearch(this, searchDepth, (-1, -1, -1, -1)).bestMove;
    }

    private (int score, (int x, int y, int newX, int newY) bestMove) DepthLimitedSearch(GameBoard board, int depth, (int x, int y, int newX, int newY) currentMove)
    {
        if (depth == 0)
        {
            return (0, currentMove);
        }

        int bestScore = -1;
        (int x, int y, int newX, int newY) bestMove = (-1, -1, -1, -1);

        var possibleMoves = board.FindAllPossibleMoves();

        foreach (var move in possibleMoves)
        {
            GameBoard testBoard = board with { };
            testBoard.SlideToken(move.x, move.y, move.newX, move.newY);

            int score = 0;
            while (testBoard.HasMatch())
            {
                score += testBoard.SimulateForward();
            }

            if (depth > 1)
            {
                var futureResult = DepthLimitedSearch(testBoard, depth - 1, move);
                score += futureResult.score;
            }

            if (score > bestScore)
            {
                bestScore = score;
                if (depth == 1)
                {
                    bestMove = currentMove;
                }
                else
                {
                    bestMove = move;
                }
            }
        }

        return (bestScore, bestMove);
    }

    public Token[] GetRow(int x)
    {
        if (x < 0 || x >= Rows)
            throw new InvalidOperationException($"The row {x} is outside of the game board bounds ({Columns}x{Rows})");

        var result = new Token[Columns];

        for (var i = 0; i < Columns; i++)
        {
            result[i] = _tokens[i, x];
        }

        return result;
    }

    public void SetRow(int x, Token[] tokens)
    {
        if (tokens.Length != Columns)
            throw new InvalidOperationException($"Cannot set a row of width {Columns} to width of {tokens.Length}");

        for (var i = 0; i < Columns; i++)
            _tokens[i, x] = new Token(tokens[i].Type);
    }

    public Token[] GetColumn(int y)
    {
        if (y < 0 || y >= Columns)
            throw new InvalidOperationException($"The column {y} is outside of the game board bounds ({Columns}x{Rows})");

        var result = new Token[Rows];

        for (var i = 0; i < Rows; i++)
        {
            result[i] = _tokens[y, i];
        }

        return result;
    }

    public void SetColumn(int y, Token[] tokens)
    {
        if (tokens.Length != Rows)
            throw new InvalidOperationException($"Cannot set a column of height {Rows} to height of {tokens.Length}");

        for (var i = 0; i < Rows; i++)
        {
            _tokens[y, i] = tokens[i] is null ? null! : new Token(tokens[i].Type);
        }
    }

    public Token GetToken(int x, int y)
    {
        if (x < 0 || x >= Columns || y < 0 || y >= Rows)
            throw new InvalidOperationException($"The position ({x}x{y}) is outside of the game board bounds ({Columns}x{Rows})");

        return _tokens[x, y];
    }

    public void SetToken(int x, int y, Token token)
    {
        if (x < 0 || x >= Columns || y < 0 || y >= Rows)
            throw new InvalidOperationException($"The position ({x}x{y}) is outside of the game board bounds ({Columns}x{Rows})");

        _tokens[x, y] = token;
    }

    public Token GetTokenSafe(int x, int y)
    {
        if (x < 0 || x >= Columns || y < 0 || y >= Rows)
            return new Token(TokenType.Unknown);

        return _tokens[x, y];
    }

    public IEnumerable<Tuple<Point, Token>> Where(Func<Token, int, int, bool> predicate)
    {
        var result = new List<Tuple<Point, Token>>();

        for (var x = 0; x < Columns; x++)
        {
            for (var y = 0; y < Rows; y++)
            {
                if (predicate?.Invoke(_tokens[x, y], x, y) ?? false)
                    result.Add(new(new Point(x, y), _tokens[x, y]));
            }
        }

        return result;
    }

    public void SlideToken(int x, int y, int newX, int newY)
    {
        if (x != newX && y != newY)
            throw new InvalidOperationException("Diagonal slides are not allowed.");

        var direction = x != newX ? SlideDirection.Horizontal : SlideDirection.Vertical;

        var source = x != newX ? x : y;
        var destionation = x != newX ? newX : newY;

        SlideToken(x, y, direction, source, destionation);
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
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                _tokens[x, y].Draw();
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                sb.Append(_tokens[x, y].ToString());
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public bool IsInBounds(Point p)
        => p.X >= 0 &&
            p.X < Columns &&
            p.Y >= 0 &&
            p.Y < Rows;

    public bool HasMatch()
    {
        return Where((token, x, y) =>
        {
            return
                (token != new Token(TokenType.Unknown)) &&
                (
                    (GetTokenSafe(x + 1, y) == token && GetTokenSafe(x - 1, y) == token) ||
                    (GetTokenSafe(x, y + 1) == token && GetTokenSafe(x, y - 1) == token)
                );
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
                score -= 250;
            else if (token == new Token(TokenType.Stamina))
                score += 2;
            else if (token == new Token(TokenType.Romance))
                score += 2;
            else if (token == new Token(TokenType.Unknown))
                throw new InvalidOperationException("We're popping unknowns?");
            else
                score += 10;

            _tokens[pos.X, pos.Y] = null!;
        }

        if (matches.Count() > 3)
        {
            score += matches.Count() * 100;
        }

        System.Console.WriteLine($"score: {score} - matches: {matches.Count()}");

        for (var i = 0; i < Columns; i++)
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

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(Columns, Rows);

        for (var x = 0; x < Columns; x++)
        {
            for (var y = 0; y < Rows; y++)
            {
                hash = HashCode.Combine(hash, _tokens[x, y]);
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
