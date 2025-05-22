using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevExpress.GridHelpers;

public class GridDebouncer<T> : IDisposable
{
    private readonly GridView _gridView;
    private readonly Func<T, Task> _callback;
    private readonly Func<object, T?> _getRowFunc;
    private CancellationTokenSource _cts;
    private int _delay;
    private readonly Control? _loadingControl;

    public bool IsEnabled { get; set; } = true;

    public int Delay
    {
        get => _delay;
        set => _delay = Math.Max(0, value);
    }

    public GridDebouncer(GridView gridView,
                         Func<object, T?> getRowFunc,
                         Func<T, Task> callback,
                         int delayMs = 250,
                         Control? loadingControl = null)
    {
        _gridView = gridView;
        _getRowFunc = getRowFunc;
        _callback = callback;
        _delay = delayMs;
        _loadingControl = loadingControl;

        _gridView.FocusedRowChanged += OnFocusedRowChanged;
    }

    private async void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        if (!IsEnabled || _gridView.FocusedRowHandle < 0)
            return;

        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            ShowLoading(true);
            await Task.Delay(_delay, token);

            if (token.IsCancellationRequested) return;

            var raw = _gridView.GetFocusedRow();
            var typedRow = _getRowFunc(raw);

            if (typedRow != null)
                await _callback(typedRow);
        }
        catch (TaskCanceledException) { }
        finally
        {
            ShowLoading(false);
        }
    }

    private void ShowLoading(bool show)
    {
        if (_loadingControl != null)
        {
            _loadingControl.Invoke(new Action(() => _loadingControl.Visible = show));
        }
    }

    public void Dispose()
    {
        _gridView.FocusedRowChanged -= OnFocusedRowChanged;
        _cts?.Cancel();
        _cts?.Dispose();
    }
}