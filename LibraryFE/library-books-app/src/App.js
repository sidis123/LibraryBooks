import { useEffect, useState } from "react";
import BookCard from "./BookCard";
import SearchIcon from "./search.svg";
import "./App.css";

const book1 = {
  id_Book: "1",
  Name: "To Kill a Bird",
  Year: "1960",
  Picture: "picture",
  Type: "Book",
};

const App = () => {
  const [books, setBooks] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");

  const searchMovies = async (name) => {
    const response = await fetch(`https://localhost:7133/api/Book`);
    const data = await response.json();
    setBooks(data);
  };

  useEffect(() => {
    searchMovies();
  }, []);

  return (
    <div className="app">
      <h1>Library</h1>

      <div className="search">
        <input
          placeholder="Search for books"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <img src={SearchIcon} alt="search" onClick={() => {}} />
      </div>

      {books?.length > 0 ? (
        <div className="container">
          {books.map((book) => (
            <BookCard book={book} />
          ))}
        </div>
      ) : (
        <div className="empty">
          <h2> No Movies Found </h2>
        </div>
      )}
    </div>
  );
};

export default App;
