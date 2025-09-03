namespace PropiedadesAPI.Filtros
{
    public class AtributoPersonal
    {
        #region Atributos
        public readonly bool ContainsAttribute;
        public readonly bool Mandatory;
        #endregion

        #region Constructor
        public AtributoPersonal(bool _ContainsAttribute, bool _Mandatory)
        {
            ContainsAttribute = _ContainsAttribute;
            Mandatory = _Mandatory;
        }
        #endregion
    }
}
