using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limcap.EasyTools.PowerSyntax {
	public static class GeneralExt {
		public static O Update<O>( this O obj, ref O variable ) { variable = obj; return obj; }
		public static O Set<O>( this O obj, out O variable ) { variable = obj; return obj; }
		public static O Set<O, R>( this O obj, out R result, R value ) { result = value; return obj; }
		public static O EvalTo<O, R>( this O obj, out R result, Func<O, R> method ) { result = method( obj ); return obj; }
		public static O EvalIf<O>( this O obj, bool condition, Action<O> action ) { if(condition) action( obj ); return obj; }
		public static R Eval<O, R>( this O obj, Func<O, R> func ) { return func( obj ); }
		public static O Eval<O>( this O obj, Action<O> action ) { action( obj ); return obj; }
		//public static O AssignTo<O>( this O obj, ref O variable ) { variable = obj; return obj; }
		//public static O DeclareAs<O>( this O obj, out O variable ) { variable = obj; return obj; }
		//public static O UseAs<O>( this O obj, Action<O> action ) { action( obj ); return obj; }
		//public static R EvalAs<O, R>( this O obj, Func<O, R> func ) { return func( obj ); }
		//public static O EvalTo<O, R>( this O obj, out R result, Func<O, R> method ) { result = method( obj ); return obj; }
		//public static O Define<O, R>( this O obj, out R result, R value ) { result = value; return obj; }
	}
}
