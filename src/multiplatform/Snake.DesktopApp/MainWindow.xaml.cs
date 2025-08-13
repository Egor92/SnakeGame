
﻿using System.Windows.Input;
using Snake.DesktopApp.ViewModels;
using Snake.Logic;

namespace Snake.DesktopApp;

public partial class MainWindow
{
    private readonly GameViewModel _viewModel;
    private readonly GameLogic _gameLogic;

    public MainWindow(GameViewModel viewModel, GameLogic gameLogic)
    {
        _viewModel = viewModel;
        _gameLogic = gameLogic;
        InitializeComponent();
        Width = 434;
        Height = 480;
        PreviewKeyDown += OnPreviewKeyDown;
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        Direction? requestedDirection = e.Key switch
        {
            Key.Up => Direction.Up,
            Key.Down => Direction.Down,
            Key.Left => Direction.Left,
            Key.Right => Direction.Right,
            _ => null
        };

        if (requestedDirection != null)
        {
            _gameLogic.ChangeDirection(requestedDirection.Value);
        }
    }
}