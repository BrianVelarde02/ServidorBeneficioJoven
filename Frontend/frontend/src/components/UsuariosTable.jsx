import React from "react";

export default function UsuariosTable({ usuarios, onSelect }) {
  return (
    <table className="usuarios-table">
      <thead>
        <tr>
          <th>Nombre Completo</th>
          <th>CURP</th>
          <th>Fecha de Nacimiento</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {usuarios.map(item => (
          <tr key={item.id}>
            <td>{item.nombre}</td>
            <td>{item.curp}</td>
            <td>{new Date(item.fecha_nacimiento).toLocaleDateString()}</td>
            <td>
              <span 
                onClick={() => onSelect(item)} 
                style={{ color: "blue", textDecoration: "underline", cursor: "pointer" }}
              >
                Detalles
              </span>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
