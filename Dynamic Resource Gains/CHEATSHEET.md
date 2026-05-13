# ⚡ Cheatsheet - Szybka Ściąga

Szybka referencja do Dynamic Resource Gains. Drukuj i trzymaj obok komputera!

---

## 🎮 Szybki Start (30 sekund)

```
1. Space Center → Szukaj przycisku DRG (górny lewy róg)
2. Kliknij przycisk
3. Zmień liczby w polach
4. Apply → sprawdzaj
5. Save → pamiętaj
```

---

## 🔢 Mnożniki - Co Wpisać

### Łatwa Gra
```
Science:     200 - 300 %
Funds:       150 - 250 %
Reputation:  100 - 200 %
```

### Normalna Gra
```
Science:     100 %
Funds:       100 %
Reputation:  100 %
```

### Trudna Gra
```
Science:     50 - 75 %
Funds:       50 - 75 %
Reputation:  70 - 80 %
```

---

## ✅ Checkboxy - Włącz/Wyłącz

```
☑ = Włączone (będzie dynamika)
☐ = Wyłączone (bez dynamiki)
```

Zawsze najpierw zaznacz, potem wpisz wartości w rozwijanych polach!

---

## 🎯 Parametry Dynamiki

### Science (Nauka)
```
Science per Tier:    0.01 - 0.10  (za każdy tech)
Science Step:        5000 - 20000 (wielkość skoku)
Step Penalty:        0.01 - 0.05  (kara za krok)
```

### Funds (Pieniądze)
```
Funds Scale Factor:  0.00001 - 0.0001
(mała liczba! im większa, tym szybciej maleją)
```

### Reputation
```
Reputation Scale Factor: 0.01 - 0.05
(za każde 100 rep)
```

---

## 🔘 Guziki

```
[Apply]  = Testuj zmiany (nie zapisuje)
[Save]   = Zapisz na trwałe
```

**Sekwencja:**
```
Zmiana wartości → Apply → OK? → Save
```

---

## 📍 Gdzie Znaleźć Przycisk

```
Gra KSP → Space Center (TYLKO TAM!) → Pasku narzędzi
```

Pamiętaj: **MOD WIDOCZNY TYLKO W SPACE CENTER!**

---

## 🚫 Gdzie NIE Będzie Przycisków

```
❌ W locie (Flight)
❌ W edytorze (Editor)
❌ W tracking station
❌ Na mapie (Map view)
❌ W menu głównym
```

**To normalne! Mod pracuje tylko w Space Center.**

---

## 🔧 Najczęstsze Zmiany

### "Chcę szybciej tech"
```
Science: 150-200 %
Apply → test → Save
```

### "Chcę mniej pieniędzy"
```
Funds: 50 %
Apply → test → Save
```

### "Chcę dynamiki"
```
Zaznacz ☑ Enable Dynamic Science
Wpisz Science per Tier: 0.05
Apply → test → Save
```

### "Chcę wyłączyć dynamikę"
```
Odznacz ☑ → zmienia się na ☐
Apply → Save
```

---

## 📊 Liczby - Co Znaczą

```
100 = Normalnie
50 = Połowa
150 = Półtora raza więcej
200 = Dwa razy więcej
0.05 = Kara 5%
0.001 = Kara 0.1%
```

---

## ⚠️ Częste Błędy

### ❌ BŁĄD: Wpisałem "sto" zamiast "100"
- MOD: Nie będzie działać!
- FIX: Wpisz liczbę: `100`

### ❌ BŁĄD: Klikam Apply ale nic się nie dzieje
- MOD: Może zaznaczasz checkbox a wpisujesz w głównym polu
- FIX: Sprawdź czy wpisujesz w odpowiednich polach

### ❌ BŁĄD: Zamknąłem grę i ustawienia zniknęły
- MOD: Nie kliknąłeś Save!
- FIX: Zawsze Apply → Save

### ❌ BŁĄD: Ustawienia są dziwne (Tech = -5)
- MOD: Gra się wczytuje, dane nie zsynchronizowane
- FIX: Wyjdź i wróć do Space Center

### ❌ BŁĄD: Przycisk DRG nie widać
- MOD: Jesteś w złej scenie
- FIX: Wejdź do Space Center

---

## 🎯 Praktyczne Sekwencje

### Chcę Testować Ustawienie

```
1. Wpisz liczbę w pole
2. Kliknij Apply
3. Zajrzyj do gry
4. Zróbmy coś (zarabiaj naukę)
5. Sprawdź czy działa
6. Wróć do okna moda
7. Jeśli ok - Save
8. Jeśli nie - zmień i Apply
```

### Chcę Włączyć Dynamikę

```
1. Otwórz okno moda
2. Znajduj checkbox: ☐ Enable Dynamic [X]
3. Kliknij na checkbox
4. Pojawi się sekcja z polami
5. Wpisz wartości w poła
6. Kliknij Apply
7. Jeśli ok - Save
```

### Chcę Wyłączyć Dynamikę

```
1. Otwórz okno moda
2. Aby ☑ Enable Dynamic [X]
3. Kliknij na checkbox
4. Zmienia się na ☐
5. Sekcja znika
6. Kliknij Apply
7. Save
```

---

## 📝 Moja Własna Konfiguracja

Wypełnij sobie ulubioną konfigurację:

```
Science:        _____ %
Funds:          _____ %
Reputation:     _____ %

Dynamic Science:       ☐ ☑
  Per Tier:           _____
  Step:               _____
  Step Penalty:       _____

Dynamic Funds:         ☐ ☑
  Scale Factor:       _____

Dynamic Reputation:    ☐ ☑
  Scale Factor:       _____
```

---

## 🎓 Edukacyjne Wzory

### Wzór na Mnożnik Science (z dynamiką)

```
Mnożnik = Base - (TechTier × Penalty) - (Science/Step × StepPenalty)

Przykład:
Base = 100
Tech = 10
Penalty per Tech = 0.05
Science = 50000
Step = 10000
Penalty per Step = 0.01

Mnożnik = 100 - (10 × 0.05) - (50000/10000 × 0.01)
        = 100 - 0.5 - 0.05
        = 99.45 %
```

### Wzór na Mnożnik Funds (z dynamiką)

```
Mnożnik = Base - (Funds × Scale)

Przykład:
Base = 100
Funds = 100000
Scale = 0.00005

Mnożnik = 100 - (100000 × 0.00005)
        = 100 - 5
        = 95 %
```

---

## 🎮 Gry Sytuacyjne

### Sytuacja: Chcę Szybko Tech

```
TERAZ:
Science: [100]
✓ Apply

POTEM:
Science: [200]
✓ Apply
✓ Save
```

### Sytuacja: Jestem za Biedny

```
TERAZ:
Funds: [50]

POTEM:
Funds: [150]
✓ Apply
✓ Save
```

### Sytuacja: Za Łatwo

```
TERAZ:
Science: [100]
Funds: [100]

POTEM:
Science: [50]
Funds: [50]
✓ Apply
✓ Save
```

---

## 📱 Dla Telefonów/Tabletów

Jeśli grasz na innym urządzeniu, pamiętaj:

```
1. Mod zainstalowany w KSP na komputerze
2. Okno moda się pojawia normalnie
3. Klikanie działa jak na myszce
4. Wpisywanie tekstu - touchpad taki sam
```

---

## 🆚 Apply vs Save

```
APPLY:
- Zmienia teraz
- Nie zapisuje
- Sesja się zmienia
- Po restarcie znika

SAVE:
- Zapisuje permanent
- Pamiętane po wczytaniu
- Nigdy nie znika
- Bezpieczne na trwałe
```

**Procedura:**
```
Apply (test) → Ok? → Save (na trwałe)
```

---

## 📞 Szybka Diagnostyka

### Problem: Nic się nie zmienia

```
□ Czy jesteś w Space Center?
□ Czy kliknąłeś Apply?
□ Czy liczby to liczby (100, nie "sto")?
□ Czy checkpoint jest zaznaczony (jeśli dynamika)?
□ Czy czekałeś sekundę po Apply?
```

### Problem: Przycisk nie widać

```
□ Czy jesteś w Space Center?
□ Czy gra jest w career mode?
□ Czy DLL jest w GameData?
□ Czy masz Internet (lol)?
□ Czy restartowaćales grę?
```

### Problem: Ustawienia zniknęły

```
□ Czy kliknąłeś Save?
□ Czy się gra restartowaćała?
□ Czy to ta sama gra?
```

---

## 🎯 Najczęstsze Pytania - Krótkie Odpowiedzi

**P: Gdzie kliknąć Apply?**
O: Przycisk `[Apply]` w dolnej części okna

**P: Ile czekać po Apply?**
O: 1 sekunda i możesz zrobić coś w grze

**P: Co jeśli się pomylę?**
O: Zmień wartość i znowu Apply

**P: Mogę wyłączyć dynamikę?**
O: Tak! Odznacz checkbox

**P: Czy mod psuje grę?**
O: Nie! Pracuje niezależnie

**P: Czy mogę mieć save bez moda?**
O: Tak! Gra będzie normalna bez niego

---

## 🎊 Powodzenia!

Trzymaj tę ściągę obok!

```
┌─────────────────────┐
│ Zmiana → Apply      │
│ Sprawdzenie         │
│ OK? → Save ✓        │
└─────────────────────┘
```

**Graj świadomie! 🚀**
