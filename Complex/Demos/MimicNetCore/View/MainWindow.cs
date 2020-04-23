using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using SpaceVIL.Common;

namespace MimicSpace
{
    class MainWindow : ActiveWindow
    {
        TitleBar title;
        ListBox contacts_bar;
        TextEdit input_message;
        FreeArea freeNotes;
        ListBox conversation;

        public override void InitWindow()
        {
            //window init
            SetParameters(nameof(MainWindow), nameof(MainWindow), 1300, 840, false);
            SetMinSize(500, 300);
            SetBackground(32, 34, 37);

            //title
            title = new TitleBar("Mimic");
            title.SetStyle(Styles.GetTitleBarStyle());

            HorizontalStack h_stack = new HorizontalStack();
            h_stack.SetMargin(0, 22, 0, 0);
            h_stack.SetSpacing(0, 0);

            //left block
            VerticalStack left = new VerticalStack();//70
            left.SetWidth(70);
            left.SetWidthPolicy(SizePolicy.Fixed);
            left.SetPadding(2, 0, 2, 2);
            left.SetSpacing(0, 10);

            SpaceVIL.Rectangle line = new SpaceVIL.Rectangle();
            line.SetBackground(32, 34, 37);
            line.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            line.SetHeight(1);
            line.SetShadow(2, 0, 2, Color.FromArgb(150, 0, 0, 0));
            line.SetMargin(8, 0, 8, 0);

            ButtonCore mimic_icon = new ButtonCore("M");
            mimic_icon.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 30, FontStyle.Bold));
            mimic_icon.SetSize(50, 50);
            mimic_icon.SetBackground(114, 137, 208);
            mimic_icon.SetAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            mimic_icon.SetBorderRadius(new CornerRadius(15));

            SpaceVIL.Rectangle divider = new SpaceVIL.Rectangle();
            divider.SetBackground(47, 49, 54);
            divider.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            divider.SetHeight(2);
            divider.SetMargin(15, 0, 15, 0);

            ButtonToggle notes_area_btn = new ButtonToggle("N");
            notes_area_btn.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 30, FontStyle.Bold));
            notes_area_btn.SetSize(50, 50);
            notes_area_btn.SetBackground(Color.Transparent);
            notes_area_btn.SetForeground(100, 101, 105);
            notes_area_btn.SetAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            notes_area_btn.SetBorderRadius(new CornerRadius(15));
            notes_area_btn.SetBorderFill(100, 101, 105);
            notes_area_btn.SetBorderThickness(1);
            notes_area_btn.EventMouseClick += (sender, args) =>
            {
                if (notes_area_btn.IsToggled())
                {
                    freeNotes.SetVisible(true);
                    conversation.SetVisible(false);
                    freeNotes.GetParent().Update(GeometryEventType.ResizeHeight);
                }
                else
                {
                    freeNotes.SetVisible(false);
                    conversation.SetVisible(true);
                    freeNotes.GetParent().Update(GeometryEventType.ResizeHeight);
                }
            };

            ButtonCore add_btn = new ButtonCore();
            add_btn.SetSize(50, 50);
            add_btn.SetBackground(Color.Transparent);
            add_btn.SetAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            add_btn.SetBorderRadius(new CornerRadius(25));
            add_btn.SetBorderFill(100, 101, 105);
            add_btn.SetBorderThickness(1);
            add_btn.SetToolTip("Add a new friend.");
            add_btn.EventMouseClick += (sender, args) =>
            {
                AddMenuDialog dialog = new AddMenuDialog();
                dialog.OnCloseDialog += () =>
                {
                    string result = dialog.InputResult;
                    if (!result.Equals(String.Empty))
                    {
                        contacts_bar.AddItem(InfinityItemsBox.GetVisualContact(result, input_message));
                    }
                };
                dialog.Show(this);
            };

            CustomShape plus = new CustomShape();
            plus.SetBackground(100, 101, 105);
            plus.SetSize(20, 20);
            plus.SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            plus.SetTriangles(GraphicsMathService.GetCross(20, 20, 2, 0));

            //middleblock
            VerticalStack middle = new VerticalStack();//240
            middle.SetStyle(Styles.GetCommonContainerStyle());
            middle.SetWidth(240);
            middle.SetWidthPolicy(SizePolicy.Fixed);
            middle.SetBackground(47, 49, 54);
            middle.SetBorderRadius(new CornerRadius(6, 0, 6, 0));

            Frame search_bar = new Frame();
            search_bar.SetBorderRadius(new CornerRadius(6, 0, 0, 0));
            search_bar.SetBackground(47, 49, 54);
            search_bar.SetHeight(48);
            search_bar.SetPadding(15, 0, 15, 0);
            search_bar.SetHeightPolicy(SizePolicy.Fixed);
            search_bar.SetShadow(2, 0, 2, Color.FromArgb(150, 0, 0, 0));

            contacts_bar = new ListBox();
            contacts_bar.SetPadding(8, 8, 8, 8);
            contacts_bar.SetBackground(Color.Transparent);
            contacts_bar.SetHScrollBarPolicy(VisibilityPolicy.Never);
            contacts_bar.SetVScrollBarPolicy(VisibilityPolicy.Never);
            contacts_bar.SetSelectionVisible(false);

            Frame user_bar = new Frame();
            user_bar.SetBorderRadius(new CornerRadius(0, 0, 6, 0));
            user_bar.SetBackground(42, 44, 49);
            user_bar.SetHeight(48);
            user_bar.SetPadding(15, 0, 15, 0);
            user_bar.SetHeightPolicy(SizePolicy.Fixed);
            user_bar.SetAlignment(ItemAlignment.Bottom);

            TextEdit search = new TextEdit();
            search.SetPadding(10, 0, 10, 0);
            search.SetFont(DefaultsService.GetDefaultFont(12));
            search.SetForeground(150, 150, 150);
            search.SetSubstrateText("Find or start conversation");
            search.SetSubstrateFontSize(12);
            search.SetSubstrateFontStyle(FontStyle.Regular);
            search.SetSubstrateForeground(100, 100, 100);
            search.SetHeight(32);
            search.SetBackground(37, 39, 43);
            search.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            search.SetBorderRadius(4);
            search.SetBorderThickness(1);
            search.SetBorderFill(32, 34, 37);

            //right block
            VerticalStack right = new VerticalStack();//expand
            right.SetStyle(Styles.GetCommonContainerStyle());
            right.SetSpacing(0, 2);

            HorizontalStack conversation_bar = new HorizontalStack();
            conversation_bar.SetBackground(54, 57, 63);
            conversation_bar.SetHeight(48);
            conversation_bar.SetHeightPolicy(SizePolicy.Fixed);
            conversation_bar.SetPadding(10, 0, 0, 0);
            conversation_bar.SetSpacing(5, 0);
            conversation_bar.SetShadow(2, 0, 2, Color.FromArgb(150, 0, 0, 0));

            freeNotes = new FreeArea();
            freeNotes.SetVisible(false);
            freeNotes.SetBackground(Color.FromArgb(5, 255, 255, 255));

            conversation = new ListBox();
            conversation.SetPadding(4, 4, 4, 4);
            conversation.SetBackground(Color.Transparent);
            conversation.SetHScrollBarPolicy(VisibilityPolicy.Never);
            conversation.GetArea().SetPadding(16, 10, 2, 2);
            conversation.SetSelectionVisible(false);

            VerticalScrollBar vs = conversation.VScrollBar;
            vs.SetWidth(16);
            vs.SetBorderThickness(4);
            vs.SetBorderFill(54, 57, 63);
            vs.SetBorderRadius(new CornerRadius(9));
            vs.SetArrowsVisible(false);
            vs.SetBackground(47, 49, 54);
            vs.SetPadding(0, 0, 0, 0);
            vs.Slider.Track.SetBackground(Color.Transparent);
            vs.Slider.SetBorderThickness(4);
            vs.Slider.SetBorderFill(54, 57, 63);
            vs.Slider.SetBorderRadius(new CornerRadius(9));
            vs.Slider.SetBackground(32, 34, 37, 255);
            vs.Slider.SetMargin(new Indents(0, 0, 0, 0));
            vs.Slider.RemoveAllItemStates();

            HorizontalStack input_bar = new HorizontalStack();
            input_bar.SetHeight(44);
            input_bar.SetHeightPolicy(SizePolicy.Fixed);
            input_bar.SetMargin(20, 10, 20, 30);
            input_bar.SetPadding(15, 0, 8, 0);
            input_bar.SetSpacing(10, 0);
            input_bar.SetBackground(72, 75, 81);
            input_bar.SetBorderRadius(new CornerRadius(6, 6, 6, 6));

            ButtonCore emoji = new ButtonCore("+");
            emoji.SetSize(24, 24);
            emoji.SetBackground(126, 128, 132);
            emoji.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            emoji.SetBorderRadius(new CornerRadius(12));

            SpaceVIL.Rectangle divider_v = new SpaceVIL.Rectangle();
            divider_v.SetBackground(126, 128, 132);
            divider_v.SetWidth(2);
            divider_v.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Expand);
            divider_v.SetMargin(0, 10, 0, 10);

            input_message = new TextEdit();
            input_message.SetBackground(Color.Transparent);
            input_message.SetForeground(Color.White);
            input_message.SetAlignment(ItemAlignment.VCenter);
            input_message.SetBorderRadius(new CornerRadius(0, 3, 0, 3));
            input_message.SetSubstrateText("Message @Jackson");
            input_message.EventKeyPress += (sender, args) =>
            {
                if (args.Key == KeyCode.Enter)
                {
                    conversation.AddItem(InfinityItemsBox.GetMessage(input_message.GetText()));
                    input_message.Clear();
                }
            };

            ButtonCore add_note = InfinityItemsBox.GetOrdinaryButton();
            add_note.SetForeground(166, 167, 168);
            add_note.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 12, FontStyle.Bold));
            add_note.SetText("Add new note");
            add_note.SetWidth(100);
            add_note.SetShadow(4, 0, 2, Color.FromArgb(150, 0, 0, 0));
            add_note.EventMouseClick += (sender, args) =>
            {
                NoteBlock block = InfinityItemsBox.GetNoteBlock();
                block.SetPosition(100, 100);
                freeNotes.AddItem(block);
            };

            //adding items
            AddItems(
                title,
                h_stack
            );
            h_stack.AddItems(
                left,
                middle,
                right
            );
            left.AddItems(
                line,
                mimic_icon,
                divider,
                notes_area_btn,
                add_btn
            );
            add_btn.AddItem(
                plus
            );
            middle.AddItems(
                search_bar,
                contacts_bar,
                user_bar
            );
            search_bar.AddItems(
                search
            );
            user_bar.AddItems(
                new UserBar("Daniel")
            );
            right.AddItems(
                conversation_bar,
                conversation,
                freeNotes,
                input_bar
            );
            conversation_bar.AddItems(
                add_note,
                InfinityItemsBox.GetOrdinaryButton(),
                InfinityItemsBox.GetOrdinaryButton(),
                InfinityItemsBox.GetOrdinaryButton(),
                InfinityItemsBox.GetOrdinaryButton()
            );
            input_bar.AddItems(
                emoji,
                divider_v,
                input_message
            );
            contacts_bar.AddItems(
                InfinityItemsBox.GetVisualContact("Jackson", input_message),
                InfinityItemsBox.GetVisualContact("Evelyn", input_message),
                InfinityItemsBox.GetVisualContact("Alexander", input_message),
                InfinityItemsBox.GetVisualContact("Matthew", input_message)
            );
        }
    }
}