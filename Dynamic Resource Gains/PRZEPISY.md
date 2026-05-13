# 🎮 Przepisy (Recipes) - Gotowe Konfiguracje

Gotowe konfiguracje do szybkiego wdrożenia w Dynamic Resource Gains.

Każdy przepis zawiera dokładnie jakie wartości wpisać!

---

## 🟢 Przepis 1: Bardzo Łatwo (Dla Noobów)

Chcesz grę łatwa? Wszystko szybko się zdobywa?

### Ustawienia:

```
Science:              300 %
Funds:                250 %
Reputation:           200 %
```

### Instrukcja:
1. Wejdź do Space Center
2. Kliknij przycisk DRG
3. W polu "Science:" wpisz `300`
4. W polu "Funds:" wpisz `250`
5. W polu "Reputation:" wpisz `200`
6. Kliknij `Apply`
7. Kliknij `Save`

### Co się zmieni:
- Nauka: 3x szybciej
- Pieniądze: 2.5x szybciej
- Reputacja: 2x szybciej
- Włażysz w technologie błyskawicznie
- Kasę masz zawsze

### Najlepiej dla:
- Pierwszej gry
- Zapoznawania się z grą
- Testowania rakiet bez stresu

---

## 🟡 Przepis 2: Normalnie+ (Rekomendowana dla większości)

Gra jest fajna, ale chcę trochę ułatwienia.

### Ustawienia:

```
Science:              125 %
Funds:                125 %
Reputation:           110 %

Dynamic Scaling:       ☐ (wyłączone)
```

### Instrukcja:
1. Wejdź do Space Center
2. Kliknij przycisk DRG
3. W polu "Science:" wpisz `125`
4. W polu "Funds:" wpisz `125`
5. W polu "Reputation:" wpisz `110`
6. Upewnij się, że wszystkie checkboxy to ☐ (nie zaznaczone)
7. Kliknij `Apply`
8. Kliknij `Save`

### Co się zmieni:
- Wszystko o 10-25% łatwiejsze
- Gra się nie zmienia, jest tylko miłosierniejsza
- Brak dynamiki - zawsze ta sama wartość

### Najlepiej dla:
- Normalnej zabawy
- Gdy chcesz reliefa bez ekstremalnych zmian
- Dobrze skorowidowanego doświadczenia

---

## 🔴 Przepis 3: Hardcore (Dla Veteów)

Chcesz wyzwania! Niech gra będzie twarda!

### Ustawienia:

```
Science:              50 %
Funds:                50 %
Reputation:           70 %

Dynamic Scaling:       ☐ (wyłączone)
```

### Instrukcja:
1. Wejdź do Space Center
2. Kliknij przycisk DRG
3. W polu "Science:" wpisz `50`
4. W polu "Funds:" wpisz `50`
5. W polu "Reputation:" wpisz `70`
6. Kliknij `Apply`
7. Kliknij `Save`

### Co się zmieni:
- Zarabiasz połowę nauki
- Zarabiasz połowę pieniędzy
- Reputacja jest trudniejsza
- Każda decyzja ma znaczenie
- Musisz być strategiczny

### Najlepiej dla:
- Doświadczonych graczy
- Tych, którzy znają grę na wylot
- Szukających wyzwania

---

## 🎯 Przepis 4: Progresywna Trudność (Progressive Difficulty)

Gra łatwa na początku, potem trudniej. Polecam!

### Ustawienia:

```
Science:              150 %
Funds:                120 %
Reputation:           100 %

Dynamic Science:      ☑ WŁĄCZONE
  Science per Tier Penalty: 0.08
  Science Step: 15000
  Science Step Penalty: 0.02

Dynamic Funds:        ☐ (wyłączone)
Dynamic Reputation:   ☐ (wyłączone)
```

### Instrukcja Krok Po Kroku:

**CZĘŚĆ 1 - Mnożniki:**
1. Wejdź do Space Center
2. Kliknij przycisk DRG
3. W polu "Science:" wpisz `150`
4. W polu "Funds:" wpisz `120`
5. W polu "Reputation:" wpisz `100`
6. Kliknij `Apply`

**CZĘŚĆ 2 - Dynamic Science:**
7. Zaznacz checkbox "☐ Enable Dynamic Science" → zmienia się na "☑"
8. Pojawi się sekcja z trzema polami
9. W "Science per Tier Penalty:" wpisz `0.08`
10. W "Science Step:" wpisz `15000`
11. W "Science Step Penalty:" wpisz `0.02`
12. Kliknij `Apply`
13. Kliknij `Save`

### Co się zmieni:

**Początek gry (Tech 0, Nauka 0):**
```
Mnożnik = 150% (supłatwo!)
Zarabiasz 1.5x nauki
```

**Macie kilka tech (Tech 5, Nauka 25000):**
```
Mnożnik = 150 - (5 × 0.08) - ((25000/15000) × 0.02)
        = 150 - 0.4 - 0.03
        = 149.57% (prawie bez zmian)
```

**Zaawansowana gra (Tech 20, Nauka 150000):**
```
Mnożnik = 150 - (20 × 0.08) - ((150000/15000) × 0.02)
        = 150 - 1.6 - 0.2
        = 148.2% (zacyna być trudniej)
```

**Endgame (Tech 40, Nauka 500000):**
```
Mnożnik = 150 - (40 × 0.08) - ((500000/15000) × 0.02)
        = 150 - 3.2 - 0.67
        = 146.13% (znacznie trudniej)
```

### Efekt:
- Gracz zaczyna z bonusem
- W miarę postępu bonus się zmniejsza
- Gra sama się przystosowuje do gracza
- Naturalnie staje się trudniejsza

### Najlepiej dla:
- Naturalnej progresji
- Tych, którzy chcą wyzwania w miarę postępu
- Realistycznej ekonomiki gry

---

## 💰 Przepis 5: Ekonomia - Bogatemu Trudniej

Im więcej masz funduszy, tym trudniej zarabiać!

### Ustawienia:

```
Science:              100 %
Funds:                100 %
Reputation:           100 %

Dynamic Science:      ☐ (wyłączone)
Dynamic Funds:        ☑ WŁĄCZONE
  Funds Scale Factor: 0.00005

Dynamic Reputation:   ☐ (wyłączone)
```

### Instrukcja:

**CZĘŚĆ 1 - Mnożniki:**
1. Wejdź do Space Center
2. Kliknij przycisk DRG
3. Ustaw wszystko na 100% (domyślnie już jest)
4. Kliknij `Apply`

**CZĘŚĆ 2 - Dynamic Funds:**
5. Zaznacz "☐ Enable Dynamic Funds" → zmienia się na "☑"
6. Pojawi się pole "Funds Scale Factor:"
7. Wpisz `0.00005` (malutka liczba!)
8. Kliknij `Apply`
9. Kliknij `Save`

### Co się zmieni:

**Biedny gracz (100,000 funduszy):**
```
Mnożnik = 100 - (100000 × 0.00005) = 100 - 5 = 95%
Zarabiasz prawie normalnie
```

**Bogatszy (500,000 funduszy):**
```
Mnożnik = 100 - (500000 × 0.00005) = 100 - 25 = 75%
Zarabiasz o 25% mniej
```

**Bogaty (1,000,000 funduszy):**
```
Mnożnik = 100 - (1000000 × 0.00005) = 100 - 50 = 50%
Zarabiasz połowę!
```

**Super bogaty (5,000,000 funduszy):**
```
Mnożnik = 100 - (5000000 × 0.00005) = 100 - 250 = ...
= Minimum 1% (gra zabezpiecza, żeby się nie zaglądało)
```

### Efekt:
- Gra daje ci powód aby wydawać pieniądze
- Nie możesz bezmyślnie kopić kasę
- Ekonomia jest dynamiczna
- Motywuje do budowy drogich projektów

### Najlepiej dla:
- Tych, którzy znają ekonomię gry
- Szukających sensu wydawania pieniędzy
- Bardziej zaawansowanych graczy

---

## 🎓 Przepis 6: Edukacyjny - Naucz się Balansu

Eksperymentuj ze wszystkim! Mix dynamiki.

### Ustawienia:

```
Science:              120 %
Funds:                110 %
Reputation:           115 %

Dynamic Science:      ☑ WŁĄCZONE
  Science per Tier Penalty: 0.05
  Science Step: 12000
  Science Step Penalty: 0.01

Dynamic Funds:        ☑ WŁĄCZONE
  Funds Scale Factor: 0.00003

Dynamic Reputation:   ☑ WŁĄCZONE
  Reputation Scale Factor: 0.005
```

### Instrukcja:
To jest bardziej skomplikowane. Zrób wszystkie trzy partie:

**CZĘŚĆ 1 - Mnożniki:**
1. Science: `120`
2. Funds: `110`
3. Reputation: `115`
4. Apply

**CZĘŚĆ 2 - Dynamic Science (zaznacz checkbox):**
5. Science per Tier: `0.05`
6. Science Step: `12000`
7. Science Step Penalty: `0.01`
8. Apply

**CZĘŚĆ 3 - Dynamic Funds (zaznacz checkbox):**
9. Funds Scale Factor: `0.00003`
10. Apply

**CZĘŚĆ 4 - Dynamic Reputation (zaznacz checkbox):**
11. Reputation Scale Factor: `0.005`
12. Apply
13. Save

### Co się zmieni:
- Wszystko jest dynamiczne
- Gra przystosowuje się do gracza
- Każdy system ma swoje skalowanie
- Bardzo naturalna progresja

### Najlepiej dla:
- Testowania moda
- Nauki jak działają parametry
- Doświadczonych graczy szukających balansu

---

## 🎪 Przepis 7: Chaotycznie! Szaleństwo!

Chcesz totalnego szaleństwa? Proszę!

### Ustawienia:

```
Science:              500 %
Funds:                1000 %
Reputation:           800 %

Wszystko wyłączone - bez dynamiki
```

### Instrukcja:
1. Science: `500`
2. Funds: `1000`
3. Reputation: `800`
4. Apply
5. Save

### Co się zmieni:
- WSZYSTKO jest dostępne od razu
- Nieskończone pieniądze
- Wszystkie tech natychmiast
- Zupełna chaos
- Brak celów

### Najlepiej dla:
- Zabawy bez ograniczeń
- Budowania fantazyjnych projektów
- Eksperymentów
- Testiania nowych modów

---

## 📊 Szybka Tabela Porównawcza

```
┌─────────────┬──────┬──────┬──────┐
│ Przepis     │ Sci  │ Fund │ Rep  │
├─────────────┼──────┼──────┼──────┤
│ Bardzo łatw │ 300% │ 250% │ 200% │
│ Normalnie+  │ 125% │ 125% │ 110% │
│ Hardcore    │  50% │  50% │  70% │
│ Progresywna │ 150% │ 120% │ 100% │
│ Ekonomia    │ 100% │ 100% │ 100% │
│ Edukacyjny  │ 120% │ 110% │ 115% │
│ Szaleństwo  │ 500% │1000% │ 800% │
└─────────────┴──────┴──────┴──────┘
```

---

## 🔄 Jak Przełączać Się Między Przepisami

Jeśli chcesz zmienić przepis:

1. Otwórz okno moda (przycisk DRG)
2. Zmień wartości na nowe
3. Kliknij `Apply` (sprawdzisz czy pasuje)
4. Jeśli ok - kliknij `Save`
5. Jeśli nie - wpisz inne wartości i znowu `Apply`

**Nie potrzebujesz restartować gry!**

---

## 💡 Porady do Przepisów

### Porrada 1: Start z Przepisem
Wybranie już gotowego przepisu jest łatwsze niż zgadywanie!

### Porrada 2: Eksperymentuj
Przepisy to tylko punkt startu. Zmieniaj je!

### Porrada 3: Notuj Ulubione
Pamiętaj jakie ustawienia Ci się podoba do kolejnej gry.

### Porrada 4: Dynamika To Nie Obowiązek
Możesz wyłączyć wszystkie checkboxy i mieć prosty mnożnik.

### Porrada 5: Apply Zawsze Przed Save
Taka jest procedura:
```
Zmiana → Apply (test) → Save (jeśli ok)
```

---

## 🎯 Podsumowanie Przepisów

Mamy 7 gotowych konfiguracji:

1. **Bardzo Łatwo** - Dla noobów
2. **Normalnie+** - Rekomendowana
3. **Hardcore** - Dla veteów
4. **Progresywna** - Najlepsza dynamika
5. **Ekonomia** - Bogatemu trudniej
6. **Edukacyjny** - Mix wszystkiego
7. **Szaleństwo** - Zero ograniczeń

Wybierz sobie ulubioną i baw się!

---

**Powodzenia w grze! 🚀**
