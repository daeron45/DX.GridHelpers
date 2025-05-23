# DX.GridHelpers

📦 Reusable utility library for DevExpress WinForms XtraGrid (GridView). Includes debounce-enabled row change handling (`GridDebouncer<T>`) and helpful extensions to simplify selection, scrolling, styling, and focused row access.

> Compatible with **.NET 8.0 WinForms** and **DevExpress 24.x**

## ✨ Features

- ✅ `GridDebouncer<T>`: Debounce logic for `FocusedRowChanged` with optional loading indicator
- 🔄 GridView extension methods:
  - Row selection by value
  - Safe access to focused row or cell
  - Scroll to focused row
  - Conditional styling
  - Refresh or clear selection

## 📦 Installation

```bash
dotnet add package DX.GridHelpers
```

> Your project must target `net8.0-windows` with `<UseWindowsForms>true</UseWindowsForms>` enabled in `.csproj`.

## 🚀 Quick Start

### Using GridDebouncer

```csharp
var debouncer = new GridDebouncer<MyRow>(
    gvwMain,
    raw => raw as MyRow,
    async row =>
    {
        // Executed after debounce delay
        Console.WriteLine($"Focused row changed: {row.Id}");
        await Task.CompletedTask;
    },
    delayMs: 300,
    loadingControl: lblLoadingIndicator
);
```

### Disposing

```csharp
debouncer.Dispose(); // When closing the form
```

## 🧩 Extension Methods

```csharp
gvw.SelectRowByValue("Id", 10);                   // Selects row with Id = 10
int? year = gvw.GetFocusedCellValue<int>("Year"); // Reads cell from focused row
var row = gvw.GetFocusedRow<MyRow>();             // Safely casts focused row
gvw.ScrollToFocusedRow();                         // Scroll to focused row
gvw.RefreshCurrentRow();                          // Redraw current row
gvw.ClearSelectionSafe();                         // Clears selection
gvw.FocusFirstRow();                              // Focus top
gvw.FocusLastRow();                               // Focus bottom
```

## 🎨 Conditional Styling

```csharp
gvw.SetRowStyleConditionally(
    row => (row as MyRow)?.IsPassive == true,
    Color.DarkGray,
    Color.LightYellow
);
```

## 🛠 Requirements

- .NET 8.0 or later
- DevExpress WinForms (XtraGrid) libraries (v24.1 or above)

## 🔒 License

MIT License

> This project is independent and not affiliated with Developer Express Inc.
> `DevExpress`, `XtraGrid`, and all trademarks are property of Developer Express Inc.

## 🌐 Author

**Rahim AYDIN**  
[GitHub](https://github.com/daeron45/DX.GridHelpers) · [NuGet](https://www.nuget.org/packages/DX.GridHelpers)
