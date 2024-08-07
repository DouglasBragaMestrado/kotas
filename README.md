<h1 align="center">Api Kotas Pokemon</h1>
<div align="center">
<h4>Projeto desenvolvido para challenge by coodesh.</h4>
</div>

<img src="https://raw.githubusercontent.com/DouglasBragaMestrado/kotas/main/Kotas_rest/icon/print.png" >

## 👋 Sobre o Projeto

O Kotas api é uma aplicação web que permite aos usuários consultar pokemons, criar Mestres e Capturar e excluir pokemons. Foi desenvolvido com as seguintes tecnologias:

- Linguagem: C#
- Framework: .Ne 6.0
- Banco de Dados: SQLite
- Front-end: Swagger

## 🛠️ Pré-requisitos

Antes de começar, certifique-se de que você tenha as seguintes ferramentas instaladas em sua máquina:

<p>
	<p>
	   <p> -  Visual Studio</p>
	   <p> - Framework Net6.0</p>
		<img src="https://skillicons.dev/icons?i=visualstudio,dotnet" /> 		
</p>


## 🚀 Passo a passo.

Siga os passos abaixo para iniciar o projeto em seu ambiente local:

1️⃣ **Clone o repositório**

   Clique no botão "Clone" acima ou execute o seguinte comando no terminal:

   ```bash
   git clone https://github.com/DouglasBragaMestrado/kotas.git
   ```

   Isso criará uma cópia local do repositório em seu ambiente.

2️⃣ **Inicialize a solution**

   Navegue até o diretório Kotas_rest e na raiz do projeto clique na solution:

   ```bash
   Kotas_rest.sln
   ```

Carregado a Solution clique no Play e execute o projeto. Que deve abrir uma janela em seu navegador.

3️⃣ **Acesse o Swagger**

   Após iniciar a solution, você pode acessar a API REST do pelo Swagger no seu navegador no endereço [https://localhost:7258/swagger/index.html).

4️⃣ **As funções disponiveis**

   - Listar 10 Pokemons de forma aleatório 
```bash
https://localhost:7258/api/GetPokemonRandom
```
   - Pegar um pokemon por seu Id
```bash
https://localhost:7258/api/GetPokemon?pokemon=25'
```
   - Incluir um mestre pokemon
```bash
https://localhost:7258/api/PostInsetMestrePokemon?Nome=Doug&Idade=14&CPF=1235'
```
   - Listar todos mestres pokemon
```bash
https://localhost:7258/api/GetMestrePokemon
```
   - Pegar um mestres pokemon por id
```bash
https://localhost:7258/api/GetSelectMestrePokemon?id=3
```
   - Atualizar o registro de um mestres pokemon 
```bash
https://localhost:7258/api/UpdateMestrePokemon?Id=1&Nome=Marcelo&Idade=45&CPF=7859
```
   - Exclui o registro de um mestres pokemon por id
```bash
https://localhost:7258/api/DeleteMestrePokemon
```
   - Vincular um mestre pokemon a um pokemon (captura)
```bash
https://localhost:7258/api/Capturar?id_Mestre=1&id_pokemon=25
```
   - Lista os pokemon vinculado ao mestre (capturados)
```bash
https://localhost:7258/api/Capturar/mestre?id_Mestre=1
```
   - Exclui o pokemon vinculado ao mestre (libertar)
```bash
https://localhost:7258/api/Capturar/delete?id=8
```

<div align="center">
  Espero que este guia tenha sido útil e que você aproveite ao máximo o projeto.   Para entrar em contato: Douglas Braga doug_dev@outlook.com 🎉😄
</div>
