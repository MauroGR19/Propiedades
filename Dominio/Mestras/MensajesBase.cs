using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mestras
{
    public static class MensajesBase
    {
        public static string ObtenerDeascripcionEnum(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public enum Satisfactorio
        {
            [Description("Insertado correctamente")]
            Insertado,
            [Description("Actualizado correctamente")]
            Actualizado,
            [Description("Eliminado correctamente")]
            Eliminado
        }

        public enum Error
        {
            [Description("No se pudo insertar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Insertar,
            [Description("No se pudo actualizar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Actualizar,
            [Description("No se pudo eliminar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Eliminar,
            [Description("No se pudo obtener, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Obtener
        }

        public enum ErrorOtro
        {
            [Description("El comentario no puede estar vacío para una tarea de alta prioridad.")]
            ValidaComentario,
            [Description("Ya existe una tarea con ese código y fecha de inicio.")]
            ValidaTarea,
            [Description("La fecha fin no puede superar por más de 15 días la fecha inicio.")]
            ValidaFecha15,
            [Description("La fecha fin no puede superar por más de 2 días la fecha inicio.")]
            ValidaFecha2,
            [Description("La fecha inicio no puede ser menor que la fecha actual.")]
            ValidaFecha,
            [Description("No se puede eliminar tareas con los estados finalizada o en proceso.")]
            ValidaEstadoEli,
            [Description("Solo puede eliminarse cuando el estado es Nueva.")]
            ValidaEstadoPrio,
            [Description("No se pueden eliminar tareas que ya cumplan el tiempo l´ımite de ejecuci´on (Fecha Fin).")]
            ValidaFechaLimite,
            [Description("No se puede editar una tarea que cuente con el estado Finalizada.")]
            ValidaEstadoFinal,
            [Description("No se puede editar una tarea que cuente con la prioridad Alta y estado En Proceso.")]
            ValidaEstadoProceso,
            [Description("La fecha fin NO puede ser menor a la fecha inicio.")]
            ValidaFechaIniFin,
            [Description("La fecha fin es menor a la fecha actual, solo se puede cambiar el estado a cancelada.")]
            ValidaFechaFinCancel,
            [Description("No se puede actualizar la persona asignada cuando es estado es diferente a nueva.")]
            ValidaEstadoPersona,
        }
    }
}
