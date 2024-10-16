## Descrição
O **CashFlowManagerEQS** é uma aplicação para gerenciar o fluxo de caixa de um comerciante, permitindo a criação de lançamentos financeiros (débitos e créditos) e gerando um relatório consolidado de saldo diário.

## Tecnologias Utilizadas
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **Bootstrap (para estilização)**
- **JavaScript** para interações no front-end

## Instalação e Execução Local

### Pré-requisitos:
- .NET 6.0 SDK ou superior instalado.
- SQL Server LocalDB instalado.

### Passos para execução local:

1. Clone este repositório:
   ```bash
   git clone https://github.com/eduardoquelhas/CashFlowManagerEQS.git

# CashFlowManagerEQS

## Mapeamento de Domínios Funcionais e Capacidades de Negócio

### Domínios Funcionais:

- **Controle de Lançamentos**:
  - Registro de transações financeiras (créditos e débitos).
  - Validação de dados de lançamentos.

- **Consolidação Diária**:
  - Geração de relatórios de saldo diário consolidado com base nos lançamentos.

### Capacidades de Negócio:

- **Registro de Transações**: Permite ao comerciante registrar todas as movimentações financeiras.
- **Geração de Relatórios**: Exibe um relatório de saldo diário consolidado para fácil visualização das finanças.

---

## Refinamento do Levantamento de Requisitos Funcionais e Não Funcionais

### Requisitos Funcionais:

- Inserir lançamentos de débitos e créditos.
- Gerar um relatório de saldo consolidado para o dia.
- Visualizar relatórios de dias anteriores.

### Requisitos Não Funcionais:

- **Disponibilidade**: O sistema de controle de lançamentos deve funcionar mesmo que o sistema de consolidação esteja temporariamente indisponível.
- **Escalabilidade**: O sistema deve suportar até 50 requisições por segundo, com uma taxa de falha de no máximo 5%.
- **Desempenho**: O tempo de geração dos relatórios deve ser quase em tempo real.
- **Segurança**: Proteção dos dados financeiros por meio de autenticação e autorização.

---

## Desenho da Solução Completo (Arquitetura Alvo)

A arquitetura da solução é baseada em microsserviços para garantir alta disponibilidade e escalabilidade. Os componentes principais são:

- **API de Lançamentos**: Para gerenciar os lançamentos financeiros.
- **API de Consolidação Diária**: Responsável pela geração de relatórios de saldo consolidado.
- **Banco de Dados SQL Server**: Para armazenar os lançamentos e dados financeiros.
- **Interface Web (ASP.NET Core MVC)**: Para permitir que os comerciantes façam lançamentos e visualizem os relatórios.

---

## Justificativa na Decisão/Escolha de Ferramentas/Tecnologias e Tipo de Arquitetura

### Ferramentas e Tecnologias:

- **ASP.NET Core MVC**: Framework robusto para construção de aplicações web com alta escalabilidade.
- **Entity Framework Core**: Para gerenciar o acesso ao banco de dados de forma eficiente.
- **SQL Server**: Banco de dados relacional confiável com suporte à alta disponibilidade.
- **JavaScript e Bootstrap**: Para melhorar a interatividade e usabilidade da interface.

### Tipo de Arquitetura:

A aplicação foi construída seguindo uma arquitetura de microsserviços, onde o controle de lançamentos e a consolidação diária são serviços independentes, possibilitando escalabilidade e independência de falhas.

---

## Testes

A aplicação inclui testes unitários e de integração desenvolvidos com **xUnit**. Os principais componentes testados incluem:

- **Serviço de Lançamentos**: Verificação da inserção, listagem e exclusão de lançamentos.
- **Serviço de Consolidação Diária**: Verificação da geração correta do relatório consolidado.
---
## Configuração do Banco de Dados

### Pré-requisitos:

- **.NET 6.0 SDK** ou superior.
- **SQL Server** (ou **SQL Server LocalDB**) instalado.

### Arquitetura: 
![Arquitetura](images/Carrefour.jpg)

---

## 1. Desenho da Solução da Arquitetura de Transição (Migração de Legado)

### Contexto:

Se estamos migrando de um sistema legado, o principal objetivo é garantir a continuidade dos negócios durante a transição para a nova arquitetura. A arquitetura de transição deve fornecer suporte tanto ao sistema legado quanto ao novo sistema até que a migração seja concluída.

### Solução Proposta:

- **Arquitetura Híbrida (Coexistência)**:
  - Durante o processo de migração, propomos uma arquitetura que permita a coexistência do sistema legado e do novo sistema baseado em microsserviços.
  - O **sistema legado** continuará lidando com dados antigos e operações críticas que ainda não foram migradas.
  - O **novo sistema de microsserviços** será implementado em paralelo, sendo integrado por meio de APIs para consolidar dados e gerar relatórios financeiros.

- **Sincronização de Dados**:
  - Será necessário um middleware que sincronize os dados entre o sistema legado e o novo sistema para garantir a consistência nas transações financeiras até que todo o sistema esteja completamente migrado.

- **Migração Gradual**:
  - Os módulos mais críticos, como o controle de lançamentos, seriam os primeiros a ser migrados, enquanto a consolidação diária pode ser mantida no legado por mais tempo, até que a estabilidade do novo sistema esteja assegurada.

- **APIs Unificadas**:
  - Para garantir a interoperabilidade entre o legado e a nova plataforma, podemos usar um gateway API que sirva tanto para os serviços novos quanto para os antigos, proporcionando uma transição gradual e sem interrupção dos serviços.

---

## 2. Estimativa de Custos com Infraestrutura e Licenças

### Custos de Infraestrutura:

- **Serviços de Nuvem** (Azure, AWS, Oracle Cloud):
  - **Servidores de Aplicação (VMs ou serviços gerenciados)**: Entre $100 a $500/mês por servidor.
  - **Armazenamento de Dados (SQL Server na nuvem)**: Entre $50 a $200/mês, dependendo da necessidade de replicação e backup.
  - **Serviços de Balanceamento de Carga**: Custo adicional de $20 a $100/mês.
  - **Monitoramento e Logs**: Entre $10 a $50/mês, dependendo do volume de logs gerados.

### Custos com Licenças:

- **SQL Server**: Licenciamento por núcleo ou instância, variando de $2000 a $7000 por ano.
- **ASP.NET Core**: Open-source, sem custos diretos de licenciamento.
- **Entity Framework Core**: Também open-source, sem custos diretos.
- **Ferramentas de Observabilidade e Monitoramento**: **Datadog** ou **NewRelic** variam de $30 a $200/mês, dependendo do número de hosts monitorados.

---

## 3. Monitoramento e Observabilidade

### Ferramentas e Práticas Recomendadas:

- **Logging Centralizado**:
  - Usar **Serilog** para capturar logs e armazená-los em serviços como **Elasticsearch**, ou soluções nativas da nuvem (AWS CloudWatch, Azure Monitor).

- **Monitoramento de Aplicação**:
  - Utilizar **APM** (Application Performance Monitoring) como **NewRelic**, **Datadog** ou **Prometheus/Grafana** para monitorar a saúde do sistema, tempos de resposta e falhas.

- **Monitoramento de Banco de Dados**:
  - Implementar monitoramento de consultas SQL para detectar e otimizar consultas demoradas ou ineficientes.

- **Alertas em Tempo Real**:
  - Configurar alertas automáticos para notificar a equipe de suporte quando certos limites críticos forem ultrapassados (ex.: utilização de CPU, falhas de transação ou queda de serviço).

### Observabilidade:

- **Tracing**:
  - Implementar tracing com **OpenTelemetry** para visualizar como as requisições fluem entre diferentes microsserviços e identificar gargalos de performance.

- **Dashboards**:
  - Criar dashboards em ferramentas como **Grafana**, **AWS CloudWatch**, ou **Azure Monitor** para acompanhar KPIs críticos, como tempo de resposta e taxa de erros.

---

## 4. Critérios de Segurança para Consumo (Integração) de Serviços

### Práticas de Segurança Recomendadas:

- **Autenticação e Autorização via OAuth2 e OpenID Connect**:
  - Implementar **OAuth2** com tokens **JWT** para garantir que apenas clientes autenticados possam acessar os serviços.

- **Criptografia**:
  - As comunicações entre os serviços devem ser criptografadas usando **TLS 1.2+**.

- **Rate Limiting**:
  - Implementar **rate limiting** e **throttling** nos gateways de API para proteger contra ataques de negação de serviço (DoS).

- **Auditoria**:
  - Todas as requisições aos serviços críticos devem ser auditadas, permitindo a rastreabilidade de qualquer ação realizada no sistema.

- **WAF (Web Application Firewall)**:
  - Utilizar um WAF para proteger contra ataques como **SQL Injection**, **XSS** e força bruta.

---

## 5. Requisitos Não Funcionais

- **Alta Disponibilidade**:
  - O serviço de controle de lançamentos será independente do sistema de consolidação diária. Mesmo que o sistema de consolidação caia, os lançamentos continuarão sendo registrados.

- **Escalabilidade**:
  - O sistema de consolidação será capaz de suportar até 50 requisições por segundo durante picos.

### Soluções de Escalabilidade:

- **Auto Scaling**: Implementação de **auto-scaling** nas instâncias, garantindo que mais servidores sejam alocados automaticamente durante picos de demanda.
- **Fila de Mensagens**: Utilizar serviços de fila como **AWS SQS** ou **Azure Service Bus** para processar requisições de maneira assíncrona.
- **Cache**: Implementar um cache distribuído (ex.: **Redis** ou **Memcached**) para reduzir a carga do sistema e aumentar a velocidade de resposta.

---

## 6. Observações e Evoluções Futuras

### Evoluções Futuras:

- **Integração com Sistemas de Pagamento**:
  - O sistema pode ser estendido para integrar sistemas de pagamento e bancos, permitindo a automação do recebimento de créditos.

- **Relatórios Avançados**:
  - Além dos relatórios diários, relatórios semanais, mensais e previsões de fluxo de caixa com base em **inteligência artificial** podem ser introduzidos.

- **Migração Completa para Microsserviços**:
  - Após a migração completa do sistema legado, a arquitetura pode ser modularizada ainda mais, dividindo os serviços de relatórios, lançamentos e autenticação em microsserviços independentes.

- **Testes de Stress e Carga**:
  - Implementar testes contínuos de carga e stress para garantir a performance sob diferentes condições, além de monitoramento em tempo real das métricas de uso.



### Configuração do Banco de Dados:

1. Certifique-se de que você tem o **SQL Server** ou o **LocalDB** rodando em sua máquina.

2. Abra o arquivo `appsettings.json` e configure a string de conexão conforme necessário. Se estiver usando **LocalDB**, a string de conexão pode ser algo como:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CashFlowManagerEQS;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   
```bash
dotnet ef migrations add InitialCreate
dotnet ef migrations add CreateLancamentosTable
dotnet ef database update
dotnet restore
dotnet run

Acesse a aplicação no navegador em http://localhost:5000
--
### Como executar os testes:

Para rodar os testes localmente, utilize o seguinte comando:

```bash
dotnet test
