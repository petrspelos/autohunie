using AutoHunie.ConsoleApp;
using AutoHunie.ConsoleApp.Entities;
using AutoHunie.Core;
using AutoHunie.Core.Entities;
using System.Drawing.Imaging;

Console.Clear();
Logo.Draw();





var tileSize = 86;



//var solver = new GameSolver(board);
//var moveImages = solver.GetNextBestMove(bmp);

var halfTile = tileSize / 2;
//// draw move
//if (move.FromX == move.ToX)
//{
//    if (move.ToY < move.FromY)
//    {
//        var temp = move.ToY;
//        move.ToY = move.FromY;
//        move.FromY = temp;
//    }

//    // moving up or down
//    var stableX = move.FromX * tileSize + halfTile;
//    for (var i = move.FromY * tileSize + halfTile; i < move.ToY * tileSize + halfTile; i++)
//    {
//        bmp.SetPixel(stableX, i, Color.Black);
//    }

//    // var end = move.ToY * tileSize + halfTile;
//    // for (var i = 1; i < 11; i++)
//    // {
//    //     bmp.SetPixel(stableX, end - i, Color.Black);
//    //     bmp.SetPixel(stableX + i, end - i, Color.Black);
//    //     bmp.SetPixel(stableX - i, end - i, Color.Black);
//    // }
//}
//else
//{
//    if (move.ToX < move.FromX)
//    {
//        var temp = move.ToX;
//        move.ToX = move.FromX;
//        move.FromX = temp;
//    }

//    // moving horizontally
//    var stableY = move.FromY * tileSize + halfTile;
//    for (var i = move.FromX * tileSize + halfTile; i < move.ToX * tileSize + halfTile; i++)
//    {
//        bmp.SetPixel(i, stableY, Color.Black);
//    }

//    // var end = move.ToX * tileSize + halfTile;
//    // for (var i = 1; i < 11; i++)
//    // {
//    //     bmp.SetPixel(end - i, stableY, Color.Black);
//    //     bmp.SetPixel(end - i, stableY + i, Color.Black);
//    //     bmp.SetPixel(end - i, stableY - i, Color.Black);
//    // }
//}


//tileBmp.Save($"single-token.png", ImageFormat.Png);
//bmp.Save("full-board.png", ImageFormat.Png);

//bmp.DrawStraightLine(10, 10, 100, 10, Color.Aqua, 5);
//bmp.DrawStraightLine(10, 100, 10, 500, Color.Bisque, 2);
//bmp.DrawStraightLine(10, 10, 10, 300, Color.Aqua, 10);

if (Screen.PrimaryScreen is null)
    throw new InvalidOperationException("Could not determine the size of primary screen, because it was null. Is there a screen at all?");

var transparentColor = Color.Magenta;

var girlInfos = new GirlInfo[]
{
    new()
    {
        FullName = "Abia Nawazi",
        BackgroundColor = Color.Crimson,
        BackgroundColorDark = Color.Firebrick,
        HeadImagePath = @"resources\Abia head.png",
        FavTokenImagePath = @"resources\sexuality token trans.png",
        LeastFavTokenImagePath = @"resources\talent token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Sex Addict", IconPath = @"resources\Item_baggage_sex_addict.png" },
            new() { Name = "One Pump Chump", IconPath = @"resources\Item_baggage_one_pump_chump.png" },
            new() { Name = "Self-Effacing", IconPath = @"resources\Item_baggage_self_effacing.png" }
        }
    },
    new()
    {
        FullName = "Ashley Rosemarry",
        BackgroundColor = Color.Crimson,
        BackgroundColorDark = Color.Firebrick,
        HeadImagePath = @"resources\Ashley head.png",
        FavTokenImagePath = @"resources\sexuality token trans.png",
        LeastFavTokenImagePath = @"resources\romance token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Allergies", IconPath = @"resources\Item_baggage_allergies.png" },
            new() { Name = "Commitment Issues", IconPath = @"resources\Item_baggage_commitment_issues.png" },
            new() { Name = "Easily Bored", IconPath = @"resources\Item_baggage_easily_bored.png" }
        }
    },
    new()
    {
        FullName = "Brooke Belrose",
        BackgroundColor = Color.DodgerBlue,
        BackgroundColorDark = Color.FromArgb(0, 102, 204),
        HeadImagePath = @"resources\Brooke head.png",
        FavTokenImagePath = @"resources\talent token trans.png",
        LeastFavTokenImagePath = @"resources\romance token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Expensive Tastes", IconPath = @"resources\Item_baggage_expensive_tastes.png" },
            new() { Name = "Unsentimental", IconPath = @"resources\Item_baggage_unsentimental.png" },
            new() { Name = "Gold Digger", IconPath = @"resources\Item_baggage_gold_digger.png" }
        }
    },
    new()
    {
        FullName = "Candace Crush",
        BackgroundColor = Color.LimeGreen,
        BackgroundColorDark = Color.ForestGreen,
        HeadImagePath = @"resources\Candace head.png",
        FavTokenImagePath = @"resources\flirtation token trans.png",
        LeastFavTokenImagePath = @"resources\romance token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Intellectually Challenged", IconPath = @"resources\Item_baggage_intellectually_challenged.png" },
            new() { Name = "Forgetful", IconPath = @"resources\Item_baggage_forgetful.png" },
            new() { Name = "Hypersensitive", IconPath = @"resources\Item_baggage_hypersensitive.png" }
        }
    },
    new()
    {
        FullName = "Jessie Maye",
        BackgroundColor = Color.Orange,
        BackgroundColorDark = Color.FromArgb(255, 128, 0),
        HeadImagePath = @"resources\Jessie head.png",
        FavTokenImagePath = @"resources\romance token trans.png",
        LeastFavTokenImagePath = @"resources\talent token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Depression", IconPath = @"resources\Item_baggage_depression.png" },
            new() { Name = "Emphysema", IconPath = @"resources\Item_baggage_emphysema.png" },
            new() { Name = "Busted Vadge", IconPath = @"resources\Item_baggage_busted_vadge.png" }
        }
    },
    new()
    {
        FullName = "Lailani Kealoha",
        BackgroundColor = Color.Orange,
        BackgroundColorDark = Color.FromArgb(255, 128, 0),
        HeadImagePath = @"resources\Lailani head.png",
        FavTokenImagePath = @"resources\romance token trans.png",
        LeastFavTokenImagePath = @"resources\sexuality token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Old Fashioned", IconPath = @"resources\Item_baggage_old_fashioned.png" },
            new() { Name = "Low Self-Esteem", IconPath = @"resources\Item_baggage_low_self_esteem.png" },
            new() { Name = "Sheepish", IconPath = @"resources\Item_baggage_sheepish.png" }
        }
    },
    new()
    {
        FullName = "Lillian Aurawell",
        BackgroundColor = Color.Crimson,
        BackgroundColorDark = Color.Firebrick,
        HeadImagePath = @"resources\Lillian head.png",
        FavTokenImagePath = @"resources\sexuality token trans.png",
        LeastFavTokenImagePath = @"resources\flirtation token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "The Darkness", IconPath = @"resources\Item_baggage_the_darkness.png" },
            new() { Name = "Asthma", IconPath = @"resources\Item_baggage_asthma.png" },
            new() { Name = "Teen Angst", IconPath = @"resources\Item_baggage_teen_angst.png" }
        }
    },
    new()
    {
        FullName = "Lola Rembrite",
        BackgroundColor = Color.DodgerBlue,
        BackgroundColorDark = Color.FromArgb(0, 102, 204),
        HeadImagePath = @"resources\Lola head.png",
        FavTokenImagePath = @"resources\talent token trans.png",
        LeastFavTokenImagePath = @"resources\flirtation token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Busy Schedule", IconPath = @"resources\Item_baggage_busy_schedule.png" },
            new() { Name = "Caffeine Junkie", IconPath = @"resources\Item_baggage_caffeine_junkie.png" },
            new() { Name = "Miss Independent", IconPath = @"resources\Item_baggage_miss_independent.png" }
        }
    },
    new()
    {
        FullName = "Nora Delrio",
        BackgroundColor = Color.Orange,
        BackgroundColorDark = Color.FromArgb(255, 128, 0),
        HeadImagePath = @"resources\Nora head.png",
        FavTokenImagePath = @"resources\romance token trans.png",
        LeastFavTokenImagePath = @"resources\flirtation token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Abandonment Issues", IconPath = @"resources\Item_baggage_abandonment_issues.png" },
            new() { Name = "Emotionally Guarded", IconPath = @"resources\Item_baggage_emotionally_guarded.png" },
            new() { Name = "Vindictive", IconPath = @"resources\Item_baggage_vindictive.png" }
        }
    },
    new()
    {
        FullName = "Polly Bendelson",
        BackgroundColor = Color.LimeGreen,
        BackgroundColorDark = Color.ForestGreen,
        HeadImagePath = @"resources\Polly head.png",
        FavTokenImagePath = @"resources\flirtation token trans.png",
        LeastFavTokenImagePath = @"resources\talent token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Drama Queen", IconPath = @"resources\Item_baggage_drama_queen.png" },
            new() { Name = "Jealousy", IconPath = @"resources\Item_baggage_jealousy.png" },
            new() { Name = "Brand Loyalist", IconPath = @"resources\Item_baggage_brand_loyalist.png" }
        }
    },
    new()
    {
        FullName = "Sarah Stevens",
        BackgroundColor = Color.LimeGreen,
        BackgroundColorDark = Color.ForestGreen,
        HeadImagePath = @"resources\Sarah head.png",
        FavTokenImagePath = @"resources\flirtation token trans.png",
        LeastFavTokenImagePath = @"resources\sexuality token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Annoying as Fuck", IconPath = @"resources\Item_baggage_annoying_as_fuck.png" },
            new() { Name = "Attention Whore", IconPath = @"resources\Item_baggage_attention_whore.png" },
            new() { Name = "Smelly Pussy", IconPath = @"resources\Item_baggage_smelly_pussy.png" }
        }
    },
    new()
    {
        FullName = "Zoey Greene",
        BackgroundColor = Color.DodgerBlue,
        BackgroundColorDark = Color.FromArgb(0, 102, 204),
        HeadImagePath = @"resources\Zoey head.png",
        FavTokenImagePath = @"resources\talent token trans.png",
        LeastFavTokenImagePath = @"resources\sexuality token trans.png",
        Baggages = new GirlBaggage[]
        {
            new() { Name = "Kinda Crazy", IconPath = @"resources\Item_baggage_kinda_crazy.png" },
            new() { Name = "Aquaphobic", IconPath = @"resources\Item_baggage_aquaphobic.png" },
            new() { Name = "Tinnitus", IconPath = @"resources\Item_baggage_tinnitus.png" }
        }
    }
};

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
    Size = new Size(336, 376)
};


foreach (var girlInfo in girlInfos)
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


//new Font("Exo Black", 9F, FontStyle.Regular, GraphicsUnit.Point);

//PictureBox pictureBox = new PictureBox();
//pictureBox.Image = bmp;
//pictureBox.Width = 563;
//pictureBox.Height = 415;
//pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;



//form.Controls.Add(pictureBox);
form.Controls.Add(tabControl);

var settingsPanel = new Panel
{
    Location = new Point(Screen.PrimaryScreen.Bounds.Width - 210, 10),
    Size = new Size(200, 300),
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
//var windowWidth = Screen.PrimaryScreen.Bounds.Width;
//var windowHeight = Screen.PrimaryScreen.Bounds.Height;

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
    Checked = true
};

playNextMove.Click += async (sender, args) =>
{
    try
    {
        while (continuePlayingCheck.Checked)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Aight! Let's see what we've got here...");
            // > TAKE SCREENSHOT OF WHERE WE THINK THE BOARD IS
            var gameBoardRect = new Rectangle(540, 155, 775, 603);
            Bitmap bmp = new Bitmap(gameBoardRect.Width, gameBoardRect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(gameBoardRect.Left, gameBoardRect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            // > RECREATE THE TOKENS FOLDER
            if (Directory.Exists("token"))
                Directory.Delete("tokens", true);

            Directory.CreateDirectory("tokens");

            var tokenRecognizer = new TokenRecognizer();
            var board = new GameBoard();

            for (var y = 0; y < 7; y++)
            {
                for (var x = 0; x < 9; x++)
                {
                    var tileBmp = bmp.Clone(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), PixelFormat.Format32bppArgb);
                    var type = tokenRecognizer.GetTokenTypeFromImage(tileBmp);
                    tileBmp.Save($"tokens/{type}-{x}-{y}.png", ImageFormat.Png);
                    board.SetToken(x, y, new Token(type));
                }
            }

            //Console.WriteLine("RECOGNIZED GAME BOARD:");
            //board.Draw();

            var solver = new GameSolver(board);
            var move = solver.GetNextBestMove(bmp);

            WindowsApi.MoveCursorToPointScreenSpace(move.FromScreenX + gameBoardRect.Left, move.FromScreenY + gameBoardRect.Top);
            WindowsApi.DoMouseClick();
            await Task.Delay(TimeSpan.FromSeconds(1));
            WindowsApi.PressMouseButton();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            WindowsApi.MoveCursorToPointScreenSpace(move.ToScreenX + gameBoardRect.Left, move.ToScreenY + gameBoardRect.Top);
            await Task.Delay(TimeSpan.FromSeconds(0.2));
            WindowsApi.ReleaseMouseButton();
            //bmp.Save("board.png", ImageFormat.Png);
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            WindowsApi.MoveCursorToPointScreenSpace(gameBoardRect.Left - 10, gameBoardRect.Top - 10);
            WindowsApi.DoMouseClick();

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Stopped Due to an Exception: {e}");
        throw;
    }
};


settingsPanel.Controls.Add(moveGameGroupBox);
settingsPanel.Controls.Add(testMouseButton);
settingsPanel.Controls.Add(playNextMove);
settingsPanel.Controls.Add(continuePlayingCheck);

form.Controls.Add(settingsPanel);

Application.Run(form);



// var pixel = bmp.GetPixel(10, 10);
// Console.WriteLine($"R:{pixel.R} G:{pixel.G} B:{pixel.B}");



//Console.WriteLine("Ctrl + C to EXIT");
//await Task.Delay(-1);
