import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import UsuarioForm from '../components/UsuarioForm';
import UsuariosTable from '../components/UsuariosTable';
import Pagination from '../components/Pagination';
import UsuarioModal from '../components/UsuarioModal';
import "./Usuarios.css";

export default function Usuarios() {
  const [usuarios, setUsuarios] = useState([]);
  const [mensaje, setMensaje] = useState(null); 
  const [mostrarFormulario, setMostrarFormulario] = useState(false);
  const [usuarioModal, setUsuarioModal] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const usersPerPage = 10;

  const fetchUsuarios = () => {
    fetch("/usuarios")
      .then(res => res.json())
      .then(data => setUsuarios(data))
      .catch(err => console.error(err));
  };

  useEffect(() => {
    fetchUsuarios();
  }, []);

 const handleAgregarUsuario = (nuevoUsuario) => {
    const fecha = new Date(nuevoUsuario.fecha_nacimiento);
    const edad = new Date().getFullYear() - fecha.getFullYear();
    if (edad < 12 || edad > 30) {
        setMensaje({ tipo: "error", texto: "La edad debe ser entre 12 y 30 años" });
        setTimeout(() => setMensaje(null), 2000); // <--- desaparece después de 2s
        return;
    }

    fetch("/usuario", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(nuevoUsuario)
    })
        .then(res => res.json())
        .then(data => {
        setMostrarFormulario(false);
        fetchUsuarios();
        setMensaje({ tipo: "success", texto: data.mensaje });
        setTimeout(() => setMensaje(null), 2000); 
        })
        .catch(err => {
        setMensaje({ tipo: "error", texto: "Error de conexión con la API" });
        setTimeout(() => setMensaje(null), 2000);
        });
    };



  const handleEliminar = (id) => {
    if (!window.confirm("¿Estás seguro de eliminar este usuario?")) return;
    fetch(`/usuario/${id}`, { method: "DELETE" })
      .then(res => res.json())
      .then(() => {
        setUsuarioModal(null);
        fetchUsuarios();
      })
      .catch(err => console.error(err));
  };

  const usuariosFiltrados = usuarios.filter(u =>
    u.nombre.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const indexOfLastUser = currentPage * usersPerPage;
  const indexOfFirstUser = indexOfLastUser - usersPerPage;
  const currentUsers = usuariosFiltrados.slice(indexOfFirstUser, indexOfLastUser);
  const totalPages = Math.ceil(usuariosFiltrados.length / usersPerPage);

  return (
    <div>
      <h1>Usuarios</h1>
      <Link to="/">Volver a Inicio</Link>

      {/* Búsqueda */}
      <div style={{ margin: "1rem 0" }}>
        <input
          type="text"
          placeholder="Buscar usuario por nombre..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          style={{ padding: "0.5rem", width: "250px" }}
        />
      </div>

      {/* Botón Formulario */}
      <div className="form-toggle">
        <button className="btn-agregar" onClick={() => setMostrarFormulario(!mostrarFormulario)}>
          {mostrarFormulario ? "Cancelar" : "Agregar Usuario"}
        </button>
      </div>

      {/* Formulario */}
      {mostrarFormulario && (
        <UsuarioForm
          onSubmit={handleAgregarUsuario}
          onCancel={() => setMostrarFormulario(false)}
        />
      )}
        {/* Mensaje */}
        {mensaje && (
        <p style={{ color: mensaje.tipo === "success" ? "green" : "red", fontWeight: "bold" }}>
            {mensaje.texto}
        </p>
        )}

      {/* Tabla */}
      {usuariosFiltrados.length > 0 ? (
        <>
          <UsuariosTable usuarios={currentUsers} onSelect={setUsuarioModal} />
          <Pagination
            totalPages={totalPages}
            currentPage={currentPage}
            onPageChange={setCurrentPage}
          />
        </>
      ) : (
        <p>No se encontraron usuarios.</p>
      )}

      {/* Modal */}
      {usuarioModal && (
        <UsuarioModal
          usuario={usuarioModal}
          onClose={() => setUsuarioModal(null)}
          onDelete={handleEliminar}
        />
      )}
    </div>
  );
}
