import React, { useState } from "react";
import styled from "styled-components";

const ModalBackground = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 100;
`;

const ModalContent = styled.div`
  background: gray;
  padding: 30px;
  border-radius: 8px;
  width: 1000px;
  height: 500px;
  max-width: 150%;
  text-align: center;
`;

const BookImage = styled.img`
  width: 80px; /* Set a specific width */
  height: auto; /* Maintain aspect ratio */
  margin: 10px 0; /* Add some space around the image */
`;
const Input = styled.input`
  padding: 10px;
  border-radius: 8px;
  margin-bottom: 35px;
  margin-top: 15px;
  width: 100%;
  max-width: 300px;
  &::placeholder {
    color: #000 !important;
  }
`;

const Button = styled.button`
  padding: 10px;
  margin-bottom: 10px;
  width: 100%;
  max-width: 100px;
  align-items: center;
  cursor: pointer;
`;
const ReserveModal = ({ book, onClose, onReserve }) => {
  const [days, setDays] = useState("");
  const [isQuickPickup, setIsQuickPickup] = useState(false);
  const handleReserve = () => {
    onReserve({ days, isQuickPickup });
    onClose();
  };

  return (
    <ModalBackground>
      <ModalContent>
        <h2>{book.name}</h2>
        <BookImage src={book.picture} alt={book.name} />
        <div>
          <p>The book is only available in type of | {book.type} |</p>
        </div>
        <div>
          <Input
            type="number"
            placeholder="How many days do you want to reserve?"
            value={days}
            onChange={(e) => setDays(e.target.value)}
            required
          />
          <div>
            <p>Check the box if you require a quick pickup</p>
            <Input
              type="checkbox"
              checked={isQuickPickup}
              onChange={() => setIsQuickPickup(!isQuickPickup)}
            />
          </div>
        </div>
        <div>
          <Button onClick={handleReserve}>Reserve It</Button>
        </div>
        <Button onClick={onClose}>Close</Button>
      </ModalContent>
    </ModalBackground>
  );
};

export default ReserveModal;
