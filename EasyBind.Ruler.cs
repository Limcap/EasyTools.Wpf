namespace Limcap.EasyTools.Wpf.DataBinding {
	public partial class EasyBind {
		public class Ruler {
			readonly double Base;
			readonly double Value;
			public Ruler( double _base, double _value ) {
				Base = _base;
				Value = _value;
			}
			public double Ratio1 => Value / Base;
			public double OneThird => Base / 3.0 * Ratio1;
			public double TwoThirds => Base / 3.0 * 2.0 * Ratio1;
		}
	}

}