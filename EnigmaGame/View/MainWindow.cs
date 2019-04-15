using System;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Timers;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class MainWindow : ActiveWindow
    {
        Header header = new Header();
        TimeLimit time_limit = new TimeLimit();
        CardsBoard cards_board = new CardsBoard();
        Hand hand = new Hand();
        StopMenu menu = new StopMenu();

        SoundPlayer wrong;
        SoundPlayer key;
        SoundPlayer next;

        public override void InitWindow()
        {
            SetParameters(nameof(MainWindow), "EnigmaGame", 798, 640, false);
            SetAntiAliasingQuality(MSAA.MSAA8x);
            SetBackground(Common.Background);
            SetPadding(1, 1, 1, 1);
            SetMinSize(GetWidth(), GetHeight());

            //icons
            var big = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("EnigmaGame.icon.png"));
            var small = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("EnigmaGame.icon.png"));
            SetIcon(big, small);

            TitleBar title = new TitleBar("EnigmaGame");
            title.SetIcon(small, 20, 20);
            title.SetShadow(5, 0, 2, Color.Black);

            VerticalStack layout = new VerticalStack();
            layout.SetBackground(GetBackground());
            layout.SetPadding(30, 30, 30, 30);
            layout.SetMargin(10, title.GetHeight() + 10, 10, 10);
            layout.SetSpacing(0, 10);
            layout.SetBorder(new Border(Common.Selected, new CornerRadius(10), 4));
            layout.SetShadow(10, 0, 0, Color.Black);


            //adding
            AddItems(
                layout,
                title,
                menu
                );

            layout.AddItems(
                header,
                time_limit,
                cards_board,
                hand
            );

            //events
            EventKeyPress += OnKeyPress;

            wrong = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("EnigmaGame.wrong.wav"));
            key = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("EnigmaGame.switch.wav"));
            next = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("EnigmaGame.next.wav"));
        }

        private void OnKeyPress(IItem sender, KeyArgs args)
        {
            if (args.Key == KeyCode.Escape)
            {
                if (menu.IsVisible())
                    Close();
                else
                {
                    _stop.Stop();
                    _stop = null;
                    menu.SetVisible(true);
                }
            }
            if (args.Key == KeyCode.Space)
            {
                if (_stop == null)
                {
                    header.ResetLevel();
                    menu.SetVisible(false);
                    time_limit.SetValue(15);
                    ResetTimer();
                    cards_board.RandomHand();
                    next.Play();
                }
            }

            if (menu.IsVisible())
                return;

            if (args.Key == KeyCode.Left)
            {
                key.Play();
                hand.MoveLeft();
            }
            if (args.Key == KeyCode.Right)
            {
                key.Play();
                hand.MoveRight();
            }
            if (args.Key == KeyCode.Up)
            {
                key.Play();
                hand.SetChosen(CardType.Top);
            }
            if (args.Key == KeyCode.Down)
            {
                key.Play();
                hand.SetChosen(CardType.Bottom);
            }
            if (args.Key == KeyCode.Enter)
            {
                if (Common.CompareMatrices(cards_board.GetCard(CardType.Top), hand.GetCard(CardType.Top))
                    && Common.CompareMatrices(cards_board.GetCard(CardType.Bottom), hand.GetCard(CardType.Bottom))
                )
                {
                    _stop.Stop();
                    time_limit.SetValue(15);
                    ResetTimer();

                    if (cards_board.GetSelected() == 6)
                    {
                        cards_board.RandomHand();
                        header.LevelUp();
                        if (header.GetLevel() % 5 == 0)
                            time_limit.SetMaxValue(time_limit.GetMaxValue() - 1);
                    }
                    else
                    {
                        cards_board.SetSelected(cards_board.GetSelected() + 1);
                    }
                    next.Play();
                }
                else
                {
                    wrong.Play();
                    header.LevelDown();
                }
            }
        }

        internal System.Timers.Timer _stop;
        private void ResetTimer()
        {
            _stop = new System.Timers.Timer(1000);
            _stop.Elapsed += TimesOut;
            _stop.Start();
        }

        private void TimesOut(Object source, ElapsedEventArgs e)
        {
            _stop.Stop();
            if (time_limit.GetValue() == 0)
            {
                time_limit.SetValue(15);
                header.LevelDown();
            }
            else
                time_limit.SetValue(time_limit.GetValue() - 1);
            _stop.Start();
        }
    }
}