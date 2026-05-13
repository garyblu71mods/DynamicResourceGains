---
# 📦 Instalacja Dynamic Resource Gains

Przewodnik instalacji moda do Kerbal Space Program 1.x.

## 📋 Spis Treści

- [Wymagania](#wymagania)
- [Instalacja Krok Po Kroku](#instalacja-krok-po-kroku)
- [Weryfikacja](#weryfikacja)
- [Pierwsze Uruchomienie](#pierwsze-uruchomienie)
- [Rozwiązywanie Problemów](#rozwiązywanie-problemów)
- [Linki do Dokumentacji](#linki-do-dokumentacji)

---

## ✅ Wymagania

- **Kerbal Space Program 1.x**
- **Career Mode** (mod pracuje TYLKO w Career Mode!)
- **Windows** (mod testowany na Windows 10/11)
- Prawa do zapisu w folderze KSP

---

## 🔧 Instalacja Krok Po Kroku

### Krok 1: Zlokalizuj Folder KSP

#### Epic Games
```
C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\
```

#### Steam
```
C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\
```

#### Inne
Poszukaj folderu zawierającego `KSP.exe`

### Krok 2: Utwórz Strukturę Folderów

Przejdź do: `[KSP]\GameData\`

Jeśli folder `GameData` nie istnieje - utwórz go.

W `GameData` utwórz nowy folder:
```
DynamicResourceGains
```

W `DynamicResourceGains` utwórz:
```
Plugins
```

**Finalna struktura:**
```
[KSP]\
  ├── KSP.exe
  ├── GameData\
  │   └── DynamicResourceGains\
  │       └── Plugins\
```

### Krok 3: Skopiuj DLL

Skopiuj plik `Dynamic Resource Gains.dll` do:
```
[KSP]\GameData\DynamicResourceGains\Plugins\
```

**Weryfikacja:** Plik powinien być tutaj:
```
C:\Program Files\Epic Games\KerbalSpaceProgram\
  KerbalSpaceProgram\GameData\DynamicResourceGains\
  Plugins\Dynamic Resource Gains.dll
```

### Krok 4: Uruchom Grę

1. Otwórz Kerbal Space Program
2. Wybierz **Career Mode**
3. Załaduj lub utwórz grę
4. Wejdź do **Space Center**

---

## ✔️ Weryfikacja

### Krok 1: Sprawdzenie Pliku

W PowerShell lub CMD wpisz:
```powershell
Test-Path "C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\DynamicResourceGains\Plugins\Dynamic Resource Gains.dll"
```

Powinno wyświetlić: `True`

### Krok 2: Sprawdzenie w Grze

1. Uruchom KSP
2. Wejdź do **Space Center**
3. Poszukaj przycisku **DRG** na pasku narzędzi (górny lewy róg)

Jeśli widzisz przycisk - mod się załadował! ✓

### Krok 3: Sprawdzenie Logów

```
[KSP]\KSP_Data\output_log.txt
```

Szukaj linii zawierającej `[DRG]` - potwierdzi że mod się załadował.

---

## 🚀 Pierwsze Uruchomienie

1. **Uruchom grę** w Career Mode
2. **Wejdź do Space Center**
3. **Kliknij przycisk DRG** na pasku
4. **Zmień wartości** w polach (opcjonalnie)
5. **Kliknij Apply** aby testować
6. **Kliknij Save** aby zapisać

→ **[INSTRUKCJA.md](INSTRUKCJA.md)** - Pełny przewodnik użytkowania

---

## 🆘 Rozwiązywanie Problemów

### Problem: Mod się nie pojawia

1. Czy jesteś w **Space Center**? (wymagane!)
2. Czy to **Career Mode**? (sandbox nie działa!)
3. Czy DLL jest w `GameData\DynamicResourceGains\Plugins\`?
4. Czy nazwa folderu jest dokładnie `DynamicResourceGains`?
5. Czy DLL nie jest w podfolderze folder-w?

### Problem: Przycisk nie widać

- Restart gry (całkowite zamknięcie KSP)
- Sprawdzenie ścieżki do DLL
- Sprawdzenie logów (szukaj `[DRG]`)

### Problem: Gra się crashuje

1. Sprawdź `KSP_Data\output_log.txt`
2. Szukaj błędów zawierających `[DRG]`
3. Spróbuj usunąć folder `DynamicResourceGains` i zainstalować od nowa

→ **[FAQ.md](FAQ.md)** - Więcej rozwiązań

---

## 📚 Linki do Dokumentacji

| Dokument | Zawartość |
|----------|-----------|
| [README.md](README.md) | Przegląd moda |
| [INSTRUKCJA.md](INSTRUKCJA.md) | Pełny przewodnik użytkowania |
| [PRZEPISY.md](PRZEPISY.md) | 7 gotowych konfiguracji |
| [CHEATSHEET.md](CHEATSHEET.md) | Szybka ściąga |
| [FAQ.md](FAQ.md) | Pytania i odpowiedzi |

---

## 🎯 Co Dalej?

Po zainstalowaniu:

1. **Przeczytaj [INSTRUKCJA.md](INSTRUKCJA.md)** - Dowiedz się jak używać
2. **Sprawdź [PRZEPISY.md](PRZEPISY.md)** - Wybierz gotową konfigurację
3. **Zajrzyj do [CHEATSHEET.md](CHEATSHEET.md)** - Szybka referencja
4. **Graj i ciesz się!** 🚀

---

**Instalacja ukończona!**
