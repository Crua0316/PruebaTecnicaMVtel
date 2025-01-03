PRUEBA TECNICA DESARROLLADOR MID MV -TEL

Introducción

Este proyecto fue desarrollado como una solución integral para gestionar transacciones y mostrar resúmenes en un formato visual agradable. La solución se divide en dos partes:

API: Implementada en .NET para manejar la lógica de negocio.

Frontend: Creado en Angular para consumir el API y mostrar los datos al usuario.

A continuación, detallo las decisiones que tome durante el desarrollo, por qué las tomamos y cómo usar este proyecto.

Decisiones de Diseño

1. Separación del Backend y el Frontend

Tomamos la decisión de separar el backend (API) y el frontend (Angular) en proyectos independientes para:

Modularidad: Permitir que ambas partes sean desarrolladas y desplegadas de forma independiente.

Escalabilidad: Facilitar futuras integraciones y actualizaciones.

2. Estructura del API

El API fue diseñado en .NET con dos endpoints principales:

POST /transacciones: Permite agregar nuevas transacciones al archivo JSON.

GET /resumen: Genera un resumen con el cliente que tuvo el mayor gasto y el producto más vendido.

3. Frontend con Angular

El frontend fue implementado usando Angular 19. Esto me permitio:

Consumir el API de manera eficiente mediante servicios.

Mostrar el resumen en un diseño amigable y funcional.

Mantener un código organizado con componentes y enrutamiento.

4. Algoritmo para Resúmenes y Análisis

Durante el desarrollo, también resolvimos algunos problemas específicos:

4.1 Resumen de Transacciones (Primer Punto)

Desarrollamos un programa en .NET que:

Procesa un archivo JSON con información de pedidos.

Determina:

El cliente con el mayor gasto total.

El producto más vendido basado en la cantidad.

4.2 Solución del Anagrama (Desafío Algorítmico)

Implementamos una función en C# para determinar si dos cadenas son anagramas. La función compara dos cadenas tras eliminar espacios y convertirlas a minúsculas.


Instrucciones de Uso

1. Configurar el API

Requisitos

.NET SDK instalado.

Pasos:

Clona el repositorio del backend:

git clone (https://github.com/Crua0316/PruebaTecnicaMVtel)
cd TransaccionesApi

Restaura las dependencias:

dotnet restore

Inicia el servidor:

dotnet run

La API estará disponible en http://localhost:5247.

Accede a la documentación Swagger en:

http://localhost:5247/swagger

Endpoints principales

POST /transacciones

Cuerpo (JSON):

{
  "cliente": "Juan Pérez",
  "productos": [
    { "nombre": "Laptop", "precio": 1500, "cantidad": 1 },
    { "nombre": "Mouse", "precio": 20, "cantidad": 2 }
  ]
}

Respuesta esperada:

"Transacción agregada exitosamente."

GET /resumen

Respuesta esperada (JSON):

{
  "clienteMayorGasto": {
    "cliente": "Juan Pérez",
    "totalGasto": 1540
  },
  "productoMasVendido": {
    "producto": "Mouse",
    "totalCantidad": 2
  }
}

2. Configurar el Frontend

Requisitos

Node.js y npm instalados.

Pasos:

Clona el repositorio del frontend:

git clone (https://github.com/Crua0316/PruebaTecnicaMVtel)
cd TransaccionesFrontend

Instala las dependencias:

npm install

Inicia el servidor de desarrollo:

ng serve

Accede al frontend en http://localhost:4200.

Estructura del Proyecto

Backend (API)

/Controllers      <-- Lógica de los endpoints
/Models           <-- Clases de datos (Pedido, Producto)
Program.cs        <-- Configuración principal de la API
appsettings.json  <-- Configuraciones generales

Frontend (Angular)

/src
  /app
    /components   <-- Componentes de la aplicación
    /services     <-- Servicios para consumir el API
    app.module.ts <-- Módulo principal
  index.html      <-- Archivo principal del frontend