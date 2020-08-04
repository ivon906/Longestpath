using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LongestPath
{
    public class Program
    {
        static void Main(string[] args)
        {
            int PosIniVal = 0;
            int contador = 0;
            bool nvaTrayectoria = true;
            int principalx;
            int principaly;

            List<Ruta> arrayRutas = new List<Ruta>();
            CargarArchivo();
            List<Ruta> Trayectorias = new List<Ruta>();
            Array array = new int[4, 4] { { 4, 8, 7, 3 }, { 2, 5, 9, 3 }, { 6, 3, 2, 5 }, { 4, 4, 1, 6 } };

            
            for (int x = 0; x < array.GetLength(0) - 1; x += 1)
            {
                for (int y = 0; y < array.GetLength(1) - 1; y += 1)
                {
                    PosIniVal = int.Parse(array.GetValue(x, y).ToString());
                    Ruta ruta = new Ruta();
                    ruta.posicionx = x;
                    ruta.posiciony = y;
                    ruta.Estacion = new List<int>();
                    ruta.Estacion.Add(PosIniVal);
                    Trayectorias.Add(ruta);


                    principalx = x;
                    principaly = y;

                    CalcularRuta(x, y, array, PosIniVal, ref Trayectorias, ref ruta, ref contador, nvaTrayectoria, principalx, principaly);
                    if (ruta.Estacion.Count == contador)// && (ruta.posicionx == x && ruta.posiciony == y))
                    {
                        contador = ruta.Estacion.Count;
                        Ruta rutalongest = new Ruta();
                        rutalongest.posicionx = x;
                        rutalongest.posiciony = y;
                        rutalongest.Norutas = contador;
                        contador = 0;
                        arrayRutas.Add(rutalongest);
                    }
                }
            }
            foreach (Ruta r in Trayectorias)
            {
                Console.WriteLine(string.Format("Rutas posicion {0}{1}", r.posicionx, r.posiciony));
                foreach (int estacion in r.Estacion)
                {
                    Console.Write(estacion.ToString() + " ");   
                }
                Console.WriteLine(@"\n");  


            }

            Console.ReadLine();  
        }

   

        private static void CargarArchivo()
        {
            string sLine = ""; // Creamos un string donde se guardaran las lineas del archivo
           
            ArrayList arrText = new ArrayList(); // Creamos una matriz para guardar linea por linea
            StreamReader objLeer = new StreamReader("C:\\Users\\Documents\\Desarrollo\\PuebaTecnica Ease Solutions\\4x4.txt");

            while (sLine != null)
            {
                sLine = objLeer.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            //Cerramos el archivo
            objLeer.Close();        
        }
        private static List<Ruta> CalcularRuta(int x, int y, Array array, int PosIniVal,  ref List<Ruta> Trayectoria, ref Ruta ruta,  ref int contador, bool IsNvaTrayectoria, int principalx, int principaly)
        {
            int Pdy = y;
            int Pdx = x;
            int CoordenadaY;
            int CoordenadaX;
            int PosNvaVal = 0;

            //if (IsNvaTrayectoria)
            //    contador = 1;
           
            //if(ruta != null)
             contador = ruta.Estacion.Count;


            //Ir al oriente suma 1 a Y
            
            CoordenadaY = Pdy + 1;
                MoverseSobreY(x, y, array, ref PosIniVal, ref Trayectoria, ref ruta, ref Pdy, CoordenadaY, ref PosNvaVal, ref contador,  false, principalx, principaly);

            //if (IsNvaTrayectoria)
            //{
            //    ruta = new Ruta();
            //    ruta.posicionx = x;
            //    ruta.posiciony = y;
            //    ruta.Estacion = new List<int>();
            //    ruta.Estacion.Add(PosIniVal);
            //    Trayectoria.Add(ruta);
            //}

            //if (IsNvaTrayectoria)
            //    contador = 1;
            ////Ir al Occidente resta 1 a Y 
            CoordenadaY = Pdy - 1;
                MoverseSobreY(x, y, array, ref PosIniVal, ref Trayectoria, ref  ruta, ref Pdy, CoordenadaY, ref PosNvaVal, ref contador,  false, principalx, principaly);

            //if (IsNvaTrayectoria)
            //    contador = 1;
            //Ir al norte Resta 1 a X
            CoordenadaX = Pdx - 1;
                MoverseSobreX(x, y, array, ref PosIniVal, ref Trayectoria, ref ruta, ref Pdx, CoordenadaX, ref PosNvaVal, ref contador,  false, principalx, principaly);

            //if (IsNvaTrayectoria)
            //    contador = 1;
            //Ir al Sur suma 1 a X
            CoordenadaX = Pdx + 1;
            MoverseSobreX(x, y, array, ref PosIniVal, ref Trayectoria, ref ruta,  ref Pdx, CoordenadaX, ref PosNvaVal, ref contador,  false, principalx, principaly);
                //if (ruta.Estacion.Count == contador) && (posicionx== x &&
                
            return Trayectoria;
        }

        private static void MoverseSobreY(int x, int y, Array array, ref int PosIniVal, ref List<Ruta> Trayectoria, ref Ruta ruta,  ref int Pdy, int CoordenadaY, ref int PosNvaVal, ref int contador, bool IsNvaTrayectoria, int principalx, int principaly)
        {
          if (CoordenadaY > 0 )
            if(CoordenadaY < array.GetLength(1))
            {
                PosNvaVal = int.Parse(array.GetValue(x, CoordenadaY).ToString());
                if (PosIniVal >= PosNvaVal)
                {
                        //if (contador == 1)
                        //{
                        //        ruta = new Ruta();
                        //        ruta.posicionx = principalx;
                        //        ruta.posiciony = principaly;
                        //        ruta.Estacion = new List<int>();
                        //        //ruta.Estacion.Add(ruta.Estacion[0]);
                        //        ruta.Estacion.Add(PosIniVal);
                        //    Trayectoria.Add(ruta);
                        //}


                      Pdy = CoordenadaY;
                    PosIniVal = PosNvaVal;
                    ruta.Estacion.Add(PosNvaVal);

                        if(PosIniVal == 3)
                        {
                            PosIniVal = 3;
                        }
                    CalcularRuta(x, Pdy, array, PosIniVal, ref Trayectoria, ref ruta, ref contador, IsNvaTrayectoria, principalx, principaly);
                }
            }
        }

        private static void MoverseSobreX(int x, int y, Array array, ref int PosIniVal, ref List<Ruta> Trayectoria, ref Ruta ruta,  ref int Pdx, int CoordenadaX, ref int PosNvaVal, ref int contador, bool IsNvaTrayectoria,int  principalx, int principaly)
        {
            if (CoordenadaX > 0)
                if (CoordenadaX < array.GetLength(1))
                {

                PosNvaVal = int.Parse(array.GetValue(CoordenadaX, y).ToString());
                if (PosIniVal >= PosNvaVal)
                {
                        //if (contador == 1)
                        //{
                        //    ruta = new Ruta();
                        //    ruta.posicionx = principalx;
                        //    ruta.posiciony = principaly;
                        //    ruta.Estacion = new List<int>();
                        //    //ruta.Estacion.Add(ruta.Estacion[0]);
                        //    ruta.Estacion.Add(PosIniVal);
                        //    Trayectoria.Add(ruta);
                        //}
                        Pdx = CoordenadaX;
                    PosIniVal = PosNvaVal;
                    ruta.Estacion.Add(PosNvaVal);
                    CalcularRuta(Pdx, y, array, PosIniVal, ref Trayectoria, ref ruta, ref contador,  IsNvaTrayectoria, principalx, principaly);
    }
            }
        }
    }
}


public class Ruta
{ 
       public int posicionx { get; set; }
       public int posiciony { get; set; }
       public int Norutas{ get; set; }
    
        public List<int> Estacion { get; set; }
}