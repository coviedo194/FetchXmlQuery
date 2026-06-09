# ovg_FetchXmlQuery

Solucion de Dataverse para exponer una Custom API que ejecuta consultas FetchXML y devuelve resultados desde Canvas Apps.

## Contenido

- [Que hace esta solucion](#que-hace-esta-solucion)
- [Como usar](#como-usar)
- [Estructura de la solucion en este repo](#estructura-de-la-solucion-en-este-repo)
- [Para developers](#para-developers)
- [Autor y Licencia](#autor-y-licencia)

## Que hace esta solucion

Permite realizar consultas FetchXML desde Canvas Apps y Custom Pages, algo que hoy no es posible de forma nativa. Esto ayuda a mejorar performance al habilitar consultas mas complejas, todo a traves de una Custom API.

El objetivo principal de este repositorio es facilitar la adopcion por makers: importar, probar en Canvas App y publicar sin entrar en detalles high-code.

## Como usar

1. Importa la solucion en tu entorno de Dataverse.
2. Abre la Canvas App y agrega el origen de datos `Environment`.
3. Invoca `Environment.ovg_fetchxmlqueryapi` pasando el FetchXML deseado en `ovg_fetchxmlquery`.
4. Recorre los resultados con `ForAll` para volcarlos a una coleccion o variable.

Consideraciones importantes:

- Si la Custom API queda vinculada al contexto de Environment, el rol de seguridad debe tener lectura sobre la tabla del sistema Environment (Entorno).
- Conviene tipar o convertir explicitamente los datos devueltos con funciones como `GUID()`, `Text()`, `DateTimeValue()`, `Value()`, entre otras.
- Power Apps no sugiere los nombres de campos devueltos; revisalos en Monitor y escribelos tal como aparecen.
- En campos provenientes de `link-entity`, los campos vienen con el texto `_x002e_` en lugar de `.`.

Ejemplo de invocacion en Canvas App:

```powerfx
ClearCollect(
		colTest;
		ForAll(
				Environment.ovg_fetchxmlqueryapi(
						{
								ovg_fetchxmlquery: $"
										<fetch top='5'>
											<entity name='systemuser'>
												<attribute name='systemuserid' />
												<attribute name='fullname' />
												<link-entity name='businessunit' from='businessunitid' to='businessunitid' alias='businessunit'>
													<attribute name='name' />
												</link-entity>
											</entity>
										</fetch>"
						}
				).value As item;
				{
						usuarioGuid: GUID(item.Value.systemuserid);
						usuarioNombre: Text(item.Value.fullname);
						buNombre: Text(item.Value.businessunit_x002e_name)
				}
		)
)
```

## Estructura de la solucion en este repo

La carpeta `solution/` contiene la solucion de Dataverse descomprimida para permitir seguimiento de cambios (diffs) en control de versiones.

Incluye:

- La Custom API.
- El plugin que ejecuta la consulta FetchXML.
- Una Canvas App de ejemplo que muestra como invocarla.

Este formato esta orientado a colaboracion y trazabilidad; no es necesario conocer conceptos high-code para usar la solucion como maker.

## Para developers

Si quieres extender o modificar el comportamiento, este repositorio contiene el codigo fuente del plugin y las instrucciones basicas de compilacion.

- `ovg_FetchXmlQuery.cs`: logica principal del plugin.
- `PluginBase.cs`: base comun para plugins generados con `pac plugin init`.
- `ovg_FetchXmlQuery.csproj`: proyecto y dependencias.
- Compilacion: `dotnet build ovg_FetchXmlQuery.csproj`
- Salida del ensamblado: `bin/Debug/net462/publish/`

## Autor y Licencia

Carlos Oviedo Gibbons

[Github/coviedo194](https://github.com/coviedo194)

[LinkedIn/coviedo194](https://www.linkedin.com/in/coviedo194/)

powercreatorpy@outlook.com

MIT - Ver [LICENSE](LICENSE)