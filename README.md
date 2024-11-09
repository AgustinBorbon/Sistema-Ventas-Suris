## Backend
En los launchsettings la URL está escrito en el puerto 7138, y los cors están declarados para cualquier origen, además el path que usa
está configurado para traer la dirección actual, por lo que no debería haber problemas en ejecutarlo.
Se puede probar el backend con Swagger, funcionando ambos métodos GET con total funcionalidad, y el método POST para guardar el pedido funciona pero guarda un formato vacío.


## Frontend
Requiere tener instalado npm, Tutorial para instalarlo: https://www.youtube.com/watch?v=Z-Ofqd2yBCc

Para ejecutar el front abrimos la terminal, ingresamos en la terminal cd: "dirección en la que se encuentre la carpeta frontSistemaVentas", al encontrarse ahí
ingresamos en la terminal "npm run dev", esto nos mostrará información con la URL local para ingresar que debería ser parecida a esta Local:   http://localhost:5173/
Mientras que el backend esté corriendo, no habría problemas al ingresar, el puerto al que se accede en las APIs es el 7138, que es el escrito en el backend.

Una vez adentro, ya se pueden ver los artículos que fueron filtrados en el backend, y se encuentra el desplegable para ver los vendedores.
Una vez seleccionado uno o varios articulos con el checkbox, y al seleccionar un vendedor, se puede hacer click en el botón Guardar Pedido, esto nos va a guardar
el pedido en un archivo .json
