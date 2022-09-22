# CrawlerProxyInfo
CrawlerProxyInfo é um scraping/crawler de dados sobre proxys. Para a busca dos dados das páginas foi utilizado o pacote Selenium WebDriver e para a extração dos dados foi utilizado o pacote HtmlAgilityPack, a execução do programa é multithread, utilizando a classe nativa Parallel.

Esta aplicação foi desenvolvida em .net core 6, usando o editor de texto VScode. O banco de dados utilizado é MySql executado no servidor XAMPP, no sistema operacional Windowa 11. 
## Funcionalidade
A aplicação desenvolvida analisa todas as páginas do site [https://proxyservers.pro] e extrair os campos "IP Adress", "Port", "Country" e "Protocol", os dados coletados são salvos em um arquivo Json. 
Cada página analisada tem uma cópia do arquivo HTML é salva, e após a finalização da análise das páginas, é armazenado em um base de dados as informações de Inicio e fim da execução do programa, o arquivo Json coletado, quantidade de páginas análisadas e a quantidade total de linhas extraídas

## Instalação

Para a execução da aplicação é preciso que o ambiente .Net de execução contenha  os seguintes pacotes

```sh
HtmlAgilityPack: ^1.11.46
Microsoft.EntityFrameworkCore.Design: ^6.0.9">
Newtonsoft.Json: ^13.0.1
Pomelo.EntityFrameworkCore.MySql: ^6.0.2
Selenium.WebDriver: ^4.4.0
Selenium.WebDriver.ChromeDriver: ^105.0.5195.5200
System.Net.Http: ^4.3.4

Em caso de troca de bases de dados
Pomelo.EntityFrameworkCore.MySql
Microsoft.EntityFrameworkCore.Design 
```

A aplicação foi desenvolvida sobre o conceito de Database-First, para a utlização de novas tbelas, usuários e senha da base de dados é preciso a excecução do comando para criação das classes de conexão com o Banco de dados, alterando os parâmetros: Server, DataBas, Uid,Pwd e ProxyDbContext.

```sh
dotnet tool install --global dotnet-ef
dotnet ef dbcontext scaffold "Server=localhost;DataBase=proxy_crawler;Uid=root;Pwd=" Pomelo.EntityFrameworkCore.MySql -o Models -f -c ProxyDbContext
```

