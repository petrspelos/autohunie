using AutoHunie.Core.Entities;
using System.Drawing;

namespace AutoHunie.Core;

public class GameSolver
{
    private readonly GameBoard _board;

    public GameSolver(GameBoard board)
    {
        _board = board;
    }

    public GameMove GetNextBestMove(Bitmap image)
    {
        var validMoves = GetValidMoves(_board);
        var newBoards = validMoves.Select(vm => SimulateBoard(_board, vm));

        //return possibleMoves.SelectMany(ms => ms).ToList().Random();
        return new GameMove(0, 0, 1, 1);
    }

    private Tuple<GameBoard, int> SimulateBoard(GameBoard boardToCheck, GameMove vm)
    {
        var board = boardToCheck.Clone();

        var score = board.SimulateForward();

        return Tuple.Create(board, score);
    }

    private IEnumerable<GameMove> GetValidMoves(GameBoard board)
    {
        var boardCopy = _board.Clone();
        var pairs = boardCopy.Where((token, x, y) =>
        {
            return
                boardCopy.GetTokenSafe(x + 1, y) == token ||
                boardCopy.GetTokenSafe(x - 1, y) == token ||
                boardCopy.GetTokenSafe(x, y + 1) == token ||
                boardCopy.GetTokenSafe(x, y - 1) == token;
        });

        var reducedPairs = new HashSet<Point>();
        foreach (var pair in pairs)
        {
            var pos = pair.Item1;
            var token = pair.Item2;

            if (token == new Token(TokenType.Unknown))
                continue;

            if (boardCopy.GetTokenSafe(pos.X + 1, pos.Y) == token)
            {
                reducedPairs.Add(new Point(pos.X - 1, pos.Y));
            }

            if (boardCopy.GetTokenSafe(pos.X - 1, pos.Y) == token)
            {
                reducedPairs.Add(new Point(pos.X + 1, pos.Y));
            }

            if (boardCopy.GetTokenSafe(pos.X, pos.Y + 1) == token)
            {
                reducedPairs.Add(new Point(pos.X, pos.Y - 1));
            }

            if (boardCopy.GetTokenSafe(pos.X, pos.Y - 1) == token)
            {
                reducedPairs.Add(new Point(pos.X, pos.Y + 1));
            }
        }

        var pointsToExplore = reducedPairs.Where(pos => boardCopy.IsInBounds(pos));

        return pointsToExplore.Select(p => GetPossibleMoves(p, boardCopy.Clone())).Where(moves => moves.Any()).SelectMany(ms => ms).ToList();
    }

    private IEnumerable<GameMove> GetPossibleMoves(Point p, GameBoard board)
    {
        var moves = new List<GameMove>();

        // explore horizontal slides
        for (var i = 0; i < board.Width; i++)
        {
            if (i == p.X)
                continue;

            var futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Horizontal, p.X, i);
            
            if (futureBoard.HasMatch())
            {

                // found a match yo!
                Console.WriteLine("🎉🎉🎉🎉 I FUCKING CANNOT BELIEVE IT RIGHT NOW... SHITS LIKE MATCHING AND STUFF 🎉🎉🎉🎉\nSee for yourself, human:");
                Console.WriteLine($"I moved {p.X}x{p.Y} horizontally from {p.X} to index {i}");
                futureBoard.Draw();
                Console.WriteLine("====");
                moves.Add(new(p.X, p.Y, i, p.Y));
            }

            // them to me
            futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Horizontal, i, p.X);

            if (futureBoard.HasMatch())
            {

                // found a match yo!
                Console.WriteLine("🎉🎉🎉🎉 I FUCKING CANNOT BELIEVE IT RIGHT NOW... SHITS LIKE MATCHING AND STUFF 🎉🎉🎉🎉\nSee for yourself, human:");
                Console.WriteLine($"I moved {p.X}x{p.Y} horizontally from {p.X} to index {i}");
                futureBoard.Draw();
                Console.WriteLine("====");
                moves.Add(new(i, p.Y, p.X, p.Y));
            }
        }

        // explore vertical slides
        for (var i = 0; i < board.Height; i++)
        {
            if (i == p.Y)
                continue;

            var futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Vertical, p.Y, i);
            
            if (futureBoard.HasMatch())
            {
                // found a match yo!
                Console.WriteLine("🎉🎉🎉🎉 I FUCKING CANNOT BELIEVE IT RIGHT NOW... SHITS LIKE MATCHING AND STUFF 🎉🎉🎉🎉\nSee for yourself, human:");
                Console.WriteLine($"I moved {p.X}x{p.Y} vertically from {p.Y} to index {i}");
                futureBoard.Draw();
                Console.WriteLine("====");
                moves.Add(new(p.X, p.Y, p.X, i));
            }

            futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Vertical, i, p.Y);

            if (futureBoard.HasMatch())
            {
                // found a match yo!
                Console.WriteLine("🎉🎉🎉🎉 I FUCKING CANNOT BELIEVE IT RIGHT NOW... SHITS LIKE MATCHING AND STUFF 🎉🎉🎉🎉\nSee for yourself, human:");
                Console.WriteLine($"I moved {p.X}x{p.Y} vertically from {p.Y} to index {i}");
                futureBoard.Draw();
                Console.WriteLine("====");
                moves.Add(new(p.X, i, p.X, p.Y));
            }
        }

        return moves;
    }
}
