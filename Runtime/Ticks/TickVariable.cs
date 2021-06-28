namespace GGTickBase
{
    public sealed class TickVariable : Tick
    {
        #region CONSTRUCTION

        public TickVariable(DataConfigTickVariable data)
        {
            TickName = data.TickName;
            SetTicksets(data.Ticksets);
        }

        #endregion CONSTRUCTION
    }
}