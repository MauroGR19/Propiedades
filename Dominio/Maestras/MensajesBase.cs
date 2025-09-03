using System.ComponentModel;

namespace Dominio.Maestras
{
    public static class MensajesBase
    {
        #region Metodos
        public static string ObtenerDeascripcionEnum(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
        #endregion

        #region Enums
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

        #endregion
    }
}
