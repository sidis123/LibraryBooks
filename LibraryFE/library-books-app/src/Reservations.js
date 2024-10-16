import React from "react";
import { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import ReservationCard from "./ReservationCard";

const Reservations = () => {
  const user = useSelector((state) => state.auth.user);
  const isLoggedIn = useSelector((state) => state.auth.isLoggedIn);
  const [reservations, setReservations] = useState([]);
  const dispatch = useDispatch();

  const fetchReservations = async () => {
    const response = await fetch(
      `https://localhost:7133/api/Reservation/user/${user.id_User}`
    );
    const ReservationData = await response.json();
    setReservations(ReservationData);
  };
  useEffect(() => {
    fetchReservations();
  }, []);

  if (!isLoggedIn) {
    return <div>Please sign in</div>;
  }

  return (
    <div style={styles.container}>
      <h1>Book reservations of {user.name}</h1>
      {reservations?.length > 0 ? (
        <div className="container">
          {reservations.map((reservation) => (
            <ReservationCard key={reservation.id} reservation={reservation} />
          ))}
        </div>
      ) : (
        <div className="empty">
          <h2>No Books Found</h2>
        </div>
      )}
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
