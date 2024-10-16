import { useEffect, useState } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import styled from "styled-components";
import BookCard from "./BookCard";
import Header from "./Header";
import Login from "./Login";
import Reservations from "./Reservations";
import ReservationModal from "./ReservationModal";
import { useSelector } from "react-redux";
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
  const [selectedBook, setSelectedBook] = useState(null); // State for the selected book
  const [isModalOpen, setIsModalOpen] = useState(false); // Modal visibility
  const isLoggedInNow = useSelector((state) => state.auth.isLoggedIn);
  const user = useSelector((state) => state.auth.user);
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
    if (searchTerm) {
      filtered = filtered.filter((book) =>
        book.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }
    if (type) {
      filtered = filtered.filter((book) => book.type === type);
    }
    if (year) {
      filtered = filtered.filter((book) => book.year === parseInt(year));
    }

    setFilteredBooks(filtered);
  };

  // Trigger the filtering whenever the search term, type, or year changes
  useEffect(() => {
    filterBooks();
  }, [searchTerm, type, year]);

  const handleBookClick = (book) => {
    if (isLoggedInNow) {
      setSelectedBook(book);
      setIsModalOpen(true);
    }
  };

  const handleReserve = async (reservationDetails) => {
    try {
      const requestBody = {
        creationDate: new Date().toISOString(),
        quickPickup: reservationDetails.isQuickPickup,
        days: reservationDetails.days,
        totalCost: 0,
        id_User: user.id_User,
        id_Book: selectedBook.id_Book,
      };

      const response = await fetch("https://localhost:7133/api/Reservation", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "*/*",
        },
        body: JSON.stringify(requestBody),
      });

      if (response.ok) {
        alert("Reservation successful!");
      } else {
        alert("Failed to reserve the book.");
      }
    } catch (error) {
      console.error(error);
    }
  };

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
                    <BookCard
                      key={book.id}
                      book={book}
                      onClick={handleBookClick}
                    />
                  ))}
                </div>
              ) : (
                <div className="empty">
                  <h2>No Books Found</h2>
                </div>
              )}

              {isModalOpen && (
                <ReservationModal
                  book={selectedBook}
                  onClose={() => setIsModalOpen(false)}
                  onReserve={handleReserve}
                />
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
