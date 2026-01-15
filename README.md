# 🚦 Sistema de Gestión de Siniestros Viales

Sistema de gestión de siniestros viales desarrollado con **.NET 8**, aplicando **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS** y **Entity Framework Core**, enfocado en mantenibilidad, escalabilidad y alta testabilidad.

---

## 📌 Descripción General

Este sistema permite gestionar información relacionada con siniestros viales, modelando correctamente las reglas del negocio y separando claramente las responsabilidades mediante patrones y principios de arquitectura moderna.

---

## 🏗️ Arquitectura

La solución está estructurada siguiendo **Clean Architecture**, con dependencias dirigidas hacia el dominio.

### Capas

- **Domain**  
  Contiene la lógica de negocio pura: entidades, agregados, value objects y reglas del dominio.

- **Application**  
  Orquesta los casos de uso mediante **CQRS** (Commands & Queries).

- **Infrastructure**  
  Implementaciones técnicas: EF Core, repositorios, configuración de base de datos.

- **API**  
  Exposición de endpoints REST y configuración de la aplicación.

---

## 🧠 Principios Aplicados

- **Clean Architecture**  
  Separación clara de responsabilidades en capas concéntricas.

- **Domain-Driven Design (DDD)**  
  El dominio es el centro de la solución.

- **CQRS**  
  Separación entre operaciones de escritura (Commands) y lectura (Queries).

- **SOLID**  
  Aplicados en todo el código para mejorar mantenibilidad y extensibilidad.

- **Repository Pattern**  
  Abstracción del acceso a datos.

- **Unit of Work**  
  Gestión de transacciones de manera consistente.

---

## 📦 Modelo de Dominio

### 🔷 Agregado Principal

#### **Siniestro (Aggregate Root)**

- **Identidad**
  - `Guid Id`

- **Value Objects**
  - `FechaHoraSiniestro`  
    Encapsula la fecha y hora del siniestro con validaciones de negocio.
  - `Ubicacion`  
    Contiene Departamento y Ciudad.

- **Entidades Internas**
  - `Vehiculo`  
    Vehículos involucrados en el siniestro.
  - `Victima`  
    Personas afectadas.

### 📜 Reglas de Negocio

- Un siniestro debe tener **ubicación y fecha válidas**.
- La fecha:
  - No puede ser futura.
  - No puede ser mayor a 10 años.
- Vehículos y víctimas solo se acceden **a través del agregado Siniestro**.

---

## 📚 Enumeraciones

### TipoSiniestro

```csharp
Colision = 1,
Atropello = 2,
Volcamiento = 3,
CaidaOcupante = 4,
ChoqueFijo = 5,
Incendio = 6,
Otro = 99

### TipoVehiculo
Automovil = 1,
Motocicleta = 2,
Camion = 3,
Bus = 4,
Bicicleta = 5,
Otro = 99
```
---

# 🚦 Sistema de Gestión de Siniestros Viales

Sistema backend desarrollado en **.NET 8**, aplicando **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, **Entity Framework Core** y buenas prácticas de arquitectura orientadas a mantenibilidad, escalabilidad y testabilidad.

---

## 🧾 Architecture Decision Records (ADR)

### ADR-001: Uso de Clean Architecture
- **Estado:** Aceptado
- **Contexto:**  
  Necesitamos una arquitectura mantenible y testeable que permita evolucionar el sistema.
- **Decisión:**  
  Implementar Clean Architecture con 4 capas:
  - Domain
  - Application
  - Infrastructure
  - API
- **Consecuencias:**
  - ✅ Alta testabilidad
  - ✅ Independencia del framework
  - ✅ Código mantenible
  - ⚠️ Mayor complejidad inicial
  - ⚠️ Más archivos y proyectos

---

### ADR-002: Domain-Driven Design (DDD)
- **Estado:** Aceptado
- **Contexto:**  
  El dominio de siniestros viales posee reglas de negocio complejas.
- **Decisión:**  
  Aplicar DDD con agregados, value objects y lenguaje ubicuo.
- **Consecuencias:**
  - ✅ El código refleja el lenguaje del negocio
  - ✅ Reglas de negocio encapsuladas
  - ✅ Value Objects inmutables garantizan consistencia
  - ⚠️ Curva de aprendizaje para el equipo

**Implementación:**
- `Siniestro` como Aggregate Root
- `Ubicacion` y `FechaHoraSiniestro` como Value Objects
- `Vehiculo` y `Victima` como entidades del agregado

---

### ADR-003: CQRS con MediatR
- **Estado:** Aceptado
- **Contexto:**  
  Necesitamos separar operaciones de lectura y escritura.
- **Decisión:**  
  Implementar CQRS usando MediatR como mediador.
- **Consecuencias:**
  - ✅ Commands y Queries separados
  - ✅ Handlers con responsabilidad única
  - ✅ Fácil agregar validaciones y comportamientos
  - ⚠️ Dependencia de MediatR

**Implementación:**
- Commands: `RegistrarSiniestroCommand`
- Queries: `ObtenerSiniestrosQuery`
- Handlers independientes para cada operación

---

### ADR-004: Entity Framework Core como ORM
- **Estado:** Aceptado
- **Contexto:**  
  Necesitamos persistencia con soporte para migraciones.
- **Decisión:**  
  Usar EF Core con SQL Server.
- **Consecuencias:**
  - ✅ Migraciones automáticas
  - ✅ LINQ para consultas
  - ✅ Change Tracking
  - ⚠️ Posible overhead en queries complejas

**Configuración:**
- Fluent API para entidades
- Value Objects como *Owned Types*
- Índices en campos de búsqueda frecuente

---

### ADR-005: Repository Pattern
- **Estado:** Aceptado
- **Contexto:**  
  Necesitamos abstraer el acceso a datos del dominio.
- **Decisión:**  
  Implementar Repository Pattern solo para Aggregate Roots.
- **Consecuencias:**
  - ✅ Dominio desacoplado de la persistencia
  - ✅ Fácil de mockear en tests
  - ✅ Posibilidad de cambiar ORM
  - ⚠️ Capa adicional de abstracción

---

### ADR-006: FluentValidation
- **Estado:** Aceptado
- **Contexto:**  
  Necesitamos validaciones robustas y expresivas.
- **Decisión:**  
  Usar FluentValidation con *pipeline behavior* en MediatR.
- **Consecuencias:**
  - ✅ Validaciones declarativas y legibles
  - ✅ Separadas de la lógica de negocio
  - ✅ Reutilizables y testeables
  - ⚠️ Dependencia adicional

---

### ADR-007: Paginación en Queries
- **Estado:** Aceptado
- **Contexto:**  
  Los resultados pueden ser grandes conjuntos de datos.
- **Decisión:**  
  Implementar paginación obligatoria con un límite máximo de **100 registros**.
- **Consecuencias:**
  - ✅ Mejor rendimiento
  - ✅ Previene carga excesiva de memoria
  - ⚠️ Los clientes deben manejar paginación

---

## ▶️ Guía de Ejecución

### 📋 Prerrequisitos

- .NET 8 SDK
- SQL Server o SQL Server LocalDB
- Visual Studio 2022 / VS Code / Rider

---

