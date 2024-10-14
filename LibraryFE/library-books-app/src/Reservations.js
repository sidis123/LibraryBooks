import React from "react";

const Reservations = () => {
  return (
    <div style={styles.container}>
      <h1>My Reservations</h1>
      <p>This is the reservations page where you can view your reservations.</p>
    </div>
  );
};

const styles = {
  container: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    height: "100vh",
    background: "#1f2123",
    color: "#fff",
  },
};

export default Reservations;
