import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "./store"; // Import logout action
import { Link } from "react-router-dom";

const Header = () => {
  const dispatch = useDispatch();
  const isLoggedIn = useSelector((state) => state.auth.isLoggedIn);
  const user = useSelector((state) => state.auth.user);

  const handleLogout = () => {
    dispatch(logout()); // Call the logout action
  };

  return (
    <header style={styles.header}>
      {isLoggedIn ? (
        <div>
          <span>Welcome, {user.name}</span>
          <button onClick={handleLogout}>Log Out</button>
          <Link to="/reservations">My Reservations</Link>
        </div>
      ) : (
        <Link to="/login">Log In</Link>
      )}
    </header>
  );
};

const styles = {
  header: {
    position: "fixed", // Fix the header at the top
    top: 0,
    left: 0,
    width: "100%", // Ensure it spans the full width of the viewport
    display: "flex",
    justifyContent: "flex-end",
    padding: "1rem",
    background: "#1f2123",
    zIndex: 1000, // Ensure it's on top of other elements
  },
  button: {
    margin: "0 1rem",
    padding: "0.5rem 1rem",
    fontSize: "1rem",
    backgroundColor: "#f0a500",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
    color: "#fff",
  },
};

export default Header;
