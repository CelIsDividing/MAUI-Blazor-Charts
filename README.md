# 📊 MAUI Blazor Charts - Dinamički Prikazi Podataka

Kompletna aplikacija za prikaz podataka kroz različite tipove **dinamičkih grafika** sa modernom UI arhitekturom. Aplikacija koristi .NET 10.0 preview verziju i najnoviju Blazor WebAssembly arhitekturu.

## 🎯 Karakteristike

✅ **Četiri tipa grafika sa dinamičkim ažuriranjem:**
- 📈 **Bar Chart** - Prodaja po mesecima
- 🥧 **Pie Chart** - Tržišni udeo po proizvodima  
- 🍩 **Doughnut Chart** - Kvalitet servisa sa indikatorima
- 📉 **Line Chart** - Trend aktivnih korisnika (12 meseci)

✅ **Dinamičko ažuriranje:**
- Auto-refresh svakih 10 sekundi
- Interaktivni dugmići za osvežavanje
- Mogućnost dodavanja novih podataka

✅ **Moderan dizajn:**
- Gradijentna pozadina (plava → ljubičasta)
- Responzivni layout sa CSS Grid
- Animacije i hover efekti

✅ **Visoke performanse:**
- Chart.js biblioteka
- Minimalne zavisnosti
- Brzo učitavanje

## 🚀 Pokretanje

### Preduslov
- .NET 10.0.101 SDK
- Windows PowerShell ili terminal

### Koraci

```bash
# Navigacija u projekat
cd "c:\Users\krist\Documents\Fakultet\New folder\MauiBlazorCharts"

# Pokretanje dev servera
dotnet watch
# ili
dotnet run --urls "http://localhost:5001"
```

Otvori browser na: **http://localhost:5001/charts.html**

## 📁 Struktura

```
MauiBlazorCharts/
├── wwwroot/
│   ├── charts.html              # Glavna aplikacija sa graficima
│   ├── index.html               # Blazor host
│   ├── blazor.boot.json         # Blazor konfiguracija
│   ├── css/app.css              # Stilovi
│   └── _framework/              # Blazor runtime (auto-generiš)
├── Pages/                       # Blazor komponente
├── Models/                      # Modeli podataka
├── ViewModels/                  # MVVM ViewModels
├── Services/                    # Servisne klase
├── MauiBlazorCharts.csproj      # Project datoteka
└── README.md
```

## 🔧 Tehnologije

- **.NET 10.0.101** - Preview verzija
- **Blazor WebAssembly** - WASM runtime
- **Chart.js 4.x** - Profesionalni grafici
- **CommunityToolkit.Mvvm 8.2.2** - MVVM pattern
- **SkiaSharp 3.116.0** - Grafika

## 📊 Grafici

### 1. Bar Chart - Prodaja (5 meseci)
```
Vrednosti: 15k, 25k, 20k, 30k, 28k EUR
Status: +12% rast
```

### 2. Pie Chart - Tržišni Udeo
```
Proizvod A: 30%
Proizvod B: 25%
Proizvod C: 25%
Proizvod D: 20%
```

### 3. Doughnut Chart - Kvalitet Servisa
```
Uspešno:     85% (zeleno)
Upozorenja:  10% (žuto)
Greške:       5% (crveno)
Dostupnost:  99.9%
```

### 4. Line Chart - Trend Korisnika
```
Period: 12 meseci
Rast: Od 1,000 → 6,200+ korisnika
```

## 🎨 Dizajn

- Gradijent: `linear-gradient(135deg, #667eea 0%, #764ba2 100%)`
- Kartice: Bela, shadow, hover animation
- Font: Segoe UI, Tahoma, Geneva
- Responsive: CSS Grid sa auto-fit

## 💻 Korišćenje

### Osvežavanje grafika
```javascript
// Ručno
updateBarChart()
updatePieChart()
updateDoughnutChart()
updateLineChart()

// Automatski (svakih 10 sekundi)
// Ugrađeno u JavaScript
```

### Dodavanje podataka
```javascript
// Dodaj novi red u Bar chart
addBarData()
```

### Dugmići
- **"Osvežiti"** - Nova random vrednost
- **"Dodaj Podatak"** - Novi red

## 🔌 API Integracija

Prosti primer za integraciju sa REST API-jem:

```javascript
async function loadData(endpoint) {
    const response = await fetch(`/api/${endpoint}`);
    return await response.json();
}

// Korišćenje
loadData('sales').then(data => {
    barChart.data.datasets[0].data = data;
    barChart.update();
});
```

## 🧪 Testiranje

```bash
# Dev server
dotnet watch

# Čišćenje
dotnet clean

# Gradnja
dotnet build

# Pokretanje
dotnet run
```

## 🔍 Troubleshooting

| Problem | Rešenje |
|---------|---------|
| "Došlo je do greške" | Osvežite stranicu (F5) |
| Port 5001 zauzet | Koristite: `dotnet run --urls "http://localhost:5002"` |
| Chart.js ne učitava | Proverite internet, ili koristite lokalnu kopiju |
| Grafici se ne ažuriraju | Proverite DevTools console za greške |

## 📚 Resursi

- [Chart.js dokumentacija](https://www.chartjs.org/docs/latest/)
- [Blazor WebAssembly](https://learn.microsoft.com/en-us/aspnet/core/blazor/webassembly)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/mvvm_introduction)

## ✅ Status

- ✅ Aplikacija pokrenuta
- ✅ Svi 4 grafika funkcionalni
- ✅ Dinamičko ažuriranje radi
- ✅ Responzivni dizajn
- ✅ Interaktivni dugmići

## 🎓 Edukativni Primeri

Ovaj projekat demonstrira:
- ✅ Blazor WebAssembly arhitektura
- ✅ MVVM pattern sa CommunityToolkit
- ✅ WeakReferenceMessenger komunikacija
- ✅ Chart.js integracija
- ✅ Dinamičko ažuriranje UI-ja
- ✅ Responzivni CSS dizajn

## 📝 Verzija

**Verzija:** 1.0.0  
**Status:** ✅ Potpuno funkcionalno  
**Datum:** 9. Junski 2026  

---

**Napomena:** Za optimalne rezultate, koristi moderni browser (Chrome, Edge, Firefox) sa JavaScript omogućenim.

# Za Windows
dotnet build -f net8.0-windows10.0.19041.0

# Ili za sve platforme
dotnet build
```

#### 5. Pokretanje Aplikacije

```bash
# Za Windows
dotnet run -f net8.0-windows10.0.19041.0
```

---

## 📊 Struktura Projekta

```
MauiBlazorCharts/
├── Models/                      # Podatkovni modeli
│   └── ChartModels.cs          # ChartItem, DataPoint, ChartType
├── ViewModels/                  # MVVM ViewModels
│   └── MainViewModel.cs         # Glavna ViewModel sa komandama
├── Views/                       # Blazor komponente za grafike
│   ├── BarChartCard.razor      # Stubičasti grafikon
│   ├── PieChartCard.razor      # Kružni dijagram
│   ├── RoundGaugeCard.razor    # Okrugla mera
│   └── HistoricalPlotCard.razor # Linijski grafikon
├── Pages/                       # Blazor stranice
│   ├── App.razor               # Root aplikacija
│   ├── MainLayout.razor        # Master layout
│   ├── Index.razor             # Početna stranica
│   ├── Charts.razor            # Stranica sa karticama
│   └── Tutorial.razor          # Tutorial stranica
├── Services/                    # Poslovne logike
│   ├── DataService.cs          # Generisanje test podataka
│   └── MessengerMessages.cs    # Messenger poruke
├── Resources/                   # Resursi aplikacije
│   ├── Styles/
│   │   └── app.css            # CSS stilovi
│   └── Images/
├── MauiProgram.cs              # Konfiguracija MAUI programa
├── MainPage.xaml               # XAML stranica sa BlazorWebView
├── MainPage.xaml.cs            # Code-behind
├── App.xaml                    # Aplikacijski resursi
├── AppShell.xaml               # Shell navigacija
└── MauiBlazorCharts.csproj     # Projekat fajl
```

---

## 🛠️ Tehnologije i Biblioteke

### Glavne Komponente

| Biblioteka | Verzija | Svrha |
|-----------|---------|-------|
| Microsoft.Maui.Controls | 8.0.70 | Desktop/Mobile framework |
| Microsoft.AspNetCore.Components.WebView.Maui | 8.0.70 | Blazor integracija |
| CommunityToolkit.Mvvm | 8.2.2 | MVVM pattern implementacija |
| LiveChartsCore | 4.0.0-rc1.2 | Grafički prikazi |
| LiveChartsCore.SkiaSharp | 4.0.0-rc1.2 | SkiaSharp backend |
| SkiaSharp | 2.88.8 | 2D grafika |
| OxyPlot.Core | 2.1.2 | Dodatni grafički alati |

### NuGet Instalacija

Ako trebate da dodate dodatne pakete:

```bash
dotnet add package [NazivPaketa] --version [Verzija]
```

---

## 📚 Detaljni Tutorial

### 1. Tipovi Grafikona

#### 🔵 Bar Chart (Stubičasti Grafikon)
- **Svrha**: Prikazivanje vrednosti kao stubice
- **Korišćenje**: Poređenje vrednosti, prikazivanje količina
- **Boja**: Plava (#1976d2)
- **Datoteka**: `BarChartCard.razor`

**Primer:**
```
Prodaja po mesecima:
┌──┐
│  │
│  │ 45000
│  │
└──┘
```

#### 🟡 Pie Chart (Kružni Dijagram)
- **Svrha**: Prikazivanje procenata i udela
- **Korišćenje**: Tržišni udeo, distribucija resursa
- **Boja**: Narandžasta (#ff9800)
- **Datoteka**: `PieChartCard.razor`

**Primer:**
```
Tržišni udeo:
    ╔═══╗
   ╱    ╲ 35% (Naš)
  │      │ 65% (Ostali)
   ╲    ╱
    ╚═══╝
```

#### 🎯 Round Gauge (Okrugla Mera)
- **Svrha**: Prikazivanje statusa kao brzinomer
- **Korišćenje**: Procenat završenosti, kvalitet, učinkovitost
- **Boja**: Crvena (#ff5722)
- **Datoteka**: `RoundGaugeCard.razor`

**Karakteristike:**
- Igla se kreće u krug
- Boja se menja: zelena (dobro) → narandžasta (upozorenje) → crvena (kritično)

#### 📈 Historical Plot (Linijski Grafikon)
- **Svrha**: Prikazivanje trenda kroz vreme
- **Korišćenje**: Analiza trendova, monitoring
- **Boja**: Ljubičasta (#673ab7)
- **Datoteka**: `HistoricalPlotCard.razor`

**Karakteristike:**
- X-osa: Vremenske oznake
- Y-osa: Vrednosti
- Prikazuje do 30 poslednje vrednosti

---

### 2. MVVM Toolkit Korišćenje

#### Observable Property

```csharp
[ObservableProperty]
private ObservableCollection<ChartItem> charts = new();
```

Automatski generiše:
- `Charts` property sa PropertyChanged notifikacijama
- Binding u XAML/Blazor

#### Relay Command

```csharp
[RelayCommand]
public void IncreaseValue(ChartItem? chart)
{
    // Logika...
}
```

Generiše:
- `IncreaseValueCommand` sa `CanExecute`/`Execute`
- Parameter binding: `Command="{Binding IncreaseValueCommand}" CommandParameter="{Binding Chart}"`

#### ObservableRecipient

Automatski se registruje/uregistruje sa messenger-om:

```csharp
public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
        IsActive = true; // Omugućava messenger
    }
}
```

---

### 3. WeakReferenceMessenger Pattern

#### Slanje Poruke

```csharp
WeakReferenceMessenger.Default.Send(
    new ChartValueChangedMessage(chartId, newValue));
```

#### Primanje Poruke

```csharp
WeakReferenceMessenger.Default.Register<ChartValueChangedMessage>(
    this, HandleChartValueChanged);

private void HandleChartValueChanged(
    object recipient, 
    ChartValueChangedMessage message)
{
    // Obrada poruke
}
```

#### Prednosti

- ✅ **Weak references**: Nema memory leak-ova
- ✅ **Decoupling**: Komponente nisu direktno zavisne
- ✅ **Broadcast**: Poruke ide svim slušačima
- ✅ **Type-safe**: Kompajler proverava tipove

---

### 4. Data Flow Dijagram

```
User Interaction
        ↓
MainViewModel (IncreaseValueCommand)
        ↓
Chart Value Updated + Historical Data
        ↓
WeakReferenceMessenger.Send(ChartValueChangedMessage)
        ↓
Blazor Components (Update via Parameter Change)
        ↓
SVG/HTML Rerendering
        ↓
Visual Chart Update
```

---

### 5. Dodavanje Nove Kartice

Ako želite da dodate novu karticu:

1. **Kreirajte model u `DataService.cs`:**

```csharp
var newChart = new ChartItem
{
    Title = "Nova Kartica",
    Type = ChartType.Bar,
    CurrentValue = 50,
    MaxValue = 100,
    HistoricalData = GenerateHistoricalData(5, () => random.NextDouble() * 100)
};
```

2. **Dodajte u `GetInitialCharts()`:**

```csharp
initialCharts.Add(newChart);
```

3. **Kreirajte Blazor komponentu** (ako je novi tip)

4. **Dodajte u `Charts.razor`:**

```csharp
@case ChartType.NoviTip:
    <NovaTipoviCard ... />
    break;
```

---

## 🎨 Prilagođavanje Stilova

### CSS Datoteke

- `Resources/Styles/app.css` - Glavni stilovi

### Boje

Lako se mogu promeniti u CSS datoteci:

```css
--primary: #2196f3;
--secondary: #ff9800;
--success: #4caf50;
--error: #f44336;
```

### Responsive Design

Aplikacija je responsive i prilagođava se različitim rezolucijama:

- Desktop: 3-4 kartice po redu
- Tablet: 2 kartice po redu
- Mobilni: 1 kartica po redu

---

## ⚙️ Napredna Konfiguracija

### Zavisnosti (Dependency Injection)

Konfiguracija u `MauiProgram.cs`:

```csharp
.Services
    .AddScoped<DataService>()
    .AddSingleton<MainViewModel>()
    .AddBlazorWebView()
```

### Messenger Aktivacija

ViewModeli nasledjuju `ObservableRecipient`:

```csharp
public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
        IsActive = true; // Aktiviraj messenger
    }
}
```

---

## 🐛 Otklanjanje Greške

### Problem: Aplikacija se ne pokreće

**Rešenje:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Problem: NuGet paketi nisu pronađeni

**Rešenje:**
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Problem: MAUI workload nije instaliran

**Rešenje:**
```bash
dotnet workload install maui
dotnet workload restore
```

### Problem: BlazorWebView nije prikazan

**Rešenje:**
1. Proverite da je `MainPage.xaml` ispravno konfigurisan
2. Proverite `MauiProgram.cs` ima `.AddBlazorWebView()`
3. Proverite da se `.css` datoteke nalaze u `Resources/Styles/`

---

## 📖 Dodatni Resursi

### Dokumentacija

- [MAUI Official Docs](https://learn.microsoft.com/en-us/dotnet/maui/)
- [Blazor Official Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [MVVM Toolkit Docs](https://github.com/CommunityToolkit/dotnet/blob/main/tools/MVVM%20Toolkit/README.md)
- [LiveCharts Documentation](https://livecharts.dev/)

### Korisni Linkovi

- WeakReferenceMessenger: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger
- Observable Collections: https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1

---

## 📝 Primeri Korišćenja

### Primer 1: Dinamička Promena Vrednosti

```csharp
// U MainViewModel
[RelayCommand]
public void IncreaseValue(ChartItem? chart)
{
    if (chart == null) return;
    
    // Povećaj
    chart.CurrentValue = Math.Min(
        chart.CurrentValue + (chart.MaxValue * 0.1), 
        chart.MaxValue
    );
    
    // Dodaj u istoriju
    chart.HistoricalData.Add(new DataPoint
    {
        Value = chart.CurrentValue,
        Timestamp = DateTime.Now
    });
    
    // Pošalji poruku
    WeakReferenceMessenger.Default.Send(
        new ChartValueChangedMessage(chart.Id, chart.CurrentValue)
    );
}
```

### Primer 2: Prilagođena Vrednost

```csharp
// U Blazor komponenti
<input type="range" min="0" max="@ChartData?.MaxValue" 
       @onchange="SetCustomValue" />

@code {
    private void SetCustomValue(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            ViewModel.SetValueCommand.Execute((ChartData, value));
        }
    }
}
```

---

## 🎓 Vežbe

### Vežba 1: Dodaj novu karticu
Dodajte novu karticu sa `ChartType.HistoricalPlot` koja prati korisnikove akcije.

### Vežba 2: Prilagodi stilove
Promenite boje i fontove da odgovaraju vašem brend identitetu.

### Vežba 3: Dodaj export
Implementirajte mogućnost izvoza podataka u CSV format.

### Vežba 4: Real-time ažuriranje
Kreirajte BackgroundService koji redovno ažurira vrednosti.

---

## 📄 Licenca

Ovaj projekat je besplatan za korišćenje i modifikaciju.

---

## 💬 Kontakt i Pitanja

Za pitanja ili predloge, slobodno kontaktirajte.

---

## 📊 Verzija i Istorija

- **v1.0.0** (2024) - Početna verzija sa 4 tipa grafikona
- Planirano: Real-time data binding, export, custom themes

---

**Uživajte u razvoju! 🚀**
