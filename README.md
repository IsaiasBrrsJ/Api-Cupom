# Projeto de API - Sistema de Cupons

Este é um projeto de API desenvolvido com **.NET 8**, utilizando a arquitetura **Clean Architecture**. A API foi projetada para interagir com um banco de dados **SQL Server** e permite gerenciar cupons, incluindo o upload e armazenamento de fotos de cupons usando o **Azure Blob Storage**.

## Tecnologias Utilizadas

- **.NET 8**: Framework para desenvolvimento de aplicações backend.
- **SQL Server**: Banco de dados relacional utilizado para armazenar dados de cupons e fotos.
- **Azure Blob Storage**: Armazenamento de fotos dos cupons na nuvem.
- **Clean Architecture**: Estrutura organizacional que separa as responsabilidades em camadas, facilitando a manutenção e escalabilidade do sistema.

## Estrutura do Repositório

O repositório segue a arquitetura **Clean Architecture**, organizada nas seguintes camadas:

- **Core**: Contém as entidades de domínio, interfaces e lógica de negócios principais.
- **Application**: Contém os casos de uso da aplicação, serviços e lógica de aplicação.
- **Infrastructure**: Implementações de integração com sistemas externos, como banco de dados (SQL Server) e Azure Blob Storage.
- **WebAPI**: Camada responsável por expor a API RESTful, contendo controladores e configurações de rotas.

## Estrutura de Branches

O repositório segue um modelo de desenvolvimento com **três branches principais**:

- **`main`**: Branch principal que contém o código em produção. Apenas código validado e pronto para produção é mesclado nesta branch.
- **`homolog`**: Branch para homologação, onde o código finalizado é testado e validado antes de ser promovido para a produção. Serve como um ambiente de pré-produção.
- **`developer`**: Branch de desenvolvimento ativa onde as novas funcionalidades e melhorias são implementadas. Todos os desenvolvedores devem trabalhar nesta branch e fazer pull requests para `homolog` ou `main` após revisão e testes.
