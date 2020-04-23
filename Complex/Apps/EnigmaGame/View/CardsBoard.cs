using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class CardsBoard : VerticalStack
    {
        Random rnd = new Random();
        List<EnigmaCode> enigma_cards = new List<EnigmaCode>();
        List<int[]> random_box_top = new List<int[]>();
        List<int[]> random_box_bottom = new List<int[]>();

        public CardsBoard()
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            SetBackground(Color.Transparent);
            SetSpacing(0, 10);

            IsFocusable = false; 
        }

        public override void InitElements()
        {
            //board
            Grid board = new Grid(1, 7);
            board.SetSpacing(-1, 0);

            AddItems(
                board
            );

            for (int i = 0; i < 7; i++)
            {
                EnigmaCode enigma_code = new EnigmaCode();
                enigma_code.SetNumber(i + 1);
                board.AddItem(enigma_code);
                enigma_cards.Add(enigma_code);
                if (i == 0)
                    enigma_code.SetAsLeftSide();
                if (i == 6)
                    enigma_code.SetAsRightSide();
            }

            random_box_top.Add(Common.Card_tetris_top);
            random_box_top.Add(Common.Card_triangle_top);
            random_box_top.Add(Common.Card_Y_top);
            random_box_top.Add(Common.Card_6_top);
            random_box_top.Add(Common.Card_halfmoon_top);
            random_box_top.Add(Common.Card_Z_top);
            random_box_top.Add(Common.Card_4_square_top);
            random_box_top.Add(Common.Card_line_top);
            random_box_top.Add(Common.Card_4_poligon_top);

            random_box_bottom.Add(Common.Card_4_poligon_bottom);
            random_box_bottom.Add(Common.Card_Y_bottom);
            random_box_bottom.Add(Common.Card_halfmoon_bottom);
            random_box_bottom.Add(Common.Card_tetris_bottom);
            random_box_bottom.Add(Common.Card_4_square_bottom);
            random_box_bottom.Add(Common.Card_Z_bottom);
            random_box_bottom.Add(Common.Card_triangle_bottom);
            random_box_bottom.Add(Common.Card_line_bottom);
            random_box_bottom.Add(Common.Card_6_bottom);

            RandomHand();
        }

        internal void RandomHand()
        {
            
            foreach (var card in enigma_cards)
            {
                card.Top.SetCard(GetRandomСard(CardType.Top, rnd));
                card.Bottom.SetCard(GetRandomСard(CardType.Bottom, rnd));
            }
            SetSelected(0);
        }

        internal int[] GetRandomСard(CardType type, Random rnd)
        {
            //randomizing
            int index = 0;
            switch (type)
            {
                case CardType.Top:
                    index = rnd.Next(0, random_box_top.Count);
                    return random_box_top[index];

                case CardType.Bottom:
                    index = rnd.Next(0, random_box_bottom.Count);
                    return random_box_bottom[index];
                default:
                    index = rnd.Next(0, random_box_top.Count);
                    return random_box_top[index];
            }
        }
        int _index = 0;
        internal int GetSelected()
        {
            return _index;
        }
        internal void SetSelected(int index)
        {
            if (index < 0 || index > 6)
                return;
            enigma_cards.ElementAt(_index).SetSelected(false);
            enigma_cards.ElementAt(index).SetSelected(true);
            _index = index;
        }

        internal int[] GetCard(CardType type)
        {
            switch (type)
            {
                case CardType.Bottom:
                    return enigma_cards.ElementAt(_index).Bottom.GetCardValue();
                case CardType.Top:
                default:
                    return enigma_cards.ElementAt(_index).Top.GetCardValue();
            }
        }
    }
}