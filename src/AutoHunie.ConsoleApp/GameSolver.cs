using System.Collections.ObjectModel;
using AutoHunie.Core.Entities;
using System.Drawing;
using AutoHunie.Core;

namespace AutoHunie.ConsoleApp;

public class GameSolver
{
    private readonly Token[,] _board;

    public GameSolver(Token[,] board)
    {
        _board = board;
    }

    public GameMove GetNextBestMove(Bitmap image)
    {
        if (new Point(10, 20) != new Point(10, 20))
            throw new InvalidOperationException("I'm not sure reality really is what we think, chief.");

        // the algorithm
        var columns = _board.GetLength(0);
        var rows = _board.GetLength(1);
        Console.WriteLine($"columns: {columns}");
        Console.WriteLine($"Rows: {rows}");

        var almostMatches = new HashSet<AlmostMatch>();

        ForEachTile((x, y) =>
        {
            var thisType = _board[x, y].Type;

            if (thisType == TokenType.Unknown)
                return;

            // if (x == 2 && y == 3)
            // {
            //     System.Console.WriteLine("X:");
            //     _board[x-2,y].Draw();
            //     _board[x-1,y].Draw();
            //     _board[x,y].Draw();
            //     _board[x+1,y].Draw();
            //     _board[x+2,y].Draw();
            //     System.Console.WriteLine();

            //     System.Console.WriteLine("Y:");
            //     _board[x,y-2].Draw();
            //     _board[x,y-1].Draw();
            //     _board[x,y].Draw();
            //     _board[x,y+1].Draw();
            //     _board[x,y+2].Draw();
            //     System.Console.WriteLine();
            // }

            if ((GetBoardToken(x-1,y) == GetBoardToken(x-2,y)) ||
                (GetBoardToken(x+1,y) == GetBoardToken(x+2,y)) ||
                (GetBoardToken(x,y-1) == GetBoardToken(x,y-2)) ||
                (GetBoardToken(x,y+1) == GetBoardToken(x,y+2)))
            {
                almostMatches.Add(new AlmostMatch(x, y, thisType, Direction.Both));
            }

            // LEFT
            if (x - 2 >= 0 && _board[x - 2, y].Type == thisType)
            {
                // we found an Almost-Match
                var direction = GetBoardToken(x + 1, y) == thisType || GetBoardToken(x - 3, y) == thisType ? Direction.Both : Direction.Vertical;
                almostMatches.Add(new AlmostMatch(x - 1, y, thisType, direction));
            }

            // RIGHT
            if (x + 2 < columns && _board[x + 2, y].Type == thisType)
            {
                // we found an Almost-Match
                var direction = GetBoardToken(x + 3, y) == thisType || GetBoardToken(x - 1, y) == thisType ? Direction.Both : Direction.Vertical;
                almostMatches.Add(new AlmostMatch(x + 1, y, thisType, direction));
            }

            // UP
            if (y - 2 >= 0 && _board[x, y - 2].Type == thisType)
            {
                // we found an Almost-Match
                var direction = GetBoardToken(x, y - 3) == thisType || GetBoardToken(x, y + 1) == thisType ? Direction.Both : Direction.Horizontal;
                almostMatches.Add(new AlmostMatch(x, y - 1, thisType, direction));
            }

            // DOWN
            if (y + 2 < rows && _board[x, y + 2].Type == thisType)
            {
                // we found an Almost-Match
                var direction = GetBoardToken(x, y + 3) == thisType || GetBoardToken(x, y - 1) == thisType ? Direction.Both : Direction.Horizontal;
                almostMatches.Add(new AlmostMatch(x, y + 1, thisType, direction));
            }
        });

        foreach (var point in almostMatches)
        {
            //Console.WriteLine($"FOUND MATCH: COL {point.X}, ROW {point.Y}, TOK {point.TokenType}");
        }

        var fillMoves = almostMatches.Where(HasPossibleMove);

        foreach (var point in fillMoves)
        {
            //Console.WriteLine($"FOUND MATCH: COL {point.X}, ROW {point.Y}, TOK {point.TokenType}");
        }
        // (1) For the whole board
        // Find Almost-Matches
        // For each Almost-Match, see if can be filled
        // collect all possible fill-moves

        // For each possible fill-move
        // simulate what the board would look like after this move
        // Repeat (1) for new board


        // Pick best move and return

        return new(1, 1, 1, 4);
    }

    private TokenType GetBoardToken(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _board.GetLength(0) || y >= _board.GetLength(1))
        {
            return TokenType.Unknown;
        }

        return _board[x, y].Type;
    }

    private bool HasPossibleMove(AlmostMatch almostMatch)
    {
        var shouldLog = false;

        if (shouldLog)
            Console.WriteLine("ABOUT TO CHECK COL X2");

        var row = GetRow(2);
        foreach (var token in row)
        {
            //token.Draw();
        }
//System.Console.WriteLine("");
//System.Console.WriteLine("slide 1 to 3");
        foreach (var token in SlideToken(row, 1, 3))
        {
            //token.Draw();
        }
//System.Console.WriteLine("");
//System.Console.WriteLine("slide 4 to 1");
        foreach (var token in SlideToken(row, 4, 1))
        {
            //token.Draw();
        }

        //System.Console.WriteLine();

        if (almostMatch.Direction == Direction.Vertical || almostMatch.Direction == Direction.Both)
        {
            // CHECK VERTICALLY
            var column = new ObservableCollection<Token>();

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                column.Add(_board[i, almostMatch.Y]);
            }

            for (int i = 0; i < column.Count; i++)
            {
                for (int j = 0; j < column.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (shouldLog) Console.WriteLine($"Trying to move token at {i} to {j}");
                    if (shouldLog)PrintTokens(column);
                    column.Move(i, j);
                    if (shouldLog)PrintTokens(column);
                    if (shouldLog) System.Console.WriteLine("=== 😢 nope, TRYING SOMETHING ELSE ===");
                    
                    if (HasThreeOrMore(column))
                    {
                        if (shouldLog) System.Console.WriteLine("=== 🎉 YUP WE GOT A MATCH ===");
                        if (shouldLog)PrintTokens(column);
                        return true;
                    }

                    column.Move(j, i);
                }
            }
        }

        if (almostMatch.Direction == Direction.Horizontal || almostMatch.Direction == Direction.Both)
        {
            // // CHECK HORIZONTALLY
            // for (int i = 0; i < _board.GetLength(1); i++)
            // {
            //     if (_board[almostMatch.X, i].Type == almostMatch.TokenType)
            //     {
            //         return true;
            //     }
            // }
        }

        return false;
    }

    private Token[] SlideToken(Token[] line, int source, int destination)
    {
        var isFlipped = false;

        if (destination < source)
        {
            line.Reverse();
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
            result.Reverse();
        
        return result;
    }

    private Token[] GetColumn(int num)
    {
        var result = new Token[_board.GetLength(1)];
        for (var i = 0; i < _board.GetLength(1); i++)
        {
            result[i] = _board[num, i];
        }

        return result;
    }

    private Token[] GetRow(int num)
    {
        var result = new Token[_board.GetLength(0)];
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            result[i] = _board[i, num];
        }

        return result;
    }

    private void PrintTokens(ObservableCollection<Token> column)
    {
        foreach (var token in column) {
            //token.Draw();
        }

        System.Console.WriteLine();
    }

    private bool HasThreeOrMore(ObservableCollection<Token> tokens)
    {
        for (var i = 0; i < tokens.Count - 2; i++) {
            var type = tokens[i].Type;
            if (tokens[i + 1].Type == type && tokens[i + 2].Type == type)
            {
                // Got three in a row
                return true;
            }
        }

        return false;
    }

    private void ForEachTile(Action<int, int> action)
    {
        for (var x = 0; x < _board.GetLength(0); x++)
        {
            for (var y = 0; y < _board.GetLength(1); y++)
            {
                action?.Invoke(x, y);
            }
        }
    }

    private bool IsBetween(int x, int min, int max) => x < max && x > min;
}

public class GameMove
{
    public int FromX { get; set; }
    public int FromY { get; set; }
    public int ToX { get; set; }
    public int ToY { get; set; }

    public GameMove(int fromX, int fromY, int toX, int toY)
    {
        FromX = fromX;
        FromY = FromY;
        ToX = toX;
        ToY = toY;
    }
}

public class AlmostMatch
{
    public AlmostMatch(int x, int y, TokenType tokenType, Direction direction)
    {
        X = x;
        Y = y;
        TokenType = tokenType;
        Direction = direction;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public TokenType TokenType { get; set; }
    public Direction Direction { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as AlmostMatch);
    }

    public bool Equals(AlmostMatch? other)
    {
        return other != null &&
               X == other.X &&
               Y == other.Y &&
               TokenType == other.TokenType &&
               Direction == other.Direction;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, TokenType, Direction);
    }
}

public enum Direction
{
    Horizontal,
    Vertical,
    Both
}
