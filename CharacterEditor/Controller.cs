using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace CharacterEditor
{
    internal class Controller
    {
        private MainWindow _mw;
        private Model _model;
        internal Controller(Model model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can not be NULL");

            _model = model;
            _mw = new MainWindow();
        }

        internal void Start()
        {
            InitController();
            _mw.Show();
        }

        private void InitController()
        {
            FillListBox(_model.GetListOfNames((int)_mw.NumberCount.GetValue()));
            if (_mw.ItemList.GetListContent().Count > 0)
                _mw.ItemList.SetSelection(0);

            _mw.ItemList.GetArea().SetFocus();

            _mw.BtnGenerate.EventMouseClick += (sender, args) =>
            {
                _mw.ItemList.Clear();
                FillListBox(_model.GetListOfNames((int)_mw.NumberCount.GetValue()));
            };

            _mw.BtnSave.EventMouseClick += (sender, args) =>
            {
                OpenEntryDialog opd = new OpenEntryDialog("Save File:", FileSystemEntryType.File, OpenDialogType.Save);
                opd.AddFilterExtensions("Text files (*.txt);*.txt");
                opd.OnCloseDialog += () =>
                {
                    if (opd.GetResult() != null)
                    {
                        if (_model.WriteFile(opd.GetResult(), _mw.ItemText.GetText()))
                        {
                            PopUpMessage popUpInfo = new PopUpMessage("Character save successfully!");
                            popUpInfo.SetBackground(188, 188, 188);
                            popUpInfo.SetForeground(Color.Black);
                            popUpInfo.Show(_mw.GetHandler());
                        }
                        else
                        {
                            PopUpMessage popUpInfo = new PopUpMessage("Character save failed!");
                            popUpInfo.SetBackground(188, 188, 188);
                            popUpInfo.SetForeground(Color.Black);
                            popUpInfo.Show(_mw.GetHandler());
                        }
                    }
                };
                opd.Show(_mw.GetHandler());
            };
        }

        private void AssignListItemEvents(CharacterCard item)
        {
            item.EventMouseClick += (sender, args) =>
            {
                _mw.ItemText.SetText(item.ToString());
            };
            item.EventKeyPress += (sender, args) =>
            {
                if (args.Key == KeyCode.Enter)
                    item.EventMouseClick?.Invoke(sender, null);
            };
        }

        private void FillListBox(List<CharacterInfo> listNames)
        {
            foreach (var name in listNames)
            {
                CharacterCard item = new CharacterCard(name);
                AssignListItemEvents(item);
                _mw.ItemList.AddItem(item);
            }
        }
    }
}