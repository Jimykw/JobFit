# Anotações do JobFit

Diário de estudo do projeto JobFit (assistente de aderência a vagas em C#).
Serve para revisar o que aprendi e como fonte para currículo, LinkedIn e portfólio.

---

## Histórico por etapa

### Etapas 1 e 2: C# básico e motor de aderência
- Tipos de dado (`int`, `double`, `bool`, `string`), listas e LINQ (`Where`, `Select`, `Count`, `Except`).
- `record` para modelar `Requisito` e `Vaga`; métodos reutilizáveis e princípio DRY.
- Cálculo de aderência com pesos 70/30 (obrigatórios e desejáveis).
- Tratamento de casos de borda: lista vazia, divisão por zero (`NaN`), requisitos repetidos (`DistinctBy`), espaços e maiúsculas (`Trim`, `OrdinalIgnoreCase`).
- Refatoração com o método `Proporcao`.

### Git e GitHub
- `git init`, `.gitignore`, `add`, `commit`, `push`, remote `origin`.
- Repositório publicado em github.com/Jimykw/JobFit.

### Etapa 3: classes e organização
- Diferença entre `record` e `class` (igualdade por valor vs. por objeto).
- Classe `Perfil` com propriedades e método `AdicionarSkill`.
- Separação em `Models` e `Services`, com `namespace` e `using`.
- Encapsulamento com `public` e `private` na classe `CalculadoraAderencia`.

### Etapa 4: debugger
- Breakpoints, execução passo a passo (F10, F11, F5) e inspeção de variáveis no VS Code.
- Leitura de mensagens de erro do compilador e correção de erros de namespace e acessibilidade.

### Próximas etapas
- [ ] Etapa 5: testes automatizados com xUnit
- [ ] API com ASP.NET Core
- [ ] SQL Server com Entity Framework Core
- [ ] Integração com IA (extração de requisitos em JSON)
- [ ] Docker e CI com GitHub Actions

---

## Glossário: termo a termo

### 1. Variáveis e tipos

| Termo | O que é | Quando usar |
|---|---|---|
| `var` | O C# descobre o tipo sozinho | Quando o tipo é óbvio pela direita: `var perfil = new Perfil();` |
| `int` | Número inteiro | Contagens: `obrigatorios.Count` |
| `double` | Número decimal | Porcentagens e notas: `double aderencia` |
| `bool` | `true` ou `false` | Perguntas de sim/não: `Obrigatorio` |
| `string` | Texto | Nomes, skills, títulos |
| `(double)x` | **Cast**: converte o tipo | Antes de dividir inteiros, senão `2 / 3` dá `0` |
| `$"...{x}..."` | **Interpolação**: coloca variáveis dentro do texto | Montar mensagens: `$"Aderência: {aderencia:F0}%"` |

### 2. Coleções

| Termo | O que é | Quando usar |
|---|---|---|
| `List<T>` | Lista de itens do tipo `T` | Guardar vários itens: `List<string>`, `List<Requisito>` |
| `new List<string>()` | Cria uma lista vazia | Iniciar uma lista que vai receber itens depois |
| `.Count` | Quantidade de itens da lista | Saber o tamanho ou checar se está vazia (`== 0`) |
| `.Add(x)` | Adiciona um item | `Skills.Add(limpa)` |

### 3. Moldes: `record` e `class`

| Termo | O que é | Quando usar |
|---|---|---|
| `record` | Molde de **dados**, comparado pelo conteúdo | Dados que não mudam: `Requisito`, `Vaga` |
| `class` | Molde de **objeto** com estado e ações | Coisas que mudam e têm comportamento: `Perfil`, `CalculadoraAderencia` |
| `new X(...)` | Cria um objeto a partir do molde | Sempre que precisar de uma instância |
| Propriedade `{ get; set; }` | Dado do objeto que pode ser lido e alterado | Campos de uma class: `public string Nome { get; set; }` |
| Método | Ação que o objeto sabe fazer | Lógica ligada ao objeto: `AdicionarSkill` |

**Resposta para entrevista:** "Uso `record` para dados imutáveis comparados por valor, e `class` para objetos que mudam de estado e têm comportamento."

### 4. Métodos

```csharp
double Proporcao(int atendidos, int total)
```

- **`double`**: o tipo que o método devolve. Sem retorno, use `void`.
- **`Proporcao`**: o nome.
- **`(int atendidos, int total)`**: os parâmetros, ou seja, os dados que ele recebe.
- **`return`**: devolve o resultado e **encerra** o método.

**Quando criar um método:** quando você copiou e colou o mesmo código (**DRY**: *Don't Repeat Yourself*) ou quando um bloco tem uma tarefa clara que merece um nome.

### 5. Organização e acesso

| Termo | O que é | Quando usar |
|---|---|---|
| `public` | Qualquer arquivo pode usar | A "porta de entrada" da classe: `Calcular` |
| `private` | Só a própria classe usa | Detalhes internos: `ContarAtendidos`, `Proporcao` |
| `namespace` | O "endereço" da classe | Topo de cada arquivo: `namespace JobFit.Lab.Models;` |
| `using` | "Quero usar o que mora nesse endereço" | Quando uma classe precisa de outra pasta: `using JobFit.Lab.Models;` |
| Encapsulamento | Esconder o interno, mostrar só o necessário | Sempre: `public` só no que os outros precisam |

**Regra prática:** comece tudo como `private` e só abra para `public` o que alguém de fora precisa usar.

**Resposta para entrevista:** "Deixo público só o que quem usa a classe precisa, e escondo os detalhes internos para poder mudá-los sem quebrar nada."

### 6. Decisões e repetição

| Termo | O que é | Quando usar |
|---|---|---|
| `if (condição) { }` | Executa só se for verdadeiro | Qualquer decisão |
| `==` | "É igual a?" | `if (total == 0)` |
| `!` | "Não" (inverte) | `!r.Obrigatorio` = os desejáveis |
| `&&` | "E": as duas precisam ser verdadeiras | `a.Count == 0 && b.Count == 0` |
| `\|\|` | "OU": basta uma ser verdadeira | Quando qualquer condição serve |
| `foreach (var x in lista)` | Repete para cada item | Percorrer as vagas: `foreach (var vaga in vagas)` |
| Cláusula de guarda | `if` + `return` no começo do método para tratar casos especiais | Antes do cálculo normal, para o resto assumir que está tudo OK |

### 7. LINQ e lambda

A **lambda** `r => r.Obrigatorio` é uma mini função: "para cada `r`, faça isso". O **LINQ** são métodos que usam lambdas para trabalhar com listas.

| Termo | O que faz | Exemplo / quando usar |
|---|---|---|
| `.Where(r => ...)` | **Filtra**: mantém quem passa no teste | Separar obrigatórios dos desejáveis |
| `.Select(r => ...)` | **Transforma**: cada item vira outra coisa | Ficar só com os nomes: `.Select(r => r.Nome)` |
| `.Count(r => ...)` | **Conta** quantos passam no teste | Quantos requisitos eu atendo |
| `.Contains(x)` | A lista tem esse item? | Checar se tenho a skill |
| `.DistinctBy(r => ...)` | Remove repetidos pelo campo escolhido | Evitar contar o mesmo requisito 2 vezes |
| `.ToList()` | Transforma o resultado em lista | Depois de `Where`, `Select`, `DistinctBy` |
| `.Any()` | Tem pelo menos um item? | Só imprimir "Faltando" se houver algo |
| `.Except(outra)` | O que está numa lista e não na outra | Calcular o gap |

### 8. Limpeza de texto

| Termo | O que faz | Quando usar |
|---|---|---|
| `.Trim()` | Tira espaços do começo e do fim | Sempre que o texto vier de fora (digitado, IA, site) |
| `.ToLower()` | Tudo minúsculo | Comparar sem diferenciar maiúsculas |
| `StringComparer.OrdinalIgnoreCase` | Compara ignorando maiúsculas e minúsculas | Em `Contains`: `"c#"` igual a `"C#"` |

**Princípio:** nunca confie nos dados de entrada. Limpe antes de comparar.

### 9. Casos especiais (como pensar)

Pergunta mágica: **"Que entrada faria meu código se comportar de forma estranha?"** Teste sempre:

1. **Vazio** (lista sem itens)
2. **Só um tipo** (só obrigatórios, só desejáveis)
3. **Tudo certo / tudo errado** (100% e 0%)
4. **Valor limite** (divisão por zero, 1 único item)
5. **Sujeira** (repetidos, espaços, maiúsculas)

Bug perigoso é o que **não quebra, só mente** (`NaN`, nota inflada).

### 10. Git e GitHub

| Comando | O que faz | Quando usar |
|---|---|---|
| `git init` | Cria o repositório | Uma vez por projeto |
| `git status` | Mostra o que mudou | **Antes** de qualquer commit |
| `git add .` | Separa os arquivos para o commit | Antes de commitar |
| `git commit -m "..."` | Cria o ponto de salvamento | A cada etapa que funciona |
| `git push` | Envia para o GitHub | Depois do commit |
| `git log --oneline` | Mostra o histórico | Conferir os commits |

**Git** guarda o histórico no seu PC. **GitHub** é o site onde fica a cópia online (seu portfólio).

**Atenção à pasta:** `git` e `dotnet run` precisam ser executados dentro de `JobFit.Lab`.

### 11. Debugger

| Termo | O que é | Quando usar |
|---|---|---|
| Breakpoint | Ponto de parada: o programa congela **antes** de executar aquela linha | Quando quiser ver o que acontece por dentro |
| `F5` | Inicia / continua até o próximo breakpoint | Começar ou seguir adiante |
| `F10` | Executa a linha e vai para a próxima | Andar linha por linha |
| `F11` | Entra no método chamado | Ver por dentro de `ContarAtendidos` |
| `Shift + F11` | Sai do método | Voltar depois de entrar |
| `Shift + F5` | Para tudo | Encerrar o debug |
| Locals | Painel com as variáveis do momento | Conferir valores e `Count` |

### 12. Conceitos para entrevista

- **Refatorar:** melhorar o código **sem mudar o que ele faz**.
- **Regra de negócio:** decisão sua sobre como o sistema se comporta (ex.: "só desejáveis vale 100%").
- **Top-level statements:** o `Program.cs` sem `class Program`, onde o código roda direto.

---

## Erros que já encontrei (e a causa)

| Erro | Causa | Solução |
|---|---|---|
| `NaN` na aderência | Divisão `0 / 0` com `double` | Método `Proporcao` que devolve 0 quando o total é 0 |
| `CS0103` nome não existe | Variável usada antes de ser criada, ou método sumiu na colagem | Declarar antes de usar; conferir se nada foi apagado |
| `already defined in this scope` | Duas variáveis com o mesmo nome no mesmo método | Apagar a declaração duplicada |
| `Cannot convert lambda ... IEqualityComparer` | Usei `Distinct` em vez de `DistinctBy` | Trocar para `DistinctBy` |
| `CS0246` tipo não encontrado | Faltava `using` ou o namespace estava diferente | `using JobFit.Lab.Models;` e namespaces idênticos |
| `CS0051` acessibilidade inconsistente | Tipo sem `public` usado em método `public` | Colocar `public` no `record`/`class` |
| `does not contain a definition for ...` | Classe `Perfil` ainda vazia | Completar as propriedades e métodos |
| `not a git repository` | Terminal na pasta de fora do repositório | `cd JobFit.Lab` |
| `Repository not found` no push | Usei o nome de exibição em vez do username na URL | URL com `Jimykw`, não `Jimy.dev` |
| Breakpoint não parou | `Program.cs` antigo não chamava a classe nova | Atualizar o `Program.cs` |

---

## Dúvidas para revisar

- [ ] Explicar `record` x `class` sem olhar
- [ ] Explicar `namespace` e `using` sem olhar
- [ ] Explicar `public` x `private` (encapsulamento)
- [ ] Explicar o `CalculadoraAderencia` linha por linha em voz alta
