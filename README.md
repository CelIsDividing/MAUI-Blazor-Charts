# MAUI Blazor Charts

Dynamic data visualizations (bar, pie, round gauge, historical plot) with an **MVVM Toolkit** and **WeakReferenceMessenger** architecture.
The same code is shared between a **.NET MAUI** desktop application (SQLite persistence) and a **Blazor WebAssembly** web application (deployed to Netlify).

## Structure

```
MauiBlazorCharts.slnx
src/
├── MauiBlazorCharts.Shared/     # Shared code: models, services, ViewModel, Blazor components
│   ├── Models/                  # ChartItem, DataPoint, ChartType
│   ├── Services/                # DataService, MessengerMessages, IChartStore, IUiDispatcher, SeedData
│   ├── ViewModels/              # MainViewModel
│   ├── Pages/                   # App, MainLayout, Index, Charts, Tutorial, *Card components
│   └── wwwroot/                 # app.css, app.js (shared by both heads via _content/)
├── MauiBlazorCharts/           # MAUI (Windows) head
│   ├── Data/AppDbContext.cs     # Entity Framework Core + SQLite
│   ├── Services/EfChartStore.cs # IChartStore -> SQLite
│   └── Services/MauiUiDispatcher.cs
└── MauiBlazorCharts.Web/       # Blazor WebAssembly head (Netlify)
    ├── Program.cs
    ├── Services/InMemoryChartStore.cs
    ├── Services/WebUiDispatcher.cs
    └── wwwroot/                 # index.html, _redirects
```

Platform differences are isolated behind two abstractions:

- `IChartStore` — MAUI uses EF Core + SQLite (`charts.db`), web keeps data in memory.
- `IUiDispatcher` — MAUI uses `MainThread`, web executes directly.

All other code (models, ViewModel, Messenger, Blazor UI) is shared in `MauiBlazorCharts.Shared`.

## Running

### MAUI (Windows)

```bash
dotnet build src/MauiBlazorCharts/MauiBlazorCharts.csproj -f net10.0-windows10.0.19041.0
dotnet run   --project src/MauiBlazorCharts/MauiBlazorCharts.csproj -f net10.0-windows10.0.19041.0
```

### Blazor WebAssembly (locally)

```bash
dotnet run --project src/MauiBlazorCharts.Web/MauiBlazorCharts.Web.csproj
```

It opens at `https://localhost:7153` (or the port from `launchSettings`). Routes: `/` (dashboard), `/charts` (cards), `/tutorial`.

## Deploying to Netlify

Netlify **cannot build .NET**, so the web application is built in **GitHub Actions** and published
via the Netlify CLI. The workflow is in `.github/workflows/deploy-netlify.yml`.

Setup (one-time):

1. In Netlify: **Site settings → Build & deploy → Stop builds** (Netlify should not build on its own).
2. Get a **Personal access token** (Netlify → User settings → Applications) and the **Site ID** (Site settings → General).
3. In the GitHub repository: **Settings → Secrets and variables → Actions** and add:
   - `NETLIFY_AUTH_TOKEN`
   - `NETLIFY_SITE_ID`
4. A push to `main` triggers the build and deploy.

Manual deploy (without CI):

```bash
dotnet publish src/MauiBlazorCharts.Web/MauiBlazorCharts.Web.csproj -c Release -o publish
npx netlify-cli deploy --prod --dir publish/wwwroot
```

### Netlify build fields

Since the build runs in GitHub Actions, leave the fields empty in the Netlify UI. If you use a manual CLI deploy,
`publish/wwwroot` is the publish directory.

| Field                  | Value |
| ---------------------- | ----- |
| Base directory         | (empty) |
| Build command          | (empty) |
| Publish directory      | `src/MauiBlazorCharts.Web/bin/Release/net10.0/publish/wwwroot` (or wherever you publish) |
| Functions directory    | (empty) |

## Technologies

- .NET 10, Blazor WebAssembly / Blazor Hybrid (MAUI)
- CommunityToolkit.Mvvm 8.4.0
- Entity Framework Core + SQLite (MAUI)
- Bootstrap 5 (CDN) + shared `app.css`

## Note

The web version keeps data in memory (it resets when the page is refreshed). For persistent storage in the browser,
`InMemoryChartStore` can be replaced with an IndexedDB/localStorage implementation.
