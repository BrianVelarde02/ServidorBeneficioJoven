import React from "react";

export default function UsuarioModal({ usuario, onClose, onDelete }) {
  if (!usuario) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <h2>Detalles del Usuario</h2>
        <p><strong>Nombre:</strong> {usuario.nombre}</p>
        <p><strong>CURP:</strong> {usuario.curp}</p>
        <p><strong>Fecha de Nacimiento:</strong> {new Date(usuario.fecha_nacimiento).toLocaleDateString()}</p>
        <button className="delete-btn" onClick={() => onDelete(usuario.id)}>Eliminar Usuario</button>
        <button onClick={onClose}>Cerrar</button>
      </div>
    </div>
  );
}
