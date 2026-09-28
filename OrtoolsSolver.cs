using Google.OrTools.Sat;
using System;
using System.Collections.Generic;
using System.Linq;

public class NonOverlappingPuzzle
{
    // Definicja klocka jako lista przesunięć (wiersz, kolumna) względem kotwicy
    public class Piece
    {
        public string Name { get; set; }
        public List<(int r, int c)> Shape { get; set; } = new List<(int r, int c)>();
    }

    public static void Solve()
    {
        // 1. Definiujemy planszę (np. 4x4)
        int boardRows = 4;
        int boardCols = 4;

        // 2. Definiujemy klocki (Kształty)
        var pieces = new List<Piece>
        {
            new Piece { 
                Name = "L-Shape", 
                Shape = new List<(int, int)> { (0,0), (1,0), (1,1) } // Zajmuje 3 pola
            },
            new Piece { 
                Name = "Domino", 
                Shape = new List<(int, int)> { (0,0), (0,1) } // Zajmuje 2 pola
            }
        };

        // 3. Inicjalizacja modelu
        CpModel model = new CpModel();

        // Słownik przechowujący zmienne: zmienne[pieceIndex][anchorRow, anchorCol]
        // Wartość 1 oznacza, że klocek jest umieszczony w tym miejscu.
        var placementVars = new Dictionary<int, IntVar[,]>();

        for (int p = 0; p < pieces.Count; p++)
        {
            placementVars[p] = new IntVar[boardRows, boardCols];
            for (int r = 0; r < boardRows; r++)
            {
                for (int c = 0; c < boardCols; c++)
                {
                    // Sprawdzamy, czy klocek zmieści się na planszy z kotwicą w (r,c)
                    bool fits = pieces[p].Shape.All(offset => 
                        r + offset.r < boardRows && c + offset.c < boardCols);

                    if (fits)
                    {
                        placementVars[p][r, c] = model.NewBoolVar($"piece_{p}_at_{r}_{c}");
                    }
                    else
                    {
                        // Jeśli nie pasuje, tworzymy zmienną, która zawsze wynosi 0
                        // (Można to też obsłużyć po prostu nie dodając zmiennej do listy)
                        placementVars[p][r, c] = model.NewConstant(0);
                    }
                }
            }
        }

        // ==========================================
        // PUNKT 3: OGRANICZENIE BRAKU NAKŁADANIA SIĘ
        // ==========================================
        
        // Iterujemy po każdym polu na planszy
        for (int R = 0; R < boardRows; R++)
        {
            for (int C = 0; C < boardCols; C++)
            {
                // Zbieramy wszystkie zmienne, które "pokrywają" pole (R, C)
                List<ILiteral> coveringVars = new List<ILiteral>();

                for (int p = 0; p < pieces.Count; p++)
                {
                    foreach (var offset in pieces[p].Shape)
                    {
                        // Obliczamy, gdzie musi być kotwica, aby ten fragment klocka trafił w (R, C)
                        int anchorR = R - offset.r;
                        int anchorC = C - offset.c;

                        // Sprawdzamy, czy taka kotwica jest w ogóle możliwa (mieści się w planszy)
                        if (anchorR >= 0 && anchorR < boardRows && anchorC >= 0 && anchorC < boardCols)
                        {
                            // Dodajemy zmienną do listy
                            coveringVars.Add(placementVars[p][anchorR, anchorC]);
                        }
                    }
                }

                // Dodajemy ograniczenie: suma zmiennych pokrywających to pole musi być <= 1
                if (coveringVars.Count > 0)
                {
                    model.Add(LinearExpr.Sum(coveringVars) <= 1);
                }
            }
        }

        // 4. (Opcjonalnie) Ograniczenie: Każdy klocek musi być użyty dokładnie raz
        // W tym przypadku sumujemy wszystkie możliwe umieszczenia danego klocka.
        for (int p = 0; p < pieces.Count; p++)
        {
            List<ILiteral> allPlacementsForPiece = new List<ILiteral>();
            for (int r = 0; r < boardRows; r++)
            {
                for (int c = 0; c < boardCols; c++)
                {
                    allPlacementsForPiece.Add(placementVars[p][r, c]);
                }
            }
            model.Add(LinearExpr.Sum(allPlacementsForPiece) == 1);
        }

        // 5. Rozwiązanie
        CpSolver solver = new CpSolver();
        CpSolverStatus status = solver.Solve(model);

        // 6. Wyświetlenie wyników
        if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
        {
            Console.WriteLine("Znaleziono rozwiązanie!");
            for (int p = 0; p < pieces.Count; p++)
            {
                for (int r = 0; r < boardRows; r++)
                {
                    for (int c = 0; c < boardCols; c++)
                    {
                        if (solver.Value(placementVars[p][r, c]) == 1)
                        {
                            Console.WriteLine($"Klocek {pieces[p].Name} umieszczony z kotwicą w ({r}, {c})");
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Brak rozwiązania.");
        }
    }
}
