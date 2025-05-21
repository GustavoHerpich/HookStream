# 🚀 HookStream

**HookStream** é uma aplicação web em tempo real que exibe eventos recebidos do GitHub via WebHook em um painel visual, utilizando ASP.NET Core no backend e Vue 3 no frontend.

---

## 📌 Objetivo

O objetivo do projeto é monitorar atividades em repositórios GitHub (como push, pull request etc.) e exibi-las em tempo real de forma clara e interativa. Ideal para quem deseja acompanhar contribuições ou atividades em projetos de forma visual.

---

## ⚙️ Como funciona

1. Um **WebHook do GitHub** envia eventos para a API.
2. O backend **valida a assinatura** e processa os dados.
3. O evento é transmitido via **WebSocket** para o frontend.
4. O painel exibe os dados em uma interface.

---

## 🧱 Tecnologias principais

- **Backend:** ASP.NET Core (WebAPI + WebSocket)
- **Frontend:** Vue 3 + TypeScript + Vuetify 3
- **Comunicação em tempo real:** WebSockets
- **Teste local de WebHooks:** [smee.io](https://smee.io/)
- **GitHub WebHook:** configurado com secret (ex: `Senha123@`)

---

## ▶️ Como rodar localmente

### 1. Clone o repositório

```bash
git clone https://github.com/GustavoHerpich/HookStream.git
cd HookStream
```

### 2. Baixe o smee-cli globalmente

```bash
npm install -g smee-cli
```

### 3. Inicie o backend

```bash
cd backend/HookStream.API
dotnet run
```

### 4. Inicie o frontend

```bash
cd frontend
npm install
npm run dev
```

### 5. Configure o WebHook do GitHub

1. Vá até as configurações de **WebHooks** do seu repositório no GitHub.
2. Acesse [smee.io](https://smee.io/) e crie um canal (link único).
3. Configure o WebHook no GitHub com as seguintes opções:

   - **Payload URL:** `https://smee.io/seu-canal`
   - **Content type:** `application/json`
   - **Secret:** `Senha123@` (a mesma configurada no backend)

4. Copie o link do canal gerado e execute o seguinte comando no terminal:

```bash
smee -u https://smee.io/seu-canal -t http://localhost:5000/api/webhook/github
```

### 5. Demostração Visual do Painel

![image](https://github.com/user-attachments/assets/08ed0ca6-2ef1-4812-a51c-15e4e0d57269)

