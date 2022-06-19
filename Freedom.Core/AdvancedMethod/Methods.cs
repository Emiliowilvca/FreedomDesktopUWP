using System;

namespace Freedom.Core
{
    public static class Methods
    {
        /* Ex: Int, Decimal, Double is struct
           Ex: string is Class and other models class */

        // for use struct
        public static TOut Pipe<TIn, TOut>(this TIn value, Func<TIn, TOut> funcOut) where TIn :
                                          struct where TOut : struct
        {
            return funcOut(value);
        }

        //for use class
        public static TOut PipeClass<TIn, TOut>(this TIn value, Func<TIn, TOut> funcOut)
                                                where TIn : class where TOut : class
        {
            return funcOut(value);
        }

        /* for use struc to class, Example: Convert stringTodouble */
        public static TOut PipeToClass<TIn, TOut>(this TIn value, Func<TIn, TOut> funcOut)
                                                  where TIn : class where TOut : struct
        {
            return funcOut(value);
        }

        #region Funciones

        //public static Func<decimal, decimal> iva = (decimal amount) => amount + (amount * 0.16m);
        //public static Func<decimal, decimal> discount = (decimal amount) => amount - (amount - 0.1m);
        //public static Func<decimal, decimal> surcharge = (decimal amount) => amount + (amount * 0.2m);
        //public static Func<decimal, decimal> fees = (decimal amount) => amount / 12;

        /*  example use
            ------------------------
            decimal total = amount.Pipe(surcharge)
                                  .Pipe(discount)
                                  .Pipe(iva)
                                  .Pipe(fees);
            console.WriteLine(total);
         */

        #endregion Funciones
    }
}