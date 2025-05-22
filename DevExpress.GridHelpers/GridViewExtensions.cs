using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System;

namespace DevExpress.GridHelpers;

public static class GridViewExtensions
{
    /// <summary>
    /// GridView seçimlerini güvenli şekilde temizler (boş grid kontrolüyle).
    /// </summary>
    public static void ClearSelectionSafe(this GridView view)
    {
        if (view is { RowCount: > 0 })
        {
            view.ClearSelection();
            view.FocusedRowHandle = -1;
        }
    }

    /// <summary>
    /// GridView'de belirli koşula göre satırın görünümünü değiştirir.
    /// </summary>
    public static void SetRowStyleConditionally(this GridView view, Func<object, bool> condition, Color foreColor, Color? backColor = null)
    {
        view.RowStyle += (s, e) =>
        {
            var row = view.GetRow(e.RowHandle);
            if (row != null && condition(row))
            {
                e.Appearance.ForeColor = foreColor;
                if (backColor.HasValue)
                    e.Appearance.BackColor = backColor.Value;
            }
        };
    }

    /// <summary>
    /// GridView'de ilk satırı odaklar (satır varsa).
    /// </summary>
    public static void FocusFirstRow(this GridView view)
    {
        if (view is { RowCount: > 0 })
        {
            view.FocusedRowHandle = 0;
        }
    }

    /// <summary>
    /// GridView'de son satırı odaklar.
    /// </summary>
    public static void FocusLastRow(this GridView view)
    {
        if (view is { RowCount: > 0 })
        {
            view.FocusedRowHandle = view.RowCount - 1;
        }
    }

    /// <summary>
    /// GridView'deki mevcut odaklı satırı yeniden çizdirir.
    /// </summary>
    public static void RefreshCurrentRow(this GridView view)
    {
        if (view.FocusedRowHandle >= 0)
            view.RefreshRow(view.FocusedRowHandle);
    }

    /// <summary>
    /// GridView'de odaklı satıra scroll yapar.
    /// </summary>
    public static void ScrollToFocusedRow(this GridView view)
    {
        if (view.FocusedRowHandle >= 0)
            view.MakeRowVisible(view.FocusedRowHandle);
    }
}