# 🤖 agents.md — Guía para Agentes de IA en CeluMarket

Este archivo está diseñado para que cualquier agente de IA (como tú, yo o los que vengan después) entienda rápidamente las reglas del juego en este repositorio, la arquitectura y no repita errores del pasado. 

---

## 🎯 Contexto del Proyecto

**CeluMarket** es una plataforma para compra-venta de celulares.
- **Backend:** .NET 8.0 Web API, diseñado bajo principios de Arquitectura Limpia (Clean Architecture).
- **Frontend:** Astro con Tailwind CSS v4 (usando el plugin de Vite `@tailwindcss/vite` para procesamiento súper veloz).
- **Base de Datos:** MariaDB (o MySQL) con Entity Framework Core.

---

## ⚠️ Reglas Críticas para Agentes

### 1. Variables de Entorno del Frontend
- **Regla:** Cualquier variable de entorno que deba ser leída en el navegador por Astro **DEBE** llevar el prefijo `PUBLIC_`.
- **Mal:** `VITE_API_URL=...` (Astro no la cargará en el cliente por defecto).
- **Bien:** `PUBLIC_API_URL=...` (Se lee en el cliente usando `import.meta.env.PUBLIC_API_URL`).

### 2. Estilos del Frontend (Tailwind CSS v4)
- **Regla:** El proyecto usa la versión moderna de Tailwind v4. Los estilos se inicializan en `src/styles/global.css` usando `@import "tailwindcss";`.
- **Regla:** Cada página nueva de Astro que use clases de Tailwind **debe** importar este archivo CSS en su sección de frontmatter:
  ```astro
  ---
  import '../styles/global.css';
  ---
  ```

### 3. Compilación del Backend (.NET Core)
- **Regla:** Detén la ejecución de la API (`CeluMarket.API.exe`) antes de intentar compilar con `dotnet build` o antes de ejecutar migraciones de base de datos. Si no lo haces, la compilación fallará debido a bloqueos de archivos en disco.

### 4. Configuración de CORS
- **Regla:** El frontend corre por defecto en `http://localhost:4321`. La política de CORS en el backend se llama `"AllowAstroFrontEnd"`.
- Cualquier endpoint nuevo que se agregue debe respetar esta política para no romper las peticiones en el cliente.

---

## 🛠️ Comandos Frecuentes para Agentes

### Base de Datos & Migraciones (Desde la raíz)
```bash
# Agregar una nueva migración
dotnet ef migrations add NombreMigracion --project CeluMarket.Infrastructure --startup-project CeluMarket.API

# Aplicar las migraciones a la BD MariaDB/MySQL
dotnet ef database update --project CeluMarket.Infrastructure --startup-project CeluMarket.API
```

### Ejecutar Servidores locales
```bash
# Backend (API)
dotnet run --project CeluMarket.API

# Frontend (Astro)
cd celumarket-front
pnpm dev
```

### Construcción para Producción
```bash
# Compilar todo el frontend estático
pnpm run build
```

---

## 📝 Bitácora de Agentes (Contribuciones)

Aquí registramos qué agente hizo qué para mantener el historial del proyecto:

### 1. Agente: **Antigravity** (Mayo 2026)
* **CORS Setup:** Configuré y activé las políticas de CORS en `Program.cs` del backend para permitir la conexión desde `http://localhost:4321`.
* **Tailwind Integration:** Corregí el error de carga de estilos importando `global.css` en `index.astro`.
* **Rediseño Premium:** Diseñé un dashboard interactivo en modo oscuro (`slate-950`) para la gestión de marcas con efecto de brillo, buscador en tiempo real y contador de estadísticas.
* **Integración Dynamic CRUD:** Creé scripts client-side dentro de `index.astro` para interceptar el formulario de añadir marcas, mandar un `POST` a la API e inyectar la nueva tarjeta dinámicamente con alertas flotantes sin recargar la página.
* **Documentación:** Creé y estructuré el `README.md` de la raíz, actualicé el del frontend y generé este archivo `agents.md`.
