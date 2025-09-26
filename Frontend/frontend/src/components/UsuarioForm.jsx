import React, { useState } from "react";

export default function UsuarioForm({ onSubmit, onCancel }) {
  const [nombre, setNombre] = useState("");
  const [apellidos, setApellidos] = useState("");
  const [curp, setCurp] = useState("");
  const [fechaNacimiento, setFechaNacimiento] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!nombre.trim() || !apellidos.trim()) {
      alert("Nombre y Apellidos son obligatorios");
      return;
    }
    onSubmit({
      nombre: `${nombre.trim()} ${apellidos.trim()}`,
      curp,
      fecha_nacimiento: fechaNacimiento,
    });
    setNombre(""); setApellidos(""); setCurp(""); setFechaNacimiento("");
  };

  return (
    <form onSubmit={handleSubmit} className="usuario-form">
      <input type="text" placeholder="Nombre" value={nombre} onChange={(e) => setNombre(e.target.value)} required />
      <input type="text" placeholder="Apellidos" value={apellidos} onChange={(e) => setApellidos(e.target.value)} required />
      <input type="text" placeholder="CURP" value={curp} onChange={(e) => setCurp(e.target.value)} required />
      <input type="date" value={fechaNacimiento} onChange={(e) => setFechaNacimiento(e.target.value)} max={new Date().toISOString().split("T")[0]} required />
      <button type="submit">Agregar Usuario</button>
    </form>
  );
}
