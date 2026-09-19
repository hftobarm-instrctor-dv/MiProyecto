using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Fuerza a la consola a usar UTF-8 para que lea correctamente el alien
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false; // Oculta el cursor para que se vea más limpio
        int x = 10, y = 5;             // Posición del jugador
        int frames = 0;                // Contador para demostrar que el bucle no se detiene

        Console.WriteLine("Usa las FLECHAS del teclado para moverte. Presiona ESC para salir.");

        // --- GAME LOOP ---
        while (true)
        {
            // 1. LEER ENTRADA (Sin bloquear el hilo)
            if (Console.KeyAvailable)
            {
                // Leemos la tecla y pasamos 'true' para que no se pinte el caracter en la consola
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                // Salir del juego si se presiona Escape
                if (tecla.Key == ConsoleKey.Escape) break;

                // Lógica de movimiento tipo Unity
                if (tecla.Key == ConsoleKey.LeftArrow)  x--;
                if (tecla.Key == ConsoleKey.RightArrow) x++;
                if (tecla.Key == ConsoleKey.UpArrow)    y--;
                if (tecla.Key == ConsoleKey.DownArrow)  y++;
            }

            // 2. ACTUALIZAR Y DIBUJAR
            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write("👾"); // Dibujamos al jugador

            // Dibujamos información extra para ver el loop activo
            Console.SetCursorPosition(0, 0);
            Console.Write($"Frame: {frames++} | Posición: X:{x}, Y:{y}");

            // 3. CONTROL DE TIEMPO (Aproximadamente 30 FPS)
            Thread.Sleep(10); 
        }

        Console.CursorVisible = true;
    }
}
