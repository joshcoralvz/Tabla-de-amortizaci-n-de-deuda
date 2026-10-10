//uso de la libreria Spectre.Console
using Spectre.Console; 
//solicitud de datos
decimal montoPrestamo = AnsiConsole.Ask<decimal>("Ingresa el monto del préstamo ");
decimal interesAnual = AnsiConsole.Ask<decimal>("Ingrese el interés anual ");
int plazoPrestamo = AnsiConsole.Ask<int>("Ingrese el plazo del préstamo ");

var tabla = new Table(); //creacion de la tabla con sus columnas
tabla.AddColumn("No. cuotas");
tabla.AddColumn("Pago de couta");
tabla.AddColumn("Interés a pagar");
tabla.AddColumn("Abono a capital");
tabla.AddColumn("Saldo");

//calculo del interes mensual
decimal interesMensual = interesAnual / 12 / 100;
//operaciones de la fórmula para calcular la cuota fija
decimal divArriba = (decimal)Math.Pow(1 + (double)interesMensual, plazoPrestamo);
decimal divAbajo = (decimal)Math.Pow(1 + (double)interesMensual, plazoPrestamo) - 1;
decimal coutaFija = montoPrestamo * interesMensual * (divArriba / divAbajo); //calculo del valor de la cuota fija mensual
decimal saldo = montoPrestamo; //guarda el monto inicial

//ciclo para repeticion del calculo segun cada cuota mensual
for (int i = 1; i <= plazoPrestamo; i++)
{
    decimal interesPagar = saldo * interesMensual; //calculo del interes del mes actual
    decimal abonoCapital = coutaFija - interesPagar; //calculo del abono a capital
    saldo = saldo - abonoCapital; //actualizar el saldo
    //agrega los valores calculados a la tabla y coloca dos decimales
    tabla.AddRow($"{i}", $"{coutaFija:C2}", $"{interesPagar:C2}", $"{abonoCapital:C2}", $"{saldo:C2}");
}
//muestra la tabla en la consola
AnsiConsole.Write(tabla);