import { createSlice } from "@reduxjs/toolkit";

const initialUser = JSON.parse(localStorage.getItem("user")) || null;

const authSlice = createSlice({
  name: "auth",
  initialState: {
    user: initialUser,
    isLoggedIn: !!initialUser,
  },
  reducers: {
    signIn: (state, action) => {
      state.user = action.payload;
      state.isLoggedIn = true;
      const expiry = new Date().getTime() + 24 * 60 * 60 * 1000;
      localStorage.setItem("user", JSON.stringify(action.payload));
      localStorage.setItem("expiry", expiry);
    },
    signOut: (state) => {
      state.user = null;
      state.isLoggedIn = false;
      localStorage.removeItem("user");
      localStorage.removeItem("expiry");
    },
    checkExpiry: (state) => {
      const expiry = localStorage.getItem("expiry");
      if (expiry && new Date().getTime() > expiry) {
        state.user = null;
        state.isLoggedIn = false;
        localStorage.removeItem("user");
        localStorage.removeItem("expiry");
      }
    },
  },
});

export const { signIn, signOut } = authSlice.actions;

export default authSlice.reducer;
