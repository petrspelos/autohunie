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

    public GameMove GetNextBestMove()
    {
        var moves = GetValidMoves(_board.Clone());

        return moves.MaxBy(m => GetScoreOfDepth(m)) ?? throw new InvalidOperationException("Max score couldn't be determined");
    }

    private int GetScoreOfDepth(GameMove move)
    {
        var (newBoard, score) = SimulateBoard(_board, move);

        var moves = GetValidMoves(newBoard);

        var additionalScore = moves.Select(m => SimulateBoard(newBoard, m).Item2).Max();

        return score + additionalScore;
    }

    private GraphNode GetBestNode(List<GraphNode> graph)
    {
        var terminals = graph.Where(node => 
        {
            if (graph.Any(n => n.ParentId == node.ChildId))
            {
                return false;
            }
            else
            {
                return true;
            }

        }).ToList();

        var bestScore = int.MinValue;
        GraphNode bestNode = null!;

        foreach (var node in terminals)
        {
            var parentId = node.ParentId;
            var score = node.Score;
            GraphNode thisNode = null!;

            while (parentId != 0)
            {
                var parent = graph.First(n => node.ParentId == n.ChildId);
                parentId = parent.ChildId;
                score += parent.Score;
                thisNode = parent;
            }

            if (score > bestScore)
            {
                bestScore = score;
                bestNode = thisNode;
            }
        }

        return bestNode;
    }

    private Tuple<GameBoard, int> SimulateBoard(GameBoard boardToCheck, GameMove vm)
    {
        var board = boardToCheck.Clone().ApplyMove(vm);

        var score = board.SimulateForward();

        if (boardToCheck == board)
            throw new InvalidOperationException("This move results in nothing");

        return Tuple.Create(board.Clone(), score);
    }

    private int GetRecursiveScore(GameBoard board, int score)
    {
        var moves = GetValidMoves(_board);

        if (!moves.Any())
            return score;

        var simulatedMoves = moves.Select(vm => SimulateBoard(board, vm)).ToList();

        var (bestBoard, bestScore) = simulatedMoves.MaxBy(sim => GetRecursiveScore(sim.Item1, sim.Item2))!; // get sim with highest score

        return score + bestScore;
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

        for (var i = 0; i < board.Width; i++)
        {
            if (i == p.X)
                continue;

            var futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Horizontal, p.X, i);
            
            if (futureBoard.HasMatch())
            {
                moves.Add(new(p.X, p.Y, i, p.Y));
            }

            // them to me
            futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Horizontal, i, p.X);

            if (futureBoard.HasMatch())
            {

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
                moves.Add(new(p.X, p.Y, p.X, i));
            }

            futureBoard = board.Clone();
            futureBoard.SlideToken(p.X, p.Y, SlideDirection.Vertical, i, p.Y);

            if (futureBoard.HasMatch())
            {
                moves.Add(new(p.X, i, p.X, p.Y));
            }
        }

        return moves;
    }
}

internal class GraphNode
{
    internal int ParentId { get; set; }
    internal int Score { get; set; }
    internal int ChildId { get; set; }
    internal int MoveId { get; set; }
}
