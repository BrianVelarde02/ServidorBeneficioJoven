import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

export default function Home() {
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    fetch('/inicio')
      .then(res => res.text())
      .then(data => setMessage(data))
      .catch(err => setMessage(`Error al conectar con la API: ${err.message}`));
  }, []);

  return (
    <div>
      <h1>Inicio</h1>
      <p>{message}</p>
      <button onClick={() => navigate('/usuarios')}>Ver Usuarios</button>
    </div>
  );
}
