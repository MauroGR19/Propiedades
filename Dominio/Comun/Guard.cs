using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Dominio.Excepciones;

namespace Dominio.Comun
{
    public static class Guard
    {
        public static void NoNuloOVacio(string valor, string nombreCampo, string mensaje = null) 
        {
            if (string.IsNullOrEmpty(valor))
                throw new ValidacionDominioException(nombreCampo, valor, mensaje ?? "No puede estar vacío");
        }
            
        public static void LongitudMinima(string valor, int longitudMinima, string nombreCampo )
        {
            if (valor.Length < longitudMinima)
                throw new ValidacionDominioException(nombreCampo, valor, $"debe tener al menos {longitudMinima} caracteres");
        }

        public static void NoNulo<T>(T valor, string nombreCampo, string mensaje = null) where T : class
        {
            if (valor == null)
                throw new ValidacionDominioException(nombreCampo, "null", mensaje ?? "no puede ser nulo");
        }

        public static void MayorQue(int valor, int minimo, string nombreCampo)
        {
            if (valor <= minimo)
                throw new ValidacionDominioException(nombreCampo, valor.ToString(), $"debe ser mayor que {minimo}");
        }

        public static void MayorQue(decimal valor, decimal minimo, string nombreCampo)
        {
            if (valor <= minimo)
                throw new ValidacionDominioException(nombreCampo, valor.ToString(), $"debe ser mayor que {minimo}");
        }

        public static void MayorOIgualQue(decimal valor, decimal minimo, string nombreCampo)
        {
            if (valor < minimo)
                throw new ValidacionDominioException(nombreCampo, valor.ToString(), $"debe ser mayor o igual que {minimo}");
        }

        public static void MenorOIgualQue(int valor, int maximo, string nombreCampo)
        {
            if (valor > maximo)
                throw new ValidacionDominioException(nombreCampo, valor.ToString(), $"debe ser menor o igual que {maximo}");
        }

        public static void FechaNoFutura(DateTime fecha, string nombreCampo)
        {
            if (fecha > DateTime.Now)
                throw new ValidacionDominioException(nombreCampo, fecha.ToString(), "no puede ser futura");
        }

        public static void FechaNoMuyAntigua(DateTime fecha, int aniosMaximos, string nombreCampo)
        {
            var fechaMinima = DateTime.Now.AddYears(-aniosMaximos);
            if (fecha < fechaMinima)
                throw new ValidacionDominioException(nombreCampo, fecha.ToString(), $"no puede ser anterior a {aniosMaximos} años");
        }

        public static void AnioValido(int anio, string nombreCampo)
        {
            var anioActual = DateTime.Now.Year;
            if (anio < 1900 || anio > anioActual)
                throw new ValidacionDominioException(nombreCampo, anio.ToString(), $"debe estar entre 1900 y {anioActual}");
        }

        public static void ArchivoImagenValido(string archivo, string nombreCampo)
        {
            NoNuloOVacio(archivo, nombreCampo);
            
            var extensionesValidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(archivo)?.ToLower();
            
            if (!extensionesValidas.Contains(extension))
                throw new ValidacionDominioException(nombreCampo, archivo, "debe ser una imagen válida (.jpg, .jpeg, .png, .gif, .bmp)");
        }

        public static void LongitudEnRango(string valor, int minimo, int maximo, string nombreCampo)
        {
            if (valor?.Length < minimo || valor?.Length > maximo)
                throw new ValidacionDominioException(nombreCampo, valor, $"debe tener entre {minimo} y {maximo} caracteres");
        }
    }
}
