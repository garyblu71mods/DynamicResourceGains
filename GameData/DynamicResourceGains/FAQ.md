# ❓ FAQ - Często Zadawane Pytania

Odpowiedzi na najczęstsze pytania o modie **Dynamic Resource Gains**.

---

## 🎮 Instalacja i Setup

### P: Gdzie powinienem zainstalować mod?

**O:** W folderze `GameData` gry KSP. Ścieżka powinna być:

```
[KSP_FOLDER]\GameData\DynamicResourceGains\Plugins\Dynamic Resource Gains.dll
```

**Przykład dla Epic Games:**
```
C:\Program Files\Epic Games\KerbalSpaceProgram\
  KerbalSpaceProgram\
  GameData\
  DynamicResourceGains\
  Plugins\
  Dynamic Resource Gains.dll
```

### P: Czemu mod się nie pojawia?

**O:** Sprawdzaj po kolei:

1. **Czy jesteś w Space Center?**
   - ✅ Mod widoczny TYLKO tam!
   - ❌ Nie w locie, VAB, edytorze, tracking station

2. **Czy to Career Mode?**
   - ✅ Career Mode - OK! 
   - ❌ Sandbox Mode - NIE DZIAŁA

3. **Czy DLL jest w dobrym folderze?**
   - Check: `GameData\DynamicResourceGains\Plugins\`
   - Sprawdź pisownię (wielkie litery!)

4. **Czy restartowałeś grę?**
   - Zamknij KSP całkowicie
   - Uruchom ponownie

### P: Jak mogę się upewnić, że mod się załadował?

**O:** Sprawdź logi KSP:

```
Player.log (w katalogu AppData\LocalLow\Squad\Kerbal Space Program\)
```

Szukaj linii zawierającej `[DRG]` - to oznacza że mod się załadował poprawnie.

---

## 🖱️ Interfejs i Podstawy

### P: Gdzie znaleźć przycisk moda?

**O:** W górnym lewym rogu ekranu Space Center, na pasku narzędzi (toolbar).
Jest to kolorowy przycisk z wykresem/krzywą (graficzna ikona).

### P: Co różni Apply od Save?

**O:** Dwie różne funkcje:

```
APPLY:
  ✅ Zmienia wartości NATYCHMIAST (w bieżącej sesji)
  ✅ Możesz testować bez zapisywania
  ❌ Po wznowieniu gry zmiany znikają
  👉 Używaj gdy testujesz ustawienia

SAVE:
  ✅ Zapisuje wartości NA TRWAŁE
  ✅ Po wznowieniu gry będą te same ustawienia
  ✅ Tworzy backup w konfiguracji saves
  👉 Używaj gdy jesteś zadowolony
```

**Zalecana procedura:**
```
1. Zmień wartości w polu
2. Klikaj APPLY (test w grze)
3. Sprawdź efekty (patrz "Current" na górze)
4. Jeśli ok → klikaj SAVE
5. Jeśli nie ok → zmień i Apply ponownie
```

### P: Czy mogę zmienić ustawienia bez restartu gry?

**O:** **TAK!** Klikając Apply natychmiast zmienia się dla aktualnej sesji. Nie musisz restartować!

### P: Jak usunąć mod?

**O:** Po prostu usuń folder:

```
GameData\DynamicResourceGains\
```

I gotowe! Mod zostanie usunięty.

---

## 📊 Dynamic Science

### P: Jak dokładnie działa Dynamic Science?

**O:** Dynamic Science zmniejsza ilość nauki w miarę twojego postępu. Składa się z **dwóch kar:**

#### 1️⃣ **Kara za Tier (Tech Tier Penalty)**
- Za każdy **odblokowany tier technologiczny** tracisz procent nauki
- Domyślnie: **2% na tier**

**Przykład:**
```
Tier 0 (początek):  0 tiery × 2% = 0% kary → 100% nauki
Tier 5:             5 × 2% = 10% kary → 90% nauki
Tier 10:           10 × 2% = 20% kary → 80% nauki
Tier 15:           15 × 2% = 30% kary → 70% nauki
```

#### 2️⃣ **Kara za Kroki (Science Step Penalty)**
- Za każde **X punktów nauki**, którą otrzymałeś całkowicie, dodawana jest dodatkowa kara
- Domyślnie: **co 3000 nauki** → **0.1% dodatkowej kary**

**Przykład:**
```
Otrzymałeś 0 nauki:       0 ÷ 3000 = 0 kroków × 0.1% = 0% kary
Otrzymałeś 3000 nauki:    3000 ÷ 3000 = 1 krok × 0.1% = 0.1% kary
Otrzymałeś 15000 nauki:   15000 ÷ 3000 = 5 kroków × 0.1% = 0.5% kary
Otrzymałeś 30000 nauki:   30000 ÷ 3000 = 10 kroków × 0.1% = 1% kary
```

#### **Razem:**
```
Tier penalty + Step penalty = CAŁKOWITA KARA

Przykład: Tier 10, 15000 nauki
- Tier: 10 × 2% = 20%
- Step: 5 × 0.1% = 0.5%
- Razem: 20.5% kary
- Otrzymujesz: 100% - 20.5% = 79.5% nauki
```

### P: Czy mogę wyłączyć Dynamic Science?

**O:** **TAK!** Odznacz checkbox "Enable Dynamic Science" i zawsze otrzymujesz 100% nauki (bez kar).

### P: Jakie są dobre ustawienia Science?

**O:** Zależy od preferencji:

```
DLA ŁATWEJ GRY:
  Per Tier Penalty: 1%     (zamiast 2%)
  Science Step: 5000       (zamiast 3000)
  Step Penalty: 0.05%      (zamiast 0.1%)

DLA NORMALNEJ GRY:
  Per Tier Penalty: 2%     (domyślnie)
  Science Step: 3000       (domyślnie)
  Step Penalty: 0.1%       (domyślnie)

DLA TRUDNEJ GRY:
  Per Tier Penalty: 3%     (zamiast 2%)
  Science Step: 2000       (zamiast 3000)
  Step Penalty: 0.2%       (zamiast 0.1%)
```

---

## 💰 Dynamic Funds

### P: Jak działa Dynamic Funds?

**O:** Zmniejsza zarobki pieniędzy (Funds) w miarę jak zdobywasz ich więcej.

```
Funds Step: Co ile pieniędzy dodawana jest 1 kara
  (Domyślnie: 100,000 funds = 1 krok)

Penalty per Step: Jaka strata za każdy krok
  (Domyślnie: 2% straty)
```

**Przykład:**
```
Masz 500,000 funds:
  Kroki: 500,000 ÷ 100,000 = 5 kroków
  Kara: 5 × 2% = 10%
  Otrzymujesz: 90% fund rewards

Masz 1,000,000 funds:
  Kroki: 1,000,000 ÷ 100,000 = 10 kroków
  Kara: 10 × 2% = 20%
  Otrzymujesz: 80% fund rewards
```

### P: Jak zmienić szybkość penalizacji Funds?

**O:** Masz dwie opcje:

1. **Zmień Funds Step** (ile fundsów na krok):
   - Mniejsza liczba = szybsza penalizacja
   - Większa liczba = wolniejsza penalizacja

2. **Zmień Penalty/Step** (ile procent na krok):
   - Mniejsza liczba = mniej straty
   - Większa liczba = więcej straty

**Przykład:**
```
Domyślnie: Step 100,000, Penalty 2%

SZYBKA PENALIZACJA:
  Step: 50,000 (2× szybciej)
  Penalty: 3% (większa strata)

WOLNA PENALIZACJA:
  Step: 200,000 (2× wolniej)
  Penalty: 1% (mniejsza strata)
```

---

## ⭐ Dynamic Reputation

### P: Jak działa Dynamic Reputation?

**O:** Zmniejsza zarobki reputacji w miarę jak zdobywasz jej więcej.

```
Reputation Step: Co ile reputacji dodawana jest 1 kara
  (Domyślnie: 100 reputation = 1 krok)

Penalty per Step: Jaka strata za każdy krok
  (Domyślnie: 0.1% straty)
```

**Przykład:**
```
Masz 400 reputacji:
  Kroki: 400 ÷ 100 = 4 kroki
  Kara: 4 × 0.1% = 0.4%
  Otrzymujesz: 99.6% reputation rewards

Masz 1000 reputacji:
  Kroki: 1000 ÷ 100 = 10 kroków
  Kara: 10 × 0.1% = 1%
  Otrzymujesz: 99% reputation rewards
```

---

## 🎯 Mówienie o Multiplierach

### P: Co oznaczają wartości w "Career Multipliers"?

**O:** To są **bazowe mnożniki** - działają na WSZYSTKIE zarobki:

```
100% = normalny zarobek (domyślnie)
50%  = połowa normalnego zarobku
150% = 1.5× zarobek
200% = dwa razy większy zarobek
```

**Przykład:**
```
Nauka z misji bez modyfikacji: 50 nauki
Jeśli Science: 100% → dostajesz 50 nauki
Jeśli Science: 200% → dostajesz 100 nauki
Jeśli Science: 50%  → dostajesz 25 nauki
```

**+ Dynamic bonus:**
```
Jeśli włączysz Dynamic Science, bazowy mnożnik 
się łączy z karami dynamiki:

100% (bazowy) - 20% (kara za tier) = 80% FINAL
```

### P: Jaka jest różnica między mnożnikami?

**O:**

```
SCIENCE MULTIPLIER:
  Liczba = procent nauki każdej misji

FUNDS MULTIPLIER:
  Liczba = procent pieniędzy za każdą kontrakt

REPUTATION MULTIPLIER:
  Liczba = procent reputacji ze wszystkiego
```

---

## 🔍 Current Section (Sekcja na Górze)

### P: Co pokazuje sekcja "Current"?

**O:** Pokazuje **twój obecny stan** i **ile kar masz**:

```
GÓRNA LINIA:
  Tier: 10              = Ile tieru masz odblokowanego
  Science: 15000.0      = Ile nauki otrzymałeś całkowicie
  Funds: 500000.0       = Ile pieniędzy masz
  Reputation: 450.0     = Ile reputacji masz

DOLNA LINIA:
  Penalty (Tier): -20%  = Kara za odblokowane tiery
  Penalty (Science): -0.5% = Kara za kroki nauki
  Penalty (Funds): -10% = Kara za kroki fundsów
  Penalty (Reputation): -0.45% = Kara za kroki reputacji
```

**Razem kary dla nauki:** -20% + -0.5% = **-20.5%** (otrzymujesz 79.5% nauki)

---

## ⚙️ Zaawansowane Pytania

### P: Co się stanie jeśli ustawię mnożnik na 0%?

**O:** Nie będziesz otrzymywać **NIC** z tej kategorii - bardzo głupia decyzja! 😄

```
Science: 0% → Żadnej nauki z niczego
Funds: 0% → Żadnych pieniędzy
Reputation: 0% → Żadnej reputacji
```

Lepiej trzymaj się 1% - 1000%.

### P: Czy mogę mieć dynamikę dla Science ale nie dla Funds?

**O:** **TAK!** Każda kategorii jest niezależna:

```
✅ Enable Dynamic Science
❌ Enable Dynamic Funds
❌ Enable Dynamic Reputation
```

Wtedy tylko Science ma kary dynamiczne, reszta jest bez kar.

### P: Czy Reset to Default coś psuje?

**O:** **NIE!** Przywraca tylko ustawienia do domyślnych:

```
Science: 100% → 100%
Funds: 100% → 100%
Reputation: 100% → 100%

Twoje Save w grze: BEZ ZMIAN!
Aktualne multiplicatory w grze: POZOSTAJĄ!
```

Po Reset musisz klikać Apply lub Save jeśli chcesz nowe wartości.

### P: Ile gry zajmuje przeczytanie manuala?

**O:** Ca. 3-5 minut! 😀 Jest warto!

---

## 🐛 Problemy i Troubleshooting

### P: Gra się zawieszała po zainstalowaniu moda

**O:** Sprawdź:
1. Czy DLL jest w dobrej ścieżce?
2. Czy KSP jest najnowszą wersją?
3. Usuń mod i uruchom grę - jeśli będzie ok, mod jest winny

### P: Zmieniłem wartości ale nic się nie zmienia w grze

**O:** Sprawdź:
1. Czy kliknąłeś **Apply**? (bez tego nic się nie zmienia!)
2. Czy jesteś w **Space Center**? (mod działa TYLKO tam)
3. Czy jesteś w **Career Mode**? (Sandbox nie wspierany)
4. Czy zmieniłeś prawidłowe pola? (check sekcje)

### P: Gdzie jest moja stara konfiguracja po Update?

**O:** Powinna być zachowana! Jeśli nie:
1. Klikaj **Reset to Default**
2. Potem **Save**
3. Restart gry

---

## 📚 Podsumowanie

| Funkcja | Domyślnie | Min | Max |
|---------|-----------|-----|-----|
| Science Multiplier | 100% | 1% | 1000% |
| Funds Multiplier | 100% | 1% | 1000% |
| Reputation Multiplier | 100% | 1% | 1000% |
| Per Tier Penalty | 2% | 0.1% | 10% |
| Science Step | 3000 | 100 | 100000 |
| Step Penalty (Sci) | 0.1% | 0.01% | 1% |
| Funds Step | 100000 | 10000 | 1000000 |
| Penalty/Step (Funds) | 2% | 0.1% | 10% |
| Rep Step | 100 | 10 | 10000 |
| Penalty/Step (Rep) | 0.1% | 0.01% | 1% |

---

**Masz pytanie które tu nie ma? Sprawdź Manual w grze (przycisk Manual)!** 🎮
