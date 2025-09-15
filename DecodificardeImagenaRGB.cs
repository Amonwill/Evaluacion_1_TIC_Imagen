//Codigo para leer una imagen, decodificar sus valores RGB y guardarlos en un archivo de texto.
//Creado por: William Cruz Hernandez 
//GitHub: Amonwill
//Fecha: 14/09/2025
//Esime Culhuacan
//Teoria de la informacion Y codificacion

using System;
using System.Collections.Generic;
using System.Drawing;
//Cuando utilizamos System.Drawing en .NET Core o .NET 5/6/7, necesitamos agregar el paquete NuGet System.Drawing.Common
//En Visual Studio, puedes hacerlo a través del Administrador de Paquetes NuGet o usando la consola de NuGet con el siguiente comando:
//Install-Package System.Drawing.Common
//Cuando utilizas .NET framework 4.7.2 o superior, System.Drawing ya está incluido en el framework, por lo que no necesitas instalar nada adicionalmente.
using System.IO;

namespace ImagenDecodificadaenRGB
{
    internal class DecodificadorImagen
    {
        static void Main(string[] args)
        {
            int intento;

            Console.WriteLine("Desea decodificar una imagen: \n 1. Si \n 2. No");
            intento = Convert.ToInt32(Console.ReadLine());
            // si el usuario no desea continuar, se cierra el programa
            // hacemos uso de un ciclo do -while para validar la entrada del usuario
            while (intento == 1)
            {
                if (intento == 2)
                {
                    Console.WriteLine("El programa se cerrará. Presione ENTER para salir...");
                    Console.ReadLine();
                    return; // salir del programa
                }
                else if (intento == 1)
                {
                    // solicitar al usuario la ruta de la imagen
                    Console.WriteLine("Ingrese la dirección de la imagen con extensión: ");
                    string rutaImagen = Console.ReadLine();

                    // variable para almacenar la cadena de valores RGB en archivo de texto
                    string resultado = "";

                    // lee y decodifica la imagen para obtener los valores RGB
                    try
                    {
                        // cargar la imagen a partir de la ruta proporcionada por el usuario
                        // bitmap es una clase en System.Drawing que representa una imagen en memoria
                        Bitmap imagen = new Bitmap(rutaImagen);
                        // lista para almacenar los valores RGB
                        List<int> valoresRGB = new List<int>();

                        // recorrer cada píxel de la imagen y obtener sus valores RGB
                        // este primer bucle recorre las filas (altura) de la imagen
                        for (int y = 0; y < imagen.Height; y++)
                        {
                            // este segundo bucle recorre las columnas (ancho) de la imagen
                            for (int x = 0; x < imagen.Width; x++)
                            {
                                Color pixelColor = imagen.GetPixel(x, y);
                                // agregar los valores RGB a la lista
                                // agrega cadena de valores RGB en el orden R, G, B para cada píxel

                                valoresRGB.Add(pixelColor.R); // Componente Roja
                                valoresRGB.Add(pixelColor.G); // Componente Verde
                                valoresRGB.Add(pixelColor.B); // Componente Azul
                            }
                        }

                        // convertimos la lista de valores RGB a una cadena separada por comas usando Join
                        resultado = string.Join(",", valoresRGB);

                        // mostramos la cadena de valores RGB en la consola
                        Console.WriteLine("Cadena de valores RGB:");
                        Console.WriteLine(resultado);
                    }
                    catch (Exception ex)
                    {
                        // cuando la imagen no se encuentra en la ruta dada por el usuario
                        Console.WriteLine("Error al procesar la imagen (No existe o no fue escrita correctamente): " + ex.Message);
                    }

                    // limpiamos la consola después de mostrar la cadena RGB y que se oprima ENTER para continuar
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Cadena RGB mostrada anteriormente se limpiará de la consola.\n");
                    Console.WriteLine("Ahora procederemos a guardar la cadena de valores RGB en un archivo de texto.\n");

                    // guardar en archivo
                    try
                    {
                        // ruta definida
                        // string rutaArchivo = "C:\\Users\\Wcruz\\OneDrive\\Desktop\\prueba1.txt"; 
                        // ruta dada por el usuario
                        Console.WriteLine("Ingrese la ruta donde desea guardar el archivo de texto (incluya el nombre y la extensión .txt): ");
                        string rutaArchivo = Console.ReadLine();
                        string resultadoArchivo = resultado; // Usar la variable resultado declarada anteriormente
                        File.WriteAllText(rutaArchivo, resultado);
                        Console.WriteLine($"Los valores RGB han sido guardados en {rutaArchivo}");

                        Console.WriteLine("\nContenido del archivo guardado desea decodificar otra imagen: \n 1. Si \n 2. No");
                        intento = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No se pudo guardar el archivo: " + ex.Message);
                    }

                    Console.WriteLine("\nPresione ENTER para salir...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }
            }
        }
    }
}