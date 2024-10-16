import { useEffect, useState } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import styled from "styled-components";
import BookCard from "./BookCard";
import Header from "./Header";
import Login from "./Login";
import Reservations from "./Reservations";
import "./App.css";

const Select = styled.select`
  padding: 10px;
  margin-bottom: 10px;
  width: 108%;
  max-width: 400px;
`;

const App = () => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]); // Books after filtering
  const [searchTerm, setSearchTerm] = useState("");
  const [type, setType] = useState("");
  const [year, setYear] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const fetchBooks = async () => {
    const response = await fetch(`https://localhost:7133/api/Book`);
    const data = await response.json();
    setBooks(data);
    setFilteredBooks(data);
  };

  useEffect(() => {
    fetchBooks();
  }, []);

  const filterBooks = () => {
    let filtered = books;

    // Filter by name if search term is provided
    if (searchTerm) {
      filtered = filtered.filter((book) =>
        book.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    // Filter by type if type is selected
    if (type) {
      filtered = filtered.filter((book) => book.type === type);
    }

    // Filter by year if year is selected
    if (year) {
      filtered = filtered.filter((book) => book.year === parseInt(year));
    }

    setFilteredBooks(filtered); // Update the filtered books state
  };

  // Trigger the filtering whenever the search term, type, or year changes
  useEffect(() => {
    filterBooks();
  }, [searchTerm, type, year]);

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
      <Routes>
        <Route
          path="/"
          element={
            <div className="app">
              <h1>Library</h1>

              <div className="search">
                <input
                  type="text"
                  placeholder="Search for books"
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
                <Select value={type} onChange={(e) => setType(e.target.value)}>
                  <option value="">Type</option>
                  <option value="Book">Book</option>
                  <option value="Audiobook">Audiobook</option>
                </Select>

                <input
                  type="number"
                  placeholder="Year"
                  value={year}
                  onChange={(e) => setYear(e.target.value)}
                />
              </div>

              {filteredBooks?.length > 0 ? (
                <div className="container">
                  {filteredBooks.map((book) => (
                    <BookCard key={book.id} book={book} />
                  ))}
                </div>
              ) : (
                <div className="empty">
                  <h2>No Books Found</h2>
                </div>
              )}
            </div>
          }
        />
        <Route
          path="/login"
          element={<Login setIsLoggedIn={setIsLoggedIn} />}
        />
        <Route path="/reservations" element={<Reservations />} />
      </Routes>
    </Router>
  );
};

export default App;
