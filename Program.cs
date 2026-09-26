using System;
using System.Threading;

class Program
{
    //globales
    static int xAlien = 10; // Posición inicial en X
    static int yAlien = 5; // Posición inicial en Y

    static bool gameOver = false; // Variable para controlar el estado del juego

    static int xNave = 60; // Posición inicial en X
    static int yNave = 20; // Posición inicial en Y

    static int xBala = -1;
    static int yBala = -1;
    static bool balaActiva = false;

    public static void MoverALien()
    {
        xAlien++;
        if (xAlien >= 80)
        {
            xAlien = 0;

        }
        if (yAlien >= 20)
        {
            gameOver = true;
        }

    }

    // Método: "mover nave"
    static void MoverNave(ConsoleKey key)
    {

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (yNave > 0) yNave--;
                break;
            case ConsoleKey.DownArrow:
                if (yNave < Console.WindowHeight - 1) yNave++;
                break;
            case ConsoleKey.LeftArrow:
                if (xNave > 0) xNave--;
                break;
            case ConsoleKey.RightArrow:
                if (xNave < Console.WindowWidth - 1) xNave++;
                break;
            case ConsoleKey.Escape:
                gameOver = true;
                break;
        }
    }

    public static void PintarBala()
    {
        //Pone el curso en la posiciponde la bala
        if (!balaActiva) return;


        Console.SetCursorPosition(xBala, yBala);
        //Dibuja la bala
        Console.Write("🔴");

    }

    public static void DispararBala()
    {
        if (!balaActiva)
        {
            xBala = xNave;
            yBala = yNave - 1; // La bala sale desde arriba de la nave
            balaActiva = true;
        }
    }

    public static void MoverBala()
    {
        if (balaActiva)
        {
            yBala--;
            if (yBala < 0)
            {
                balaActiva = false; // La bala sale de la pantalla
            }
        }
    }

    public static bool SeDetectaDisparo(ConsoleKey key)
    {


        if (key == ConsoleKey.Spacebar)
        {
            return true;
        }
        return false;
    }

    static void Main()
    {
        // Fuerza a la consola a usar UTF-8 para que lea correctamente el alien
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false; // Oculta el cursor para que se vea más limpio

        int frames = 0;                // Contador para demostrar que el bucle no se detiene

        Console.WriteLine("Usa las FLECHAS del teclado para moverte. Presiona ESC para salir.");

        // --- GAME LOOP ---
        while (true)
        {
            // 1. LEER ENTRADA (Sin bloquear el hilo)



            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                MoverNave(key);
                if (SeDetectaDisparo(key))
                {
                    DispararBala();
                }
            }

            MoverALien();
            MoverBala();

            if (gameOver) break;

            // 2. ACTUALIZAR Y DIBUJAR
            Console.Clear();
            Console.SetCursorPosition(xAlien, yAlien);
            Console.Write("👾"); // Dibujamos al jugador

            Console.SetCursorPosition(xNave, yNave);
            Console.Write("🚀"); // Dibujamos al jugador

            PintarBala();

            // Dibujamos información extra para ver el loop activo
            Console.SetCursorPosition(0, 0);
            Console.Write($"Frame: {frames++} | Posición: X:{xBala}, Y:{yBala}");

            // 3. CONTROL DE TIEMPO (Aproximadamente 30 FPS)
            Thread.Sleep(30);
        }

        Console.CursorVisible = true;
    }
}
