import React from "react";
import { useEffect, useState } from "react";

const ReservationCard = ({ reservation }) => {
  const [book, setBook] = useState(null);
  const fetchReservations = async () => {
    const response = await fetch(
      `https://localhost:7133/api/Book/${reservation.id_Book}`
    );
    const ReservationData = await response.json();
    setBook(ReservationData);
  };
  useEffect(() => {
    fetchReservations();
  }, [reservation.id_Book]);
  return (
    <div className="Reservation">
      <div style={styles.card}>
        <p>Reservacijos id : {reservation.id_Reservation}</p>
        <p>Reservacijos dienu sk. : {reservation.days}</p>
        <p>Reservuota knyga : {reservation.id_Book}</p>
        <p>Reservacijos visa kaina Eurais : {reservation.totalCost}</p>
        <p>Reservuota knyga pavadinimas : {book ? book.name : "Loading..."}</p>
        <p>Reservacijos sukurimo data : {reservation.creationDate}</p>
      </div>
    </div>
  );
};
export default ReservationCard;
const styles = {
  card: {
    border: "2px solid green",
    borderRadius: "8px",
    padding: "16px",
    margin: "10px",
    boxShadow: "0 2px 5px rgba(0, 0, 0, 0.1)",
  },
};
