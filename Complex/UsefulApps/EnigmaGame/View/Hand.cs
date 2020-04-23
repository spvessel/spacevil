using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class Hand : HorizontalStack
    {
        CardType _type = CardType.Top;
        Grid top_grid = new Grid(1, 9);
        Grid bottom_grid = new Grid(1, 9);
        List<Card> top_list = new List<Card>();
        List<Card> bottom_list = new List<Card>();

        public Hand()
        {
            SetSize(718, 150);
            SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            SetBackground(Color.Transparent);
            SetAlignment(ItemAlignment.HCenter);
            SetMargin(0, 20, 0, 20);
            IsFocusable = false;
            SetPadding(10, 0, 10, 0);
        }

        public override void InitElements()
        {
            Grid left_arrows = new Grid(2, 1);
            left_arrows.SetWidth(20);
            left_arrows.SetWidthPolicy(SizePolicy.Fixed);
            Grid right_arrows = new Grid(2, 1);
            right_arrows.SetStyle(left_arrows.GetCoreStyle());

            //board
            VerticalStack board = new VerticalStack();
            board.SetMargin(5, 0, 5, 0);
            board.SetSpacing(0, 0);

            top_grid.SetBorder(new Border(Common.Significant, new CornerRadius(10, 10, 0, 0), 1));
            top_grid.SetPadding(5, 1, 1, 0);

            //numbers
            bottom_grid.SetBorder(new Border(Common.Significant, new CornerRadius(0, 0, 10, 10), 1));
            bottom_grid.SetPadding(5, 0, 1, 1);

            //adding
            AddItems(
                left_arrows,
                board,
                right_arrows
            );

            left_arrows.AddItems(
                Common.GetArrow(135),
                Common.GetArrow(135)
            );

            right_arrows.AddItems(
                Common.GetArrow(-45),
                Common.GetArrow(-45)
            );

            board.AddItems(
                top_grid,
                bottom_grid
            );

            for (int i = 0; i < 9; i++)
            {
                Card top_card = new Card();
                top_grid.AddItem(top_card);

                Card bottom_card = new Card();
                bottom_grid.AddItem(bottom_card);

                top_list.Add(top_card);
                bottom_list.Add(bottom_card);

                if (i == 0 || i == 8)
                {
                    top_card.SetBorderFill(Color.Transparent);
                    bottom_card.SetBorderFill(Color.Transparent);
                }
            }
            InitHand();

        }

        public void InitHand()
        {
            top_list[0].SetCard(Common.Card_halfmoon_top);
            top_list[1].SetCard(Common.Card_tetris_top);
            top_list[2].SetCard(Common.Card_triangle_top);
            top_list[3].SetCard(Common.Card_line_top);
            top_list[4].SetCard(Common.Card_6_top);
            top_list[5].SetCard(Common.Card_4_poligon_top);
            top_list[6].SetCard(Common.Card_4_square_top);
            top_list[7].SetCard(Common.Card_Y_top);
            top_list[8].SetCard(Common.Card_Z_top);

            bottom_list[0].SetCard(Common.Card_halfmoon_bottom);
            bottom_list[1].SetCard(Common.Card_tetris_bottom);
            bottom_list[2].SetCard(Common.Card_triangle_bottom);
            bottom_list[3].SetCard(Common.Card_line_bottom);
            bottom_list[4].SetCard(Common.Card_6_bottom);
            bottom_list[5].SetCard(Common.Card_4_poligon_bottom);
            bottom_list[6].SetCard(Common.Card_4_square_bottom);
            bottom_list[7].SetCard(Common.Card_Y_bottom);
            bottom_list[8].SetCard(Common.Card_Z_bottom);

            top_list[4].SetSelected(true, true);
        }

        internal void SetChosen(CardType type)
        {
            switch (type)
            {
                case CardType.Bottom:
                    _type = CardType.Bottom;
                    bottom_list[4].SetSelected(true, true);
                    top_list[4].SetSelected(false, false);
                    break;
                case CardType.Top:
                default:
                    _type = CardType.Top;
                    top_list[4].SetSelected(true, true);
                    bottom_list[4].SetSelected(false, false);
                    break;
            }
        }

        internal void MoveLeft()
        {
            List<int[]> moved_list = new List<int[]>();
            switch (_type)
            {
                case CardType.Top:
                    foreach (var card in top_list)
                        moved_list.Add(card.GetCardValue());
                    for (int i = 0; i < moved_list.Count - 1; i++)
                    {
                        top_list[i + 1].SetCard(moved_list[i]);
                    }
                    top_list[0].SetCard(moved_list[top_list.Count - 1]);
                    top_list[4].SetSelected(true, true);
                    break;
                case CardType.Bottom:
                    foreach (var card in bottom_list)
                        moved_list.Add(card.GetCardValue());
                    for (int i = 0; i < moved_list.Count - 1; i++)
                    {
                        bottom_list[i + 1].SetCard(moved_list[i]);
                    }
                    bottom_list[0].SetCard(moved_list[bottom_list.Count - 1]);
                    bottom_list[4].SetSelected(true, true);
                    break;
            }
        }
        internal void MoveRight()
        {
            List<int[]> moved_list = new List<int[]>();
            switch (_type)
            {
                case CardType.Top:
                    foreach (var card in top_list)
                        moved_list.Add(card.GetCardValue());

                    for (int i = 1; i < moved_list.Count; i++)
                    {
                        top_list[i - 1].SetCard(moved_list[i]);
                    }
                    top_list[top_list.Count - 1].SetCard(moved_list[0]);
                    top_list[4].SetSelected(true, true);
                    break;
                case CardType.Bottom:
                    foreach (var card in bottom_list)
                        moved_list.Add(card.GetCardValue());

                    for (int i = 1; i < moved_list.Count; i++)
                    {
                        bottom_list[i - 1].SetCard(moved_list[i]);
                    }
                    bottom_list[bottom_list.Count - 1].SetCard(moved_list[0]);
                    bottom_list[4].SetSelected(true, true);
                    break;
            }
        }

        internal int[] GetCard(CardType type)
        {
            switch (type)
            {
                case CardType.Bottom:
                    return bottom_list[4].GetCardValue();
                case CardType.Top:
                default:
                    return top_list[4].GetCardValue();
            }
        }
    }
}