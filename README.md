# Proyecto de Gestión de Cursos y Alumnos

## Descripción

Este proyecto es una prueba de concepto para ayudar a la escuela ACME en la gestión de sus cursos y alumnos.

## Requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado en tu máquina.

## Construcción del Proyecto

1. **Abre la línea de comandos.**

2. **Navega al directorio del proyecto:**

   ```bash
   cd Ruta\De\Tu\Proyecto` 

3.  **Ejecuta el siguiente comando para compilar el proyecto:**
      ```bash
    dotnet build
    
   Este comando compilará el proyecto y generará los archivos binarios en la carpeta `bin`.
    

## Ejecución de Pruebas

-   Para ejecutar todas las pruebas, utiliza el siguiente comando:
      ```bash
    dotnet test
    
   Este comando  ejecutará todas las pruebas en el proyecto y mostrará los resultados en la consola.
    

## Ejecución de la prueba automatizada

-   Si deseas ejecutar la prueba automatizada que demuestra la corrección del programa, utiliza el siguiente comando:
    
      ```bash
    dotnet test -filter FullyQualifiedName=Application.Students.UnitTest.GetStudentWithCoursesInDataRange.GetStudentsWithCoursesInDataRangeEndToEndTest.GetStudentsWithCoursesInDataRange
    
## Información Adicional

Este proyecto sigue los principios de diseño basados en la arquitectura limpia, utilizando el patrón CQRS (Command Query Responsibility Segregation) y siguiendo la metodología de Desarrollo Guiado por el Dominio (DDD).

### Patrón CQRS

Se ha implementado el patrón CQRS para separar las operaciones de lectura (queries) de las operaciones de escritura (commands). Esto permite una mayor escalabilidad y flexibilidad al optimizar cada aspecto por separado.

### Arquitectura Limpia

La arquitectura del proyecto sigue los principios de arquitectura limpia, manteniendo las capas bien definidas y separadas. Cada capa (presentación, aplicación, dominio e infraestructura) tiene responsabilidades específicas, facilitando el mantenimiento y la evolución del sistema.

### Vertical Slicing

Dentro de cada capa, se ha aplicado el concepto de "vertical slicing", lo que significa que las diferentes funcionalidades relacionadas están agrupadas verticalmente en lugar de horizontalmente. Esto mejora la cohesión y facilita la comprensión y el mantenimiento de cada característica del sistema.

### Desarrollo Guiado por el Dominio (DDD)

La metodología de Desarrollo Guiado por el Dominio (DDD) ha influido en la modelización del dominio, asegurando que la lógica del negocio esté representada de manera clara en el código. Se han identificado agregados, entidades y objetos de valor para modelar las relaciones y comportamientos del sistema de manera más fiel a la realidad del dominio.

Para obtener más detalles sobre la implementación de estos conceptos, se recomienda revisar el código fuente en el directorio del proyecto.


