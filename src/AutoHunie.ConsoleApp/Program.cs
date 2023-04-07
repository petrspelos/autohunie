using System.Drawing;
using System.Drawing.Imaging;
using AutoHunie.ConsoleApp;
using AutoHunie.Core.Entities;

Console.Clear();
Logo.Draw();

WindowsApi.MoveWindow("HuniePop 2", 0, 0);

var rect = new Rectangle(463, 139, 666, 518);
Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
Graphics g = Graphics.FromImage(bmp);

Directory.Delete("tokens", true);
Directory.CreateDirectory("tokens");

var tileSize = 74;

g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

var tokenRecognizer = new TokenRecognizer();

var board = new Token[9, 7];

Console.WriteLine("RECOGNIZED BOARD");
Console.WriteLine("================");
for (var y = 0; y < 7; y++)
{
    for (var x = 0; x < 9; x++)
    {
        var tileBmp = bmp.Clone(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), PixelFormat.Format32bppArgb);
        var type = tokenRecognizer.GetTokenTypeFromImage(tileBmp);
        tileBmp.Save($"tokens/{type}-{x}-{y}.png", ImageFormat.Png);
        board[x, y] = new Token(type);
        board[x, y].Draw();
    }
    Console.WriteLine();
}
Console.WriteLine("================");

var solver = new GameSolver(board);
var move = solver.GetNextBestMove(bmp);

var halfTile = tileSize / 2;
// draw move
if (move.FromX == move.ToX)
{
    if (move.ToY < move.FromY)
    {
        var temp = move.ToY;
        move.ToY = move.FromY;
        move.FromY = temp;
    }

    // moving up or down
    var stableX = move.FromX * tileSize + halfTile;
    for (var i = move.FromY * tileSize + halfTile; i < move.ToY * tileSize + halfTile; i++)
    {
        bmp.SetPixel(stableX, i, Color.Black);
    }

    // var end = move.ToY * tileSize + halfTile;
    // for (var i = 1; i < 11; i++)
    // {
    //     bmp.SetPixel(stableX, end - i, Color.Black);
    //     bmp.SetPixel(stableX + i, end - i, Color.Black);
    //     bmp.SetPixel(stableX - i, end - i, Color.Black);
    // }
}
else
{
    if (move.ToX < move.FromX)
    {
        var temp = move.ToX;
        move.ToX = move.FromX;
        move.FromX = temp;
    }

    // moving horizontally
    var stableY = move.FromY * tileSize + halfTile;
    for (var i = move.FromX * tileSize + halfTile; i < move.ToX * tileSize + halfTile; i++)
    {
        bmp.SetPixel(i, stableY, Color.Black);
    }

    // var end = move.ToX * tileSize + halfTile;
    // for (var i = 1; i < 11; i++)
    // {
    //     bmp.SetPixel(end - i, stableY, Color.Black);
    //     bmp.SetPixel(end - i, stableY + i, Color.Black);
    //     bmp.SetPixel(end - i, stableY - i, Color.Black);
    // }
}


//tileBmp.Save($"single-token.png", ImageFormat.Png);
bmp.Save("full-board.png", ImageFormat.Png);

bmp.DrawStraightLine(10, 10, 100, 10, Color.Aqua, 5);
bmp.DrawStraightLine(10, 100, 10, 500, Color.Bisque, 2);
bmp.DrawStraightLine(10, 10, 10, 300, Color.Aqua, 10);

Form form = new Form();
form.Width = 678;
form.Height = 555;

form.Text = "AutoHunie 2";
form.TopMost = true;

PictureBox pictureBox = new PictureBox();
pictureBox.Image = bmp;
pictureBox.Width = 563;
pictureBox.Height = 415;
pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;



form.Controls.Add(pictureBox);
Application.Run(form);



// var pixel = bmp.GetPixel(10, 10);
// Console.WriteLine($"R:{pixel.R} G:{pixel.G} B:{pixel.B}");



//Console.WriteLine("Ctrl + C to EXIT");
//await Task.Delay(-1);
