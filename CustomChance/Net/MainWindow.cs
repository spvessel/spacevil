// using System;
// using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Input;
// using System.Collections.ObjectModel;

// namespace CustomChance
// {
//     /// <summary>
//     /// Interaction logic for MainWindow.xaml
//     /// </summary>
//     public partial class MainWindow : Window
//     {
//         protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
//         {
//             Application.Current.Shutdown();
//         }

//         private void ButtonClose(object sender, RoutedEventArgs e)
//         {
//             CommonLogic.GetInstance().TrySerialize();
//             this.Close();
//         }

//         private void ButtonMinimize(object sender, RoutedEventArgs e)
//         {
//             WindowState = WindowState.Minimized;
//         }

//         private void Window_MouseDown(object sender, MouseButtonEventArgs e)
//         {
//             if (e.ChangedButton == MouseButton.Left)
//                 this.DragMove();
//         }

//         public MainWindow()
//         {
//             InitializeComponent();
//             DataContext = CommonLogic.GetInstance();
//         }        

//         private void OnKeyDownHandler(object sender, KeyEventArgs e)
//         {
//             if (e.Key == Key.Return)
//                 CommonLogic.GetInstance().TryRandomize.Execute(new object());
//             if(e.Key == Key.Space)
//                 CommonLogic.GetInstance().TryAddMember.Execute(new object());
//         }
//     }
// }
