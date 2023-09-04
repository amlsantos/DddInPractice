#region

using System;
using System.Windows;
using System.Windows.Media;
using Logic.Common;
using Logic.SnackMachines;

#endregion

namespace UI.SnackMachines;

public class SnackPileViewModel
{
    private readonly SnackPile _snackPile;

    public SnackPileViewModel(SnackPile snackPile)
    {
        _snackPile = snackPile;
    }

    public string Price => _snackPile.Price.ToString("C2");
    public int Amount => _snackPile.Quantity;
    public int ImageWidth => GetImageWidth(_snackPile.Snack);
    public ImageSource? Image => Application.Current.FindResource("img" + _snackPile.Snack.Name) as ImageSource;

    private int GetImageWidth(Entity snack)
    {
        if (snack == Snack.Chocolate)
            return 120;

        if (snack == Snack.Soda)
            return 70;

        if (snack == Snack.Gum)
            return 70;

        throw new InvalidOperationException($"There is no image, for the given Snack: {snack.Id}");
    }
}