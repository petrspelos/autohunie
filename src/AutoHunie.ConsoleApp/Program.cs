﻿using AutoHunie.ConsoleApp;
using AutoHunie.ConsoleApp.Entities;
using AutoHunie.Core;
using AutoHunie.Core.Entities;

Console.Clear();
Logo.Draw();

if (Screen.PrimaryScreen is null)
    throw new InvalidOperationException("Could not determine the size of primary screen, because it was null. Is there a screen at all?");

var transparentColor = Color.Magenta;

IImageProcessing imgProcessing = new WindowsImageProcessing();

var form = new Form
{
    Text = "AutoHunie 2",
    StartPosition = FormStartPosition.Manual,
    Location = new Point(0, 0),
    Width = Screen.PrimaryScreen.Bounds.Width,
    Height = Screen.PrimaryScreen.Bounds.Height,
    FormBorderStyle = FormBorderStyle.None,
    BackColor = transparentColor,
    TransparencyKey = transparentColor,
    TopMost = true
};

var tabControl = new TabControl
{
    Location = new Point(0, 50),
    Size = new Size(336, 376)
};

foreach (var girlInfo in GirlInfo.GetAll())
{
    var tabIndex = tabControl.TabPages.Count;

    tabControl.TabPages.Add(girlInfo.FullName);

    tabControl.TabPages[tabIndex].BackColor = girlInfo.BackgroundColor;

    var headImage = new PictureBox
    {
        Image = new Bitmap(girlInfo.HeadImagePath),
        InitialImage = null,
        Location = new Point(8, 8),
        Margin = new Padding(3, 4, 3, 4),
        Size = new Size(100, 98),
        SizeMode = PictureBoxSizeMode.Zoom,
        TabIndex = 4,
        TabStop = false
    };
    tabControl.TabPages[tabIndex].Controls.Add(headImage);
    tabControl.TabPages[tabIndex].Controls.Add(new PictureBox
    {
        Image = new Bitmap(girlInfo.FavTokenImagePath),
        InitialImage = null,
        Location = new Point(130, 40),
        Margin = new Padding(3, 4, 3, 4),
        Size = new Size(63, 58),
        SizeMode = PictureBoxSizeMode.Zoom,
        BackColor = girlInfo.BackgroundColorDark
    });


    tabControl.TabPages[tabIndex].Controls.Add(new PictureBox
    {
        Image = new Bitmap(girlInfo.LeastFavTokenImagePath),
        InitialImage = null,
        Location = new Point(223, 40),
        Margin = new Padding(3, 4, 3, 4),
        Size = new Size(63, 58),
        SizeMode = PictureBoxSizeMode.Zoom,
        BackColor = girlInfo.BackgroundColorDark
    });
    var fontUtils = new FontUtilities();
    fontUtils.AddToFontCollection(@"resources\Exo-Black.ttf");
    fontUtils.AddToFontCollection(@"resources\Exo-BlackItalic.ttf");

    var font = new Font(
       fontUtils.FontCollection.Families.First(),
       16,
       FontStyle.Regular,
       GraphicsUnit.Pixel);
    tabControl.TabPages[tabIndex].Controls.Add(new Label
    {
        Text = girlInfo.FullName,
        Font = font,
        ForeColor = SystemColors.ButtonHighlight,
        Location = new Point(114, 8),
        Size = new Size(206, 34),
    });

    tabControl.TabPages[tabIndex].Controls.Add(new Label
    {
        Text = "Least Fav",
        Font = new Font(
       fontUtils.FontCollection.Families.First(),
       14,
       FontStyle.Regular,
       GraphicsUnit.Pixel),
        ForeColor = SystemColors.ButtonHighlight,
        Location = new Point(223, 101)
    });

    tabControl.TabPages[tabIndex].Controls.Add(new Label
    {
        Text = "Most Fav",
        Font = new Font(
       fontUtils.FontCollection.Families.First(),
       14,
       FontStyle.Regular,
       GraphicsUnit.Pixel),
        ForeColor = SystemColors.ButtonHighlight,
        Location = new Point(130, 101)
    });


    var baggageBox = new GroupBox
    {
        BackColor = girlInfo.BackgroundColorDark,
        Font = new Font("Exo Black", 12F, FontStyle.Italic, GraphicsUnit.Point),
        ForeColor = Color.White,
        Location = new Point(8, 123),
        Size = new Size(314, 220),
        TabIndex = 16,
        TabStop = false,
        Text = "Baggage"
    };

    var heightOffset = 49;
    for (var i = 0; i < girlInfo.Baggages.Count(); i++)
    {
        var baggage = girlInfo.Baggages.ElementAt(i);
        var y = 23 + (heightOffset * i);

        var box = new PictureBox();
        box.Image = new Bitmap(baggage.IconPath);
        box.Location = new Point(242, y);
        box.Size = new Size(51, 43);
        box.SizeMode = PictureBoxSizeMode.Zoom;
        box.TabIndex = 12;
        box.TabStop = false;

        baggageBox.Controls.Add(box);

        baggageBox.Controls.Add(new RadioButton
        {
            BackColor = Color.Transparent,
            Font = new Font("Exo Black", 12F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.White,
            Location = new Point(6, y),
            Size = new Size(321, 43),
            TabIndex = 0,
            Text = baggage.Name,
            UseVisualStyleBackColor = false
        });
    }

    baggageBox.Controls.Add(new RadioButton
    {
        BackColor = Color.Transparent,
        Font = new Font("Exo Black", 12F, FontStyle.Regular, GraphicsUnit.Point),
        ForeColor = Color.White,
        Location = new Point(6, 23 + (heightOffset * girlInfo.Baggages.Count())),
        Size = new Size(321, 43),
        TabIndex = 0,
        Text = "None",
        UseVisualStyleBackColor = false,
        Checked = true
    });

    tabControl.TabPages[tabIndex].Controls.Add(baggageBox);
}

form.Controls.Add(tabControl);

var settingsPanel = new Panel
{
    Location = new Point(Screen.PrimaryScreen.Bounds.Width - 210, 50),
    Size = new Size(200, 750),
    BackColor = Color.Gray
};

var exitButton = new Button
{
    Image = Image.FromFile(@"resources\x.ico"),
    ImageAlign = ContentAlignment.MiddleLeft,
    TextAlign = ContentAlignment.MiddleCenter,
    Location = new Point(10, 10),
    Size = new Size(180, 40),
    Text = "Exit",
    BackColor = Color.White
};

exitButton.Click += (sender, args) =>
{
    Application.Exit();
};

settingsPanel.Controls.Add(exitButton);

var moveGameGroupBox = new GroupBox
{
    Text = "Move Game Window",
    Location = new Point(5, 60),
    Size = new Size(190, 100),
    ForeColor = Color.White
};

var windowWidth = 1536;
var windowHeight = 864;

var xResolutionSelect = new NumericUpDown
{
    Minimum = 0,
    Maximum = 100000,
    Value = windowWidth,
    Location = new Point(5, 70),
    Size = new Size(85, 40)
};

var yResolutionSelect = new NumericUpDown
{
    Minimum = 0,
    Maximum = 100000,
    Value = windowHeight,
    Location = new Point(100, 70),
    Size = new Size(85, 40)
};

xResolutionSelect.ValueChanged += (sender, args) =>
{
    if (sender as NumericUpDown is var control && control is not null)
        windowWidth = (int)control.Value;
};


yResolutionSelect.ValueChanged += (sender, args) =>
{
    if (sender as NumericUpDown is var control && control is not null)
        windowHeight = (int)control.Value;
};

var moveGameButton = new Button
{
    Image = Image.FromFile(@"resources\topleftarrow.ico"),
    ImageAlign = ContentAlignment.MiddleLeft,
    TextAlign = ContentAlignment.MiddleCenter,
    Location = new Point(5, 20),
    Size = new Size(180, 40),
    Text = "Move Game Window",
    BackColor = Color.White,
    ForeColor = Color.Black
};

moveGameButton.Click += (sender, args) =>
{
    WindowsApi.MoveWindow("HuniePop 2", 0, 0, windowWidth, windowHeight);
};

moveGameGroupBox.Controls.Add(moveGameButton);
moveGameGroupBox.Controls.Add(xResolutionSelect);
moveGameGroupBox.Controls.Add(yResolutionSelect);

var testMouseButton = new Button
{
    Image = Image.FromFile(@"resources\mouse.ico"),
    ImageAlign = ContentAlignment.MiddleLeft,
    TextAlign = ContentAlignment.MiddleCenter,
    Location = new Point(10, 170),
    Size = new Size(180, 40),
    Text = "Automate Mouse Events",
    BackColor = Color.White
};

testMouseButton.Click += async (sender, args) =>
{
    Console.WriteLine("Talent: " + TokenWeights.Talent);
    Console.WriteLine("Stamina: " + TokenWeights.Stamina);
    Console.WriteLine("Joy: " + TokenWeights.Joy);
    Console.WriteLine("Passion: " + TokenWeights.Passion);
    Console.WriteLine("Sexuality: " + TokenWeights.Sexuality);
    Console.WriteLine("Romance: " + TokenWeights.Romance);
    Console.WriteLine("Flirtation: " + TokenWeights.Flirtation);
    Console.WriteLine("BrokenHeart: " + TokenWeights.BrokenHeart);

    WindowsApi.MoveCursorToPointScreenSpace(1097, 458);
    WindowsApi.DoMouseClick();
    await Task.Delay(TimeSpan.FromSeconds(1));
    WindowsApi.PressMouseButton();
    await Task.Delay(TimeSpan.FromSeconds(0.5));
    WindowsApi.MoveCursorToPointScreenSpace(1097, 632);
    await Task.Delay(TimeSpan.FromSeconds(0.2));
    WindowsApi.ReleaseMouseButton();
};

var playNextMove = new Button
{
    Image = Image.FromFile(@"resources\flower.ico"),
    ImageAlign = ContentAlignment.MiddleLeft,
    TextAlign = ContentAlignment.MiddleCenter,
    Location = new Point(10, 220),
    Size = new Size(180, 40),
    Text = "Auto-Play Next Move",
    BackColor = Color.White
};


var continuePlayingCheck = new CheckBox
{
    Location = new Point(10, 250),
    Size = new Size(180, 40),
    Text = "Continue Making Moves",
    ForeColor = Color.Black,
    Checked = false
};

playNextMove.Click += async (sender, args) =>
{
    try
    {
        do
        {
            if (Directory.Exists("token"))
                Directory.Delete("tokens", true);

            Directory.CreateDirectory("tokens");

            var tokenRecognizer = new TokenRecognizer();

            var boardImg = imgProcessing.GetExpectedBoardScreenshot();
            var board = imgProcessing.RecognizeBoard(boardImg);

            var bestMove = board.FindBestMoveConsideringFuture(2);
            var move = new GameMove(bestMove.x, bestMove.y, bestMove.newX, bestMove.newY);
            var (fromX, fromY, toX, toY) = imgProcessing.GetScreenCoordinatesFromMove(move);

            WindowsApi.MoveCursorToPointScreenSpace(fromX, fromY);
            WindowsApi.DoMouseClick();
            await Task.Delay(TimeSpan.FromSeconds(1));
            WindowsApi.PressMouseButton();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            WindowsApi.MoveCursorToPointScreenSpace(toX, toY);
            await Task.Delay(TimeSpan.FromSeconds(0.2));
            WindowsApi.ReleaseMouseButton();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            WindowsApi.MoveCursorToPointScreenSpace(0, 0);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
        while (continuePlayingCheck.Checked);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
};

var tokenWeightsGroupBox = new GroupBox
{
    Text = "Token Weights",
    Location = new Point(5, 290),
    Size = new Size(190, 450),
    ForeColor = Color.White
};

var tokenImages = new (string, int, Action<int>)[]
{
    (@"resources\stamina token trans.png", TokenWeights.Stamina, (int val) => TokenWeights.Stamina = val),
    (@"resources\sentiment token trans.png", TokenWeights.Sentiment, (int val) => TokenWeights.Stamina = val),
    (@"resources\joy token trans.png", TokenWeights.Joy, (int val) => TokenWeights.Joy = val),
    (@"resources\passion token trans.png", TokenWeights.Passion, (int val) => TokenWeights.Passion = val),
    (@"resources\talent token trans.png", TokenWeights.Talent, (int val) => TokenWeights.Talent = val),
    (@"resources\sexuality token trans.png", TokenWeights.Sexuality, (int val) => TokenWeights.Sexuality = val),
    (@"resources\romance token trans.png", TokenWeights.Romance, (int val) => TokenWeights.Romance = val),
    (@"resources\flirtation token trans.png", TokenWeights.Flirtation, (int val) => TokenWeights.Flirtation = val),
    (@"resources\broken heart token trans.png", TokenWeights.BrokenHeart, (int val) => TokenWeights.BrokenHeart = val),
    (@"resources\multiplier.png", TokenWeights.QuadMultiplier, (int val) => TokenWeights.QuadMultiplier = val),
};

for (var i = 0; i < tokenImages.Length; i++)
{
    var (tokenImage, weightValue, setFunc) = tokenImages[i];
    tokenWeightsGroupBox.Controls.Add(new PictureBox
    {
        Image = new Bitmap(tokenImage),
        InitialImage = null,
        Location = new Point(10, (42 * i) + 20),
        Size = new Size(40, 40),
        SizeMode = PictureBoxSizeMode.Zoom
    });
    
    var weightControl = new NumericUpDown
    {
        Minimum = -100000,
        Maximum = 100000,
        Value = weightValue,
        Location = new Point(60, (42 * i) + 20),
        Size = new Size(85, 40)
    };
    weightControl.ValueChanged += (object? sender, EventArgs args) => setFunc?.Invoke((int)weightControl.Value);

    tokenWeightsGroupBox.Controls.Add(weightControl);
}

settingsPanel.Controls.Add(moveGameGroupBox);
settingsPanel.Controls.Add(testMouseButton);
settingsPanel.Controls.Add(playNextMove);
settingsPanel.Controls.Add(continuePlayingCheck);
settingsPanel.Controls.Add(tokenWeightsGroupBox);

form.Controls.Add(settingsPanel);

Application.Run(form);
