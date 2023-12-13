# Cadastro de Protocolos
###         _Solicita��o de Emiss�o de Documentos_
## Funcionalidades
O sistema permite a realiza��o de cadastro e consulta de protocolos para solicita��o de emiss�o de documentos, conforme podemos identificar abaixo:
- GET/ api/Protocols;
- POST/ api/Protocols;
- GET/ api/Protocols/filter;

Entretanto, para utilizar essas funcionalidades � necess�rio utilizar um token, que ser� gerado por um dos endpoints da aplica��o, dispon�vel no swagger, conforme ser� explicado melhor a seguir.

Ap�s a aplica��o estar rodando corretamente no conteiner Docker todoas as funcionalidades poder�o ser acessadas atrav�s do endere�o: http://localhost:32777/swagger/index.html

## Executando e utilizando a sua aplica��o

Esta aplica��o foi criada utilizando-se do NetCore 6.0, RabbitMQ e MySQL, rodando em conteiner no Docker, para tanto, � necess�rio que o docker esteja configurado na sua m�quina, para tanto, sugiro a instala��o da vers�o mais atual do [Docker](https://www.docker.com/products/docker-desktop/). Com a instala��o conclu�da e o Docker devidamente funcional, segue o passo a passo para utiliza��o da aplica��o:

- Clone o projeto diretamente atrav�s do Visual Studio utilizando do c�digo https://github.com/sabrinafrosseto/desafiotecnico.git ou acessando o [reposit�rio do projeto](https://github.com/sabrinafrosseto/desafiotecnico) e realizando a clonagem do projeto pelo m�todo que melhor lhe convier.
- Com o projeto devidamente clonado, execute no terminal, dentro do pasta do projeto, o comando: docker-compose up --build
- Com isso, o projeto come�ar� a rodar no Docker, com uma imagem similar a esta:
![Imagem](Images/app_running.png)
- Caso alguns dos conteiners n�o esteja como "Running, clique no final de 'play' para executar o conteiner.

#### Criando base de dados
A fim de garantir a exist�ncia de uma base de dados com informa��es foi criada uma funcionalidade que cria alguns protocolos automaticamente, assim, ap�s estar com todos os conteineres rodando adequadamente, o ideal � executar novamente o publisher, para tanto, clique no bot�o de 'stop' do conteiner 'Publisher' e clique novamente no 'play'. Desta forma, o Publisher rodar� as suas funcionalidades e publicar� algumas mensagens para a fila do RabbitMQ. As mensagens estar�o na fila e poder�o ser vistas consultando: http://localhost:15672/ e logando com os dados:
- usu�rio: guest
- senha guest
Voc� ter� vis�o de uma tela similar esta abaixo, onde � poss�vel ver a chegada de uma mensagem:
![Imagem](Images/docker.png)
Ap�s esta etapa ser� necess�rio parar e rodar o conteiner do Consumer, realizando o mesmo procedimento anterior, mas, agora no conteiner '*consumer*'. Ao fazer isso ele ler� as mensagens contidas nas filas do rabbitMQ e proceder� com a cria��o de novos objetos 'protocolo' no banco de dados.

#### Consultando os dados gerados
A fim de permitir a consulta dos protocolos existe a API, com uma interface simples de uso, chamada Swagger, atrav�s do qual � poss�vel utilizar as funcionalidades expostas da API.
Para tanto, acesse a [API](http://localhost:32777/swagger/index.html) e voc� ter� acesso a dois tipos de consulta e uma op��o de cadastro, conforme pode ser visto na imagem abaixo:
![Imagem](Images/api.png)

Nesta p�gina do Swagger voc� pode clicar em cada um dos m�todos para utiliz�-lo, conforme ser� explicado a seguir.

Por quest�es de seguran�a, entretanto, antes de utilizar os m�todos � necess�rio gerar um TOKEN, atrav�s de um login e senha. Para tanto, voc� utilizar� o m�todo POST/ api/Login e enviar� como dados de login e senha os dados a seguir:
- usuario: admin
- senha: password
*Obs: Por ora, o login � �nico, entretanto, ser� trazida melhoria que garantir� o registro de novos usu�rios, devidamente cadastrados em um banco de dados, para gera��o de token individual.
Conforme segue na imagem a seguir:
![Imagem](Images/login.png)

Ao executar este m�todo o usu�rio receber� um Bearer Token que ele dever� copiar e inserir na �rea de Authorize no topo da p�gina, � direita. Ali ele inserir� este token com a palavra 'Bearer' na frente, conforme exemplo a seguir, e clicando em 'Authorize':
![Imagem](Images/bearertoken.png)

Agora sim, o usu�rio est� autorizado a utilizar as funcionalidades da API de protocolos.

Seguem algumas informa��es sobre os m�todos dispon�veis:

- GET /api/Protocols
Atrav�s deste m�todo � poss�vel consultar todos os protocolos cadastrados.

- GET /api/Protocols/filter
Atrav�s deste m�todo � poss�vel consultar protocolos espec�ficos utilizando como filtro os campos protocolNumber(N�mero do Protocolo), RG ou CPF, conforme a prefer�ncia do usu�rio, sendo obrigat�ria a utiliza��o de apenas um deles.

- POST /api/Protocols
Atrav�s deste m�todo � poss�vel cadastrar um novo protocolo, preenchendo os campos da requisi��o, conforme modelo abaixo, onde o usu�rio substituir� onde est� escrito *string* pelo dado correspondente, mantendo as aspas:
{
  "numeroProtocolo": "string",
  "numeroViaDocumento": "string",
  "cpf": "string",
  "rg": "string",
  "nomeCompleto": "string",
  "nomeMae": "string",
  "nomePai": "string",
  "foto": "string",
  "ID": "string"
}
E assim, o usu�rio ter� rodado a aplica��o e utilizado as funcionalidades dispon�veis.

Com essas funcionalidades a aplica��o atende seus requisitos m�nimos e encontra-se dispon�vel para utiliza��o. Novas funcionalidades ser�o inclu�das com o tempo e podem trazer oportunidades de novos neg�cios.