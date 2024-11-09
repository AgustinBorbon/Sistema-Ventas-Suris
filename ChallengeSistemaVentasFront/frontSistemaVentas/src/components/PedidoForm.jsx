import React, { useState, useEffect } from 'react';

const OrderForm = () => {
  const [items, setItems] = useState([]);
  const [vendors, setVendors] = useState([]);
  const [selectedVendor, setSelectedVendor] = useState('');
  const [selectedItems, setSelectedItems] = useState({});

  // Cargamos los articulos desde que inicia el componente, ya que no especifica cuándo en los requisitos
  useEffect(() => {
    fetch('https://localhost:7138/Articulo/GetAllArticulos')
      .then(response => {
        if (!response.ok) throw new Error('Error al obtener los artículos');
        return response.json();
      })
      .then(data => setItems(data))
      .catch(error => console.error('Error fetcheando articuloss:', error));
  }, []);

  // Obtengo los vendedores por la API que creé, en los requisitos dice que los vendedores tienen que ser
  // obtenidos con una API proporcionada, pero en el mail solo me llego el .docx y los 2 archivos .json
  const loadVendors = () => {
    if (vendors.length === 0) {  // Solo hace la petición si la lista está vacía
      fetch('https://localhost:7138/Vendedor/GetAllVendedores')
        .then(response => {
          if (!response.ok) throw new Error('Error al obtener los vendedores');
          return response.json();
        })
        .then(data => setVendors(data))
        .catch(error => console.error('Error fetcheando vendedores:', error));
    }
  };

  // Control para la seleccion de articulos con los chexkbox
  const handleItemSelection = (itemId) => {
    setSelectedItems((prevSelectedItems) => ({
      ...prevSelectedItems,
      [itemId]: !prevSelectedItems[itemId]
    }));
  };

  const handleVendorSelection = (e) => {
    setSelectedVendor(e.target.value);
  };

  //Funcion extra al ejercicio para guardar en un archivo .json el o los articulos seleccionados y el vendedor seleccionado en un pedido.
  const saveOrder = () => {
    // Creo un array de los articulos que seleccione en los checkbox y los datos
    const selectedItemsArray = items
      .filter(item => selectedItems[item.codigo])  // Filtrar solo los artículos seleccionados
      .map(item => ({
        codigo: item.codigo,
        descripcion: item.descripcion,
        precio: item.precio,
        deposito: item.deposito
      }));
  
    if (!selectedVendor || selectedItemsArray.length === 0) {
      alert("Selecciona un vendedor y al menos un artículo.");
      return;
    }
  
    const selectedVendorData = vendors.find(v => v.id === parseInt(selectedVendor));
  
    const order = {
      vendedor: {
        id: selectedVendorData.id,
        descripcion: selectedVendorData.descripcion
      },
      articulos: selectedItemsArray
    };
  
    //console.log("Order JSON enviado:", JSON.stringify(order));
  
    fetch('https://localhost:7138/Pedido/PostPedido', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(order)
    })
      .then(response => {
        if (response.ok) {
          alert('Pedido guardado exitosamente.');
          setSelectedVendor('');
          setSelectedItems({});
        } else {
          alert('Error al guardar el pedido.');
        }
      })
      .catch(error => console.error('Error guardando pedido:', error));
  };
  

  return (
    <div>
      <h1>Pedidos</h1>
      {/* Desplegable de Vendedores */}
      <select value={selectedVendor} onClick={loadVendors} onChange={handleVendorSelection}>
        <option value="">Selecciona un vendedor</option>
        {vendors.map(vendor => (
          <option key={vendor.id} value={vendor.id}>{vendor.descripcion}</option>
        ))}
      </select>

      {/* Artículos con Checkbox */}
      <table>
        <thead>
          <tr>
            <th>Seleccionar</th>
            <th>Nombre del Artículo</th>
            <th>Precio</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item => (
            <tr key={item.codigo}>
              <td>
                <input
                  type="checkbox"
                  checked={!!selectedItems[item.codigo]}
                  onChange={() => handleItemSelection(item.codigo)}
                />
              </td>
              <td>{item.descripcion}</td>
              <td>{item.precio}</td>
            </tr>
          ))}
        </tbody>
      </table>

      
      <button onClick={saveOrder}>Guardar Pedido</button>
    </div>
  );
};

export default OrderForm;
