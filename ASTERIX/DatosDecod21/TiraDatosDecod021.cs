namespace DatosDecod21
{
    public class TiraDatosDecod021
    {
         public int DataSourceID { get; set; }
         public int TargetRep { get; set; }
         public int PosWGS84 { get; set; }
         public int TargetAddrs { get; set; }
         public int TimeOfRecep { get; set; }
         public int Mode3ACode { get; set; }
         public int FL { get; set; }
         public int ReservedExpField { get; set; }

         public TiraDatosDecod021()
         {
            this.DataSourceID = -1;
            this.TargetRep = -1;
            this.PosWGS84 = -1;
            this.TargetAddrs = -1;
            this.TimeOfRecep = -1;
            this.Mode3ACode = -1;
            this.FL = -1;
            this.ReservedExpField = -1;
         }
    }
}
