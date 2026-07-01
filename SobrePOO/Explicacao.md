# Pilares da Programação Orientada a Objetos (POO) — C#

## O que é POO?

Programação Orientada a Objetos (POO) é um paradigma que organiza o código em objetos que representam entidades do mundo real.

Exemplos:

* Carro
* Usuário
* Produto
* Pedido

Os quatro pilares da POO são:

1. Abstração
2. Encapsulamento
3. Herança
4. Polimorfismo

---

# 1. Abstração

## O que é?

Abstração consiste em mostrar apenas as características importantes de um objeto e esconder detalhes desnecessários.

A ideia é responder:

> "O que esse objeto faz?"

e não:

> "Como ele faz isso internamente?"

### Exemplo

```csharp
public interface IVeiculo
{
    void Mover();
}
```

Aqui estamos dizendo:

* Todo veículo deve possuir um método `Mover()`
* Não estamos dizendo como ele será implementado

Agora uma implementação:

```csharp
public class Carro : IVeiculo
{
    public void Mover()
    {
        Console.WriteLine("Carro andando");
    }
}
```

Outro exemplo:

```csharp
public class Aviao : IVeiculo
{
    public void Mover()
    {
        Console.WriteLine("Avião voando");
    }
}
```

A interface representa uma abstração:

```text
Veículo precisa se mover
```

Mas cada objeto decide como fará isso.

---

# 2. Encapsulamento

## O que é?

Encapsulamento é proteger dados internos de uma classe e controlar o acesso a eles.

A ideia é:

> "Nem tudo deve ser acessado diretamente."

### Exemplo

Sem encapsulamento:

```csharp
public class ContaBancaria
{
    public decimal Saldo;
}
```

Problema:

```csharp
ContaBancaria conta = new ContaBancaria();

conta.Saldo = -5000;
```

Qualquer pessoa poderia alterar o valor incorretamente.

Com encapsulamento:

```csharp
public class ContaBancaria
{
    private decimal saldo;

    public decimal Saldo
    {
        get { return saldo; }
    }

    public void Depositar(decimal valor)
    {
        if(valor > 0)
        {
            saldo += valor;
        }
    }
}
```

Uso:

```csharp
ContaBancaria conta = new ContaBancaria();

conta.Depositar(100);

Console.WriteLine(conta.Saldo);
```

Agora o valor só pode ser alterado pelas regras definidas.

---

# 3. Herança

## O que é?

Herança permite que uma classe herde características e comportamentos de outra classe.

A ideia é:

> "Reaproveitar código."

### Exemplo

Classe base:

```csharp
public class Animal
{
    public void Dormir()
    {
        Console.WriteLine("Animal dormindo");
    }
}
```

Classe filha:

```csharp
public class Cachorro : Animal
{
    public void Latir()
    {
        Console.WriteLine("Au Au");
    }
}
```

Uso:

```csharp
Cachorro cachorro = new Cachorro();

cachorro.Dormir();
cachorro.Latir();
```

Saída:

```text
Animal dormindo
Au Au
```

`Cachorro` herdou comportamentos de `Animal`.

---

# 4. Polimorfismo

## O que é?

Polimorfismo significa:

> "Muitas formas"

Permite usar o mesmo método de maneiras diferentes dependendo do objeto.

### Exemplo

Classe abstrata:

```csharp
public abstract class Animal
{
    public abstract void FazerSom();
}
```

Implementações:

```csharp
public class Cachorro : Animal
{
    public override void FazerSom()
    {
        Console.WriteLine("Au Au");
    }
}
```

```csharp
public class Gato : Animal
{
    public override void FazerSom()
    {
        Console.WriteLine("Miau");
    }
}
```

Uso:

```csharp
Animal animal1 = new Cachorro();
Animal animal2 = new Gato();

animal1.FazerSom();
animal2.FazerSom();
```

Saída:

```text
Au Au
Miau
```

Mesmo método:

```csharp
FazerSom()
```

Com comportamentos diferentes.

---

# Diferença entre Interface e Classe Abstrata

Essa é uma dúvida muito comum porque ambas trabalham bastante com abstração.

## Interface

Uma interface define apenas regras que as classes devem seguir.

Exemplo:

```csharp
public interface IAnimal
{
    void FazerSom();
}
```

Classe implementando:

```csharp
public class Cachorro : IAnimal
{
    public void FazerSom()
    {
        Console.WriteLine("Au Au");
    }
}
```

Características:

* Define contratos
* Normalmente descreve comportamentos
* Permite múltiplas implementações
* Uma classe pode implementar várias interfaces

Exemplo:

```csharp
public class Robo : IAnimal, ICalculadora
{
}
```

---

## Classe Abstrata

Uma classe abstrata funciona como uma classe parcialmente pronta.

Exemplo:

```csharp
public abstract class Animal
{
    public void Dormir()
    {
        Console.WriteLine("Dormindo");
    }

    public abstract void FazerSom();
}
```

Implementação:

```csharp
public class Cachorro : Animal
{
    public override void FazerSom()
    {
        Console.WriteLine("Au Au");
    }
}
```

Características:

* Pode possuir implementação pronta
* Pode possuir atributos
* Pode possuir métodos concretos
* Não pode ser instanciada diretamente

Erro:

```csharp
Animal animal = new Animal();
```

---

## Resumo rápido

| Interface                | Classe Abstrata               |
| ------------------------ | ----------------------------- |
| Define regras            | Define regras + implementação |
| Sem estado compartilhado | Pode possuir atributos        |
| Implementada com `:`     | Herdada com `:`               |
| Pode implementar várias  | Só pode herdar uma            |
| Foco em comportamento    | Foco em estrutura comum       |

---

## Regra prática

Use Interface quando pensar:

> "O que esse objeto consegue fazer?"

Exemplo:

```text
Imprimir
Salvar
EnviarEmail
```

Use Classe Abstrata quando pensar:

> "O que esses objetos têm em comum?"

Exemplo:

```text
Animal
Veículo
Funcionário
```

Uma forma simples de lembrar:

```text
Interface = capacidade

Classe abstrata = modelo base
```
