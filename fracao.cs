using System;
using System.Globalization;

public class Fracao : IEquatable<Fracao>
{
    public int Numerador { get; private set; }
    public int Denominador { get; private set; }

    public Fracao(int numerador, int denominador)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(denominador, 0, nameof(denominador));
        
        int mdc = CalcularMDC(Math.Abs(numerador), Math.Abs(denominador));
        Numerador = numerador / mdc;
        Denominador = denominador / mdc;

        if (Denominador < 0)
        {
            Numerador = -Numerador;
            Denominador = -Denominador;
        }
    }

    public Fracao(int inteiro) : this(inteiro, 1) { }

    public Fracao(string fracao)
    {
        var partes = fracao.Split('/');
        if (partes.Length != 2)
            throw new ArgumentException("Formato inválido para fração");

        int num = int.Parse(partes[0]);
        int den = int.Parse(partes[1]);
        
        ArgumentOutOfRangeException.ThrowIfEqual(den, 0, nameof(den));
        
        int mdc = CalcularMDC(Math.Abs(num), Math.Abs(den));
        Numerador = num / mdc;
        Denominador = den / mdc;

        if (Denominador < 0)
        {
            Numerador = -Numerador;
            Denominador = -Denominador;
        }
    }

    public Fracao(double valor)
    {
        if (double.IsNaN(valor) || double.IsInfinity(valor))
            throw new ArgumentException("Valor inválido para conversão em fração");

        var (numerador, denominador) = ConverterDecimalParaFracao(valor);
        
        int mdc = CalcularMDC(Math.Abs(numerador), Math.Abs(denominador));
        Numerador = numerador / mdc;
        Denominador = denominador / mdc;

        if (Denominador < 0)
        {
            Numerador = -Numerador;
            Denominador = -Denominador;
        }
    }

    private static (int numerador, int denominador) ConverterDecimalParaFracao(double valor)
    {
        if (valor == 0) return (0, 1);

        bool negativo = valor < 0;
        valor = Math.Abs(valor);

        int sinal = negativo ? -1 : 1;
        
        string strValor = valor.ToString("R", CultureInfo.InvariantCulture);
        
        if (!strValor.Contains('.'))
        {
            return (sinal * (int)valor, 1);
        }

        string parteDecimal = strValor.Split('.')[1];
        int casasDecimais = parteDecimal.Length;
        
        int denominador = (int)Math.Pow(10, casasDecimais);
        int numerador = (int)(valor * denominador);
        
        return (sinal * numerador, denominador);
    }

    private static int CalcularMDC(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public Fracao Somar(int valor)
    {
        return this + new Fracao(valor);
    }

    public static Fracao operator +(Fracao f1, Fracao f2)
    {
        int novoNumerador = f1.Numerador * f2.Denominador + f2.Numerador * f1.Denominador;
        int novoDenominador = f1.Denominador * f2.Denominador;
        return new Fracao(novoNumerador, novoDenominador);
    }

    public static Fracao operator +(Fracao f, int valor)
    {
        return f + new Fracao(valor);
    }

    public static Fracao operator +(Fracao f, double valor)
    {
        var resultado = f + new Fracao(valor);
        f.Numerador = resultado.Numerador;
        f.Denominador = resultado.Denominador;
        return resultado;
    }

    public static Fracao operator +(Fracao f, string valor)
    {
        var resultado = f + new Fracao(valor);
        f.Numerador = resultado.Numerador;
        f.Denominador = resultado.Denominador;
        return resultado;
    }

    public static bool operator ==(Fracao f1, Fracao f2)
    {
        if (ReferenceEquals(f1, f2)) return true;
        if (f1 is null || f2 is null) return false;
        return f1.Numerador == f2.Numerador && f1.Denominador == f2.Denominador;
    }

    public static bool operator !=(Fracao f1, Fracao f2)
    {
        return !(f1 == f2);
    }

    public static bool operator <(Fracao f1, Fracao f2)
    {
        return f1.Numerador * f2.Denominador < f2.Numerador * f1.Denominador;
    }

    public static bool operator >(Fracao f1, Fracao f2)
    {
        return f1.Numerador * f2.Denominador > f2.Numerador * f1.Denominador;
    }

    public static bool operator <=(Fracao f1, Fracao f2)
    {
        return f1 < f2 || f1 == f2;
    }

    public static bool operator >=(Fracao f1, Fracao f2)
    {
        return f1 > f2 || f1 == f2;
    }

    public bool IsImpropria => Math.Abs(Numerador) >= Denominador;
    public bool IsPropria => Math.Abs(Numerador) < Denominador;
    public bool IsAparente => Numerador % Denominador == 0;
    public bool IsUnitaria => Math.Abs(Numerador) == 1;

    public bool Equals(Fracao other)
    {
        if (other is null) return false;
        return Numerador == other.Numerador && Denominador == other.Denominador;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Fracao);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerador, Denominador);
    }

    public override string ToString()
    {
        return $"{Numerador}/{Denominador}";
    }
}