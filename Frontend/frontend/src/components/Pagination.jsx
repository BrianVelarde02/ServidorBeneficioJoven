import React from "react";

export default function Pagination({ totalPages, currentPage, onPageChange }) {
  return (
    <div style={{ marginTop: "1rem" }}>
      {Array.from({ length: totalPages }, (_, i) => i + 1).map(number => (
        <button
          key={number}
          onClick={() => onPageChange(number)}
          style={{
            margin: "0 2px",
            padding: "0.3rem 0.6rem",
            backgroundColor: currentPage === number ? "#ac2ebdff" : "#f0f0f0",
            color: currentPage === number ? "#fff" : "#000",
            border: "none",
            borderRadius: "3px",
            cursor: "pointer"
          }}
        >
          {number}
        </button>
      ))}
    </div>
  );
}
