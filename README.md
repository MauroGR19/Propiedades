# Propiedades
plataforma de gestión de propiedades, propietarios e hitorial de las propiedades. 

## Cumplimiento de los Principios SOLID
1. Responsabilidad Única: Cada clase y carpeta tiene una única responsabilidad: Se puede definir las entidades del negocio, contiene lógica de los casos de uso, contiene mappers.
2. Abierto/Cerrado: El sistema puede extenderse sin modificar el código existente: Puedes agregar nuevos servicios, controladores o modelos sin alterar los existentes, nuevas funcionalidades se implementan como nuevas clases o interfaces
3. Sustitución de Liskov: Las clases concretas pueden sustituir sus interfaces sin romper el sistema: los controladores, el repositorio, los casos de uso, consumen abstracciones, no implementaciones concretas
4. Segregación de Interfaces: Las interfaces están separadas por dominio y son específicas: Cada servicio implementa lo que necesita
5. Inversión de Dependencias: Las capas superiores dependen de interfaces, no de implementaciones:
La lógica de negocio y aplicación no conoce detalles de infraestructura
 
## Distribucion de la Arquitectura Hexagonal
El proyecto sigue la arquitectura hexagonal dividiendo las responsabilidades en capas:

* **Capa de Aplicación**: Contiene la lógica de negocio y los casos de uso.
* **Capa de Dominio**: Define las reglas de negocio y las entidades del dominio.
* **Capa de Infraestructura**: Implementa la interacción con el mundo exterior, como componentes de UI.
* **Capa de Test**: Define todos los test unitarios.

## Por qué cumple con la Arquitectura Hexagonal
1. Independencia del Framework: La lógica de negocio no depende de Razor ni de ningún framework específico.
2. Separación de Responsabilidades: Cada capa tiene una responsabilidad clara y definida.
3. Facilidad de Pruebas: Las capas están desacopladas, lo que facilita la creación de pruebas unitarias.

## Configuración del proyecto
Debe especificar el ConnectionStrings, donde se debe modificar la cadena de conexión en el archivo de configuración appsettings.json, luego de esto solo bastaria con iniciar el proyecto ya que la DB se crea automaticamente.
![alt text]
