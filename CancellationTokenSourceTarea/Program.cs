using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("¡La Carrera de Tortugas Programadoras!");
        Console.WriteLine("¡Que comience la carrera!");

        CancellationTokenSource cts = new CancellationTokenSource();
        List<Thread> tortugas = new List<Thread>();

        string[] nombres = { "Tortuga 1", "Tortuga 2", "Tortuga 3" };
        Random rnd = new Random();

        foreach (var nombre in nombres)
        {
            Thread tortuga = new Thread(() => AvanzarTortuga(nombre, rnd.Next(3000, 7000), cts.Token));
            tortuga.Start();
            tortugas.Add(tortuga);
        }

        Console.WriteLine("Presiona 'c' para cancelar la carrera o cualquier otra tecla para salir.");
        char key = Console.ReadKey().KeyChar;
        if (key == 'c')
        {
            cts.Cancel();
            Console.WriteLine("\nCarrera cancelada por el juez.");
        }

        foreach (var tortuga in tortugas)
        {
            tortuga.Join();
        }

        Console.WriteLine("\nCarrera finalizada. Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }

    static void AvanzarTortuga(string nombreTortuga, int tiempoLimite, CancellationToken token)
    {
        Console.WriteLine($"{nombreTortuga} está avanzando...");
        int progreso = 0;

        while (progreso < tiempoLimite)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"{nombreTortuga} ha sido descalificada.");
                return;
            }
            Thread.Sleep(1000);
            progreso += 1000;
            Console.WriteLine($"{nombreTortuga} ha avanzado {progreso * 100 / tiempoLimite}%");
        }
        Console.WriteLine($"{nombreTortuga} ha llegado a la meta.");
    }
}

