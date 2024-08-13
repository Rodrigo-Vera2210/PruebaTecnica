using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model
{
    public static class ModelBuilderExtensions
    {
        //Metodo para poblar la base de datos
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //declaracion de variables para almacenar los datos de los archivos
            var datos = LeerArchivo("C:\\Users\\donra\\Escritorio\\PruebaIngreso\\datos_sensores.csv");
            List<DatoSensor> datosSensores = new List<DatoSensor>(); //lista de los datos de los sensores
            var parametros = LeerArchivo("C:\\Users\\donra\\Escritorio\\PruebaIngreso\\parametros_sensores.csv");
            List<ParametroSensor> parametrosSensores = new List<ParametroSensor>(); //lista de los datos de los parametros de los sensores

            // recorrido de los datos para poblar la tabla parametros de sensores
            for (int i = 1; i < parametros.Length - 1; i++)
            {
                string[] linea = parametros[i].Split(',');
                if (linea[0]!=string.Empty){
                    parametrosSensores.Add(new ParametroSensor
                    {
                        Id = int.Parse(linea[0]),
                        CodigoParametro = long.Parse(linea[1]),
                        DescripcionLarga = linea[2],
                        DescripcionMedia = linea[3],
                        DescripcionCorta = linea[4],
                        Abreviatura = linea[5],
                        Observacion = linea[6],
                        FechaCreacion = DateTime.ParseExact(linea[7], "M/d/yyyy H:m", CultureInfo.CreateSpecificCulture("en-US")),
                        FechaModificacion = DateTime.ParseExact(linea[8], "M/d/yyyy H:m", CultureInfo.CreateSpecificCulture("en-US")),
                        Estado = linea[9],
                        Unidad = linea[10]
                    });
                }
            }

            modelBuilder.Entity<ParametroSensor>().HasData(parametrosSensores.AsEnumerable()); // Poblar tabla parametros de sensores

            // recorrido de los datos para poblar la tabla de datos de sensores
            for (int i = 1; i < datos.Length - 1; i++)
            {
                string[] linea = datos[i].Split(',');
                datosSensores.Add(new DatoSensor{
                    Id = int.Parse(linea[0]),
                    CodigoParametro = long.Parse(linea[1]),
                    ParametroSensoresId = int.Parse(linea[2]),
                    NombreParametro = linea[3],
                    FechaDato = DateTime.ParseExact(linea[4], "M/d/yyyy H:m", null),
                    ValorNumero = decimal.Parse(linea[5]),
                });
            }

            modelBuilder.Entity<DatoSensor>().HasData(datosSensores); // Poblar tabla datos de sensores

            
        }

        // metodo para leer los archivos y acumular el resultado en un array de string, separados por linea
        private static string[] LeerArchivo(string ruta)
        {
            string[] lineas = null;
            
            if (File.Exists(ruta))
            {
                try
                {
                    lineas = File.ReadAllLines(ruta, Encoding.Default);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return lineas;
        }
    }
}
