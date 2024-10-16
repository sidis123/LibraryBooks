import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { signOut } from "./authSlice";
import { Link, useNavigate } from "react-router-dom";

const Header = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const isLoggedIn = useSelector((state) => state.auth.isLoggedIn);
  const user = useSelector((state) => state.auth.user);

  const handleLogout = () => {
    dispatch(signOut());
    navigate("/");
  };

  return (
    <header style={styles.header}>
      <Link to="/" style={styles.button}>
        Home
      </Link>
      {isLoggedIn ? (
        <div>
          <span>Welcome, {user.name}</span>
          <Link to="/reservations" style={styles.button}>
            My Reservations
          </Link>
          <button onClick={handleLogout} style={styles.buttonLog}>
            Log Out
          </button>
        </div>
      ) : (
        <Link to="/login" style={styles.button}>
          Log In
        </Link>
      )}
    </header>
  );
};

const styles = {
  header: {
    position: "fixed",
    top: 0,
    left: 0,
    width: "100%",
    justifyContent: "space-between",
    alignItems: "center",
    display: "flex",
    padding: "1rem",
    background: "#1f2123",
    zIndex: 1000,
  },
  button: {
    margin: "0 1rem",
    padding: "10px 30px",
    fontSize: "1rem",
    backgroundColor: "#626364",
    borderRadius: "10px",
    cursor: "pointer",
    color: "#1f2122",
    textDecoration: "none",
    border: "none",
  },
  buttonLog: {
    margin: "0 1rem",
    padding: "11.5px 30px",
    fontSize: "1rem",
    backgroundColor: "#626364",
    borderRadius: "10px",
    cursor: "pointer",
    color: "#1f2122",
    textDecoration: "none",
    border: "none",
  },
};

export default Header;
