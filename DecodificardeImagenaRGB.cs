//Codigo para leer una imagen, decodificar sus valores RGB y guardarlos en un archivo de texto,
//y luego reconstruir la imagen a partir de esos valores.
//Corregido por: ChatGPT (basado en tu versión original)
//Creado originalmente por: William Cruz Hernandez 
//GitHub: Amonwill
//Fecha: 15/09/2025
//Esime Culhuacan
//Teoria de la informacion Y codificacion

using System;
using System.Collections.Generic;
// Requiere agregar referencia a System.Drawing.Common desde NuGet cuando usas .NET Core o .NET 5+
using System.Drawing;
using System.IO;
using System.Text;

namespace ImagenDecodificadaenRGB
{
    internal class DecodificadorImagen
    {
        static void Main(string[] args)
        {
            try
            {
                // preguntar al usuario si desea decodificar una imagen
                Console.WriteLine("Desea decodificar una imagen: \n 1. Sí \n 2. No");
                // leer la respuesta del usuario
                int intento = Convert.ToInt32(Console.ReadLine());

                // si la respuesta es no, salir del programa
                if (intento != 1)
                {
                    Console.WriteLine("El programa se cerrará. Presione ENTER para salir...");
                    Console.ReadLine();
                    return;
                }

                // solicitar al usuario la ruta de la imagen
                Console.WriteLine("Ingrese la ruta de la imagen con extensión: ");
                string rutaImagen = Console.ReadLine();

                // verificar que la imagen existe
                if (!File.Exists(rutaImagen))
                {
                    // la imagen no existe, mostrar mensaje y salir
                    Console.WriteLine("La imagen no existe en la ruta indicada.");
                    return;
                }

                // variable para almacenar la cadena de valores RGB en archivo de texto
                string resultado = "";

                // lee y decodifica la imagen para obtener los valores RGB
                Bitmap imagen = new Bitmap(rutaImagen);

                // construir cadena: primero ancho,alto, luego los R,G,B
                var sb = new StringBuilder();
                // agregar ancho y alto al inicio
                sb.Append(imagen.Width).Append(',').Append(imagen.Height);

                // recorrer cada píxel en cada columna
                for (int y = 0; y < imagen.Height; y++)
                {
                    // recorrer cada píxel en la fila
                    for (int x = 0; x < imagen.Width; x++)
                    {
                        // obtener el color del píxel
                        Color pixelColor = imagen.GetPixel(x, y); // Color tiene propiedades R, G, B, el x, y es la posición del píxel
                        sb.Append(',').Append(pixelColor.R)    // agregar valor R
                          .Append(',').Append(pixelColor.G)   // agregar valor G
                          .Append(',').Append(pixelColor.B); // agregar valor B
                    }
                }

                // convertir StringBuilder a string
                //
                resultado = sb.ToString();

                // mostrar cadena en consola
                Console.WriteLine("\nCadena de valores RGB:");
                Console.WriteLine(resultado.Substring(0, Math.Min(500, resultado.Length)) + "...");
                Console.WriteLine("(Se muestra sólo un fragmento de la cadena a guardar)");

                // guardamos en archivo .txt
                Console.WriteLine("\nIngrese la ruta donde desea guardar el archivo de texto (incluya el nombre y la extensión .txt): ");
                string rutaArchivo = Console.ReadLine();
                File.WriteAllText(rutaArchivo, resultado, Encoding.UTF8);
                Console.WriteLine($"Los valores RGB han sido guardados en {rutaArchivo}");

                // lectura del archivo .txt para reconstrucción
                Console.WriteLine("\nLeyendo informacion del archivo : " + rutaArchivo);

                // verificar que el archivo existe
                if (!File.Exists(rutaArchivo))
                {
                    // el archivo no existe, mostrar mensaje y salir
                    Console.WriteLine("El archivo no existe en la ruta indicada.");
                    return;
                }

                // lee todo el contenido del archivo
                string contenidoArchivo = File.ReadAllText(rutaArchivo, Encoding.UTF8);

                // Convertir la cadena a lista de enteros
                string[] valoresString = contenidoArchivo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                // convertir cada valor a int
                List<int> valoresRGBLeidos = new List<int>();

                // intentar convertir cada valor a int y agregar a la lista
                foreach (string valor in valoresString)
                {
                    // intentar convertir a int
                    if (int.TryParse(valor, out int intValue))
                    {
                        // agregar a la lista
                        valoresRGBLeidos.Add(intValue);
                    }
                }

                // verificar que hay al menos ancho y alto
                if (valoresRGBLeidos.Count < 2)
                {
                    // no hay suficientes datos
                    Console.WriteLine("El archivo no contiene información suficiente.");
                    return;
                }

                // primer par de valores = ancho y alto
                int width = valoresRGBLeidos[0];
                int height = valoresRGBLeidos[1];

                // crear nueva imagen
                Bitmap nuevaImagen = new Bitmap(width, height);

                // recorrer cada píxel y asignar color
                int index = 2; // comenzamos después de ancho y alto
                // recorrer cada píxel
                for (int y = 0; y < height; y++)
                {
                    // recorrer cada columna
                    for (int x = 0; x < width; x++)
                    {
                        // verificar que hay suficientes valores para R, G, B
                        if (index + 2 < valoresRGBLeidos.Count)
                        {
                            // leemos valores R, G, B
                            int r = valoresRGBLeidos[index++];
                            int g = valoresRGBLeidos[index++];
                            int b = valoresRGBLeidos[index++];
                            // crear color y asignar al píxel
                            Color color = Color.FromArgb(r, g, b);
                            nuevaImagen.SetPixel(x, y, color); // asignar color al píxel
                        }
                        else
                        {
                            // si faltan valores, rellenar con negro
                            nuevaImagen.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                // Guardar la nueva imagen en un archivo en ruta dada por el usuario
                Console.WriteLine("\nIngrese la ruta donde desea guardar la imagen reconstruida (incluya el nombre y la extensión .png): ");
                string rutaImagenReconstruida = Console.ReadLine();
                nuevaImagen.Save(rutaImagenReconstruida, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"La imagen ha sido reconstruida y guardada en {rutaImagenReconstruida}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Presione ENTER para salir...");
            Console.ReadLine();
        }
    }
}
