# 📱 CeluMarket — El Tianguis Digital

¡Bienvenido a **CeluMarket**! El marketplace premium especializado en la compra, venta e intercambio de celulares. 

Este repositorio es un monorepo que contiene tanto el **Backend en .NET 8** (diseñado con Arquitectura Limpia) como el **Frontend en Astro con Tailwind CSS v4** (rápido como el viento y moderno a morir).

---

## 🏗️ Arquitectura General

El proyecto está dividido en dos partes principales para mantener desacoplados los fierros del backend y el diseño del frontend:

```text
celumarket/
├── CeluMarket.API/           # Capa de entrada de la API (Controllers, CORS, Program.cs)
├── CeluMarket.Application/   # Lógica de aplicación, casos de uso, DTOs y mapeos
├── CeluMarket.Domain/        # Entidades del núcleo (User, Product, Brand) y reglas de negocio
├── CeluMarket.Infrastructure/# Persistencia a base de datos (AppDbContext, Migraciones MariaDB)
├── CeluMarket.slnx           # Estructura de solución moderna para VS/Rider
└── celumarket-front/         # Frontend moderno en Astro + Tailwind CSS v4
```

---

## ⚡ Stack Tecnológico

### Backend (CeluMarket.API)
- **Framework:** .NET 8.0 Web API
- **ORM:** Entity Framework Core
- **Base de Datos:** MariaDB / MySQL (a través de `Pomelo.EntityFrameworkCore.MySql`)
- **Arquitectura:** Clean Architecture (Domain-Driven Design conceptual)
- **Documentación:** Swagger / OpenAPI

### Frontend (celumarket-front)
- **Framework:** Astro (Static & SSR Ready)
- **Estilos:** Tailwind CSS v4 (compilado directo por Vite)
- **Lenguaje:** TypeScript (Tipado fuerte compartido)
- **Gestor de Paquetes:** `pnpm` (rápido y ahorrador de espacio)

---

## 🚀 Guía de Inicio Rápido (¿Cómo ponerlo a jalar?)

Sigue estos sencillos pasos para tener todo corriendo localmente en menos de 5 minutos.

### 1. Requisitos Previos
Asegúrate de tener instalado en tu máquina:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (versión 18 o superior)
- [pnpm](https://pnpm.io/installation) (`npm install -g pnpm`)
- Servidor **MariaDB** (o MySQL) activo.

---

### 2. Configurar y Correr el Backend

1. **Abre el archivo de configuración** en `CeluMarket.API/appsettings.json` (o `appsettings.Development.json`) y configura tu cadena de conexión a MariaDB/MySQL en `DefaultConnection`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=celumarket_db;Uid=root;Pwd=tu_contraseña;"
   }
   ```

2. **Aplica las migraciones** para crear la base de datos y las tablas automáticamente:
   ```bash
   dotnet ef database update --project CeluMarket.Infrastructure --startup-project CeluMarket.API
   ```

3. **Inicia el servidor** de .NET:
   ```bash
   dotnet run --project CeluMarket.API
   ```
   *El backend estará disponible por defecto en: `http://localhost:5220`*
   *Puedes abrir la consola de Swagger en: `http://localhost:5220/swagger/index.html`*

---

### 3. Configurar y Correr el Frontend

1. **Navega al directorio del frontend**:
   ```bash
   cd celumarket-front
   ```

2. **Crea un archivo `.env`** en la raíz de la carpeta `celumarket-front` con la URL de la API:
   ```env
   PUBLIC_API_URL=http://localhost:5220/api
   ```
   *(Nota: Astro requiere el prefijo `PUBLIC_` para que las variables estén disponibles en el navegador).*

3. **Instala las dependencias**:
   ```bash
   pnpm install
   ```

4. **Arranca el servidor de desarrollo**:
   ```bash
   pnpm dev
   ```
   *El frontend estará listo en: `http://localhost:4321`*

---

## 🔌 API Endpoints (Contrato de Comunicación)

La API cuenta con soporte de CORS configurado específicamente para permitir peticiones desde `http://localhost:4321`.

### Marcas (`Brands`)

| Método | Endpoint | Descripción | Body (JSON) | Respuesta (200/201) |
|---|---|---|---|---|
| **GET** | `/api/Brands` | Obtiene la lista completa de marcas. | *Ninguno* | `[ { "id": 1, "name": "Apple" }, ... ]` |
| **POST** | `/api/Brands` | Registra una nueva marca. | `{ "name": "Motorola" }` | `{ "id": 4, "name": "Motorola" }` |

---

## 🛠️ Solución de Problemas (FAQ de cabecera)

#### 🛑 Error: `Failed to parse URL from undefined/Brands`
- **Causa:** El frontend no encuentra la variable de entorno `PUBLIC_API_URL`.
- **Solución:** Revisa que el archivo `.env` en la raíz de `celumarket-front` exista y que la variable empiece estrictamente con `PUBLIC_API_URL=...` (evita usar `VITE_API_URL` ya que Astro no lo expone por defecto si no lleva el prefijo `PUBLIC_`). Reinicia el servidor de desarrollo después de cambiarlo.

#### 🛑 Error: `CORS Policy Blocked`
- **Causa:** La API de .NET no tiene habilitado el origen del frontend.
- **Solución:** En el `Program.cs` del backend, asegúrate de tener registrada la política de CORS apuntando a `http://localhost:4321` y tener activado el middleware con `app.UseCors("AllowAstroFrontEnd")` justo antes de `app.UseAuthorization()`.

#### 🛑 Error de compilación en Backend (Archivo en uso)
- **Causa:** Intentaste compilar con `dotnet build` mientras la API seguía activa.
- **Solución:** Detén la ejecución de la API en la terminal (`Ctrl + C`) antes de volver a compilar o usar comandos de migración.

---

Desarrollado con ❤️ para el ecosistema de celulares en México. ¡A romperla! 🚀
