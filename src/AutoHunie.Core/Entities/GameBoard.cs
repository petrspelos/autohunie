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
        if (x < 0 || x >= Height || y < 0 || y >= Width)
            return new Token(TokenType.Unknown);

        return _tokens[x, y];
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
                if (this._tokens[x,y] != other.GetToken(x,y))
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
                hash = HashCode.Combine(hash, this._tokens[x,y]);
            }
        }

        return hash;
    }
}
