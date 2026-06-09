# AGENTS.md

## 1. Que hace el proyecto

Este proyecto contiene un plugin de Dataverse que expone una Custom API para ejecutar consultas FetchXML y devolver los resultados.

La documentacion del repositorio debe priorizar a makers que importan la solucion y la usan en Canvas Apps; dejar los detalles de desarrollo como apendice o seccion final.

La logica principal vive en [ovg_FetchXmlQuery.cs](ovg_FetchXmlQuery.cs).

Parentesis funcionales importantes:
- Input esperado: `ovg_fetchxmlquery`
- Output devuelto: `ovg_fetchxmlresponse`

## 2. Como construirlo

Usa el siguiente comando desde la raiz del repositorio:

```bash
dotnet build ovg_FetchXmlQuery.csproj
```

La salida del ensamblado generado queda en `bin/Debug/net462/publish/`.

Notas:
- El proyecto apunta a `.NET Framework 4.6.2`.
- Si el build falla, revisa primero [ovg_FetchXmlQuery.cs](ovg_FetchXmlQuery.cs) y [ovg_FetchXmlQuery.csproj](ovg_FetchXmlQuery.csproj).

## 5. Flujo de trabajo recomendado

1. Hacer el cambio minimo necesario.
2. Compilar el proyecto con `dotnet build ovg_FetchXmlQuery.csproj`.
3. Revisar errores de compilacion antes de seguir.
4. Mantener la logica principal concentrada en [ovg_FetchXmlQuery.cs](ovg_FetchXmlQuery.cs).
5. No tocar [PluginBase.cs](PluginBase.cs) salvo que sea estrictamente necesario.

## 6. Registro y despliegue

Al registrar en Dataverse:
- Registrar el ensamblado generado por este proyecto.
- Usar como Plugin Type la clase [ovg_FetchXmlQuery](ovg_FetchXmlQuery.cs).
- Asegurar que la Custom API reciba `ovg_fetchxmlquery` como parametro de entrada.
- Asegurar que la respuesta se exponga como `ovg_fetchxmlresponse`.
- Si la Custom API queda vinculada al contexto de Environment, verificar que el rol de seguridad tenga lectura sobre la tabla del sistema Environment (Entorno).

No reintroducir la carpeta legacy eliminada ni agregar archivos del sistema como `.DS_Store`.