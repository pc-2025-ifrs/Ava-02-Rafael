class Fracao {
    constructor(...args) {
        if (args.length === 2) {
            this._inicializarComDoisNumeros(args[0], args[1]);
        } else if (args.length === 1) {
            const arg = args[0];
            if (typeof arg === 'string') {
                this._inicializarComString(arg);
            } else if (typeof arg === 'number') {
                if (Number.isInteger(arg)) {
                    this._inicializarComDoisNumeros(arg, 1);
                } else {
                    this._inicializarComDecimal(arg);
                }
            }
        }
    }

    _inicializarComDoisNumeros(x, y) {
        if (y === 0) {
            throw new Error('Denominador não pode ser zero');
        }
        
        const g = this._obterMDC(Math.abs(x), Math.abs(y));
        this.numerador = Math.floor(x / g);
        this.denominador = Math.floor(y / g);

        if (this.denominador < 0) {
            this.numerador = -this.numerador;
            this.denominador = -this.denominador;
        }
    }

    _inicializarComString(texto) {
        const segmentos = texto.split('/');
        if (segmentos.length !== 2) {
            throw new Error('Formato inválido para fração');
        }

        const a = parseInt(segmentos[0]);
        const b = parseInt(segmentos[1]);
        
        if (b === 0) {
            throw new Error('Denominador não pode ser zero');
        }
        
        const g = this._obterMDC(Math.abs(a), Math.abs(b));
        this.numerador = Math.floor(a / g);
        this.denominador = Math.floor(b / g);

        if (this.denominador < 0) {
            this.numerador = -this.numerador;
            this.denominador = -this.denominador;
        }
    }

    _inicializarComDecimal(numero) {
        if (!isFinite(numero)) {
            throw new Error('Valor inválido para conversão em fração');
        }

        const [p, q] = this._processarDecimal(numero);
        
        const g = this._obterMDC(Math.abs(p), Math.abs(q));
        this.numerador = Math.floor(p / g);
        this.denominador = Math.floor(q / g);

        if (this.denominador < 0) {
            this.numerador = -this.numerador;
            this.denominador = -this.denominador;
        }
    }

    _processarDecimal(n) {
        if (n === 0) return [0, 1];

        const ehNegativo = n < 0;
        n = Math.abs(n);
        
        const fator = ehNegativo ? -1 : 1;
        
        const textoNumero = n.toString();
        
        if (!textoNumero.includes('.')) {
            return [fator * Math.floor(n), 1];
        }

        const decimais = textoNumero.split('.')[1];
        const casas = decimais.length;
        
        const q = Math.pow(10, casas);
        const p = Math.floor(n * q);
        
        return [fator * p, q];
    }

    _obterMDC(m, n) {
        while (n !== 0) {
            const r = n;
            n = m % n;
            m = r;
        }
        return m;
    }

    somar(k) {
        return this.adicionar(new Fracao(k));
    }

    adicionar(outra) {
        if (typeof outra === 'number') {
            outra = new Fracao(outra);
        } else if (typeof outra === 'string') {
            outra = new Fracao(outra);
        }

        const novoP = this.numerador * outra.denominador + outra.numerador * this.denominador;
        const novoQ = this.denominador * outra.denominador;
        return new Fracao(novoP, novoQ);
    }

    adicionarMutavel(valor) {
        const resultado = this.adicionar(valor);
        this.numerador = resultado.numerador;
        this.denominador = resultado.denominador;
        return resultado;
    }

    equals(outra) {
        if (!outra) return false;
        return this.numerador === outra.numerador && this.denominador === outra.denominador;
    }

    menorQue(outra) {
        return this.numerador * outra.denominador < outra.numerador * this.denominador;
    }

    maiorQue(outra) {
        return this.numerador * outra.denominador > outra.numerador * this.denominador;
    }

    menorOuIgual(outra) {
        return this.menorQue(outra) || this.equals(outra);
    }

    maiorOuIgual(outra) {
        return this.maiorQue(outra) || this.equals(outra);
    }

    get isImpropria() {
        return Math.abs(this.numerador) >= this.denominador;
    }

    get isPropria() {
        return Math.abs(this.numerador) < this.denominador;
    }

    get isAparente() {
        return this.numerador % this.denominador === 0;
    }

    get isUnitaria() {
        return Math.abs(this.numerador) === 1;
    }

    toString() {
        return `${this.numerador}/${this.denominador}`;
    }
}


const f1 = new Fracao(5, 10);
console.log('f1.numerador == 1:', f1.numerador === 1);
console.log('f1.denominador == 2:', f1.denominador === 2);
console.log('f1.toString() == "1/2":', f1.toString() === "1/2");
console.log('f1:', f1.toString());

const f2 = new Fracao(3);
console.log('f2:', f2.toString());

const f3 = new Fracao("30/40");
console.log('f3.numerador == 3:', f3.numerador === 3);
console.log('f3.denominador == 4:', f3.denominador === 4);
console.log('f3:', f3.toString());

const f4 = new Fracao(0.345);
console.log('f4:', f4.toString());

const f5 = new Fracao(0.4);
console.log('f5.numerador == 2:', f5.numerador === 2);
console.log('f5.denominador == 5:', f5.denominador === 5);
console.log('f5:', f5.toString());

const f6 = f1.somar(2);
console.log('f6:', f6.toString());

const f7 = f1.adicionar(2);
console.log('f7:', f7.toString());

const f8 = f1.adicionar(0.5);
console.log('f8:', f8.toString());

const f9 = f1.adicionar(0.2862);
console.log('f9:', f9.toString());

const f10 = f3.adicionarMutavel("7/8");
console.log('f3 após soma:', f3.toString());

const f11 = f3.adicionarMutavel(6.45);
console.log('f3 após segunda soma:', f3.toString());

const f12 = new Fracao(1, 5);
const f13 = new Fracao(1, 3);
const f14 = new Fracao(125, 375);
const f15 = new Fracao(15, 75);

console.log('f12.equals(f14):', f12.equals(f14));
console.log('f12.equals(f15):', f12.equals(f15));
console.log('f13.equals(f14):', f13.equals(f14));
console.log('new Fracao("3/19").equals(new Fracao(3, 19)):', new Fracao("3/19").equals(new Fracao(3, 19)));

const f16 = new Fracao(2, 12);
const f17 = new Fracao(3, 4);
const f18 = new Fracao(9, 10);
const f19 = new Fracao(5);
const f20 = new Fracao(24, 18);
const f21 = new Fracao(16, 8);
const f22 = new Fracao(1, 8);
const f23 = new Fracao(10, 80);

console.log('f16 < f17:', f16.menorQue(f17));
console.log('f18 > f17:', f18.maiorQue(f17));
console.log('f19 > f18:', f19.maiorQue(f18));
console.log('f12 >= f15:', f12.maiorOuIgual(f15));
console.log('f16 < f20:', f16.menorQue(f20));

console.log('f16.isImpropria:', f16.isImpropria);
console.log('f16.isPropria:', f16.isPropria);
console.log('f20.isImpropria:', f20.isImpropria);
console.log('f20.isAparente:', f20.isAparente);
console.log('f21.isPropria:', f21.isPropria);
console.log('f21.isAparente:', f21.isAparente);
console.log('f21.isUnitaria:', f21.isUnitaria);
console.log('f22.isUnitaria:', f22.isUnitaria);
console.log('f23.isUnitaria:', f23.isUnitaria);

try {
    const f24 = new Fracao(5, 0);
    console.log('ERRO: Deveria ter lançado exceção!');
} catch (e) {
    console.log('Exceção capturada corretamente:', e.message);
}
