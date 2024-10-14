import React from "react";

const BookCard = ({ book }) => {
  return (
    <div className="movie">
      <div>
        <p>{book.year}</p>
      </div>
      <div>
        <img src={book.picture} alt={book.name} />
      </div>
      <div>
        <span>{book.type}</span>
        <h3>{book.name}</h3>
      </div>
    </div>
  );
};
export default BookCard;
